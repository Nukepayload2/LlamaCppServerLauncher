Imports LlamaCppServerLauncher.Helpers

Public Class AppSettings
    Inherits ObservableBase

    Public Shared Function WithServerParameters() As AppSettings
        Dim settings As New AppSettings
        settings.ServerParameters.InitializeFromMetadata()
        Return settings
    End Function

    Public Class IoModel
        Public Property ServerParameters As ServerParameterItem()
    End Class

    Public ReadOnly Property ServerParameters As New ServerParameterCollection
    Public ReadOnly Property ServerParameterByName As Dictionary(Of String, ServerParameterItem) =
        ServerParameters.ToDictionary(Function(it) it.Argument, Function(it) it)

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

    ' 命令行生成
    Public Function GenerateCommandLine() As String
        Return _ServerParameters.GenerateCommandLine()
    End Function

    ' 私有帮助方法
    Private Function GetParameterValue(argument As String) As String
        Dim parameter = _ServerParameters.FirstOrDefault(Function(p) p.Argument = argument)
        Return If(parameter IsNot Nothing AndAlso parameter.Value.StringValue IsNot Nothing,
                   parameter.Value.StringValue, "")
    End Function

    Private Function GetParameterIntegerValue(argument As String) As Integer?
        Dim parameter = _ServerParameters.FirstOrDefault(Function(p) p.Argument = argument)
        Return parameter.Value.DoubleValue
    End Function

    Private Sub SetParameterValue(argument As String, value As String)
        Dim parameter = _ServerParameters.FirstOrDefault(Function(p) p.Argument = argument)
        If parameter IsNot Nothing Then
            parameter.Value.StringValue = value
        End If
    End Sub

    Private Sub SetParameterIntegerValue(argument As String, value As Integer?)
        Dim parameter = _ServerParameters.FirstOrDefault(Function(p) p.Argument = argument)
        If parameter IsNot Nothing Then
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