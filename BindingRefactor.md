# Avalonia Compiled Binding 重构计划

## 📋 项目现状分析

### 当前配置
- **项目配置**: 已启用 `<AvaloniaUseCompiledBindingsByDefault>true</AvaloniaUseCompiledBindingsByDefault>`
- **UI框架**: Avalonia UI 11.0.9 + FluentAvaloniaUI
- **当前绑定方式**: 手动代码绑定 (`UpdateUIFromSettings()`, `UpdateSettingsFromUI()`)
- **属性通知**: AppSettings 未实现 INotifyPropertyChanged

### 现有架构问题
1. **手动UI更新**: 需要手动调用 `UpdateUIFromSettings()` 和 `UpdateSettingsFromUI()`
2. **无属性通知**: 属性更改不会自动通知UI
3. **代码冗余**: 大量手动属性设置和获取代码
4. **性能不佳**: 使用反射而非编译时绑定

## 🎯 重构目标

### 主要目标
1. **实现属性通知**: AppSettings 类实现 INotifyPropertyChanged
2. **启用编译绑定**: 使用 `{Binding}` + `x:DataType`
3. **简化代码架构**: 移除手动UI更新方法
4. **提升性能**: 利用编译时绑定的性能优势

### 技术方案
- **属性通知**: 自定义 ObservableBase 基类
- **绑定方式**: 标准 `{Binding}` 语法（自动编译）
- **数据类型**: `x:DataType="local:AppSettings"`
- **代码简化**: 移除手动同步代码

## 📊 简化实施计划

### 阶段一：创建 ObservableBase 基类

#### 1.1 创建基础文件

**文件**: `Helpers/ObservableBase.vb`

```vb
Imports System.ComponentModel
Imports System.Runtime.CompilerServices

Namespace Helpers
    Public Class ObservableBase
        Implements INotifyPropertyChanged
        
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
        
        Protected Sub OnPropertyChanged(<CallerMemberName> Optional propertyName As String = Nothing)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub
        
        Protected Function SetProperty(Of T)(ByRef field As T, value As T, <CallerMemberName> Optional propertyName As String = Nothing) As Boolean
            If EqualityComparer(Of T).Default.Equals(field, value) Then
                Return False
            End If
            
            field = value
            OnPropertyChanged(propertyName)
            Return True
        End Function
    End Class
End Namespace
```

#### 1.2 更新项目引用

**文件**: `LlamaCppServerLauncher.vbproj`

```xml
<ItemGroup>
    <Compile Include="Helpers\ObservableBase.vb"/>
</ItemGroup>
```

### 阶段二：重构 AppSettings 类

#### 2.1 修改基类和属性模式

**文件**: `AppSettings.vb`

```vb
Imports System.Text.Json.Serialization
Imports LlamaCppServerLauncher.Helpers

Public Class AppSettings
    Inherits ObservableBase
    
    ' Basic Parameters
    Private _serverPath As String = ""
    Private _modelPath As String = ""
    Private _threads As Integer = 4
    Private _ctxSize As Integer = 4096
    Private _nGpuLayers As Integer = 0
    Private _host As String = "127.0.0.1"
    Private _port As Integer = 8080
    Private _timeout As Integer = 600
    
    ' Properties with notification
    Public Property ServerPath As String
        Get
            Return _serverPath
        End Get
        Set(value As String)
            SetProperty(_serverPath, value)
        End Set
    End Property
    
    Public Property ModelPath As String
        Get
            Return _modelPath
        End Get
        Set(value As String)
            SetProperty(_modelPath, value)
        End Set
    End Property
    
    Public Property Threads As Integer
        Get
            Return _threads
        End Get
        Set(value As Integer)
            SetProperty(_threads, value)
        End Set
    End Property
    
    Public Property CtxSize As Integer
        Get
            Return _ctxSize
        End Get
        Set(value As Integer)
            SetProperty(_ctxSize, value)
        End Set
    End Property
    
    Public Property NGpuLayers As Integer
        Get
            Return _nGpuLayers
        End Get
        Set(value As Integer)
            SetProperty(_nGpuLayers, value)
        End Set
    End Property
    
    Public Property Host As String
        Get
            Return _host
        End Get
        Set(value As String)
            SetProperty(_host, value)
        End Set
    End Property
    
    Public Property Port As Integer
        Get
            Return _port
        End Get
        Set(value As Integer)
            SetProperty(_port, value)
        End Set
    End Property
    
    Public Property Timeout As Integer
        Get
            Return _timeout
        End Get
        Set(value As Integer)
            SetProperty(_timeout, value)
        End Set
    End Property
    
    ' ... 其他所有属性按相同模式重构
```

