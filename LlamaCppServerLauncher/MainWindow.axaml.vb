#Region " Imports "

Imports Avalonia
Imports Avalonia.Controls
Imports Avalonia.Controls.ApplicationLifetimes
Imports Avalonia.Interactivity
Imports Avalonia.Platform.Storage
Imports Avalonia.Threading
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Threading.Tasks

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
        LoadSettings()
        UpdateCommandPreview()
    End Sub

#End Region

#Region " UI Updates "

    Private Sub UpdateCommandPreview()
        Dim args As New StringBuilder()

        ' Server Path
        If Not String.IsNullOrEmpty(settings.ServerPath) Then
            args.Append($"""{settings.ServerPath}""")
        End If

        ' Model Path
        If Not String.IsNullOrEmpty(settings.ModelPath) Then
            args.Append($" -m ""{settings.ModelPath}""")
        End If

        ' Basic Parameters
        If settings.Threads > 0 Then
            args.Append($" -t {settings.Threads}")
        End If

        If settings.CtxSize > 0 Then
            args.Append($" -c {settings.CtxSize}")
        End If

        If settings.NPredict > 0 Then
            args.Append($" -n {settings.NPredict}")
        End If

        If settings.NGpuLayers > 0 Then
            args.Append($" -ngl {settings.NGpuLayers}")
        End If

        ' Sampling Parameters
        args.Append($" --temp {settings.Temperature}")
        args.Append($" --top-p {settings.TopP}")
        args.Append($" --top-k {settings.TopK}")
        args.Append($" --repeat-penalty {settings.RepeatPenalty}")

        If settings.MinP > 0 Then
            args.Append($" --min-p {settings.MinP}")
        End If

        If settings.PresencePenalty <> 0 Then
            args.Append($" --presence-penalty {settings.PresencePenalty}")
        End If

        If settings.FrequencyPenalty <> 0 Then
            args.Append($" --frequency-penalty {settings.FrequencyPenalty}")
        End If

        ' Network Parameters
        args.Append($" --host {settings.Host}")
        args.Append($" --port {settings.Port}")
        args.Append($" --timeout {settings.Timeout}")

        ' Boolean Parameters
        If settings.Mlock Then
            args.Append(" --mlock")
        End If

        If settings.NoMmap Then
            args.Append(" --no-mmap")
        End If

        If settings.NoKVOffload Then
            args.Append(" --no-kv-offload")
        End If

        If settings.NoRepack Then
            args.Append(" --no-repack")
        End If

        If settings.KVUnified Then
            args.Append(" --kv-unified")
        End If

        If settings.FlashAttention Then
            args.Append(" --flash-attn")
        End If

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

        ' Additional Parameters
        If settings.ThreadsBatch > 0 Then
            args.Append($" -tb {settings.ThreadsBatch}")
        End If

        ' LoRA Adapters
        ' For Each lora As AppSettings.LoraConfig In settings.Lora
        '     If Not String.IsNullOrEmpty(lora.Path) Then
        '         args.Append($" --lora ""{lora.Path}""")
        '         If lora.Scale <> 1.0 Then
        '             args.Append($" {lora.Scale}")
        '         End If
        '     End If
        ' Next

        ' Control Vectors
        ' For Each cv As AppSettings.ControlVectorConfig In settings.ControlVector
        '     If Not String.IsNullOrEmpty(cv.Path) Then
        '         args.Append($" --control-vector ""{cv.Path}""")
        '         If cv.Scale <> 1.0 Then
        '             args.Append($" {cv.Scale}")
        '         End If
        '     End If
        ' Next

        CommandPreviewTextBox.Text = args.ToString().Trim()
    End Sub

    Private Sub UpdateUIFromSettings()
        ' Basic Settings
        ServerPathTextBox.Text = settings.ServerPath
        ModelPathTextBox.Text = settings.ModelPath
        ThreadsNumericUpDown.Value = settings.Threads
        CtxSizeNumericUpDown.Value = settings.CtxSize
        GpuLayersNumericUpDown.Value = settings.NGpuLayers
        HostTextBox.Text = settings.Host
        PortNumericUpDown.Value = settings.Port
        TimeoutNumericUpDown.Value = settings.Timeout

        ' Sampling Settings
        TemperatureNumericUpDown.Value = settings.Temperature
        TopPNumericUpDown.Value = settings.TopP
        TopKNumericUpDown.Value = settings.TopK
        RepeatPenaltyNumericUpDown.Value = settings.RepeatPenalty
        MinPNumericUpDown.Value = settings.MinP
        PresencePenaltyNumericUpDown.Value = settings.PresencePenalty
        FrequencyPenaltyNumericUpDown.Value = settings.FrequencyPenalty

        ' Advanced Settings
        MlockCheckBox.IsChecked = settings.Mlock
        NoMmapCheckBox.IsChecked = settings.NoMmap
        NoKVOffloadCheckBox.IsChecked = settings.NoKVOffload
        NoRepackCheckBox.IsChecked = settings.NoRepack
        KVUnifiedCheckBox.IsChecked = settings.KVUnified
        FlashAttentionCheckBox.IsChecked = settings.FlashAttention
        VerboseCheckBox.IsChecked = settings.Verbose
        LogColorsCheckBox.IsChecked = settings.LogColors
        LogTimestampsCheckBox.IsChecked = settings.LogTimestamps
        MetricsCheckBox.IsChecked = settings.Metrics
        SlotsCheckBox.IsChecked = settings.Slots
        ThreadsBatchNumericUpDown.Value = settings.ThreadsBatch
    End Sub

    Private Sub UpdateSettingsFromUI()
        ' Basic Settings
        settings.ServerPath = ServerPathTextBox.Text
        settings.ModelPath = ModelPathTextBox.Text
        settings.Threads = CInt(ThreadsNumericUpDown.Value)
        settings.CtxSize = CInt(CtxSizeNumericUpDown.Value)
        settings.NGpuLayers = CInt(GpuLayersNumericUpDown.Value)
        settings.Host = HostTextBox.Text
        settings.Port = CInt(PortNumericUpDown.Value)
        settings.Timeout = CInt(TimeoutNumericUpDown.Value)

        ' Sampling Settings
        settings.Temperature = CDbl(TemperatureNumericUpDown.Value)
        settings.TopP = CDbl(TopPNumericUpDown.Value)
        settings.TopK = CInt(TopKNumericUpDown.Value)
        settings.RepeatPenalty = CDbl(RepeatPenaltyNumericUpDown.Value)
        settings.MinP = CDbl(MinPNumericUpDown.Value)
        settings.PresencePenalty = CDbl(PresencePenaltyNumericUpDown.Value)
        settings.FrequencyPenalty = CDbl(FrequencyPenaltyNumericUpDown.Value)

        ' Advanced Settings
        settings.Mlock = MlockCheckBox.IsChecked.GetValueOrDefault()
        settings.NoMmap = NoMmapCheckBox.IsChecked.GetValueOrDefault()
        settings.NoKVOffload = NoKVOffloadCheckBox.IsChecked.GetValueOrDefault()
        settings.NoRepack = NoRepackCheckBox.IsChecked.GetValueOrDefault()
        settings.KVUnified = KVUnifiedCheckBox.IsChecked.GetValueOrDefault()
        settings.FlashAttention = FlashAttentionCheckBox.IsChecked.GetValueOrDefault()
        settings.Verbose = VerboseCheckBox.IsChecked.GetValueOrDefault()
        settings.LogColors = LogColorsCheckBox.IsChecked.GetValueOrDefault()
        settings.LogTimestamps = LogTimestampsCheckBox.IsChecked.GetValueOrDefault()
        settings.Metrics = MetricsCheckBox.IsChecked.GetValueOrDefault()
        settings.Slots = SlotsCheckBox.IsChecked.GetValueOrDefault()
        settings.ThreadsBatch = CInt(ThreadsBatchNumericUpDown.Value)
    End Sub

