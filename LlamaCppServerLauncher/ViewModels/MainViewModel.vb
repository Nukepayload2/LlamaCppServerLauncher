Imports System.ComponentModel
Imports System.Linq
Imports System.Text
Imports System.Text.Json
Imports System.IO
Imports System.Threading
Imports System.Threading.Tasks
Imports Avalonia.Threading
Imports Avalonia.Platform.Storage
Imports Avalonia.Controls
Imports System.Windows.Input
Imports System.Diagnostics
Imports LlamaCppServerLauncher.Helpers
Imports System.Runtime.InteropServices

Public Class MainViewModel
    Inherits ObservableBase
    Implements IDisposable

#Region " Fields and Constants "

    ' Console event constants
    Private Const CTRL_C_EVENT As Integer = 0
    Private Const CTRL_BREAK_EVENT As Integer = 1

    ' Windows API declarations
    Private Declare Function GenerateConsoleCtrlEvent Lib "kernel32" (
        ByVal dwCtrlEvent As Integer,
        ByVal dwProcessGroupId As Integer
    ) As Boolean

    Private _settings As AppSettings = AppSettings.WithServerParameters
    Private _filterText As String = ""
    Private _selectedCategory As String = "所有分类"
    Private _showModifiedOnly As Boolean = False
    Private _filteredParameters As List(Of ServerParameterItem)
    Private _availableCategories As List(Of String)

    ' 防抖相关字段
    Private WithEvents DebounceTimer As New DispatcherTimer With {
        .Interval = TimeSpan.FromMilliseconds(DEBOUNCE_DELAY)
    }
    Private Const DEBOUNCE_DELAY As Integer = 300 ' 300毫秒防抖延迟

    ' 服务器管理相关字段
    Private _serverProcess As Process
    Private _serverRunning As Boolean = False
    Private WithEvents StatusCheckTimer As New DispatcherTimer With {
        .Interval = TimeSpan.FromSeconds(1)
    }

    ' 命令预览字段
    Private _commandPreview As String = ""

#End Region

#Region " Properties "

    Public Property Settings As AppSettings
        Get
            Return _settings
        End Get
        Set(value As AppSettings)
            SetProperty(_settings, value)
        End Set
    End Property

    Public Property FilterText As String
        Get
            Return _filterText
        End Get
        Set(value As String)
            SetProperty(_filterText, value)
            DebounceUpdateFilteredParameters()
        End Set
    End Property

    Public Property SelectedCategory As String
        Get
            Return _selectedCategory
        End Get
        Set(value As String)
            SetProperty(_selectedCategory, value)
            UpdateFilteredParameters()
        End Set
    End Property

    Public Property ShowModifiedOnly As Boolean
        Get
            Return _showModifiedOnly
        End Get
        Set(value As Boolean)
            SetProperty(_showModifiedOnly, value)
            UpdateFilteredParameters()
        End Set
    End Property

    Public ReadOnly Property AvailableCategories As List(Of String)
        Get
            If _availableCategories IsNot Nothing Then Return _availableCategories

            If Settings.ServerParameters Is Nothing Then
                _availableCategories = New List(Of String) From {"所有分类"}
                Return _availableCategories
            End If

            Dim categories = Settings.ServerParameters.Select(Function(p) p.Metadata?.Category).
                                              Where(Function(c) Not String.IsNullOrEmpty(c)).
                                              Distinct().
                                              OrderBy(Function(c) c).
                                              ToList()

            _availableCategories = New List(Of String) From {"所有分类"}
            _availableCategories.AddRange(categories)
            Return _availableCategories
        End Get
    End Property

    Public ReadOnly Property FilteredParameters As List(Of ServerParameterItem)
        Get
            If _filteredParameters IsNot Nothing Then Return _filteredParameters

            Return ComputeFilteredParameters()
        End Get
    End Property

    Public ReadOnly Property TotalParameters As Integer
        Get
            Return If(Settings.ServerParameters?.Count, 0)
        End Get
    End Property

    Public ReadOnly Property ModifiedParameters As Integer
        Get
            Return If(Settings.ServerParameters?.Where(Function(p) p.HasLocalValue).Count(), 0)
        End Get
    End Property

    Public ReadOnly Property DefaultParameters As Integer
        Get
            Return If(Settings.ServerParameters?.Where(Function(p) Not p.HasLocalValue).Count(), 0)
        End Get
    End Property

    Public ReadOnly Property ParameterCountText As String
        Get
            Dim filtered = FilteredParameters.Count
            Dim total = TotalParameters
            Return $"显示 {filtered} / {total} 个参数"
        End Get
    End Property

    Public Property CommandPreview As String
        Get
            Return _commandPreview
        End Get
        Set(value As String)
            SetProperty(_commandPreview, value)
        End Set
    End Property