#### 2.2 其他属性类型示例

**数值属性**:
```vb
Private _temperature As Double = 0.8
Public Property Temperature As Double
    Get
        Return _temperature
    End Get
    Set(value As Double)
        SetProperty(_temperature, value)
    End Set
End Property
```

**布尔属性**:
```vb
Private _mlock As Boolean = False
Public Property Mlock As Boolean
    Get
        Return _mlock
    End Get
    Set(value As Boolean)
        SetProperty(_mlock, value)
    End Set
End Property
```

### 阶段三：更新 MainWindow.axaml

#### 3.1 添加 x:DataType 和数据绑定

**文件**: `MainWindow.axaml`

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:res="clr-namespace:LlamaCppServerLauncher.My.Resources"
        xmlns:local="clr-namespace:LlamaCppServerLauncher"
        mc:Ignorable="d" d:DesignWidth="1200" d:DesignHeight="800"
        x:Class="LlamaCppServerLauncher.MainWindow"
        x:DataType="local:AppSettings"
        Title="{x:Static res:Resources.AppTitle}"
        Width="1200" Height="800"
        MinWidth="800" MinHeight="600">
    
    <Grid RowDefinitions="Auto,*,Auto">
        <!-- Header -->
        <Border Grid.Row="0" Background="#f0f0f0" BorderBrush="#ddd" BorderThickness="0,0,0,1">
            <TextBlock Text="{x:Static res:Resources.AppTitle}" 
                       FontSize="24" FontWeight="Bold"
                       HorizontalAlignment="Center" VerticalAlignment="Center"
                       Margin="10"/>
        </Border>
        
        <!-- Main Content -->
        <TabControl Grid.Row="1" Margin="10">
            <!-- Basic Settings Tab -->
            <TabItem Header="{x:Static res:Resources.BasicSettingsTabHeader}">
                <ScrollViewer>
                    <StackPanel Margin="10" Spacing="10">
                        <!-- Server Path -->
                        <Grid ColumnDefinitions="Auto,*,Auto" RowDefinitions="Auto" Margin="0,5">
                            <TextBlock Grid.Column="0" Text="{x:Static res:Resources.ServerPathLabel}" 
                                     VerticalAlignment="Center" Margin="0,0,10,0"/>
                            <TextBox Grid.Column="1" Text="{Binding ServerPath}" Margin="0,0,10,0"/>
                            <Button Grid.Column="2" x:Name="BrowseServerButton" 
                                    Content="{x:Static res:Resources.BrowseButton}" MinWidth="80"/>
                        </Grid>
                        
                        <!-- Model Path -->
                        <Grid ColumnDefinitions="Auto,*,Auto" RowDefinitions="Auto" Margin="0,5">
                            <TextBlock Grid.Column="0" Text="{x:Static res:Resources.ModelPathLabel}" 
                                     VerticalAlignment="Center" Margin="0,0,10,0"/>
                            <TextBox Grid.Column="1" Text="{Binding ModelPath}" Margin="0,0,10,0"/>
                            <Button Grid.Column="2" x:Name="BrowseModelButton" 
                                    Content="{x:Static res:Resources.BrowseButton}" MinWidth="80"/>
                        </Grid>
                        
                        <!-- Basic Parameters -->
                        <Expander Header="{x:Static res:Resources.BasicParametersExpander}" IsExpanded="True">
                            <StackPanel Margin="10,5" Spacing="5">
                                <Grid ColumnDefinitions="Auto,*" RowDefinitions="Auto" Margin="0,3">
                                    <TextBlock Grid.Column="0" Text="{x:Static res:Resources.ThreadsLabel}" 
                                             VerticalAlignment="Center" Margin="0,0,10,0"/>
                                    <NumericUpDown Grid.Column="1" Value="{Binding Threads}" 
                                                  Minimum="1" Maximum="64" FormatString="0"/>
                                </Grid>
                                
                                <Grid ColumnDefinitions="Auto,*" RowDefinitions="Auto" Margin="0,3">
                                    <TextBlock Grid.Column="0" Text="{x:Static res:Resources.ContextSizeLabel}" 
                                             VerticalAlignment="Center" Margin="0,0,10,0"/>
                                    <NumericUpDown Grid.Column="1" Value="{Binding CtxSize}" 
                                                  Minimum="512" Maximum="8192" FormatString="0"/>
                                </Grid>
                                
                                <Grid ColumnDefinitions="Auto,*" RowDefinitions="Auto" Margin="0,3">
                                    <TextBlock Grid.Column="0" Text="{x:Static res:Resources.GpuLayersLabel}" 
                                             VerticalAlignment="Center" Margin="0,0,10,0"/>
                                    <NumericUpDown Grid.Column="1" Value="{Binding NGpuLayers}" 
                                                  Minimum="0" Maximum="100" FormatString="0"/>
                                </Grid>
                            </StackPanel>
                        </Expander>
                        
                        <!-- Network Settings -->
                        <Expander Header="{x:Static res:Resources.NetworkSettingsExpander}" IsExpanded="True">
                            <StackPanel Margin="10,5" Spacing="5">
                                <Grid ColumnDefinitions="Auto,*" RowDefinitions="Auto" Margin="0,3">
                                    <TextBlock Grid.Column="0" Text="{x:Static res:Resources.HostLabel}" 
                                             VerticalAlignment="Center" Margin="0,0,10,0"/>
                                    <TextBox Grid.Column="1" Text="{Binding Host}"/>
                                </Grid>
                                
                                <Grid ColumnDefinitions="Auto,*" RowDefinitions="Auto" Margin="0,3">
                                    <TextBlock Grid.Column="0" Text="{x:Static res:Resources.PortLabel}" 
                                             VerticalAlignment="Center" Margin="0,0,10,0"/>
                                    <NumericUpDown Grid.Column="1" Value="{Binding Port}" 
                                                  Minimum="1" Maximum="65535" FormatString="0"/>
                                </Grid>
                                
                                <Grid ColumnDefinitions="Auto,*" RowDefinitions="Auto" Margin="0,3">
                                    <TextBlock Grid.Column="0" Text="{x:Static res:Resources.TimeoutLabel}" 
                                             VerticalAlignment="Center" Margin="0,0,10,0"/>
                                    <NumericUpDown Grid.Column="1" Value="{Binding Timeout}" 
                                                  Minimum="10" Maximum="3600" FormatString="0"/>
                                </Grid>
                            </StackPanel>
                        </Expander>
                    </StackPanel>
                </ScrollViewer>
            </TabItem>
            
            <!-- Sampling Settings Tab -->
            <TabItem Header="{x:Static res:Resources.SamplingSettingsTabHeader}">
                <ScrollViewer>
                    <StackPanel Margin="10" Spacing="10">
                        <Expander Header="{x:Static res:Resources.SamplingParametersExpander}" IsExpanded="True">
                            <StackPanel Margin="10,5" Spacing="5">
                                <Grid ColumnDefinitions="Auto,*" RowDefinitions="Auto" Margin="0,3">
                                    <TextBlock Grid.Column="0" Text="{x:Static res:Resources.TemperatureLabel}" 
                                             VerticalAlignment="Center" Margin="0,0,10,0"/>
                                    <NumericUpDown Grid.Column="1" Value="{Binding Temperature}" 
                                                  Minimum="0" Maximum="2" Increment="0.1" FormatString="0.0"/>
                                </Grid>
                                
                                <Grid ColumnDefinitions="Auto,*" RowDefinitions="Auto" Margin="0,3">
                                    <TextBlock Grid.Column="0" Text="{x:Static res:Resources.TopPLabel}" 
                                             VerticalAlignment="Center" Margin="0,0,10,0"/>
                                    <NumericUpDown Grid.Column="1" Value="{Binding TopP}" 
                                                  Minimum="0" Maximum="1" Increment="0.05" FormatString="0.00"/>
                                </Grid>
                                
                                <Grid ColumnDefinitions="Auto,*" RowDefinitions="Auto" Margin="0,3">
                                    <TextBlock Grid.Column="0" Text="{x:Static res:Resources.TopKLabel}" 
                                             VerticalAlignment="Center" Margin="0,0,10,0"/>
                                    <NumericUpDown Grid.Column="1" Value="{Binding TopK}" 
                                                  Minimum="0" Maximum="100" FormatString="0"/>
                                </Grid>
                                
                                <Grid ColumnDefinitions="Auto,*" RowDefinitions="Auto" Margin="0,3">
                                    <TextBlock Grid.Column="0" Text="{x:Static res:Resources.RepeatPenaltyLabel}" 
                                             VerticalAlignment="Center" Margin="0,0,10,0"/>
                                    <NumericUpDown Grid.Column="1" Value="{Binding RepeatPenalty}" 
                                                  Minimum="0" Maximum="2" Increment="0.1" FormatString="0.0"/>
                                </Grid>
                            </StackPanel>
                        </Expander>
                    </StackPanel>
                </ScrollViewer>
            </TabItem>
            
            <!-- Advanced Settings Tab -->
            <TabItem Header="{x:Static res:Resources.AdvancedSettingsTabHeader}">
                <ScrollViewer>
                    <StackPanel Margin="10" Spacing="10">
                        <Expander Header="{x:Static res:Resources.MemoryManagementExpander}" IsExpanded="True">
                            <StackPanel Margin="10,5" Spacing="5">
                                <CheckBox Content="{x:Static res:Resources.MlockCheckBox}" 
                                          IsChecked="{Binding Mlock}" Margin="0,3"/>
                                <CheckBox Content="{x:Static res:Resources.NoMmapCheckBox}" 
                                          IsChecked="{Binding NoMmap}" Margin="0,3"/>
                                <CheckBox Content="{x:Static res:Resources.NoKVOffloadCheckBox}" 
                                          IsChecked="{Binding NoKVOffload}" Margin="0,3"/>
                                <CheckBox Content="{x:Static res:Resources.NoRepackCheckBox}" 
                                          IsChecked="{Binding NoRepack}" Margin="0,3"/>
                            </StackPanel>
                        </Expander>
                        
                        <Expander Header="{x:Static res:Resources.CpuThreadManagementExpander}">
                            <StackPanel Margin="10,5" Spacing="5">
                                <Grid ColumnDefinitions="Auto,*" RowDefinitions="Auto" Margin="0,3">
                                    <TextBlock Grid.Column="0" Text="{x:Static res:Resources.BatchThreadsLabel}" 
                                             VerticalAlignment="Center" Margin="0,0,10,0"/>
                                    <NumericUpDown Grid.Column="1" Value="{Binding ThreadsBatch}" 
                                                  Minimum="-1" Maximum="64" FormatString="0"/>
                                </Grid>
                                
                                <CheckBox Content="{x:Static res:Resources.KVUnifiedCheckBox}" 
                                          IsChecked="{Binding KVUnified}" Margin="0,3"/>
                                <CheckBox Content="{x:Static res:Resources.FlashAttentionCheckBox}" 
                                          IsChecked="{Binding FlashAttention}" Margin="0,3"/>
                            </StackPanel>
                        </Expander>
                        
                        <Expander Header="{x:Static res:Resources.LoggingExpander}">
                            <StackPanel Margin="10,5" Spacing="5">
                                <CheckBox Content="{x:Static res:Resources.VerboseCheckBox}" 
                                          IsChecked="{Binding Verbose}" Margin="0,3"/>
                                <CheckBox Content="{x:Static res:Resources.LogColorsCheckBox}" 
                                          IsChecked="{Binding LogColors}" Margin="0,3"/>
                                <CheckBox Content="{x:Static res:Resources.LogTimestampsCheckBox}" 
                                          IsChecked="{Binding LogTimestamps}" Margin="0,3"/>
                                <CheckBox Content="{x:Static res:Resources.MetricsCheckBox}" 
                                          IsChecked="{Binding Metrics}" Margin="0,3"/>
                                <CheckBox Content="{x:Static res:Resources.SlotsCheckBox}" 
                                          IsChecked="{Binding Slots}" Margin="0,3"/>
                            </StackPanel>
                        </Expander>
                    </StackPanel>
                </ScrollViewer>
            </TabItem>
            
            <!-- Command Preview Tab -->
            <TabItem Header="{x:Static res:Resources.CommandPreviewTabHeader}">
                <StackPanel Margin="10" Spacing="10">
                    <TextBlock Text="{x:Static res:Resources.GeneratedCommandLineLabel}" FontWeight="Bold"/>
                    
                    <!-- Update Button -->
                    <Button x:Name="UpdateCommandPreviewButton" 
                            Content="Update Command Preview" 
                            HorizontalAlignment="Left"
                            MinWidth="180"
                            Background="#2196F3" 
                            Foreground="White"/>
                    
                    <!-- Command Preview Text Box -->
                    <TextBox x:Name="CommandPreviewTextBox" 
                             TextWrapping="Wrap" 
                             VerticalAlignment="Stretch"
                             MinHeight="150"
                             IsReadOnly="True"
                             FontFamily="Consolas, monospace"
                             Background="#f8f8f8"/>
                    
                    <!-- Copy Button -->
                    <Button x:Name="CopyCommandButton" 
                            Content="{x:Static res:Resources.CopyCommandButton}" 
                            HorizontalAlignment="Right"
                            MinWidth="120"/>
                </StackPanel>
            </TabItem>
        </TabControl>
        
        <!-- Footer with Controls -->
        <Border Grid.Row="2" Background="#f0f0f0" BorderBrush="#ddd" BorderThickness="0,1,0,0" Padding="10">
            <StackPanel Orientation="Horizontal" HorizontalAlignment="Center" Spacing="10">
                <Button x:Name="StartServerButton" Content="{x:Static res:Resources.StartServerButton}" 
                        Background="#4CAF50" Foreground="White"
                        MinWidth="120" Height="35"/>
                <Button x:Name="StopServerButton" Content="{x:Static res:Resources.StopServerButton}" 
                        Background="#f44336" Foreground="White"
                        IsEnabled="False"
                        MinWidth="120" Height="35"/>
                <Button x:Name="SaveSettingsButton" Content="{x:Static res:Resources.SaveSettingsButton}" 
                        Background="#2196F3" Foreground="White"
                        MinWidth="120" Height="35"/>
                <Button x:Name="LoadSettingsButton" Content="{x:Static res:Resources.LoadSettingsButton}" 
                        Background="#FF9800" Foreground="White"
                        MinWidth="120" Height="35"/>
            </StackPanel>
        </Border>
    </Grid>
