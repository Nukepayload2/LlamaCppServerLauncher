Imports Avalonia.Controls
Imports Avalonia.Platform.Storage
Imports System.Windows.Input

Namespace DataTemplates
    Public Class BrowseFilePathCommand
        Implements ICommand

        Private _parameter As ServerParameterItem

        Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

        Public Sub New(parameter As ServerParameterItem)
            _parameter = parameter
        End Sub

        Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
            Return True
        End Function

        Public Async Sub Execute(parameter As Object) Implements ICommand.Execute
            Dim window = My.Application.ActiveWindow
            If window Is Nothing Then Return

            Dim storageProvider = window.StorageProvider
            If storageProvider Is Nothing Then Return

            Dim options = New FilePickerOpenOptions With {
                .Title = "Select File",
                .AllowMultiple = False
            }

            Dim files = Await storageProvider.OpenFilePickerAsync(options)
            If files.Any() Then
                _parameter.Value.StringValue = files.First().TryGetLocalPath()
            End If
        End Sub
    End Class

    Public Class BrowseDirectoryCommand
        Implements ICommand

        Private _parameter As ServerParameterItem

        Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

        Public Sub New(parameter As ServerParameterItem)
            _parameter = parameter
        End Sub

        Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
            Return True
        End Function

        Public Async Sub Execute(parameter As Object) Implements ICommand.Execute
            Dim window = My.Application.ActiveWindow
            If window Is Nothing Then Return

            Dim storageProvider = window.StorageProvider
            If storageProvider Is Nothing Then Return

            Dim folders = Await storageProvider.OpenFolderPickerAsync(
                New FolderPickerOpenOptions With {
                    .Title = "Select Directory",
                    .AllowMultiple = False
                })

            If folders.Any() Then
                _parameter.Value.StringValue = folders.First().TryGetLocalPath()
            End If
        End Sub
    End Class
End Namespace