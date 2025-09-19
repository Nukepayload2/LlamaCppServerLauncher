#Region " Imports "

Imports Avalonia.Controls
Imports Avalonia.Interactivity
Imports Avalonia.Platform.Storage
Imports System.Diagnostics
Imports System.Threading.Tasks
Imports System.IO
Imports System.Text.Json

#End Region

Partial Public Class MainWindow
    Inherits Window

#Region " Fields "

    Private serverProcess As Process
    Private serverRunning As Boolean = False
    Private _viewModel As New MainViewModel()

#End Region

#Region " Properties "

    Public ReadOnly Property ViewModel As MainViewModel
        Get
            Return _viewModel
        End Get
    End Property

#End Region

#Region " Constructor "

    Public Sub New()
        InitializeComponent()
        DataContext = ViewModel
        UpdateCommandPreview()
    End Sub

#End Region

#Region " UI Updates "

    Private Sub UpdateCommandPreview()
        CommandPreviewTextBox.Text = ViewModel.UpdateCommandPreview()
    End Sub

#End Region

#Region " Event Handlers "

    Private Sub UpdateCommandPreviewButton_Click(sender As Object, e As RoutedEventArgs) Handles UpdateCommandPreviewButton.Click
        UpdateCommandPreview()
    End Sub

    Private Sub ClearFiltersButton_Click(sender As Object, e As RoutedEventArgs)
        ViewModel.ClearFilters()
    End Sub

    Private Async Sub BrowseServerButton_Click(sender As Object, e As RoutedEventArgs) Handles BrowseServerButton.Click
        Await BrowseForServer()
    End Sub

    Private Async Sub BrowseModelButton_Click(sender As Object, e As RoutedEventArgs) Handles BrowseModelButton.Click
        Await BrowseForModel()
    End Sub

    Private Async Sub StartServerButton_Click(sender As Object, e As RoutedEventArgs) Handles StartServerButton.Click
        Await StartServer()
    End Sub

    Private Sub StopServerButton_Click(sender As Object, e As RoutedEventArgs) Handles StopServerButton.Click
        StopServer()
    End Sub

    Private Async Sub SaveSettingsButton_Click(sender As Object, e As RoutedEventArgs) Handles SaveSettingsButton.Click
        Await SaveSettings()
    End Sub

    Private Async Sub LoadSettingsButton_Click(sender As Object, e As RoutedEventArgs) Handles LoadSettingsButton.Click
        Await LoadSettingsAsync()
    End Sub

    Private Async Sub CopyCommandButton_Click(sender As Object, e As RoutedEventArgs) Handles CopyCommandButton.Click
        Await CopyCommandToClipboard()
    End Sub

#End Region

#Region " File Operations "

    Private Async Function BrowseForServer() As Task
        If StorageProvider IsNot Nothing Then
            Dim files As IReadOnlyList(Of IStorageFile) =
                Await StorageProvider.OpenFilePickerAsync(New FilePickerOpenOptions With {
                    .Title = "Select LLaMA.cpp Server Executable",
                    .AllowMultiple = False,
                    .FileTypeFilter = New List(Of FilePickerFileType) From {
                        New FilePickerFileType("Executable Files") With {
                            .Patterns = New List(Of String) From {"*.exe"}
                        }
                    }
                })

            If files.Count > 0 Then
                ViewModel.Settings.ServerPath = files(0).Path.LocalPath
            End If
        End If
    End Function

    Private Async Function BrowseForModel() As Task
        If StorageProvider IsNot Nothing Then
            Dim files As IReadOnlyList(Of IStorageFile) =
                Await StorageProvider.OpenFilePickerAsync(New FilePickerOpenOptions With {
                    .Title = "Select LLaMA.cpp Model File",
                    .AllowMultiple = False,
                    .FileTypeFilter = New List(Of FilePickerFileType) From {
                        New FilePickerFileType("Model Files") With {
                            .Patterns = New List(Of String) From {"*.gguf", "*.bin"}
                        }
                    }
                })

            If files.Count > 0 Then
                ViewModel.Settings.ModelPath = files(0).Path.LocalPath
            End If
        End If
    End Function

    Private Async Function SaveSettings() As Task
        Dim errorMessage As String = ""
        Try
            Dim json As String = JsonSerializer.Serialize(ViewModel.Settings, New JsonSerializerOptions With {
                .WriteIndented = True
            })
            Dim configFile As String = Path.Combine(AppContext.BaseDirectory, "serverconfig.json")
            Await File.WriteAllTextAsync(configFile, json)
            Await MsgBoxAsync(My.Resources.SettingsSaved, MsgBoxButtons.Ok, "Success")
        Catch ex As Exception
            errorMessage = ex.Message
        End Try

        If Not String.IsNullOrEmpty(errorMessage) Then
            Await MsgBoxAsync($"Error saving settings: {errorMessage}", MsgBoxButtons.Ok, "Error")
        End If
    End Function

    Private Async Function LoadSettingsAsync() As Task
        Dim errorMessage As String = ""
        Dim configFileExists As Boolean = False

        Try
            Dim configFile As String = Path.Combine(AppContext.BaseDirectory, "serverconfig.json")
            configFileExists = File.Exists(configFile)
            If configFileExists Then
                Dim json As String = Await File.ReadAllTextAsync(configFile)
                ViewModel.Settings = JsonSerializer.Deserialize(Of AppSettings)(json)
                UpdateCommandPreview()
                Await MsgBoxAsync(My.Resources.SettingsLoaded, MsgBoxButtons.Ok, "Success")
            End If
        Catch ex As Exception
            errorMessage = ex.Message
        End Try

        If Not String.IsNullOrEmpty(errorMessage) Then
            Await MsgBoxAsync($"Error loading settings: {errorMessage}", MsgBoxButtons.Ok, "Error")
        ElseIf Not configFileExists Then
            Await MsgBoxAsync("No configuration file found. Using default settings.", MsgBoxButtons.Ok, "Info")
        End If
    End Function