#Region " Commands "

    Public ReadOnly Property UpdateCommandPreviewCommand As ICommand = New RelayCommand(Function(param, cancellationToken) UpdateCommandPreviewAsync(cancellationToken))
    Public ReadOnly Property ClearFiltersCommand As ICommand = New RelayCommand(AddressOf ClearFilters)
    Public ReadOnly Property BrowseServerCommand As ICommand = New RelayCommand(Function(param, cancellationToken) BrowseForServerAsync(cancellationToken))
    Public ReadOnly Property BrowseModelCommand As ICommand = New RelayCommand(Function(param, cancellationToken) BrowseForModelAsync(cancellationToken))
    Public ReadOnly Property StartServerCommand As ICommand = New RelayCommand(Function(param, cancellationToken) StartServerAsync(cancellationToken))
    Public ReadOnly Property StopServerCommand As ICommand = New RelayCommand(Function(param, cancellationToken) StopServer(cancellationToken))
    Public ReadOnly Property SaveSettingsCommand As ICommand = New RelayCommand(Function(param, cancellationToken) SaveSettingsAsync(cancellationToken))
    Public ReadOnly Property LoadSettingsCommand As ICommand = New RelayCommand(Function(param, cancellationToken) LoadSettingsAsync(cancellationToken))
    Public ReadOnly Property CopyCommandCommand As ICommand = New RelayCommand(Function(param, cancellationToken) CopyCommandToClipboardAsync(cancellationToken))

#End Region

#End Region

#Region " Constructor "

    Public Sub New()
        LoadSettingsSync()
        InitializeDebounceTimer()
        InitializeStatusCheckTimer()
        ' 为所有参数订阅属性变化事件
        SubscribeToParameterChanges()
        ' 初始化命令预览
        CommandPreview = UpdateCommandPreview()
    End Sub

#End Region

#Region " Public Methods "

    Public Sub ClearFilters()
        FilterText = ""
        SelectedCategory = "所有分类"
        ShowModifiedOnly = False
    End Sub

    Private Sub SubscribeToParameterChanges()
        If Settings.ServerParameters IsNot Nothing Then
            For Each param In Settings.ServerParameters
                AddHandler param.PropertyChanged, AddressOf OnParameterPropertyChanged
            Next
        End If
    End Sub

    Private Sub OnParameterPropertyChanged(sender As Object, e As PropertyChangedEventArgs)
        ' 当参数的HasLocalValue状态变化时，更新统计属性
        If e.PropertyName = "HasLocalValue" Then
            OnPropertyChanged(NameOf(TotalParameters))
            OnPropertyChanged(NameOf(ModifiedParameters))
            OnPropertyChanged(NameOf(DefaultParameters))
            OnPropertyChanged(NameOf(ParameterCountText))
        End If
    End Sub

    Public Function UpdateCommandPreview() As String
        Dim fullCommand As New StringBuilder()

        ' Server Path
        Dim serverPathParam = Settings.ServerParameterByName("--server-path")
        If serverPathParam IsNot Nothing AndAlso Not String.IsNullOrEmpty(serverPathParam.Value.StringValue) Then
            fullCommand.Append($"""{serverPathParam.Value.StringValue}""")
        End If

        ' Generate arguments from ServerParameterCollection
        If Settings.ServerParameters IsNot Nothing Then
            For Each param In Settings.ServerParameters
                If param.HasLocalValue AndAlso param.Argument <> "--server-path" Then
                    Dim argument = GenerateArgumentFromParameter(param)
                    If Not String.IsNullOrEmpty(argument) Then
                        fullCommand.Append($" {argument}")
                    End If
                End If
            Next
        End If

        Dim result = fullCommand.ToString().Trim()
        CommandPreview = result
        Return result
    End Function

    Public Sub LoadSettingsSync()
        ' Reset cached values
        _filteredParameters = Nothing
        _availableCategories = Nothing
        UpdateFilteredParameters()
    End Sub

