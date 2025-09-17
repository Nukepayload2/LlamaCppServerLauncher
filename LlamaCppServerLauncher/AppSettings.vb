Imports System.Text.Json.Serialization

Public Class AppSettings
    ' Basic Parameters
    Public Property ServerPath As String = ""
    Public Property ModelPath As String = ""
    Public Property Threads As Integer = 4
    Public Property CtxSize As Integer = 4096
    Public Property NPredict As Integer = -1
    
    ' Batch and Thread Parameters
    Public Property BatchSize As Integer = 2048
    Public Property UbatchSize As Integer = 512
    Public Property ThreadsBatch As Integer = -1
    
    ' Hardware Configuration
    Public Property NGpuLayers As Integer = 0
    Public Property MainGpu As Integer = 0
    Public Property TensorSplit As String = ""
    Public Property SplitMode As String = "none"
    Public Property Mlock As Boolean = False
    Public Property NoMmap As Boolean = False
    Public Property Numa As String = ""
    Public Property Device As String = ""
    Public Property CpuMask As String = ""
    Public Property CpuRange As String = ""
    Public Property CpuStrict As Boolean = False
    Public Property Prio As Integer = 0
    Public Property Poll As Integer = 50
    Public Property CpuMaskBatch As String = ""
    Public Property CpuRangeBatch As String = ""
    Public Property CpuStrictBatch As Boolean = False
    Public Property PrioBatch As Integer = 0
    Public Property PollBatch As Integer = 50
    
    ' KV Cache Configuration
    Public Property NoKVOffload As Boolean = False
    Public Property NoRepack As Boolean = False
    Public Property CacheTypeK As String = "f16"
    Public Property CacheTypeV As String = "f16"
    Public Property DefragThold As Double = 0.5
    Public Property KVUnified As Boolean = False
    Public Property SWAFull As Boolean = False
    
    ' Sampling Parameters
    Public Property Temperature As Double = 0.8
    Public Property TopP As Double = 0.9
    Public Property TopK As Integer = 40
    Public Property MinP As Double = 0.1
    Public Property TopNSigma As Double = -1.0
    Public Property XtcProbability As Double = 0.0
    Public Property XtcThreshold As Double = 0.1
    Public Property Typical As Double = 1.0
    Public Property RepeatLastN As Integer = 64
    Public Property RepeatPenalty As Double = 1.0
    Public Property PresencePenalty As Double = 0.0
    Public Property FrequencyPenalty As Double = 0.0
    Public Property DryMultiplier As Double = 0.0
    Public Property DryBase As Double = 1.75
    Public Property DryAllowedLength As Integer = 2
    Public Property DryPenaltyLastN As Integer = -1
    Public Property DrySequenceBreaker As String = ""
    Public Property DynatempRange As Double = 0.0
    Public Property DynatempExp As Double = 1.0
    Public Property Mirostat As Integer = 0
    Public Property MirostatLr As Double = 0.1
    Public Property MirostatEnt As Double = 5.0
    Public Property Samplers As String = "penalties;dry;top_n_sigma;top_k;typ_p;top_p;min_p;xtc;temperature"
    Public Property SamplingSeq As String = "edskypmxt"
    Public Property IgnoreEOS As Boolean = False
    
    ' Rope Configuration
    Public Property RopeScaling As String = "none"
    Public Property RopeScale As Double = 1.0
    Public Property RopeFreqBase As Double = 0.0
    Public Property RopeFreqScale As Double = 1.0
    Public Property YarnOrigCtx As Integer = 0
    Public Property YarnExtFactor As Double = -1.0
    Public Property YarnAttnFactor As Double = 1.0
    Public Property YarnBetaSlow As Double = 1.0
    Public Property YarnBetaFast As Double = 32.0
    
    ' Flash Attention
    Public Property FlashAttention As Boolean = False
    
    ' Parallel Processing
    Public Property NParallel As Integer = 1
    
    ' Model Configuration
    Public Property HfRepo As String = ""
    Public Property HfRepoDraft As String = ""
    Public Property HfFile As String = ""
    Public Property HfToken As String = ""
    Public Property ModelUrl As String = ""
    Public Property HfRepoV As String = ""
    Public Property HfFileV As String = ""
    Public Property OverrideKV As String = ""
    Public Property NoOpOffload As Boolean = False
    Public Property Keep As Integer = 0
    
    ' LoRA Configuration
    Public Property Lora As New List(Of String)
    Public Property LoraScaled As New List(Of LoraScaledConfig)
    Public Property LoraInitWithoutApply As Boolean = False
    
    ' Control Vectors
    Public Property ControlVector As New List(Of String)
    Public Property ControlVectorScaled As New List(Of ControlVectorScaledConfig)
    Public Property ControlVectorLayerRange As String = ""
    
    ' Mixture of Experts
    Public Property CpuMoe As Boolean = False
    Public Property NCpuMoe As Integer = 0
    Public Property CpuMoeDraft As Boolean = False
    Public Property NCpuMoeDraft As Integer = 0
    
    ' Logging Configuration
    Public Property LogDisable As Boolean = False
    Public Property LogFile As String = ""
    Public Property LogColors As Boolean = False
    Public Property Verbose As Boolean = False
    Public Property Offline As Boolean = False
    Public Property Verbosity As Integer = 0
    Public Property LogPrefix As Boolean = False
    Public Property LogTimestamps As Boolean = False
    Public Property NoPerf As Boolean = False
    Public Property Escape As Boolean = True
    Public Property VerbosePrompt As Boolean = False
    
    ' Network Configuration
    Public Property Host As String = "127.0.0.1"
    Public Property Port As Integer = 8080
    Public Property Path As String = ""
    Public Property ApiPrefix As String = ""
    Public Property Timeout As Integer = 600
    Public Property ThreadsHttp As Integer = -1
    
    ' Server Features
    Public Property NoWebui As Boolean = False
    Public Property Embeddings As Boolean = False
    Public Property Reranking As Boolean = False
    Public Property ApiKey As String = ""
    Public Property ApiKeyFile As String = ""
    Public Property SslKeyFile As String = ""
    Public Property SslCertFile As String = ""
    Public Property ContBatching As Boolean = True
    Public Property NoContBatching As Boolean = False
    Public Property Props As Boolean = False
    Public Property Metrics As Boolean = False
    Public Property Slots As Boolean = True
    Public Property NoSlots As Boolean = False
    Public Property SlotSavePath As String = ""
    Public Property Jinja As Boolean = False
    Public Property ReasoningFormat As String = "auto"
    Public Property ReasoningBudget As Integer = -1
    Public Property NoContextShift As Boolean = False
    Public Property ContextShift As Boolean = False
    
    ' Chat Configuration
    Public Property ChatTemplate As String = ""
    Public Property ChatTemplateFile As String = ""
    Public Property ChatTemplateKwargs As String = ""
    Public Property NoPrefillAssistant As Boolean = False
    Public Property ModelAlias As String = ""
    Public Property ReversePrompt As String = ""
    Public Property Special As Boolean = False
    Public Property NoWarmup As Boolean = False
    Public Property SpmInfill As Boolean = False
    
    ' Multimodal Configuration
    Public Property Mmproj As String = ""
    Public Property MmprojUrl As String = ""
    Public Property NoMmproj As Boolean = False
    Public Property NoMmprojOffload As Boolean = False
    
    ' Embedding Configuration
    Public Property Pooling As String = ""
    Public Property EmbdBgeSmallEnDefault As Boolean = False
    Public Property EmbdE5SmallEnDefault As Boolean = False
    Public Property EmbdGteSmallDefault As Boolean = False
    
    ' Draft Model Configuration
    Public Property ModelDraft As String = ""
    Public Property CtxSizeDraft As Integer = 0
    Public Property ThreadsDraft As Integer = -1
    Public Property ThreadsBatchDraft As Integer = -1
    Public Property DeviceDraft As String = ""
    Public Property NGpuLayersDraft As Integer = 0
    Public Property DraftMax As Integer = 16
    Public Property DraftMin As Integer = 0
    Public Property DraftPMin As Double = 0.8
    Public Property CacheTypeKDraft As String = "f16"
    Public Property CacheTypeVDraft As String = "f16"
    Public Property OverrideTensorDraft As String = ""
    Public Property SpecReplace As String = ""
    
    ' Vocoder Configuration
    Public Property ModelVocoder As String = ""
    Public Property TtsUseGuideTokens As Boolean = False
    
    ' Checkpoint Configuration
    Public Property SWACheckpoints As Integer = 3
    
    ' Functionality Configuration
    Public Property CacheReuse As Integer = 0
    Public Property SlotPromptSimilarity As Double = 0.5
    
    ' List Devices
    Public Property ListDevices As Boolean = False
    
    ' Completion and Help
    Public Property Help As Boolean = False
    Public Property Usage As Boolean = False
    Public Property Version As Boolean = False
    Public Property CompletionBash As Boolean = False
    
    ' Preset Models
    Public Property FimQwen15BDefault As Boolean = False
    Public Property FimQwen3BDefault As Boolean = False
    Public Property FimQwen7BDefault As Boolean = False
    Public Property FimQwen7BSpec As Boolean = False
    Public Property FimQwen14BSpec As Boolean = False
    Public Property FimQwen30BDefault As Boolean = False
    
    Public Sub New()
        ' Initialize collections
        Lora = New List(Of String)()
        LoraScaled = New List(Of LoraScaledConfig)()
        ControlVector = New List(Of String)()
        ControlVectorScaled = New List(Of ControlVectorScaledConfig)()
    End Sub
End Class

Public Class LoraScaledConfig
    Public Property Path As String
    Public Property Scale As Double
    
    Public Sub New()
    End Sub
    
    Public Sub New(path As String, scale As Double)
        Me.Path = path
        Me.Scale = scale
    End Sub
End Class

Public Class ControlVectorScaledConfig
    Public Property Path As String
    Public Property Scale As Double
    
    Public Sub New()
    End Sub
    
    Public Sub New(path As String, scale As Double)
        Me.Path = path
        Me.Scale = scale
    End Sub
End Class