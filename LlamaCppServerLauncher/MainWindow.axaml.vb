#Region " Imports "

Imports Avalonia.Controls
Imports Avalonia.Interactivity
Imports Avalonia.Platform.Storage
Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Text.Json

#End Region

Partial Public Class MainWindow
    Inherits Window

#Region " Fields "

    Private serverProcess As Process
    Private serverRunning As Boolean = False
    Private settings As New AppSettings()
    Private configFile As String = Path.Combine(AppContext.BaseDirectory, "serverconfig.json")

#End Region

#Region " Constructor "

    Public Sub New()
        InitializeComponent()
        DataContext = settings
        LoadSettingsSync()
        UpdateCommandPreview()
    End Sub

#End Region

#Region " UI Updates "

    Private Sub UpdateCommandPreview()
        Dim fullCommand As New StringBuilder()

        ' Server Path
        If Not String.IsNullOrEmpty(settings.ServerPath) Then
            fullCommand.Append($"""{settings.ServerPath}""")
        End If

        ' Arguments
        Dim args As String = GenerateCommandLineArguments()
        If Not String.IsNullOrEmpty(args) Then
            fullCommand.Append(" " & args)
        End If

        CommandPreviewTextBox.Text = fullCommand.ToString().Trim()
    End Sub

#End Region

#Region " Event Handlers "

    Private Sub UpdateCommandPreviewButton_Click(sender As Object, e As RoutedEventArgs) Handles UpdateCommandPreviewButton.Click
        UpdateCommandPreview()
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

    Private Sub LoadSettingsSync()
        Try
            If File.Exists(configFile) Then
                Dim json As String = File.ReadAllText(configFile)
                settings = JsonSerializer.Deserialize(Of AppSettings)(json)
                DataContext = settings ' Update DataContext
            End If
        Catch
            ' If loading fails, use default settings
            settings = New AppSettings()
            DataContext = settings ' Update DataContext
        End Try
    End Sub

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
                settings.ServerPath = files(0).Path.LocalPath
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
                settings.ModelPath = files(0).Path.LocalPath
            End If
        End If
    End Function

    Private Async Function SaveSettings() As Task
        Dim errorMessage As String = ""
        Try
            Dim json As String = JsonSerializer.Serialize(settings, New JsonSerializerOptions With {
                .WriteIndented = True
            })
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
            configFileExists = File.Exists(configFile)
            If configFileExists Then
                Dim json As String = Await File.ReadAllTextAsync(configFile)
                settings = JsonSerializer.Deserialize(Of AppSettings)(json)
                DataContext = settings ' Update DataContext
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

        If String.IsNullOrEmpty(settings.ServerPath) OrElse Not File.Exists(settings.ServerPath) Then
            Await MsgBoxAsync(My.Resources.ErrorServerPathRequired, MsgBoxButtons.Ok, "Error")
            Return
        End If

        If String.IsNullOrEmpty(settings.ModelPath) OrElse Not File.Exists(settings.ModelPath) Then
            Await MsgBoxAsync(My.Resources.ErrorModelPathRequired, MsgBoxButtons.Ok, "Error")
            Return
        End If

        Try
            Dim args As String = GenerateCommandLineArguments()

            Dim startInfo As New ProcessStartInfo(settings.ServerPath, args) With {
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

    Private Function GenerateCommandLineArguments() As String
        Dim args As New StringBuilder()

        ' Model Path
        args.Append($"""{settings.ModelPath}""")

        ' Host
        If Not String.IsNullOrEmpty(settings.Host) Then
            args.Append($" --host {settings.Host}")
        End If

        ' Port
        If settings.Port > 0 Then
            args.Append($" --port {settings.Port}")
        End If

        ' Threads
        If settings.Threads > 0 Then
            args.Append($" -t {settings.Threads}")
        End If

        ' Context Size
        If settings.CtxSize > 0 Then
            args.Append($" -c {settings.CtxSize}")
        End If

        ' GPU Layers
        If settings.NGpuLayers > 0 Then
            args.Append($" -ngl {settings.NGpuLayers}")
        End If

        ' Batch Threads
        If settings.ThreadsBatch > 0 Then
            args.Append($" -tb {settings.ThreadsBatch}")
        End If

        ' Temperature
        If settings.Temperature >= 0 Then
            args.Append($" --temp {settings.Temperature.ToString(CultureInfo.InvariantCulture)}")
        End If

        ' Repeat Penalty
        If settings.RepeatPenalty >= 0 Then
            args.Append($" --repeat-penalty {settings.RepeatPenalty.ToString(CultureInfo.InvariantCulture)}")
        End If

        ' Top K
        If settings.TopK > 0 Then
            args.Append($" --top-k {settings.TopK}")
        End If

        ' Top P
        If settings.TopP >= 0 Then
            args.Append($" --top-p {settings.TopP.ToString(CultureInfo.InvariantCulture)}")
        End If

        ' Min P
        If settings.MinP >= 0 Then
            args.Append($" --min-p {settings.MinP.ToString(CultureInfo.InvariantCulture)}")
        End If

        ' Presence Penalty
        If settings.PresencePenalty >= 0 Then
            args.Append($" --presence-penalty {settings.PresencePenalty.ToString(CultureInfo.InvariantCulture)}")
        End If

        ' Frequency Penalty
        If settings.FrequencyPenalty >= 0 Then
            args.Append($" --frequency-penalty {settings.FrequencyPenalty.ToString(CultureInfo.InvariantCulture)}")
        End If

        ' Timeout
        If settings.Timeout > 0 Then
            args.Append($" --timeout {settings.Timeout}")
        End If

        ' Memory Management
        If settings.Mlock Then
            args.Append(" -mlock")
        End If

        If settings.NoMmap Then
            args.Append(" --no-mmap")
        End If

        If settings.KVUnified Then
            args.Append(" --kv-unified")
        End If

        If settings.NoKVOffload Then
            args.Append(" --no-kv-offload")
        End If

        If settings.NoRepack Then
            args.Append(" --no-repack")
        End If

        If settings.FlashAttention Then
            args.Append(" --flash-attn")
        End If

        ' Logging
        If settings.Verbose Then
            args.Append(" -v")
        End If

        If settings.LogColors Then
            args.Append(" --log-colors")
        End If

        If settings.LogTimestamps Then
            args.Append(" --log-timestamps")
        End If

        If settings.Metrics Then
            args.Append(" --metrics")
        End If

        If settings.Slots Then
            args.Append(" --slots")
        End If

        Return args.ToString().Trim()
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