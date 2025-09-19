Imports System.Collections.ObjectModel
Imports System.Windows.Input

Public Class ServerParameterCollection
    Inherits ObservableCollection(Of ServerParameterItem)

    Public ReadOnly Property ClearLocalValue As ICommand = New ClearLocalValueCommand(Me)

    Public Sub New()
        MyBase.New()
    End Sub

    Public Event HasLocalValuesChanged As EventHandler

    Public Sub InitializeFromMetadata()
        For Each metadata In ServerParameterMetadata.AllParameters
            Dim param As New ServerParameterItem(metadata.Argument)
            Add(param)
        Next
    End Sub

    Public Function GetParametersWithLocalValue() As IEnumerable(Of ServerParameterItem)
        Return Me.Where(Function(p) p.HasLocalValue)
    End Function
    
    Public Function GenerateCommandLine() As String
        Dim commandLine As New System.Text.StringBuilder()
        
        For Each parameter In GetParametersWithLocalValue()
            commandLine.Append(parameter.Argument)
            
            If parameter.Value.BooleanValue IsNot Nothing Then
                commandLine.Append(" ").Append(parameter.Value.BooleanValue.Value.ToString().ToLower())
            ElseIf Not String.IsNullOrEmpty(parameter.Value.StringValue) Then
                commandLine.Append(" """).Append(parameter.Value.StringValue).Append("""")
            ElseIf parameter.Value.DoubleValue IsNot Nothing Then
                commandLine.Append(" ").Append(parameter.Value.DoubleValue.Value)
            End If
            
            commandLine.Append(" ")
        Next
        
        Return commandLine.ToString().Trim()
    End Function
    
    Public Sub ClearAllLocalValues()
        For Each parameter In Me
            parameter.ClearLocalValue()
        Next
    End Sub
    
    Public Function GetParametersByCategory(category As String) As IEnumerable(Of ServerParameterItem)
        Return Me.Where(Function(p) p.Metadata IsNot Nothing AndAlso p.Metadata.Category = category)
    End Function
    
    Public Function FilterParameters(searchText As String, categories As IEnumerable(Of String), hasLocalValueOnly As Boolean) As IEnumerable(Of ServerParameterItem)
        Dim query = Me.AsEnumerable()
        
        If Not String.IsNullOrEmpty(searchText) Then
            query = query.Where(Function(p) 
                                    Return p.Argument.ToLower().Contains(searchText.ToLower()) OrElse
                                           (p.Metadata IsNot Nothing AndAlso p.Metadata.Explanation.ToLower().Contains(searchText.ToLower()))
                                End Function)
        End If
        
        If categories.Any() Then
            query = query.Where(Function(p) 
                                    Return p.Metadata IsNot Nothing AndAlso categories.Contains(p.Metadata.Category)
                                End Function)
        End If
        
        If hasLocalValueOnly Then
            query = query.Where(Function(p) p.HasLocalValue)
        End If
        
        Return query
    End Function
End Class

Public Class ClearLocalValueCommand
    Implements ICommand
    
    Private _collection As ServerParameterCollection
    
    Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged
    
    Public Sub New(collection As ServerParameterCollection)
        _collection = collection
    End Sub
    
    Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
        Return _collection.Any(Function(p) p.HasLocalValue)
    End Function
    
    Public Sub Execute(parameter As Object) Implements ICommand.Execute
        _collection.ClearAllLocalValues()
    End Sub
    
    Public Sub RaiseCanExecuteChanged()
        RaiseEvent CanExecuteChanged(Me, EventArgs.Empty)
    End Sub
End Class