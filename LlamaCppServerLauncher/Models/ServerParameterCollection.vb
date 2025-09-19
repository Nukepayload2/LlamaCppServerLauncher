Imports System.Collections.ObjectModel

Public Class ServerParameterCollection
    Inherits ObservableCollection(Of ServerParameterItem)

    Public Event HasLocalValuesChanged As EventHandler

    Public Sub InitializeFromMetadata()
        For Each metadata In ServerParameterMetadata.AllParameters
            Dim param As New ServerParameterItem(metadata.Argument)
            Add(param)
        Next
    End Sub

End Class