</Window>
```

### 阶段四：简化 MainWindow 代码后端

#### 4.1 重构 MainWindow.axaml.vb

**文件**: `MainWindow.axaml.vb`

```vb
Imports Avalonia.Controls
Imports Avalonia.Interactivity
Imports Avalonia.Platform.Storage
Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Text.Json

Partial Public Class MainWindow
    Inherits Window
    
    Private serverProcess As Process
    Private serverRunning As Boolean = False
    Private settings As New AppSettings()
    Private configFile As String = Path.Combine(AppContext.BaseDirectory, "serverconfig.json")
    
    Public Sub New()
        InitializeComponent()
        DataContext = settings
        LoadSettingsSync()
        UpdateCommandPreview()
    End Sub
    
    Private Function GenerateCommandLineArguments() As String
        Dim args As New StringBuilder()

        ' Model Path
        If Not String.IsNullOrEmpty(settings.ModelPath) Then
            args.Append($"""{settings.ModelPath}""")
        End If

        ' Host
        If Not String.IsNullOrEmpty(settings.Host) Then
            args.Append($" --host {settings.Host}")
        End If

        ' Port
        If settings.Port > 0 Then
            args.Append($" --port {settings.Port}")
        End If

        ' Threads
        If settings.Threads > 0 Then
            args.Append($" -t {settings.Threads}")
        End If

        ' Context Size
        If settings.CtxSize > 0 Then
            args.Append($" -c {settings.CtxSize}")
        End If

        ' GPU Layers
        If settings.NGpuLayers > 0 Then
            args.Append($" -ngl {settings.NGpuLayers}")
        End If

        ' Batch Threads
        If settings.ThreadsBatch > 0 Then
            args.Append($" -tb {settings.ThreadsBatch}")
        End If

        ' Temperature
        If settings.Temperature >= 0 Then
            args.Append($" --temp {settings.Temperature.ToString(CultureInfo.InvariantCulture)}")
        End If

        ' Repeat Penalty
        If settings.RepeatPenalty >= 0 Then
            args.Append($" --repeat-penalty {settings.RepeatPenalty.ToString(CultureInfo.InvariantCulture)}")
        End If

        ' Top K
        If settings.TopK > 0 Then
            args.Append($" --top-k {settings.TopK}")
        End If

        ' Top P
        If settings.TopP >= 0 Then
            args.Append($" --top-p {settings.TopP.ToString(CultureInfo.InvariantCulture)}")
        End If

        ' Min P
        If settings.MinP >= 0 Then
            args.Append($" --min-p {settings.MinP.ToString(CultureInfo.InvariantCulture)}")
        End If

        ' Presence Penalty
        If settings.PresencePenalty >= 0 Then
            args.Append($" --presence-penalty {settings.PresencePenalty.ToString(CultureInfo.InvariantCulture)}")
        End If

        ' Frequency Penalty
        If settings.FrequencyPenalty >= 0 Then
            args.Append($" --frequency-penalty {settings.FrequencyPenalty.ToString(CultureInfo.InvariantCulture)}")
        End If

        ' Timeout
        If settings.Timeout > 0 Then
            args.Append($" --timeout {settings.Timeout}")
        End If

        ' Memory Management
        If settings.Mlock Then
            args.Append(" -mlock")
        End If

        If settings.NoMmap Then
            args.Append(" --no-mmap")
        End If

        If settings.KVUnified Then
            args.Append(" --kv-unified")
        End If

        If settings.NoKVOffload Then
            args.Append(" --no-kv-offload")
        End If

        If settings.NoRepack Then
            args.Append(" --no-repack")
        End If

        If settings.FlashAttention Then
            args.Append(" --flash-attn")
        End If

        ' Logging
        If settings.Verbose Then
            args.Append(" -v")
        End If

        If settings.LogColors Then
            args.Append(" --log-colors")
        End If

        If settings.LogTimestamps Then
            args.Append(" --log-timestamps")
        End If

        If settings.Metrics Then
            args.Append(" --metrics")
        End If

        If settings.Slots Then
            args.Append(" --slots")
        End If

        Return args.ToString().Trim()
    End Function

