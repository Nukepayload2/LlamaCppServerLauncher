Imports System.Text.Json.Serialization
Imports LlamaCppServerLauncher.Helpers
Imports LlamaCppServerLauncher.Models

Public Class AppSettings
    Inherits ObservableBase
    
    ' 新的 ServerParameterCollection 替换原有属性
    Private _serverParameters As New ServerParameterCollection()
    
    Public Property ServerParameters As ServerParameterCollection
        Get
            Return _serverParameters
        End Get
        Private Set
            SetProperty(_serverParameters, value)
        End Set
    End Property
    
    ' 便捷属性访问器
    Public Property ServerPath As String
        Get
            Return GetParameterValue("--server-path")
        End Get
        Set
            SetParameterValue("--server-path", value)
        End Set
    End Property
    
    Public Property ModelPath As String
        Get
            Return GetParameterValue("--model")
        End Get
        Set
            SetParameterValue("--model", value)
        End Set
    End Property
    
    Public Property Threads As Integer
        Get
            Return GetParameterIntegerValue("--threads")
        End Get
        Set
            SetParameterIntegerValue("--threads", value)
        End Set
    End Property
    
    Public Property CtxSize As Integer
        Get
            Return GetParameterIntegerValue("--ctx-size")
        End Get
        Set
            SetParameterIntegerValue("--ctx-size", value)
        End Set
    End Property
    
    Public Property NPredict As Integer
        Get
            Return GetParameterIntegerValue("--n-predict")
        End Get
        Set
            SetParameterIntegerValue("--n-predict", value)
        End Set
    End Property
    
    Public Property Host As String
        Get
            Return GetParameterValue("--host")
        End Get
        Set
            SetParameterValue("--host", value)
        End Set
    End Property
    
    Public Property Port As Integer
        Get
            Return GetParameterIntegerValue("--port")
        End Get
        Set
            SetParameterIntegerValue("--port", value)
        End Set
    End Property
    
    ' 其他常用属性的便捷访问器
    Public Property NGpuLayers As Integer
        Get
            Return GetParameterIntegerValue("--n-gpu-layers")
        End Get
        Set
            SetParameterIntegerValue("--n-gpu-layers", value)
        End Set
    End Property
    
    Public Property ThreadsBatch As Integer
        Get
            Return GetParameterIntegerValue("--threads-batch")
        End Get
        Set
            SetParameterIntegerValue("--threads-batch", value)
        End Set
    End Property
    
    Public Property Temperature As Double
        Get
            Return GetParameterDoubleValue("--temperature")
        End Get
        Set
            SetParameterDoubleValue("--temperature", value)
        End Set
    End Property
    
    Public Property RepeatPenalty As Double
        Get
            Return GetParameterDoubleValue("--repeat-penalty")
        End Get
        Set
            SetParameterDoubleValue("--repeat-penalty", value)
        End Set
    End Property
    
    Public Property TopK As Integer
        Get
            Return GetParameterIntegerValue("--top-k")
        End Get
        Set
            SetParameterIntegerValue("--top-k", value)
        End Set
    End Property
    
    Public Property TopP As Double
        Get
            Return GetParameterDoubleValue("--top-p")
        End Get
        Set
            SetParameterDoubleValue("--top-p", value)
        End Set
    End Property
    
    Public Property MinP As Double
        Get
            Return GetParameterDoubleValue("--min-p")
        End Get
        Set
            SetParameterDoubleValue("--min-p", value)
        End Set
    End Property
    
    Public Property PresencePenalty As Double
        Get
            Return GetParameterDoubleValue("--presence-penalty")
        End Get
        Set
            SetParameterDoubleValue("--presence-penalty", value)
        End Set
    End Property
    
    Public Property FrequencyPenalty As Double
        Get
            Return GetParameterDoubleValue("--frequency-penalty")
        End Get
        Set
            SetParameterDoubleValue("--frequency-penalty", value)
        End Set
    End Property
    
    Public Property Timeout As Integer
        Get
            Return GetParameterIntegerValue("--timeout")
        End Get
        Set
            SetParameterIntegerValue("--timeout", value)
        End Set
    End Property
    
    Public Property Mlock As Boolean
        Get
            Return GetParameterBooleanValue("--mlock")
        End Get
        Set
            SetParameterBooleanValue("--mlock", value)
        End Set
    End Property
    
    Public Property NoMmap As Boolean
        Get
            Return GetParameterBooleanValue("--no-mmap")
        End Get
        Set
            SetParameterBooleanValue("--no-mmap", value)
        End Set
    End Property
    
    Public Property KVUnified As Boolean
        Get
            Return GetParameterBooleanValue("--kv-unified")
        End Get
        Set
            SetParameterBooleanValue("--kv-unified", value)
        End Set
    End Property
    
    Public Property NoKVOffload As Boolean
        Get
            Return GetParameterBooleanValue("--no-kv-offload")
        End Get
        Set
            SetParameterBooleanValue("--no-kv-offload", value)
        End Set
    End Property
    
    Public Property NoRepack As Boolean
        Get
            Return GetParameterBooleanValue("--no-repack")
        End Get
        Set
            SetParameterBooleanValue("--no-repack", value)
        End Set
    End Property
    
    Public Property FlashAttention As Boolean
        Get
            Return GetParameterBooleanValue("--flash-attn")
        End Get
        Set
            SetParameterBooleanValue("--flash-attn", value)
        End Set
    End Property
    
    Public Property Verbose As Boolean
        Get
            Return GetParameterBooleanValue("--verbose")
        End Get
        Set
            SetParameterBooleanValue("--verbose", value)
        End Set
    End Property
    
    Public Property LogColors As Boolean
        Get
            Return GetParameterBooleanValue("--log-colors")
        End Get
        Set
            SetParameterBooleanValue("--log-colors", value)
        End Set
    End Property
    
    Public Property LogTimestamps As Boolean
        Get
            Return GetParameterBooleanValue("--log-timestamps")
        End Get
        Set
            SetParameterBooleanValue("--log-timestamps", value)
        End Set
    End Property
    
    Public Property Metrics As Boolean
        Get
            Return GetParameterBooleanValue("--metrics")
        End Get
        Set
            SetParameterBooleanValue("--metrics", value)
        End Set
    End Property
    
    Public Property Slots As Boolean
        Get
            Return GetParameterBooleanValue("--slots")
        End Get
        Set
            SetParameterBooleanValue("--slots", value)
        End Set
    End Property
    
    ' 命令行生成
    Public Function GenerateCommandLine() As String
        Return _serverParameters.GenerateCommandLine()
    End Function
    
    ' 私有帮助方法
    Private Function GetParameterValue(argument As String) As String
        Dim parameter = _serverParameters.FirstOrDefault(Function(p) p.Argument = argument)
        Return If(parameter IsNot Nothing AndAlso parameter.Value.StringValue IsNot Nothing, 
                   parameter.Value.StringValue, "")
    End Function
    
    Private Function GetParameterIntegerValue(argument As String) As Integer
        Dim parameter = _serverParameters.FirstOrDefault(Function(p) p.Argument = argument)
        Return If(parameter IsNot Nothing AndAlso parameter.Value.DoubleValue.HasValue, 
                   CInt(parameter.Value.DoubleValue.Value), 0)
    End Function
    
    Private Function GetParameterDoubleValue(argument As String) As Double
        Dim parameter = _serverParameters.FirstOrDefault(Function(p) p.Argument = argument)
        Return If(parameter IsNot Nothing AndAlso parameter.Value.DoubleValue.HasValue, 
                   parameter.Value.DoubleValue.Value, 0.0)
    End Function
    
    Private Function GetParameterBooleanValue(argument As String) As Boolean
        Dim parameter = _serverParameters.FirstOrDefault(Function(p) p.Argument = argument)
        Return If(parameter IsNot Nothing AndAlso parameter.Value.BooleanValue.HasValue, 
                   parameter.Value.BooleanValue.Value, False)
    End Function
    
    Private Sub SetParameterValue(argument As String, value As String)
        Dim parameter = _serverParameters.FirstOrDefault(Function(p) p.Argument = argument)
        If parameter IsNot Nothing Then
            parameter.Value.StringValue = value
        End If
    End Sub
    
    Private Sub SetParameterIntegerValue(argument As String, value As Integer)
        Dim parameter = _serverParameters.FirstOrDefault(Function(p) p.Argument = argument)
        If parameter IsNot Nothing Then
            parameter.Value.DoubleValue = value
        End If
    End Sub
    
    Private Sub SetParameterDoubleValue(argument As String, value As Double)
        Dim parameter = _serverParameters.FirstOrDefault(Function(p) p.Argument = argument)
        If parameter IsNot Nothing Then
            parameter.Value.DoubleValue = value
        End If
    End Sub
    
    Private Sub SetParameterBooleanValue(argument As String, value As Boolean)
        Dim parameter = _serverParameters.FirstOrDefault(Function(p) p.Argument = argument)
        If parameter IsNot Nothing Then
            parameter.Value.BooleanValue = value
        End If
    End Sub
End Class