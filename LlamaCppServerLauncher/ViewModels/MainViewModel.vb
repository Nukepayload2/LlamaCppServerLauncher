Imports System.ComponentModel
Imports System.Linq
Imports System.Text
Imports System.Text.Json
Imports System.IO
Imports Avalonia.Threading
Imports LlamaCppServerLauncher.Helpers

Public Class MainViewModel
    Inherits ObservableBase
    Implements IDisposable

#Region " Fields "

    Private _settings As New AppSettings()
    Private _filterText As String = ""
    Private _selectedCategory As String = "所有分类"
    Private _showModifiedOnly As Boolean = False
    Private _filteredParameters As List(Of ServerParameterItem)
    Private _availableCategories As List(Of String)
    
    ' 防抖相关字段
    Private _debounceTimer As DispatcherTimer
    Private Const DEBOUNCE_DELAY As Integer = 300 ' 300毫秒防抖延迟

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

#End Region

#Region " Constructor "

    Public Sub New()
        LoadSettingsSync()
        InitializeDebounceTimer()
    End Sub

#End Region

#Region " Public Methods "

    Public Sub ClearFilters()
        FilterText = ""
        SelectedCategory = "所有分类"
        ShowModifiedOnly = False
    End Sub

    Public Function UpdateCommandPreview() As String
        Dim fullCommand As New StringBuilder()

        ' Server Path
        If Not String.IsNullOrEmpty(Settings.ServerPath) Then
            fullCommand.Append($"""{Settings.ServerPath}""")
        End If

        ' Model Path
        If Not String.IsNullOrEmpty(Settings.ModelPath) Then
            fullCommand.Append($"""{Settings.ModelPath}""")
        End If

        ' Generate arguments from ServerParameterCollection
        If Settings.ServerParameters IsNot Nothing Then
            For Each param In Settings.ServerParameters
                If param.HasLocalValue Then
                    Dim argument = GenerateArgumentFromParameter(param)
                    If Not String.IsNullOrEmpty(argument) Then
                        fullCommand.Append($" {argument}")
                    End If
                End If
            Next
        End If

        Return fullCommand.ToString().Trim()
    End Function

    Public Sub LoadSettingsSync()
        Try
            Dim configFile As String = IO.Path.Combine(AppContext.BaseDirectory, "serverconfig.json")
            If IO.File.Exists(configFile) Then
                Dim json As String = IO.File.ReadAllText(configFile)
                Settings = JsonSerializer.Deserialize(Of AppSettings)(json)
            End If
        Catch
            ' If loading fails, use default settings
            Settings = New AppSettings()
        End Try
        
        ' Reset cached values
        _filteredParameters = Nothing
        _availableCategories = Nothing
        UpdateFilteredParameters()
    End Sub

#End Region

#Region " Private Methods "

    Private Sub InitializeDebounceTimer()
        _debounceTimer = New DispatcherTimer With {
            .Interval = TimeSpan.FromMilliseconds(DEBOUNCE_DELAY)
        }
        AddHandler _debounceTimer.Tick, AddressOf OnDebounceTimerTick
    End Sub

    Private Sub OnDebounceTimerTick(sender As Object, e As EventArgs)
        ' DispatcherTimer自动在UI线程上执行
        UpdateFilteredParameters()
    End Sub

    Private Sub DebounceUpdateFilteredParameters()
        ' 重置定时器，取消之前的待执行操作
        _debounceTimer.Stop()
        _debounceTimer.Start()
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
            filtered = filtered.Where(Function(p) p.Argument.ToLower().Contains(searchText) OrElse
                                     (p.Metadata?.Explanation?.ToLower().Contains(searchText) = True))
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
        If _debounceTimer IsNot Nothing Then
            _debounceTimer.Stop()
            RemoveHandler _debounceTimer.Tick, AddressOf OnDebounceTimerTick
            _debounceTimer = Nothing
        End If
    End Sub

#End Region

End Class