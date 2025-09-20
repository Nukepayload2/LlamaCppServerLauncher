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

#Region " Fields "

    Private _viewModel As New MainViewModel()

#End Region

#Region " Properties "

    Public ReadOnly Property ViewModel As MainViewModel
        Get
            Return _viewModel
        End Get
    End Property

#End Region

#Region " Constructor "

    Public Sub New()
        InitializeComponent()
        DataContext = ViewModel
    End Sub

#End Region

#Region " Window Events "

    Private Sub MainWindow_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        ActiveWindow = Me
    End Sub

#End Region

End Class