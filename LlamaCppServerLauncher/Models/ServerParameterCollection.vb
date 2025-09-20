Imports System.Collections.ObjectModel

Public Class ServerParameterCollection
    Inherits ObservableCollection(Of ServerParameterItem)

    Public Event HasLocalValuesChanged As EventHandler

    Private Sub InitializeFromMetadata()
        For Each metadata In ServerParameterMetadata.AllParameters
            Dim param As New ServerParameterItem(metadata.Argument)
            Add(param)
        Next
    End Sub

    Public Sub ReloadFromMetadata()
        If ServerParameterMetadata.AllParameters.Count <> Count Then
            InitializeFromMetadata()
            Return
        End If

        For i = 0 To ServerParameterMetadata.AllParameters.Count - 1
            Dim metadata = ServerParameterMetadata.AllParameters(i)
            Dim item = Me(i)
            item.SetDefaultValue(metadata.DefaultValue)
        Next
    End Sub

End Class