#End Region

#Region " Event Handlers "

    Private Sub UpdateCommandPreviewButton_Click(sender As Object, e As RoutedEventArgs) Handles UpdateCommandPreviewButton.Click
        UpdateSettingsFromUI()
        UpdateCommandPreview()
    End Sub

    Private Sub BrowseServerButton_Click(sender As Object, e As RoutedEventArgs) Handles BrowseServerButton.Click
        BrowseForServer()
    End Sub

    Private Sub BrowseModelButton_Click(sender As Object, e As RoutedEventArgs) Handles BrowseModelButton.Click
        BrowseForModel()
    End Sub

    Private Sub StartServerButton_Click(sender As Object, e As RoutedEventArgs) Handles StartServerButton.Click
        StartServer()
    End Sub

    Private Sub StopServerButton_Click(sender As Object, e As RoutedEventArgs) Handles StopServerButton.Click
        StopServer()
    End Sub

    Private Sub SaveSettingsButton_Click(sender As Object, e As RoutedEventArgs) Handles SaveSettingsButton.Click
        SaveSettings()
    End Sub

    Private Sub LoadSettingsButton_Click(sender As Object, e As RoutedEventArgs) Handles LoadSettingsButton.Click
        LoadSettings()
    End Sub

    Private Sub CopyCommandButton_Click(sender As Object, e As RoutedEventArgs) Handles CopyCommandButton.Click
        CopyCommandToClipboard()
    End Sub

