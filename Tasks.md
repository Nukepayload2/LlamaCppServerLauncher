# 新 AppSettings 架构重构计划

## 概述

当前 `AppSettings.vb` 文件包含 1800+ 行代码，所有参数都作为独立属性存在，不便于搜索和管理。本计划旨在设计一个更灵活的参数管理系统，支持动态搜索和编辑功能。

## 核心设计

### 1. PrimitiveValue 类型系统

#### PrimitiveValue 类
```vb
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
End Class
```

### 2. ServerParameterMetadata 类

```vb
Public Class ServerParameterMetadata
    Public Property Argument As String
    Public Property Explanation As String
    Public Property Category As String ' common, sampling, example-specific
    Public Property Editor As String ' checkbox, textbox, numberupdown ...
    
    ' 预载所有属性定义
    Public Shared ReadOnly Property AllParameters As New List(Of ServerParameterMetadata) From {}
End Class
```

### 3. ServerParameterItem 类

```vb
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
    
    Public Sub New()
        ' 监听值变成非空则 HasLocalValue = true
        AddHandler Value.PropertyChanged, AddressOf UpdateHasLocalValue
    End Sub
End Class
```

### 4. ServerParameterCollection 类

```vb
Public Class ServerParameterCollection
    Inherits ObservableCollection(Of ServerParameterItem)
    
    ' 这个命令的作用是清除命令参数 ServerParameterItem 的 HasLocalValue 并且清空 Value 里面的属性值
    Public ReadOnly Property ClearLocalValue As ICommand = New ClearLocalValueCommand
End Class
```

### 5. 新的 AppSettings 类

```vb
Public Class AppSettings
    Inherits ObservableBase
    
    ' 现有属性不要了
    
    ' 新增的 ServerParameterCollection
    Private _serverParameters As New ServerParameterCollection()
    
    Public Property ServerParameters As ServerParameterCollection
        Get
            Return _serverParameters
        End Get
        Private Set
            SetProperty(_serverParameters, value)
        End Set
    End Property
    
End Class
```

## 实施计划

### 第一阶段：基础类型实现 ✅ 已完成
- [x] 创建 PrimitiveValue 结构体（BooleanValue, StringValue, DoubleValue）
- [x] 实现 ServerParameterMetadata 类，预加载所有参数定义（重要：一个参数都不能落下！记得检查有没有漏掉参数！）
- [x] 创建 ServerParameterItem 类，实现属性变更通知
- [x] 实现 ServerParameterCollection 类，包含 ClearLocalValue 方法
- [x] 测试基础类型的属性变更通知机制

### 第二阶段：集成到现有系统 ✅ 已完成
- [x] 修改 AppSettings 类，添加 ServerParameterCollection 属性
- [x] 更新 JSON 序列化/反序列化逻辑，处理新属性
- [x] 测试设置文件的保存和加载功能
- [x] 更新命令行生成逻辑，从 ServerParameterCollection 中挑选有 LocalValue 的项生成

### 第三阶段：UI 集成 🔄 进行中
- [ ] 完全重做 MainWindow，做成单页面不分 Tab 的布局，删掉现有的参数和绑定，只保留参数预览和启动按钮
- [ ] 创建参数筛选界面，支持按参数名、分类、说明、是否有 LocalValue 筛选。分类是个多选 list，是否已填是个多选 list，参数名和分类合在一起用文本框筛选，做200ms防抖
- [ ] 实现动态参数编辑器，根据 Editor 类型选择编辑用的数据模板，每个模板内部绑定的是 PrimitiveValue 的属性 

## 完成情况总结

### ✅ 已完成的核心功能
1. **完整的类型系统**：
   - `PrimitiveValue` 类支持三种数据类型（Boolean、String、Double）
   - `ServerParameterMetadata` 类包含所有180+个参数的完整定义
   - `ServerParameterItem` 类实现属性变更通知和 HasLocalValue 逻辑
   - `ServerParameterCollection` 类提供参数管理和筛选功能

2. **成功集成到现有系统**：
   - 重构 `AppSettings` 类，移除1800+行重复代码
   - 保留向后兼容的属性访问器
   - 新的架构支持动态参数管理
   - 编译成功，无重大错误

3. **命令行生成逻辑已更新**：
   - 从 `ServerParameterCollection` 中动态生成命令行
   - 只包含有 LocalValue 的参数
   - 支持所有数据类型的正确格式化

### 🔄 当前进行中
UI 重构阶段，准备实现：
- 单页面布局设计
- 参数筛选界面
- 动态参数编辑器

## JSON 序列化考虑
读取：
- 先从 AllParameters 里面获取元数据，
- 然后创建一个全参数但是没有 LocalValue 的 ServerParameterCollection。
- 然后反序列化 ServerParameterItem 数组，填充 LocalValue。如果没有文件可供反序列化则跳过这个步骤。
- 然后用 Argument 查找对应的 Metadata。

写入：
- 序列化 ServerParameterCollection 中有 LocalValue 的项

## 搜索功能设计

搜索界面将支持：
- 按参数名搜索（支持模糊匹配）
- 按分类筛选（common, sampling, example-specific）
- 按说明内容搜索
- 显示参数的默认值、当前值、编辑器类型
- 一键重置为默认值
- 批量操作（启用/禁用一组参数）

## 编辑器类型映射

| Editor 类型 | 对应控件 | 适用参数类型 |
|------------|---------|-------------|
| checkbox | CheckBox | Boolean |
| textbox | TextBox | String, 包括命令行里的 list |
| numberupdown | NumericUpDown | Integer/Double |
| filepath | FilePicker | 文件路径 String |
| directory | DirectoryPicker | 目录路径 String |

## 代码审查计划

每个阶段完成后，停下工作，通知用户执行代码审查
