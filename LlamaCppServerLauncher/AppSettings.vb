Imports LlamaCppServerLauncher.Helpers

Public Class AppSettings
    Inherits ObservableBase

    Public Shared Function WithServerParameters() As AppSettings
        Dim settings As New AppSettings
        settings.ResetToDefault()
        Return settings
    End Function

    Public Sub ResetToDefault()
        ServerParameters.Clear()
        ServerParameters.InitializeFromMetadata()
        _ServerParameterByName = ServerParameters.ToDictionary(Function(it) it.Argument, Function(it) it)
    End Sub

    Public Class IoModel
        Public Property ServerParameters As ServerParameterItem()
    End Class

    Public ReadOnly Property ServerParameters As New ServerParameterCollection
    Public ReadOnly Property ServerParameterByName As Dictionary(Of String, ServerParameterItem)

    ' 便捷属性访问器
    Public Property ServerPath As String
        Get
            Return GetParameterValue("--server-path")
        End Get
        Set
            SetParameterValue("--server-path", Value)
        End Set
    End Property

    Public Property ModelPath As String
        Get
            Return GetParameterValue("--model")
        End Get
        Set
            SetParameterValue("--model", Value)
        End Set
    End Property

    Public Property Host As String
        Get
            Return GetParameterValue("--host")
        End Get
        Set
            SetParameterValue("--host", Value)
        End Set
    End Property

    Public Property Port As Integer?
        Get
            Return GetParameterIntegerValue("--port")
        End Get
        Set
            SetParameterIntegerValue("--port", Value)
        End Set
    End Property

    ' 私有帮助方法
    Private Function GetParameterValue(argument As String) As String
        Dim parameter As ServerParameterItem = Nothing
        If ServerParameterByName.TryGetValue(argument, parameter) Then
            Return If(parameter.Value.StringValue IsNot Nothing, parameter.Value.StringValue, "")
        End If
        Return ""
    End Function

    Private Function GetParameterIntegerValue(argument As String) As Integer?
        Dim parameter As ServerParameterItem = Nothing
        If ServerParameterByName.TryGetValue(argument, parameter) Then
            Return parameter.Value.DoubleValue
        End If
        Return Nothing
    End Function

    Private Sub SetParameterValue(argument As String, value As String)
        Dim parameter As ServerParameterItem = Nothing
        If ServerParameterByName.TryGetValue(argument, parameter) Then
            parameter.Value.StringValue = value
        End If
    End Sub

    Private Sub SetParameterIntegerValue(argument As String, value As Integer?)
        Dim parameter As ServerParameterItem = Nothing
        If ServerParameterByName.TryGetValue(argument, parameter) Then
            parameter.Value.DoubleValue = value
        End If
    End Sub

    Public Sub NotifyBasicPropertiesChanged()
        OnPropertyChanged(NameOf(ServerPath))
        OnPropertyChanged(NameOf(ModelPath))
        OnPropertyChanged(NameOf(Host))
        OnPropertyChanged(NameOf(Port))
    End Sub
End Class