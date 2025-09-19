Imports Avalonia.Data.Converters
Imports Avalonia.Media

Namespace Converters
    Public Class BooleanToBrushConverter
        Implements IValueConverter
        
        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim boolValue = If(TypeOf value Is Boolean, DirectCast(value, Boolean), False)
            Return If(boolValue, New SolidColorBrush(Colors.LimeGreen), New SolidColorBrush(Colors.LightGray))
        End Function
        
        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
    
    Public Class BooleanToStatusConverter
        Implements IValueConverter
        
        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim boolValue = If(TypeOf value Is Boolean, DirectCast(value, Boolean), False)
            Return If(boolValue, "Has local value", "Using default value")
        End Function
        
        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace