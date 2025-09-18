# 新 AppSettings 架构重构计划

## 概述

当前 `AppSettings.vb` 文件包含 1800+ 行代码，所有参数都作为独立属性存在，不便于搜索和管理。本计划旨在设计一个更灵活的参数管理系统，支持动态搜索和编辑功能。

## 核心设计

### 1. PrimitiveValue 类型系统

#### BooleanValue 结构体
```vb
Public Structure BooleanValue
    Private _value As Boolean?
    Public Property Value As Boolean?
        Get
            Return _value
        End Get
        Set
            _value = value
            OnPropertyChanged()
        End Set
    End Property
    
    Public Event PropertyChanged As EventHandler(Of PropertyChangedEventArgs)
    Private Sub OnPropertyChanged(<CallerMemberName> Optional propertyName As String = Nothing)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub
End Structure
```

#### StringValue 结构体
```vb
Public Structure StringValue
    Private _value As String
    Public Property Value As String
        Get
            Return _value
        End Get
        Set
            _value = value
            OnPropertyChanged()
        End Set
    End Property
    
    Public Event PropertyChanged As EventHandler(Of PropertyChangedEventArgs)
    Private Sub OnPropertyChanged(<CallerMemberName> Optional propertyName As String = Nothing)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub
End Structure
```

#### DoubleValue 结构体
```vb
Public Structure DoubleValue
    Private _value As Double?
    Public Property Value As Double?
        Get
            Return _value
        End Get
        Set
            _value = value
            OnPropertyChanged()
        End Set
    End Property
    
    Public Event PropertyChanged As EventHandler(Of PropertyChangedEventArgs)
    Private Sub OnPropertyChanged(<CallerMemberName> Optional propertyName As String = Nothing)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub
End Structure
```

### 2. ServerParameterMetadata 类

```vb
Public Class ServerParameterMetadata
    Public Property Argument As String
    Public Property Explanation As String
    Public Property Category As String ' common, sampling, example-specific
    Public Property Editor As String ' checkbox, textbox, numberupdown
    Public Property DefaultValue As Object
    Public Property ValueType As Type
    
    Public Shared ReadOnly Property AllParameters As List(Of ServerParameterMetadata)
        Get
            ' 预加载所有参数定义
        End Get
    End Property
End Class
```

### 3. ServerParameterItem 类

```vb
Public Class ServerParameterItem
    Inherits ObservableBase
    
    Private _argument As String
    Private _value As PrimitiveValue
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
    
    Public Property Value As PrimitiveValue
        Get
            Return _value
        End Get
        Set
            SetProperty(_value, value)
            ' 当 Value 变化时自动更新 HasLocalValue
            HasLocalValue = Value IsNot Nothing AndAlso Not Value.IsEmpty
        End Set
    End Property
    
    Public Property HasLocalValue As Boolean
        Get
            Return _hasLocalValue
        End Get
        Private Set
            SetProperty(_hasLocalValue, value)
        End Set
    End Property
    
    Public Property Metadata As ServerParameterMetadata
        Get
            Return _metadata
        End Get
        Private Set
            SetProperty(_metadata, value)
        End Set
    End Property
    
    Public Sub New(argument As String, metadata As ServerParameterMetadata)
        Me.Argument = argument
        Me.Metadata = metadata
    End Sub
End Class
```

### 4. ServerParameterCollection 类

```vb
Public Class ServerParameterCollection
    Inherits ObservableCollection(Of ServerParameterItem)
    
    Public Sub ClearLocalValue(argument As String)
        Dim item = Me.FirstOrDefault(Function(x) x.Argument = argument)
        If item IsNot Nothing Then
            item.Value = Nothing ' 清空值会自动设置 HasLocalValue = False
            Remove(item) ' 从集合中移除
        End If
    End Sub
    
    Public Function GetParameter(argument As String) As ServerParameterItem
        Return Me.FirstOrDefault(Function(x) x.Argument = argument)
    End Function
    
    Public Sub SetParameter(argument As String, value As Object)
        Dim metadata = ServerParameterMetadata.AllParameters.FirstOrDefault(Function(x) x.Argument = argument)
        If metadata Is Nothing Then Return
        
        Dim item = GetParameter(argument)
        If item Is Nothing Then
            item = New ServerParameterItem(argument, metadata)
            Add(item)
        End If
        
        ' 根据 metadata.ValueType 设置适当的值
        Select Case metadata.ValueType
            Case GetType(Boolean)
                item.Value = New BooleanValue With {.Value = Convert.ToBoolean(value)}
            Case GetType(String)
                item.Value = New StringValue With {.Value = Convert.ToString(value)}
            Case GetType(Double)
                item.Value = New DoubleValue With {.Value = Convert.ToDouble(value)}
        End Select
    End Sub
End Class
```

