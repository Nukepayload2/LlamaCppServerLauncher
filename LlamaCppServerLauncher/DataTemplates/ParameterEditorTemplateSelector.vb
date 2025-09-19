Imports Avalonia.Controls
Imports Avalonia.Controls.Templates

Public Class ParameterEditorTemplateSelector
    Implements IDataTemplate

    Public Function Build(param As Object) As Control Implements IDataTemplate.Build
        Dim item = TryCast(param, ServerParameterItem)
        If item Is Nothing Then
            Return New TextBlock With {.Text = "Invalid parameter"}
        End If

        ' 从资源字典中获取对应的模板
        Dim templateKey = GetTemplateKey(item.Metadata?.Editor)
        Return New ContentControl With {
            .Content = item,
            .ContentTemplate = TryCast(My.Application.Resources(templateKey), IDataTemplate)
        }
    End Function

    Public Function Match(data As Object) As Boolean Implements IDataTemplate.Match
        Return TypeOf data Is ServerParameterItem
    End Function

    Private Function GetTemplateKey(editorType As String) As String
        If String.IsNullOrEmpty(editorType) Then
            Return "DefaultParameterTemplate"
        End If

        Select Case editorType.ToLower()
            Case "checkbox"
                Return "BooleanParameterTemplate"
            Case "textbox"
                Return "StringParameterTemplate"
            Case "numberupdown"
                Return "NumberParameterTemplate"
            Case "filepath"
                Return "FilePathParameterTemplate"
            Case "directory"
                Return "DirectoryParameterTemplate"
            Case Else
                Return "DefaultParameterTemplate"
        End Select
    End Function
End Class