#Region " Command Methods "

    Private Async Function UpdateCommandPreviewAsync(cancellationToken As CancellationToken) As Task
        ' 手动触发命令预览更新
        cancellationToken.ThrowIfCancellationRequested()
        UpdateCommandPreview()
        Await Task.CompletedTask
    End Function

    Private Async Function BrowseForServerAsync(cancellationToken As CancellationToken) As Task
        cancellationToken.ThrowIfCancellationRequested()
        Dim storageProvider = My.Application.MainWindow.StorageProvider
        If storageProvider IsNot Nothing Then
            Dim files = Await storageProvider.OpenFilePickerAsync(New FilePickerOpenOptions With {
                .Title = "Select LLaMA.cpp Server Executable",
                .AllowMultiple = False,
                .FileTypeFilter = New List(Of FilePickerFileType) From {
                    New FilePickerFileType("Executable Files") With {
                        .Patterns = New List(Of String) From {"*.exe"}
                    }
                }
            })

            cancellationToken.ThrowIfCancellationRequested()
            If files.Count > 0 Then
                Dim serverPathParam = Settings.ServerParameterByName("--server-path")
                If serverPathParam IsNot Nothing Then
                    serverPathParam.Value.StringValue = files(0).Path.LocalPath
                End If
            End If
        End If
    End Function

    Private Async Function BrowseForModelAsync(cancellationToken As CancellationToken) As Task
        cancellationToken.ThrowIfCancellationRequested()
        Dim storageProvider = My.Application.MainWindow.StorageProvider
        If storageProvider IsNot Nothing Then
            Dim files = Await storageProvider.OpenFilePickerAsync(New FilePickerOpenOptions With {
                .Title = "Select LLaMA.cpp Model File",
                .AllowMultiple = False,
                .FileTypeFilter = New List(Of FilePickerFileType) From {
                    New FilePickerFileType("Model Files") With {
                        .Patterns = New List(Of String) From {"*.gguf", "*.bin"}
                    }
                }
            })

            cancellationToken.ThrowIfCancellationRequested()
            If files.Count > 0 Then
                Dim modelPathParam = Settings.ServerParameterByName("--model")
                If modelPathParam IsNot Nothing Then
                    modelPathParam.Value.StringValue = files(0).Path.LocalPath
                End If
            End If
        End If
    End Function

    Private Async Function StartServerAsync(cancellationToken As CancellationToken) As Task
        cancellationToken.ThrowIfCancellationRequested()
        If _serverRunning Then
            Await MsgBoxAsync("Server is already running!", MsgBoxButtons.Ok, "Warning", My.Application.MainWindow)
            Return
        End If

        cancellationToken.ThrowIfCancellationRequested()
        Dim serverPathParam = Settings.ServerParameterByName("--server-path")
        Dim serverPath As String = If(serverPathParam?.Value.StringValue, "")
        If String.IsNullOrEmpty(serverPath) OrElse Not File.Exists(serverPath) Then
            Await MsgBoxAsync(My.Resources.ErrorServerPathRequired, MsgBoxButtons.Ok, "Error", My.Application.MainWindow)
            Return
        End If

        cancellationToken.ThrowIfCancellationRequested()
        Dim modelPathParam = Settings.ServerParameterByName("--model")
        Dim modelPath As String = If(modelPathParam?.Value.StringValue, "")
        If String.IsNullOrEmpty(modelPath) OrElse Not File.Exists(modelPath) Then
            Await MsgBoxAsync(My.Resources.ErrorModelPathRequired, MsgBoxButtons.Ok, "Error", My.Application.MainWindow)
            Return
        End If

        Dim errorMessage As String = ""
        Try
            cancellationToken.ThrowIfCancellationRequested()
            Dim args As String = UpdateCommandPreview()
            Dim startInfo As New ProcessStartInfo(serverPath, args) With {
                .UseShellExecute = True,
                .CreateNoWindow = False,
                .WindowStyle = ProcessWindowStyle.Normal
            }

            _serverProcess = Process.Start(startInfo)
            _serverRunning = True

            ' 启动状态检查定时器
            StatusCheckTimer.IsEnabled = True
        Catch ex As Exception
            errorMessage = ex.Message
        End Try

        cancellationToken.ThrowIfCancellationRequested()
        If Not String.IsNullOrEmpty(errorMessage) Then
            Await MsgBoxAsync($"Failed to start server: {errorMessage}", MsgBoxButtons.Ok, "Error", My.Application.MainWindow)
        End If
    End Function

    Private Async Function StopServer(cancel2 As CancellationToken) As Task
        If Not _serverRunning OrElse _serverProcess Is Nothing OrElse _serverProcess.HasExited Then
            Return
        End If

        Try
            ' Send graceful shutdown signal using CTRL_C_EVENT
            Dim result = GenerateConsoleCtrlEvent(CTRL_C_EVENT, _serverProcess.SessionId)
            cancel2.ThrowIfCancellationRequested()

            If result Then
                ' Wait up to 3 seconds for graceful shutdown
                Dim timeout As New CancellationTokenSource
                timeout.CancelAfter(3000)
                Try
                    Await _serverProcess.WaitForExitAsync(timeout.Token)
                Catch ex As OperationCanceledException When ex.CancellationToken = timeout.Token
                    ' If process is still running, force kill it
                    If Not _serverProcess.HasExited Then
                        _serverProcess.Kill()
                    End If
                End Try
            Else
                ' If sending event failed, force kill immediately
                _serverProcess.Kill()
            End If
        Catch
            ' Ignore kill errors
        Finally
            _serverRunning = False
            _serverProcess = Nothing
            ' 停止状态检查定时器
            StatusCheckTimer.IsEnabled = False
        End Try
    End Function

    Private Sub CheckServerStatus() Handles StatusCheckTimer.Tick
        If Not _serverRunning OrElse _serverProcess Is Nothing OrElse _serverProcess.HasExited Then
            _serverRunning = False
            _serverProcess = Nothing
            StatusCheckTimer.IsEnabled = False
        End If
    End Sub

    Private Async Function SaveSettingsAsync(cancellationToken As CancellationToken) As Task
        cancellationToken.ThrowIfCancellationRequested()
        Dim errorMessage As String = ""
        Try
            ' 创建一个新的AppSettings，只保存有值的参数
            Dim settingsToSave As New AppSettings.IoModel()

            ' 只保存HasLocalValue=True的参数
            Dim params As New List(Of ServerParameterItem)
            If Settings.ServerParameters IsNot Nothing Then
                For Each param In Settings.ServerParameters
                    If param.HasLocalValue Then
                        params.Add(param)
                    End If
                Next
            End If
            settingsToSave.ServerParameters = params.ToArray

            Dim json As String = JsonSerializer.Serialize(settingsToSave, New JsonSerializerOptions With {
                .WriteIndented = True,
                .DefaultIgnoreCondition = Serialization.JsonIgnoreCondition.WhenWritingNull
            })
            Dim configFile As String = Path.Combine(AppContext.BaseDirectory, "serverconfig.json")
            Await File.WriteAllTextAsync(configFile, json, cancellationToken)

            cancellationToken.ThrowIfCancellationRequested()
            Await MsgBoxAsync(My.Resources.SettingsSaved, MsgBoxButtons.Ok, "Success", My.Application.MainWindow)
        Catch ex As Exception
            errorMessage = ex.Message
        End Try

        cancellationToken.ThrowIfCancellationRequested()
        If Not String.IsNullOrEmpty(errorMessage) Then
            Await MsgBoxAsync($"Error saving settings: {errorMessage}", MsgBoxButtons.Ok, "Error", My.Application.MainWindow)
        End If
    End Function

    Private Async Function LoadSettingsAsync(cancellationToken As CancellationToken) As Task
        cancellationToken.ThrowIfCancellationRequested()
        Dim errorMessage As String = ""
        Dim configFileExists As Boolean = False

        Try
            Dim configFile As String = Path.Combine(AppContext.BaseDirectory, "serverconfig.json")
            configFileExists = File.Exists(configFile)
            If configFileExists Then
                Dim json As String = Await File.ReadAllTextAsync(configFile, cancellationToken)
                Dim loadedSettings = JsonSerializer.Deserialize(Of AppSettings.IoModel)(json)

                ' 先重置当前所有参数到默认值
                Settings.ResetToDefault()

                ' 然后应用加载的设置
                If loadedSettings.ServerParameters IsNot Nothing Then
                    For Each loadedParam In loadedSettings.ServerParameters
                        ' 找到对应的参数并更新其值
                        Dim existingParam As ServerParameterItem = Nothing
                        If Settings.ServerParameterByName.TryGetValue(loadedParam.Argument, existingParam) Then
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


                ' 触发UI更新
                LoadSettingsSync()

                cancellationToken.ThrowIfCancellationRequested()
                Await MsgBoxAsync(My.Resources.SettingsLoaded, MsgBoxButtons.Ok, "Success", My.Application.MainWindow)
            End If
        Catch ex As Exception
            errorMessage = ex.Message
        End Try

        cancellationToken.ThrowIfCancellationRequested()
        If Not String.IsNullOrEmpty(errorMessage) Then
            Await MsgBoxAsync($"Error loading settings: {errorMessage}", MsgBoxButtons.Ok, "Error", My.Application.MainWindow)
        ElseIf Not configFileExists Then
            Await MsgBoxAsync("No configuration file found. Using default settings.", MsgBoxButtons.Ok, "Info", My.Application.MainWindow)
        End If
    End Function

    Private Async Function CopyCommandToClipboardAsync(cancellationToken As CancellationToken) As Task
        cancellationToken.ThrowIfCancellationRequested()
        Dim commandText = UpdateCommandPreview()
        If Not String.IsNullOrEmpty(commandText) Then
            Dim errorMessage As String = ""
            Try
                Await My.Application.MainWindow.Clipboard.SetTextAsync(commandText)
                cancellationToken.ThrowIfCancellationRequested()
                Await MsgBoxAsync(My.Resources.CommandCopied, MsgBoxButtons.Ok, "Success", My.Application.MainWindow)
            Catch ex As Exception
                errorMessage = ex.Message
            End Try

            cancellationToken.ThrowIfCancellationRequested()
            If Not String.IsNullOrEmpty(errorMessage) Then
                Await MsgBoxAsync($"Failed to copy command: {errorMessage}", MsgBoxButtons.Ok, "Error", My.Application.MainWindow)
            End If
        End If
    End Function