#End Region

#Region " File Operations "

    Private Async Sub BrowseForServer()
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
                ServerPathTextBox.Text = files(0).Path.LocalPath
            End If
        End If
    End Sub

    Private Async Sub BrowseForModel()
        If StorageProvider IsNot Nothing Then
            Dim files As IReadOnlyList(Of IStorageFile) =
                Await StorageProvider.OpenFilePickerAsync(New FilePickerOpenOptions With {
                    .Title = "Select LLaMA.cpp Model File",
                    .AllowMultiple = False,
                    .FileTypeFilter = New List(Of FilePickerFileType) From {
                        New FilePickerFileType("Model Files") With {
                            .Patterns = New List(Of String) From {"*.gguf", "*.bin", "*.model"}
                        }
                    }
                })

            If files.Count > 0 Then
                ModelPathTextBox.Text = files(0).Path.LocalPath
            End If
        End If
    End Sub

    Private Async Sub SaveSettings()
        Dim errorMessage As String = ""
        Try
            UpdateSettingsFromUI()
            Dim json As String = System.Text.Json.JsonSerializer.Serialize(settings, New System.Text.Json.JsonSerializerOptions With {
                .WriteIndented = True
            })
            Await File.WriteAllTextAsync(configFile, json)
            Await Dispatcher.UIThread.InvokeAsync(Sub()
                MsgBox(My.Resources.SettingsSaved, MsgBoxStyle.Information, "Success")
            End Sub)
        Catch ex As Exception
            errorMessage = ex.Message
        End Try
        
        If Not String.IsNullOrEmpty(errorMessage) Then
            Await Dispatcher.UIThread.InvokeAsync(Sub()
                MsgBox($"Error saving settings: {errorMessage}", MsgBoxStyle.Exclamation, "Error")
            End Sub)
        End If
    End Sub

    Private Async Sub LoadSettings()
        Dim errorMessage As String = ""
        Dim configFileExists As Boolean = False
        
        Try
            configFileExists = File.Exists(configFile)
            If configFileExists Then
                Dim json As String = Await File.ReadAllTextAsync(configFile)
                settings = System.Text.Json.JsonSerializer.Deserialize(Of AppSettings)(json)
                UpdateUIFromSettings()
                UpdateCommandPreview()
                Await Dispatcher.UIThread.InvokeAsync(Sub()
                    MsgBox(My.Resources.SettingsLoaded, MsgBoxStyle.Information, "Success")
                End Sub)
            End If
        Catch ex As Exception
            errorMessage = ex.Message
        End Try
        
        If Not String.IsNullOrEmpty(errorMessage) Then
            Await Dispatcher.UIThread.InvokeAsync(Sub()
                MsgBox($"Error loading settings: {errorMessage}", MsgBoxStyle.Exclamation, "Error")
            End Sub)
        ElseIf Not configFileExists Then
            Await Dispatcher.UIThread.InvokeAsync(Sub()
                MsgBox("No configuration file found. Using default settings.", MsgBoxStyle.Information, "Info")
            End Sub)
        End If
    End Sub

#End Region

