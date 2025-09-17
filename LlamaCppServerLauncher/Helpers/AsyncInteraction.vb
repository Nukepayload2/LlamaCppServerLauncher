Imports Avalonia.Controls
Imports FluentAvalonia.UI.Controls

Module AsyncInteraction
    Async Function MsgBoxAsync(prompt As String, buttons As MsgBoxButtons,
                               title As String, Optional parent As TopLevel = Nothing) As Task(Of Boolean?)
        If parent Is Nothing Then
            parent = My.Application.ActiveWindow
        End If
        If parent Is Nothing Then
            Throw New InvalidOperationException("内部错误：消息框没地方放，检查 ActiveWindow")
        End If
        Dim dlg As New ContentDialog With {
            .Title = title,
            .Content = New TextBlock With {
                .TextWrapping = Avalonia.Media.TextWrapping.Wrap,
                .Text = prompt,
                .FontFamily = parent.FontFamily
            },
            .FontFamily = parent.FontFamily
        }
        Select Case buttons
            Case MsgBoxButtons.Ok
                dlg.IsSecondaryButtonEnabled = False
                dlg.PrimaryButtonText = "确定"
            Case MsgBoxButtons.OkCancel
                dlg.IsSecondaryButtonEnabled = True
                dlg.PrimaryButtonText = "确定"
                dlg.SecondaryButtonText = "取消"
            Case MsgBoxButtons.Yes
                dlg.IsSecondaryButtonEnabled = False
                dlg.PrimaryButtonText = "是"
            Case MsgBoxButtons.YesNo
                dlg.IsSecondaryButtonEnabled = True
                dlg.PrimaryButtonText = "是"
                dlg.SecondaryButtonText = "否"
        End Select

        Dim result = Await dlg.ShowAsync(parent)
        Select Case result
            Case ContentDialogResult.None
                Return Nothing
            Case ContentDialogResult.Primary
                Return True
            Case Else
                Return False
        End Select
    End Function
End Module

Enum MsgBoxButtons
    Ok
    OkCancel
    Yes
    YesNo
End Enum