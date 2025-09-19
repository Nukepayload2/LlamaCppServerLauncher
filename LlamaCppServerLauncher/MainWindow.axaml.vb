#Region " Imports "

Imports Avalonia.Controls
Imports Avalonia.Interactivity
Imports Avalonia.Platform.Storage
Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Text.Json
Imports System.ComponentModel
Imports System.Linq

#End Region

Partial Public Class MainWindow
    Inherits Window

#Region " Fields "

    Private serverProcess As Process
    Private serverRunning As Boolean = False
    Private _Settings As New AppSettings()
    Private configFile As String = Path.Combine(AppContext.BaseDirectory, "serverconfig.json")
    Private _filterText As String = ""
    Private _selectedCategory As String = "所有分类"
    Private _showModifiedOnly As Boolean = False

#End Region

#Region " Properties "

    Public Property Settings As AppSettings
        Get
            Return _Settings
        End Get
        Set(value As AppSettings)
            _Settings = value
        End Set
    End Property

#End Region

#Region " Properties "

    Public Property FilterText As String
        Get
            Return _filterText
        End Get
        Set(value As String)
            _filterText = value
            UpdateFilteredParameters()
        End Set
    End Property

    Public Property SelectedCategory As String
        Get
            Return _selectedCategory
        End Get
        Set(value As String)
            _selectedCategory = value
            UpdateFilteredParameters()
        End Set
    End Property

    Public Property ShowModifiedOnly As Boolean
        Get
            Return _showModifiedOnly
        End Get
        Set(value As Boolean)
            _showModifiedOnly = value
            UpdateFilteredParameters()
        End Set
    End Property

    Public ReadOnly Property AvailableCategories As List(Of String)
        Get
            If Settings.ServerParameters Is Nothing Then Return New List(Of String) From {"所有分类"}
            
            Dim categories = Settings.ServerParameters.Select(Function(p) p.Metadata?.Category).
                                              Where(Function(c) Not String.IsNullOrEmpty(c)).
                                              Distinct().
                                              OrderBy(Function(c) c).
                                              ToList()
            
            Dim result = New List(Of String) From {"所有分类"}
            result.AddRange(categories)
            Return result
        End Get
    End Property

    Public ReadOnly Property FilteredParameters As List(Of ServerParameterItem)
        Get
            If Settings.ServerParameters Is Nothing Then Return New List(Of ServerParameterItem)()
            
            Dim filtered = Settings.ServerParameters.AsEnumerable()
            
            ' Text filter
            If Not String.IsNullOrEmpty(FilterText) Then
                Dim searchText = FilterText.ToLower()
                filtered = filtered.Where(Function(p) p.Argument.ToLower().Contains(searchText) OrElse (p.Metadata?.Explanation?.ToLower().Contains(searchText) = True))
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

#End Region

#Region " Constructor "

    Public Sub New()
        InitializeComponent()
        DataContext = Me
        LoadSettingsSync()
        UpdateCommandPreview()
    End Sub

#End Region


#Region " UI Updates "

    Private Sub UpdateFilteredParameters()
        ' Properties will automatically notify due to Avalonia data binding
        ' No need to manually call OnPropertyChanged
    End Sub

    Private Sub UpdateCommandPreview()
        Dim fullCommand As New StringBuilder()

        ' Server Path
        If Not String.IsNullOrEmpty(Settings.ServerPath) Then
            fullCommand.Append($"""{Settings.ServerPath}""")
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

    Private Sub SearchTextBox_KeyUp(sender As Object, e As Avalonia.Input.KeyEventArgs)
        UpdateFilteredParameters()
    End Sub

    Private Sub ClearFiltersButton_Click(sender As Object, e As RoutedEventArgs)
        FilterText = ""
        SelectedCategory = "所有分类"
        ShowModifiedOnly = False
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
                Settings = JsonSerializer.Deserialize(Of AppSettings)(json)
                DataContext = Me ' Update DataContext
                UpdateFilteredParameters()
            End If
        Catch
            ' If loading fails, use default Settings
            Settings = New AppSettings()
            DataContext = Me ' Update DataContext
            UpdateFilteredParameters()
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
                Settings.ServerPath = files(0).Path.LocalPath
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
                Settings.ModelPath = files(0).Path.LocalPath
            End If
        End If
    End Function

    Private Async Function SaveSettings() As Task
        Dim errorMessage As String = ""
        Try
            Dim json As String = JsonSerializer.Serialize(Settings, New JsonSerializerOptions With {
                .WriteIndented = True
            })
            Await File.WriteAllTextAsync(configFile, json)
            Await MsgBoxAsync(My.Resources.SettingsSaved, MsgBoxButtons.Ok, "Success")
        Catch ex As Exception
            errorMessage = ex.Message
        End Try

        If Not String.IsNullOrEmpty(errorMessage) Then
            Await MsgBoxAsync($"Error saving Settings: {errorMessage}", MsgBoxButtons.Ok, "Error")
        End If
    End Function

    Private Async Function LoadSettingsAsync() As Task
        Dim errorMessage As String = ""
        Dim configFileExists As Boolean = False

        Try
            configFileExists = File.Exists(configFile)
            If configFileExists Then
                Dim json As String = Await File.ReadAllTextAsync(configFile)
                Settings = JsonSerializer.Deserialize(Of AppSettings)(json)
                DataContext = Me ' Update DataContext
                UpdateFilteredParameters()
                UpdateCommandPreview()
                Await MsgBoxAsync(My.Resources.SettingsLoaded, MsgBoxButtons.Ok, "Success")
            End If
        Catch ex As Exception
            errorMessage = ex.Message
        End Try

        If Not String.IsNullOrEmpty(errorMessage) Then
            Await MsgBoxAsync($"Error loading Settings: {errorMessage}", MsgBoxButtons.Ok, "Error")
        ElseIf Not configFileExists Then
            Await MsgBoxAsync("No configuration file found. Using default Settings.", MsgBoxButtons.Ok, "Info")
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

        If String.IsNullOrEmpty(Settings.ServerPath) OrElse Not File.Exists(Settings.ServerPath) Then
            Await MsgBoxAsync(My.Resources.ErrorServerPathRequired, MsgBoxButtons.Ok, "Error")
            Return
        End If

        If String.IsNullOrEmpty(Settings.ModelPath) OrElse Not File.Exists(Settings.ModelPath) Then
            Await MsgBoxAsync(My.Resources.ErrorModelPathRequired, MsgBoxButtons.Ok, "Error")
            Return
        End If

        Try
            Dim args As String = GenerateCommandLineArguments()

            Dim startInfo As New ProcessStartInfo(Settings.ServerPath, args) With {
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
        If Not String.IsNullOrEmpty(Settings.ModelPath) Then
            args.Append($"""{Settings.ModelPath}""")
        End If

        ' Generate arguments from ServerParameterCollection
        If Settings.ServerParameters IsNot Nothing Then
            For Each param In Settings.ServerParameters
                If param.HasLocalValue Then
                    Dim argument = GenerateArgumentFromParameter(param)
                    If Not String.IsNullOrEmpty(argument) Then
                        args.Append($" {argument}")
                    End If
                End If
            Next
        End If

        Return args.ToString().Trim()
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