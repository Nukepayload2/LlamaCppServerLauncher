Imports System.Text.Json.Serialization
Imports LlamaCppServerLauncher.Helpers

Public Class PrimitiveValue
    Inherits ObservableBase
    
    Private _booleanValue As Boolean? = Nothing
    Private _stringValue As String = Nothing
    Private _doubleValue As Double? = Nothing
    Private _hasValue As Boolean

    Public Property BooleanValue As Boolean?
        Get
            Return _booleanValue
        End Get
        Set
            SetProperty(_booleanValue, Value)
            UpdateHasValue()
        End Set
    End Property

    Public Property StringValue As String
        Get
            Return _stringValue
        End Get
        Set
            SetProperty(_stringValue, Value)
            UpdateHasValue()
        End Set
    End Property
    
    Public Property DoubleValue As Double?
        Get
            Return _doubleValue
        End Get
        Set
            SetProperty(_doubleValue, Value)
            UpdateHasValue()
        End Set
    End Property

    Private Sub UpdateHasValue()
        HasValue = _booleanValue IsNot Nothing OrElse
                   Not String.IsNullOrEmpty(_stringValue) OrElse
                   _doubleValue IsNot Nothing
    End Sub

    <JsonIgnore>
    Public Property HasValue As Boolean
        Get
            Return _hasValue
        End Get
        Set(value As Boolean)
            SetProperty(_hasValue, value)
        End Set
    End Property

    Public Sub Clear()
        BooleanValue = Nothing
        StringValue = Nothing
        DoubleValue = Nothing
        HasValue = False
    End Sub
End Class