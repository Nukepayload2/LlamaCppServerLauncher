Imports System.Text.Json.Serialization
Imports LlamaCppServerLauncher.Helpers
Imports LlamaCppServerLauncher.DataTemplates
Imports System.Windows.Input

Public Class ServerParameterItem
    Inherits ObservableBase
    
    Private _argument As String
    Private _hasLocalValue As Boolean = False
    Private _metadata As ServerParameterMetadata

    Public Property Argument As String
        Get
            Return _argument
        End Get
        Set
            SetProperty(_argument, Value)
        End Set
    End Property

    Public ReadOnly Property Value As New PrimitiveValue

    <JsonIgnore>
    Public Property HasLocalValue As Boolean
        Get
            Return _hasLocalValue
        End Get
        Private Set
            SetProperty(_hasLocalValue, Value)
        End Set
    End Property

    <JsonIgnore>
    Public Property Metadata As ServerParameterMetadata
        Get
            Return _metadata
        End Get
        Private Set
            SetProperty(_metadata, Value)
        End Set
    End Property

    <JsonIgnore>
    Public ReadOnly Property BrowseFilePathCommand As ICommand = New BrowseFilePathCommand(Me)

    <JsonIgnore>
    Public ReadOnly Property BrowseDirectoryCommand As ICommand = New BrowseDirectoryCommand(Me)

    <JsonIgnore>
    Public ReadOnly Property ClearValueCommand As ICommand = New ClearValueCommand(Me)

    Public Sub New(argument As String)
        _argument = argument
        _metadata = ServerParameterMetadata.GetMetadataByArgument(argument)
        
        If _metadata IsNot Nothing Then
            SetDefaultValue(_metadata.DefaultValue)
        End If
        
        AddHandler Value.PropertyChanged, AddressOf UpdateHasLocalValue
    End Sub
    
    Private Sub SetDefaultValue(defaultValue As Object)
        If defaultValue Is Nothing Then Return
        
        Select Case defaultValue.GetType()
            Case GetType(Boolean)
                Value.BooleanValue = DirectCast(defaultValue, Boolean)
            Case GetType(Integer)
                Value.DoubleValue = DirectCast(defaultValue, Integer)
            Case GetType(Double)
                Value.DoubleValue = DirectCast(defaultValue, Double)
            Case GetType(String)
                Value.StringValue = DirectCast(defaultValue, String)
        End Select
    End Sub

    Private Sub UpdateHasLocalValue(sender As Object, e As ComponentModel.PropertyChangedEventArgs)
        _hasLocalValue = Value.HasValue
        OnPropertyChanged(NameOf(HasLocalValue))
    End Sub

    Public Sub ClearLocalValue()
        If _metadata IsNot Nothing Then
            SetDefaultValue(_metadata.DefaultValue)
            Value.HasValue = False
        Else
            Value.Clear()
        End If
    End Sub
End Class

Public Class ClearValueCommand
    Implements ICommand
    
    Private _parameter As ServerParameterItem
    
    Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged
    
    Public Sub New(parameter As ServerParameterItem)
        _parameter = parameter
        AddHandler _parameter.PropertyChanged, AddressOf OnParameterChanged
    End Sub
    
    Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
        Return _parameter.HasLocalValue
    End Function
    
    Public Sub Execute(parameter As Object) Implements ICommand.Execute
        _parameter.ClearLocalValue()
    End Sub
    
    Private Sub OnParameterChanged(sender As Object, e As ComponentModel.PropertyChangedEventArgs)
        If e.PropertyName = NameOf(ServerParameterItem.HasLocalValue) Then
            RaiseEvent CanExecuteChanged(Me, EventArgs.Empty)
        End If
    End Sub
End Class