### 5. 新的 AppSettings 类

```vb
Public Class AppSettings
    Inherits ObservableBase
    
    ' 保持现有属性，用于向后兼容
    Private _serverPath As String = ""
    Private _modelPath As String = ""
    ' ... 其他现有属性
    
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
    
    ' 保持现有属性定义...
    
    Public Sub New()
        ' 初始化时迁移现有属性到 ServerParameterCollection
        MigrateExistingProperties()
    End Sub
    
    Private Sub MigrateExistingProperties()
        ' 将现有属性值迁移到 ServerParameterCollection
        ServerParameters.SetParameter("-t", Threads)
        ServerParameters.SetParameter("-c", CtxSize)
        ServerParameters.SetParameter("-n", NPredict)
        ' ... 迁移其他属性
    End Sub
    
    ' 添加方法用于从 ServerParameterCollection 更新现有属性
    Public Sub UpdateFromServerParameters()
        Dim param = ServerParameters.GetParameter("-t")
        If param?.HasLocalValue = True Then
            Threads = Convert.ToInt32(param.Value)
        End If
        
        param = ServerParameters.GetParameter("-c")
        If param?.HasLocalValue = True Then
            CtxSize = Convert.ToInt32(param.Value)
        End If
        ' ... 更新其他属性
    End Sub
End Class
```

## 实施计划

### 第一阶段：基础类型实现
- [ ] 创建 PrimitiveValue 结构体（BooleanValue, StringValue, DoubleValue）
- [ ] 实现 ServerParameterMetadata 类，预加载所有参数定义
- [ ] 创建 ServerParameterItem 类，实现属性变更通知
- [ ] 实现 ServerParameterCollection 类，包含 ClearLocalValue 方法
- [ ] 测试基础类型的属性变更通知机制

### 第二阶段：集成到现有系统
- [ ] 修改 AppSettings 类，添加 ServerParameterCollection 属性
- [ ] 实现现有属性到新系统的自动迁移逻辑
- [ ] 更新 JSON 序列化/反序列化逻辑，处理新属性
- [ ] 实现向后兼容，确保现有设置文件仍然可用
- [ ] 测试设置文件的保存和加载功能

### 第三阶段：UI 集成
- [ ] 创建参数搜索界面，支持按参数名、分类、说明搜索
- [ ] 实现动态参数编辑器，根据 Editor 类型生成对应控件
- [ ] 更新 MainWindow，添加参数搜索和编辑功能
- [ ] 实现参数的添加、删除、修改操作
- [ ] 更新命令行生成逻辑，支持 ServerParameterCollection

### 第四阶段：测试和优化
- [ ] 单元测试所有新组件
- [ ] 集成测试整个参数管理系统
- [ ] 性能优化，处理大量参数的加载和搜索
- [ ] 用户体验优化，改进搜索和编辑界面
- [ ] 文档更新，说明新功能的使用方法

## JSON 序列化考虑

为了保持 JSON 属性顺序，新的系统将：
1. 保持现有属性的序列化顺序
2. ServerParameterCollection 作为独立属性序列化
3. 使用 `[JsonPropertyOrder]` 属性确保顺序一致性

## 迁移策略

1. **渐进式迁移**: 用户可以继续使用现有属性，同时逐步迁移到新系统
2. **自动转换**: 加载旧设置文件时，自动转换到新格式
3. **向后兼容**: 保存时可以选择保存为新格式或兼容格式
4. **零数据丢失**: 确保所有现有设置都能正确迁移

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
| textbox | TextBox | String |
| numberupdown | NumericUpDown | Integer/Double |
| combobox | ComboBox | Enum/List |
| filepath | FilePicker | 文件路径 |
| directory | DirectoryPicker | 目录路径 |

## 进度跟踪

- **总体进度**: 0% (计划阶段)
- **第一阶段**: 0% (未开始)
- **第二阶段**: 0% (未开始)
- **第三阶段**: 0% (未开始)
- **第四阶段**: 0% (未开始)

## 风险评估

1. **复杂性风险**: 系统可能变得过于复杂
   - **缓解措施**: 保持简单设计，避免过度工程化
   
2. **兼容性风险**: 现有用户设置可能丢失
   - **缓解措施**: 严格的迁移测试和备份机制
   
3. **性能风险**: 大量参数可能影响性能
   - **缓解措施**: 实现虚拟化搜索和延迟加载

## 代码审查计划

每个阶段完成后，将进行代码审查：
1. 代码结构和设计模式审查
2. 功能完整性测试
3. 性能评估
4. 用户体验测试
5. 文档完整性检查

---

**文档创建日期**: 2025-09-18
**最后更新**: 2025-09-18
**负责人**: 待定
**审查状态**: 待审查