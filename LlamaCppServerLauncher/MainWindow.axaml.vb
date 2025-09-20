#Region " Imports "

Imports Avalonia.Controls
Imports Avalonia.Interactivity
Imports Avalonia.Platform.Storage
Imports System.Diagnostics
Imports System.Threading.Tasks
Imports System.IO
Imports System.Text.Json

#End Region

Partial Public Class MainWindow
    Inherits Window
    Implements IServerLogView

    Private _viewModel As New MainViewModel(Me)

    Friend ReadOnly Property ViewModel As MainViewModel
        Get
            Return _viewModel
        End Get
    End Property

    Public Sub New()
        InitializeComponent()
        DataContext = ViewModel
    End Sub

    Private Sub MainWindow_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        ActiveWindow = Me
    End Sub

    Private Sub ScrollServerLogToEnd() Implements IServerLogView.ScrollServerLogToEnd
        Dim count = LstServerOutput.Items.Count
        If count > 0 Then
            LstServerOutput.ScrollIntoView(count - 1)
        End If
    End Sub
End Class

Interface IServerLogView
    Sub ScrollServerLogToEnd()
End Interface