#Region " Server Management "

    Private Async Sub StartServer()
        Dim errorMessage As String = ""
        
        If serverRunning Then
            Await Dispatcher.UIThread.InvokeAsync(Sub()
                MsgBox("Server is already running!", MsgBoxStyle.Exclamation, "Warning")
            End Sub)
            Return
        End If

        UpdateSettingsFromUI()

        If String.IsNullOrEmpty(settings.ServerPath) OrElse Not File.Exists(settings.ServerPath) Then
            Await Dispatcher.UIThread.InvokeAsync(Sub()
                MsgBox(My.Resources.ErrorServerPathRequired, MsgBoxStyle.Exclamation, "Error")
            End Sub)
            Return
        End If

        If String.IsNullOrEmpty(settings.ModelPath) OrElse Not File.Exists(settings.ModelPath) Then
            Await Dispatcher.UIThread.InvokeAsync(Sub()
                MsgBox(My.Resources.ErrorModelPathRequired, MsgBoxStyle.Exclamation, "Error")
            End Sub)
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
            Await Dispatcher.UIThread.InvokeAsync(Sub()
                MsgBox($"Failed to start server: {errorMessage}", MsgBoxStyle.Exclamation, "Error")
            End Sub)
        End If
    End Sub

    Private Sub StopServer()
        If Not serverRunning OrElse serverProcess Is Nothing OrElse serverProcess.HasExited Then
            Return
        End If

        Try
            serverProcess.Kill()
            serverProcess.WaitForExit()
        Catch ex As Exception
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

            Await Dispatcher.UIThread.InvokeAsync(Sub()
                StartServerButton.IsEnabled = True
                StopServerButton.IsEnabled = False
            End Sub)
        End If
    End Function

    Private Function GenerateCommandLineArguments() As String
        Dim args As New StringBuilder()

        ' Model Path
        If Not String.IsNullOrEmpty(settings.ModelPath) Then
            args.Append($"-m ""{settings.ModelPath}"" ")
        End If

        ' Basic Parameters
        If settings.Threads > 0 Then
            args.Append($"-t {settings.Threads} ")
        End If

        If settings.CtxSize > 0 Then
            args.Append($"-c {settings.CtxSize} ")
        End If

        If settings.NPredict > 0 Then
            args.Append($"-n {settings.NPredict} ")
        End If

        If settings.NGpuLayers > 0 Then
            args.Append($"-ngl {settings.NGpuLayers} ")
        End If

        ' Sampling Parameters
        args.Append($"--temp {settings.Temperature} ")
        args.Append($"--top-p {settings.TopP} ")
        args.Append($"--top-k {settings.TopK} ")
        args.Append($"--repeat-penalty {settings.RepeatPenalty} ")

        If settings.MinP > 0 Then
            args.Append($"--min-p {settings.MinP} ")
        End If

        If settings.PresencePenalty <> 0 Then
            args.Append($"--presence-penalty {settings.PresencePenalty} ")
        End If

        If settings.FrequencyPenalty <> 0 Then
            args.Append($"--frequency-penalty {settings.FrequencyPenalty} ")
        End If

        ' Network Parameters
        args.Append($"--host {settings.Host} ")
        args.Append($"--port {settings.Port} ")
        args.Append($"--timeout {settings.Timeout} ")

        ' Boolean Parameters
        If settings.Mlock Then
            args.Append("--mlock ")
        End If

        If settings.NoMmap Then
            args.Append("--no-mmap ")
        End If

        If settings.NoKVOffload Then
            args.Append("--no-kv-offload ")
        End If

        If settings.NoRepack Then
            args.Append("--no-repack ")
        End If

        If settings.KVUnified Then
            args.Append("--kv-unified ")
        End If

        If settings.FlashAttention Then
            args.Append("--flash-attn ")
        End If

        If settings.Verbose Then
            args.Append("-v ")
        End If

        If settings.LogColors Then
            args.Append("--log-colors ")
        End If

        If settings.LogTimestamps Then
            args.Append("--log-timestamps ")
        End If

        If settings.Metrics Then
            args.Append("--metrics ")
        End If

        If settings.Slots Then
            args.Append("--slots ")
        End If

        ' Additional Parameters
        If settings.ThreadsBatch > 0 Then
            args.Append($"-tb {settings.ThreadsBatch} ")
        End If

        Return args.ToString().Trim()
    End Function

#End Region

#Region " Utility Methods "

    Private Async Sub CopyCommandToClipboard()
        If Not String.IsNullOrEmpty(CommandPreviewTextBox.Text) Then
            Dim errorMessage As String = ""
            Try
                Await Clipboard.SetTextAsync(CommandPreviewTextBox.Text)
                Await Dispatcher.UIThread.InvokeAsync(Sub()
                    MsgBox(My.Resources.CommandCopied, MsgBoxStyle.Information, "Success")
                End Sub)
            Catch ex As Exception
                errorMessage = ex.Message
            End Try
            
            If Not String.IsNullOrEmpty(errorMessage) Then
                Await Dispatcher.UIThread.InvokeAsync(Sub()
                    MsgBox($"Failed to copy command: {errorMessage}", MsgBoxStyle.Exclamation, "Error")
                End Sub)
            End If
        End If
    End Sub

#End Region

End Class