#End Region

#End Region

#Region " Private Methods "

    Private Sub InitializeDebounceTimer()
        ' 定时器已使用WithEvents声明并初始化
        DebounceTimer.IsEnabled = False
    End Sub

    Private Sub InitializeStatusCheckTimer()
        ' 定时器已使用WithEvents声明并初始化
        StatusCheckTimer.IsEnabled = False
    End Sub

    Private Sub OnDebounceTimerTick(sender As Object, e As EventArgs) Handles DebounceTimer.Tick
        DebounceTimer.IsEnabled = False
        UpdateFilteredParameters()
    End Sub

    Private Sub DebounceUpdateFilteredParameters()
        ' 重置定时器，取消之前的待执行操作
        DebounceTimer.Stop()
        DebounceTimer.Start()
    End Sub

    Private Sub UpdateFilteredParameters()
        _filteredParameters = ComputeFilteredParameters()
        OnPropertyChanged(NameOf(FilteredParameters))
        OnPropertyChanged(NameOf(AvailableCategories))
        OnPropertyChanged(NameOf(TotalParameters))
        OnPropertyChanged(NameOf(ModifiedParameters))
        OnPropertyChanged(NameOf(DefaultParameters))
        OnPropertyChanged(NameOf(ParameterCountText))
    End Sub

    Private Function ComputeFilteredParameters() As List(Of ServerParameterItem)
        If Settings.ServerParameters Is Nothing Then Return New List(Of ServerParameterItem)()

        Dim filtered = Settings.ServerParameters.AsEnumerable()

        ' Text filter
        If Not String.IsNullOrEmpty(FilterText) Then
            Dim searchText = FilterText.ToLower()
            filtered = filtered.Where(Function(p) p.Argument.Contains(searchText, StringComparison.OrdinalIgnoreCase) OrElse
                                     (p.Metadata?.Explanation?.Contains(searchText, StringComparison.OrdinalIgnoreCase)).GetValueOrDefault)
        End If

        ' Category filter
        If SelectedCategory <> "所有分类" Then
            filtered = filtered.Where(Function(p) p.Metadata?.Category = SelectedCategory)
        End If

        ' Modified only filter
        If ShowModifiedOnly Then
            filtered = filtered.Where(Function(p) p.HasLocalValue)
        End If

        Return filtered.OrderBy(Function(p) p.Argument).ToList()
    End Function

    Private Function GenerateArgumentFromParameter(param As ServerParameterItem) As String
        If param.Metadata Is Nothing Then Return Nothing

        Select Case param.Metadata.Editor.ToLower()
            Case "checkbox"
                If param.Value.BooleanValue.HasValue AndAlso param.Value.BooleanValue.Value Then
                    Return param.Argument
                End If

            Case "numberupdown"
                If param.Value.DoubleValue.HasValue Then
                    Return $"{param.Argument} {param.Value.DoubleValue.Value}"
                End If

            Case "textbox", "filepath", "directory"
                If Not String.IsNullOrEmpty(param.Value.StringValue) Then
                    Return $"{param.Argument} ""{param.Value.StringValue}"""
                End If
        End Select

        Return Nothing
    End Function

#End Region

#Region " IDisposable Implementation "

    Public Sub Dispose() Implements IDisposable.Dispose
        ' 释放定时器资源
        If DebounceTimer IsNot Nothing Then
            DebounceTimer.Stop()
            DebounceTimer = Nothing
        End If

        If StatusCheckTimer IsNot Nothing Then
            StatusCheckTimer.Stop()
            StatusCheckTimer = Nothing
        End If
    End Sub

#End Region

End Class