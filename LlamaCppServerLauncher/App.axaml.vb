Imports Avalonia
Imports Avalonia.Controls.ApplicationLifetimes
Imports Avalonia.Markup.Xaml

Partial Public Class App
	Inherits Application

	Public Overrides Sub Initialize()
		AvaloniaXamlLoader.Load(Me)
	End Sub

	Friend ReadOnly Property MainWindow As MainWindow
	Friend ReadOnly Property ActiveWindow As Controls.Window
		Get
			Return VbHost.ActiveWindow
		End Get
	End Property

	Public Overrides Sub OnFrameworkInitializationCompleted()
		Dim desktop = TryCast(ApplicationLifetime, IClassicDesktopStyleApplicationLifetime)
		If desktop IsNot Nothing Then
			_MainWindow = New MainWindow
			desktop.MainWindow = MainWindow
		End If

		MyBase.OnFrameworkInitializationCompleted()
	End Sub
End Class
