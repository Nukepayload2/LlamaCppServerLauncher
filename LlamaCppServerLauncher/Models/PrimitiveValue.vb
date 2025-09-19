Imports System.Text.Json.Serialization
Imports LlamaCppServerLauncher.Helpers

Public Class PrimitiveValue
    Inherits ObservableBase
    
    Private _booleanValue As Boolean? = Nothing
    Private _stringValue As String = Nothing
    Private _doubleValue As Double? = Nothing
    
    Public Property BooleanValue As Boolean?
        Get
            Return _booleanValue
        End Get
        Set
            SetProperty(_booleanValue, value)
        End Set
    End Property
    
    Public Property StringValue As String
        Get
            Return _stringValue
        End Get
        Set
            SetProperty(_stringValue, value)
        End Set
    End Property
    
    Public Property DoubleValue As Double?
        Get
            Return _doubleValue
        End Get
        Set
            SetProperty(_doubleValue, value)
        End Set
    End Property
    
    Public ReadOnly Property HasValue As Boolean
        Get
            Return _booleanValue IsNot Nothing OrElse 
                   Not String.IsNullOrEmpty(_stringValue) OrElse 
                   _doubleValue IsNot Nothing
        End Get
    End Property
    
    Public Sub Clear()
        _booleanValue = Nothing
        _stringValue = Nothing
        _doubleValue = Nothing
        OnPropertyChanged(NameOf(BooleanValue))
        OnPropertyChanged(NameOf(StringValue))
        OnPropertyChanged(NameOf(DoubleValue))
    End Sub
End Class