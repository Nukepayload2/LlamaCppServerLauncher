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

    Private Sub ClearFiltersButton_Click(sender As Object, e As RoutedEventArgs) Handles ClearFiltersButton.Click
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

    Private Shared ReadOnly s_saveOptions As New JsonSerializerOptions With {
        .WriteIndented = True, .DefaultIgnoreCondition = Serialization.JsonIgnoreCondition.WhenWritingNull
    }

    Private Async Function SaveSettings() As Task
        Dim errorMessage As String = ""
        Try
            ' 创建一个新的AppSettings，只保存有值的参数
            Dim settingsToSave As New AppSettings()

            ' 复制基本设置
            settingsToSave.ServerPath = ViewModel.Settings.ServerPath
            settingsToSave.ModelPath = ViewModel.Settings.ModelPath
            settingsToSave.Host = ViewModel.Settings.Host
            settingsToSave.Port = ViewModel.Settings.Port

            ' 只保存HasLocalValue=True的参数
            If ViewModel.Settings.ServerParameters IsNot Nothing Then
                For Each param In ViewModel.Settings.ServerParameters
                    If param.HasLocalValue Then
                        settingsToSave.ServerParameters.Add(param)
                    End If
                Next
            End If

            Dim json As String = JsonSerializer.Serialize(settingsToSave, s_saveOptions)
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
                Dim loadedSettings = JsonSerializer.Deserialize(Of AppSettings.IoModel)(json)

                ' 先重置当前所有参数到默认值
                ViewModel.Settings.ServerParameters.Clear()
                ViewModel.Settings.ServerParameters.InitializeFromMetadata()

                ' 重置基本设置
                ViewModel.Settings.ServerPath = ""
                ViewModel.Settings.ModelPath = ""
                ViewModel.Settings.Host = ""
                ViewModel.Settings.Port = 8080

                ' 然后应用加载的设置
                If loadedSettings.ServerParameters IsNot Nothing Then
                    For Each loadedParam In loadedSettings.ServerParameters
                        ' 找到对应的参数并更新其值
                        Dim existingParam = ViewModel.Settings.ServerParameters.FirstOrDefault(Function(p) p.Argument = loadedParam.Argument)
                        If existingParam IsNot Nothing Then
                            ' 复制值而不是整个对象
                            If loadedParam.Value.StringValue IsNot Nothing Then
                                existingParam.Value.StringValue = loadedParam.Value.StringValue
                            End If
                            If loadedParam.Value.DoubleValue.HasValue Then
                                existingParam.Value.DoubleValue = loadedParam.Value.DoubleValue.Value
                            End If
                            If loadedParam.Value.BooleanValue.HasValue Then
                                existingParam.Value.BooleanValue = loadedParam.Value.BooleanValue.Value
                            End If
                        End If
                    Next
                End If

                ' 更新基本设置
                ViewModel.Settings.NotifyBasicPropertiesChanged()

                ' 触发UI更新
                ViewModel.LoadSettingsSync()
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

    Private Sub MainWindow_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        ActiveWindow = Me
    End Sub

#End Region

End Class