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

    End Class