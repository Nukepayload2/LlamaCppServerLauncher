Imports System.Text.Json.Serialization
Imports LlamaCppServerLauncher.Helpers

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
            SetProperty(_argument, value)
        End Set
    End Property
    
    Public ReadOnly Property Value As New PrimitiveValue
    
    <JsonIgnore>
    Public Property HasLocalValue As Boolean
        Get
            Return _hasLocalValue
        End Get
        Private Set
            SetProperty(_hasLocalValue, value)
        End Set
    End Property
    
    <JsonIgnore>
    Public Property Metadata As ServerParameterMetadata
        Get
            Return _metadata
        End Get
        Private Set
            SetProperty(_metadata, value)
        End Set
    End Property
    
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
        Else
            Value.Clear()
        End If
    End Sub
End Class