#Region " Event Handlers "
    
    Private Sub UpdateCommandPreviewButton_Click(sender As Object, e As RoutedEventArgs) Handles UpdateCommandPreviewButton.Click
        UpdateCommandPreview()
    End Sub

    Private Async Sub BrowseServerButton_Click(sender As Object, e As RoutedEventArgs) Handles BrowseServerButton.Click
        Await BrowseForServer()
    End Sub

    Private Async Sub BrowseModelButton_Click(sender As Object, e As RoutedEventArgs) Handles BrowseModelButton.Click
        Await BrowseForModel()
    End Sub

    Private Async Sub StartServerButton_Click(sender As Object, e As RoutedEventArgs) Handles StartServerButton.Click
        Await StartServer()
    End Sub

    Private Sub StopServerButton_Click(sender As Object, e As RoutedEventArgs) Handles StopServerButton.Click
        StopServer()
    End Sub

    Private Async Sub SaveSettingsButton_Click(sender As Object, e As RoutedEventArgs) Handles SaveSettingsButton.Click
        Await SaveSettings()
    End Sub

    Private Async Sub LoadSettingsButton_Click(sender As Object, e As RoutedEventArgs) Handles LoadSettingsButton.Click
        Await LoadSettingsAsync()
    End Sub

    Private Async Sub CopyCommandButton_Click(sender As Object, e As RoutedEventArgs) Handles CopyCommandButton.Click
        Await CopyCommandToClipboard()
    End Sub

