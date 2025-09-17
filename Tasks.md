# LLaMA.cpp HTTP Server Launcher 开发任务

## 项目概述
实现一个基于 Avalonia UI 的 VB.NET 桌面应用程序，用于管理和启动 LLaMA.cpp HTTP Server。该应用将包含所有221+个启动参数的配置界面，支持参数持久化、预览和多语言。

## 技术栈
- **框架**: .NET 9.0
- **UI框架**: Avalonia UI 11.3.4
- **编程语言**: VB.NET
- **数据处理**: System.Text.Json
- **架构模式**: MVVM, Partial Class
- **异步编程**: Async/Await

## Phase 1: 项目基础设施

### 1.1 项目设置和配置
- [x] 创建 Visual Studio .gitignore 文件
- [x] 创建 Tasks.md 详细任务分解
- [ ] 设置项目文件结构

### 1.2 基础应用程序结构
- [ ] 创建 Program.vb 应用程序入口点
- [ ] 创建 App.axaml.vb 应用程序类
- [ ] 创建 MainWindow.axaml 基础UI布局

## Phase 2: 配置管理系统

### 2.1 应用设置类
- [ ] 实现 AppSettings 类，包含所有 LLaMA.cpp 参数
  - 基础参数 (线程数、上下文大小等)
  - 采样参数 (温度、top-p、top-k 等)
  - 高级参数 (GPU层、内存管理等)
  - 网络参数 (主机、端口、SSL等)
- [ ] 实现 JSON 序列化/反序列化
- [ ] 实现配置保存和加载功能

### 2.2 参数验证和默认值
- [ ] 为每个参数设置合理的默认值
- [ ] 实现参数范围验证
- [ ] 实现文件路径存在性检查

## Phase 3: 用户界面开发

### 3.1 主界面布局
- [ ] 创建 TabControl 分组界面
  - 基本设置选项卡
  - 采样参数选项卡
  - 高级设置选项卡
  - 网络配置选项卡
  - 启动预览选项卡
- [ ] 实现服务器控制按钮区域
- [ ] 实现状态显示区域

### 3.2 参数管理界面
- [ ] 创建分类的参数控制面板
  - **基本参数**: 模型路径、线程数、上下文大小等
  - **采样参数**: 温度、top-p、top-k、惩罚等
  - **硬件配置**: GPU层、内存映射、CPU亲和性等
  - **网络配置**: 主机、端口、SSL、API密钥等
  - **日志配置**: 详细程度、文件日志、颜色等
  - **模型配置**: LoRA适配器、控制向量等
- [ ] 实现文件浏览器对话框
- [ ] 实现数值输入控件和验证

### 3.3 启动参数预览
- [ ] 创建命令行参数预览文本框
- [ ] 实现实时参数更新显示
- [ ] 添加参数复制功能

## Phase 4: 服务器进程管理

### 4.1 服务器启动
- [ ] 实现命令行参数生成
- [ ] 实现服务器进程启动
- [ ] 实现独立命令窗口创建
- [ ] 实现进程状态监控

### 4.2 进程监控
- [ ] 实现服务器状态检查
- [ ] 实现输出日志捕获
- [ ] 实现错误处理和显示
- [ ] 实现服务器停止功能

## Phase 5: 国际化支持

### 5.1 资源文件
- [ ] 创建 en-US 资源文件
- [ ] 创建 zh-CN 资源文件
- [ ] 实现所有UI文本的本地化

### 5.2 语言切换
- [ ] 实现语言选择功能
- [ ] 实现运行时语言切换
- [ ] 保存语言偏好设置

## Phase 6: 高级功能

### 6.1 配置管理
- [ ] 实现配置导入/导出
- [ ] 实现配置预设管理
- [ ] 实现配置重置功能

### 6.2 用户体验优化
- [ ] 实现参数分组和折叠
- [ ] 实现搜索和过滤功能
- [ ] 实现工具提示和帮助信息