#End Region

#Region " Server Management "

    Private Async Function StartServer() As Task
        Dim errorMessage As String = ""

        If serverRunning Then
            Await MsgBoxAsync("Server is already running!", MsgBoxButtons.Ok, "Warning")
            Return
        End If

        If String.IsNullOrEmpty(ViewModel.Settings.ServerPath) OrElse Not File.Exists(ViewModel.Settings.ServerPath) Then
            Await MsgBoxAsync(My.Resources.ErrorServerPathRequired, MsgBoxButtons.Ok, "Error")
            Return
        End If

        If String.IsNullOrEmpty(ViewModel.Settings.ModelPath) OrElse Not File.Exists(ViewModel.Settings.ModelPath) Then
            Await MsgBoxAsync(My.Resources.ErrorModelPathRequired, MsgBoxButtons.Ok, "Error")
            Return
        End If

        Try
            Dim args As String = ViewModel.UpdateCommandPreview()

            Dim startInfo As New ProcessStartInfo(ViewModel.Settings.ServerPath, args) With {
                .UseShellExecute = True,
                .CreateNoWindow = False,
                .WindowStyle = ProcessWindowStyle.Normal
            }

            serverProcess = Process.Start(startInfo)
            serverRunning = True

            StartServerButton.IsEnabled = False
            StopServerButton.IsEnabled = True

            Await CheckServerStatusAsync()
        Catch ex As Exception
            errorMessage = ex.Message
        End Try

        If Not String.IsNullOrEmpty(errorMessage) Then
            Await MsgBoxAsync($"Failed to start server: {errorMessage}", MsgBoxButtons.Ok, "Error")
        End If
    End Function

    Private Sub StopServer()
        If Not serverRunning OrElse serverProcess Is Nothing OrElse serverProcess.HasExited Then
            Return
        End If

        Try
            serverProcess.Kill()
        Catch
            ' Ignore kill errors
        Finally
            serverRunning = False
            serverProcess = Nothing

            StartServerButton.IsEnabled = True
            StopServerButton.IsEnabled = False
        End Try
    End Sub

    Private Async Function CheckServerStatusAsync() As Task
        While serverRunning AndAlso serverProcess IsNot Nothing AndAlso Not serverProcess.HasExited
            Await Task.Delay(1000)
        End While

        If serverRunning Then
            serverRunning = False
            serverProcess = Nothing

            StartServerButton.IsEnabled = True
            StopServerButton.IsEnabled = False
        End If
    End Function

#End Region

#Region " Utility Methods "

    Private Async Function CopyCommandToClipboard() As Task
        If Not String.IsNullOrEmpty(CommandPreviewTextBox.Text) Then
            Dim errorMessage As String = ""
            Try
                Await Clipboard.SetTextAsync(CommandPreviewTextBox.Text)
                Await MsgBoxAsync(My.Resources.CommandCopied, MsgBoxButtons.Ok, "Success")
            Catch ex As Exception
                errorMessage = ex.Message
            End Try

            If Not String.IsNullOrEmpty(errorMessage) Then
                Await MsgBoxAsync($"Failed to copy command: {errorMessage}", MsgBoxButtons.Ok, "Error")
            End If
        End If
    End Function

#End Region

End Class