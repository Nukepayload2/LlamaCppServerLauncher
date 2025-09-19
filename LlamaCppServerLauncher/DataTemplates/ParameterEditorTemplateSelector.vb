Imports Avalonia.Controls
Imports Avalonia.Controls.Templates
Imports Avalonia

Public Class ParameterEditorTemplateSelector
    Implements IDataTemplate

    Public Function Build(param As Object) As Control Implements IDataTemplate.Build
        Dim item = TryCast(param, ServerParameterItem)
        If item Is Nothing Then
            Return New TextBlock With {.Text = "Invalid parameter"}
        End If

        ' 从资源字典中获取对应的模板
        Dim templateKey = GetTemplateKey(item.Metadata?.Editor)
        
        ' 查找资源字典中的模板
        Dim template = FindDataTemplate(templateKey)
        If template IsNot Nothing Then
            Return New ContentControl With {
                .Content = item,
                .ContentTemplate = template
            }
        End If
        
        ' 如果找不到模板，返回默认的文本块
        Return New TextBlock With {.Text = $"Template not found: {templateKey}"}
    End Function

    Private Function FindDataTemplate(templateKey As String) As IDataTemplate
        Try
            ' 通过Application.Current查找资源
            Dim app = Application.Current
            If app IsNot Nothing Then
                ' 尝试从应用程序资源中获取
                Dim resourceValue As Object = Nothing
                Dim found = app.Resources.TryGetResource(templateKey, app.ActualThemeVariant, resourceValue)
                If found AndAlso TypeOf resourceValue Is IDataTemplate Then
                    Return DirectCast(resourceValue, IDataTemplate)
                End If
            End If
        Catch ex As Exception
            ' 忽略异常，返回Nothing
        End Try
        
        Return Nothing
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