## Phase 7: 测试和优化

### 7.1 功能测试
- [ ] 测试所有参数配置
- [ ] 测试服务器启动功能
- [ ] 测试进程管理功能
- [ ] 测试国际化功能

### 7.2 性能优化
- [ ] 优化UI响应性能
- [ ] 优化内存使用
- [ ] 优化启动时间

## Phase 8: 发布准备

### 8.1 文档和说明
- [ ] 创建用户使用说明
- [ ] 创建开发者文档
- [ ] 更新README文件

### 8.2 打包和发布
- [ ] 配置发布设置
- [ ] 创建安装程序
- [ ] 准备发布版本

## 详细实现说明

### AppSettings 类结构
```vb
Public Class AppSettings
    ' 基础参数
    Public Property ModelPath As String
    Public Property Threads As Integer = 4
    Public Property CtxSize As Integer = 4096
    Public Property NPredict As Integer = -1
    
    ' 采样参数
    Public Property Temperature As Double = 0.8
    Public Property TopP As Double = 0.9
    Public Property TopK As Integer = 40
    Public Property RepeatPenalty As Double = 1.0
    
    ' 硬件配置
    Public Property NGpuLayers As Integer = 0
    Public Property Mlock As Boolean = False
    Public Property NoMmap As Boolean = False
    
    ' 网络配置
    Public Property Host As String = "127.0.0.1"
    Public Property Port As Integer = 8080
    Public Property Timeout As Integer = 600
    
    ' 其他221+参数...
End Class
```

### 命令行参数生成逻辑
```vb
Private Function GenerateCommandLineArguments() As String
    Dim args As New StringBuilder()
    
    ' 基础参数
    If Not String.IsNullOrEmpty(Settings.ModelPath) Then
        args.Append($"-m ""{Settings.ModelPath}"" ")
    End If
    
    ' 线程数
    If Settings.Threads > 0 Then
        args.Append($"-t {Settings.Threads} ")
    End If
    
    ' 上下文大小
    If Settings.CtxSize > 0 Then
        args.Append($"-c {Settings.CtxSize} ")
    End If
    
    ' 条件参数
    If Settings.Mlock Then
        args.Append("--mlock ")
    End If
    
    ' 采样参数
    args.Append($"--temp {Settings.Temperature} ")
    args.Append($"--top-p {Settings.TopP} ")
    args.Append($"--top-k {Settings.TopK} ")
    
    ' 网络参数
    args.Append($"--host {Settings.Host} ")
    args.Append($"--port {Settings.Port} ")
    
    Return args.ToString().Trim()
End Function
```

### 进程管理架构
```vb
Private serverProcess As Process
Private serverRunning As Boolean = False

Private Async Sub StartServer()
    Dim args = GenerateCommandLineArguments()
    Dim startInfo As New ProcessStartInfo(Settings.ServerPath, args) With {
        .UseShellExecute = True,
        .CreateNoWindow = False,
        .WindowStyle = ProcessWindowStyle.Normal
    }
    
    serverProcess = Process.Start(startInfo)
    serverRunning = True
    
    Await CheckServerStatusAsync()
End Sub
```

## 开发注意事项

### VB.NET 编码规范
- 使用 `Dim x As New Xxx` 而不是 `Dim x As Xxx = New Xxx`
- 使用 `With` 关键字进行对象初始化
- 使用 `Handles` 处理事件
- 使用 `Async Sub` 处理异步事件
- 遵循 Visual Basic Coding Conventions

### Avalonia UI 最佳实践
- 使用 `x:Static` 绑定资源
- 使用 `Grid` 和 `StackPanel` 进行布局
- 使用 `Expander` 控件组织参数组
- 使用 `Dispatcher.UIThread.InvokeAsync` 进行线程安全UI更新

### 性能考虑
- 使用异步操作避免阻塞UI线程
- 实现适当的资源管理
- 避免频繁的UI更新
- 使用缓存机制优化性能