#End Region

#Region " File Operations "
    
    Private Sub LoadSettingsSync()
        Try
            If File.Exists(configFile) Then
                Dim json As String = File.ReadAllText(configFile)
                settings = JsonSerializer.Deserialize(Of AppSettings)(json)
                DataContext = settings ' 更新 DataContext
            End If
        Catch
            ' If loading fails, use default settings
            settings = New AppSettings()
            DataContext = settings ' 更新 DataContext
        End Try
    End Sub

    Private Async Function BrowseForServer() As Task
        If StorageProvider IsNot Nothing Then
            Dim files As IReadOnlyList(Of IStorageFile) =
                Await StorageProvider.OpenFilePickerAsync(New FilePickerOpenOptions With {
                    .Title = "Select LLaMA.cpp Server Executable",
                    .AllowMultiple = False,
                    .FileTypeFilter = New List(Of FilePickerFileType) From {
                        New FilePickerFileType("Executable Files") With {
                            .Patterns = New List(Of String) From {"*.exe"}
                        }
                    }
                })

            If files.Count > 0 Then
                settings.ServerPath = files(0).Path.LocalPath
            End If
        End If
    End Function

    Private Async Function BrowseForModel() As Task
        If StorageProvider IsNot Nothing Then
            Dim files As IReadOnlyList(Of IStorageFile) =
                Await StorageProvider.OpenFilePickerAsync(New FilePickerOpenOptions With {
                    .Title = "Select LLaMA.cpp Model File",
                    .AllowMultiple = False,
                    .FileTypeFilter = New List(Of FilePickerFileType) From {
                        New FilePickerFileType("Model Files") With {
                            .Patterns = New List(Of String) From {"*.gguf", "*.bin"}
                        }
                    }
                })

            If files.Count > 0 Then
                settings.ModelPath = files(0).Path.LocalPath
            End If
        End If
    End Function

    Private Async Function SaveSettings() As Task
        Dim errorMessage As String = ""
        Try
            Dim json As String = JsonSerializer.Serialize(settings, New JsonSerializerOptions With {
                .WriteIndented = True
            })
            Await File.WriteAllTextAsync(configFile, json)
            Await MsgBoxAsync("Settings saved successfully", MsgBoxButtons.Ok, "Success")
        Catch ex As Exception
            errorMessage = ex.Message
        End Try

        If Not String.IsNullOrEmpty(errorMessage) Then
            Await MsgBoxAsync($"Error saving settings: {errorMessage}", MsgBoxButtons.Ok, "Error")
        End If
    End Function

    Private Async Function LoadSettingsAsync() As Task
        Dim errorMessage As String = ""
        Dim configFileExists As Boolean = False

        Try
            configFileExists = File.Exists(configFile)
            If configFileExists Then
                Dim json As String = Await File.ReadAllTextAsync(configFile)
                settings = JsonSerializer.Deserialize(Of AppSettings)(json)
                DataContext = settings ' 更新 DataContext
                UpdateCommandPreview()
                Await MsgBoxAsync("Settings loaded successfully", MsgBoxButtons.Ok, "Success")
            End If
        Catch ex As Exception
            errorMessage = ex.Message
        End Try

        If Not String.IsNullOrEmpty(errorMessage) Then
            Await MsgBoxAsync($"Error loading settings: {errorMessage}", MsgBoxButtons.Ok, "Error")
        ElseIf Not configFileExists Then
            Await MsgBoxAsync("No configuration file found. Using default settings.", MsgBoxButtons.Ok, "Info")
        End If
    End Function

#End Region

#Region " Server Management "
    
    Private Async Function StartServer() As Task
        Dim errorMessage As String = ""

        If serverRunning Then
            Await MsgBoxAsync("Server is already running!", MsgBoxButtons.Ok, "Warning")
            Return
        End If

        If String.IsNullOrEmpty(settings.ServerPath) OrElse Not File.Exists(settings.ServerPath) Then
            Await MsgBoxAsync("Server path is required!", MsgBoxButtons.Ok, "Error")
            Return
        End If

        If String.IsNullOrEmpty(settings.ModelPath) OrElse Not File.Exists(settings.ModelPath) Then
            Await MsgBoxAsync("Model path is required!", MsgBoxButtons.Ok, "Error")
            Return
        End If

        Try
            Dim args As String = GenerateCommandLineArguments()
            Dim fullCommand As String = $"""{settings.ServerPath}"" {args}"

            Dim startInfo As New ProcessStartInfo(settings.ServerPath, args) With {
                .UseShellExecute = True,
                .CreateNoWindow = False,
                .WindowStyle = ProcessWindowStyle.Normal
            }

            serverProcess = Process.Start(startInfo)
            serverRunning = True

            StartServerButton.IsEnabled = False
            StopServerButton.IsEnabled = True

            Await CheckServerStatusAsync()
        Catch ex As Exception
            errorMessage = ex.Message
        End Try

        If Not String.IsNullOrEmpty(errorMessage) Then
            Await MsgBoxAsync($"Failed to start server: {errorMessage}", MsgBoxButtons.Ok, "Error")
        End If
    End Function

    Private Sub StopServer()
        If Not serverRunning OrElse serverProcess Is Nothing OrElse serverProcess.HasExited Then
            Return
        End If

        Try
            serverProcess.Kill()
        Catch
            ' Ignore kill errors
        Finally
            serverRunning = False
            serverProcess = Nothing

            StartServerButton.IsEnabled = True
            StopServerButton.IsEnabled = False
        End Try
    End Sub

    Private Async Function CheckServerStatusAsync() As Task
        While serverRunning AndAlso serverProcess IsNot Nothing AndAlso Not serverProcess.HasExited
            Await Task.Delay(1000)
        End While

        If serverRunning Then
            serverRunning = False
            serverProcess = Nothing

            StartServerButton.IsEnabled = True
            StopServerButton.IsEnabled = False
        End If
    End Function

#End Region

#Region " Utility Methods "
    
    Private Async Function CopyCommandToClipboard() As Task
        If Not String.IsNullOrEmpty(CommandPreviewTextBox.Text) Then
            Dim errorMessage As String = ""
            Try
                Await Clipboard.SetTextAsync(CommandPreviewTextBox.Text)
                Await MsgBoxAsync("Command copied to clipboard", MsgBoxButtons.Ok, "Success")
            Catch ex As Exception
                errorMessage = ex.Message
            End Try

            If Not String.IsNullOrEmpty(errorMessage) Then
                Await MsgBoxAsync($"Failed to copy command: {errorMessage}", MsgBoxButtons.Ok, "Error")
            End If
        End If
    End Function

#End Region

End Class
```

## 📈 预期收益

### 技术收益
- **编译时绑定**: 消除反射开销，提升性能
- **类型安全**: 编译时错误检测，减少运行时错误
- **代码简化**: 移除 60%+ 的手动UI更新代码
- **自动通知**: 属性变更自动更新UI

### 开发体验
- **更好的调试**: 编译时错误提示
- **更少的错误**: 类型安全的绑定系统
- **更快的开发**: 自动化的属性通知
- **更清晰的代码**: 分离UI和数据逻辑

### 用户体验
- **实时响应**: 属性变更立即反映在UI上
- **更好的性能**: 启动和运行速度提升
- **手动预览**: 命令行预览通过按钮触发更新

## 🚀 实施步骤

1. **创建 ObservableBase 基类**
2. **重构 AppSettings 所有属性**
3. **更新 MainWindow.axaml 添加 x:DataType**
4. **更新 MainWindow.axaml.vb 简化代码**
5. **测试所有功能**

## 🎯 成功标准

- ✅ 属性变更自动更新UI
- ✅ 手动命令预览通过按钮触发正常工作
- ✅ 配置保存/加载功能正常
- ✅ 文件选择功能正常
- ✅ 编译时无错误和警告
- ✅ 性能明显提升
- ✅ 事件处理全部使用 Handles 子句

这个简化的重构计划充分利用了项目已有的 compiled binding 配置，只需要实现 INotifyPropertyChanged 和添加 x:DataType 就可以获得编译时绑定的所有好处。UpdateCommandPreview 保持按钮触发，事件处理使用 Handles 子句，符合用户的最新要求。