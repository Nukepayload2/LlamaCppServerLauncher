Imports System.Text.Json.Serialization
Imports LlamaCppServerLauncher.Helpers

Public Class AppSettings
    Inherits ObservableBase
    ' Basic Parameters
    Private _serverPath As String = ""
    Private _modelPath As String = ""
    Private _threads As Integer = 4
    Private _ctxSize As Integer = 4096
    Private _nPredict As Integer = -1
    
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
    
    Public Property NPredict As Integer
        Get
            Return _nPredict
        End Get
        Set(value As Integer)
            SetProperty(_nPredict, value)
        End Set
    End Property
    
    ' Batch and Thread Parameters
    Private _batchSize As Integer = 2048
    Private _ubatchSize As Integer = 512
    Private _threadsBatch As Integer = -1
    
    Public Property BatchSize As Integer
        Get
            Return _batchSize
        End Get
        Set(value As Integer)
            SetProperty(_batchSize, value)
        End Set
    End Property
    
    Public Property UbatchSize As Integer
        Get
            Return _ubatchSize
        End Get
        Set(value As Integer)
            SetProperty(_ubatchSize, value)
        End Set
    End Property
    
    Public Property ThreadsBatch As Integer
        Get
            Return _threadsBatch
        End Get
        Set(value As Integer)
            SetProperty(_threadsBatch, value)
        End Set
    End Property
    
    ' Hardware Configuration
    Private _nGpuLayers As Integer = 0
    Private _mainGpu As Integer = 0
    Private _tensorSplit As String = ""
    Private _splitMode As String = "none"
    Private _mlock As Boolean = False
    Private _noMmap As Boolean = False
    Private _numa As String = ""
    Private _device As String = ""
    Private _cpuMask As String = ""
    Private _cpuRange As String = ""
    Private _cpuStrict As Boolean = False
    Private _prio As Integer = 0
    Private _poll As Integer = 50
    Private _cpuMaskBatch As String = ""
    Private _cpuRangeBatch As String = ""
    Private _cpuStrictBatch As Boolean = False
    Private _prioBatch As Integer = 0
    Private _pollBatch As Integer = 50
    
    Public Property NGpuLayers As Integer
        Get
            Return _nGpuLayers
        End Get
        Set(value As Integer)
            SetProperty(_nGpuLayers, value)
        End Set
    End Property
    
    Public Property MainGpu As Integer
        Get
            Return _mainGpu
        End Get
        Set(value As Integer)
            SetProperty(_mainGpu, value)
        End Set
    End Property
    
    Public Property TensorSplit As String
        Get
            Return _tensorSplit
        End Get
        Set(value As String)
            SetProperty(_tensorSplit, value)
        End Set
    End Property
    
    Public Property SplitMode As String
        Get
            Return _splitMode
        End Get
        Set(value As String)
            SetProperty(_splitMode, value)
        End Set
    End Property
    
    Public Property Mlock As Boolean
        Get
            Return _mlock
        End Get
        Set(value As Boolean)
            SetProperty(_mlock, value)
        End Set
    End Property
    
    Public Property NoMmap As Boolean
        Get
            Return _noMmap
        End Get
        Set(value As Boolean)
            SetProperty(_noMmap, value)
        End Set
    End Property
    
    Public Property Numa As String
        Get
            Return _numa
        End Get
        Set(value As String)
            SetProperty(_numa, value)
        End Set
    End Property
    
    Public Property Device As String
        Get
            Return _device
        End Get
        Set(value As String)
            SetProperty(_device, value)
        End Set
    End Property
    
    Public Property CpuMask As String
        Get
            Return _cpuMask
        End Get
        Set(value As String)
            SetProperty(_cpuMask, value)
        End Set
    End Property
    
    Public Property CpuRange As String
        Get
            Return _cpuRange
        End Get
        Set(value As String)
            SetProperty(_cpuRange, value)
        End Set
    End Property
    
    Public Property CpuStrict As Boolean
        Get
            Return _cpuStrict
        End Get
        Set(value As Boolean)
            SetProperty(_cpuStrict, value)
        End Set
    End Property
    
    Public Property Prio As Integer
        Get
            Return _prio
        End Get
        Set(value As Integer)
            SetProperty(_prio, value)
        End Set
    End Property
    
    Public Property Poll As Integer
        Get
            Return _poll
        End Get
        Set(value As Integer)
            SetProperty(_poll, value)
        End Set
    End Property
    
    Public Property CpuMaskBatch As String
        Get
            Return _cpuMaskBatch
        End Get
        Set(value As String)
            SetProperty(_cpuMaskBatch, value)
        End Set
    End Property
    
    Public Property CpuRangeBatch As String
        Get
            Return _cpuRangeBatch
        End Get
        Set(value As String)
            SetProperty(_cpuRangeBatch, value)
        End Set
    End Property
    
    Public Property CpuStrictBatch As Boolean
        Get
            Return _cpuStrictBatch
        End Get
        Set(value As Boolean)
            SetProperty(_cpuStrictBatch, value)
        End Set
    End Property
    
    Public Property PrioBatch As Integer
        Get
            Return _prioBatch
        End Get
        Set(value As Integer)
            SetProperty(_prioBatch, value)
        End Set
    End Property
    
    Public Property PollBatch As Integer
        Get
            Return _pollBatch
        End Get
        Set(value As Integer)
            SetProperty(_pollBatch, value)
        End Set
    End Property
    
    ' KV Cache Configuration
    Private _noKVOffload As Boolean = False
    Private _noRepack As Boolean = False
    Private _cacheTypeK As String = "f16"
    Private _cacheTypeV As String = "f16"
    Private _defragThold As Double = 0.5
    Private _kvUnified As Boolean = False
    Private _swaFull As Boolean = False
    
    Public Property NoKVOffload As Boolean
        Get
            Return _noKVOffload
        End Get
        Set(value As Boolean)
            SetProperty(_noKVOffload, value)
        End Set
    End Property
    
    Public Property NoRepack As Boolean
        Get
            Return _noRepack
        End Get
        Set(value As Boolean)
            SetProperty(_noRepack, value)
        End Set
    End Property
    
    Public Property CacheTypeK As String
        Get
            Return _cacheTypeK
        End Get
        Set(value As String)
            SetProperty(_cacheTypeK, value)
        End Set
    End Property
    
    Public Property CacheTypeV As String
        Get
            Return _cacheTypeV
        End Get
        Set(value As String)
            SetProperty(_cacheTypeV, value)
        End Set
    End Property
    
    Public Property DefragThold As Double
        Get
            Return _defragThold
        End Get
        Set(value As Double)
            SetProperty(_defragThold, value)
        End Set
    End Property
    
    Public Property KVUnified As Boolean
        Get
            Return _kvUnified
        End Get
        Set(value As Boolean)
            SetProperty(_kvUnified, value)
        End Set
    End Property
    
    Public Property SWAFull As Boolean
        Get
            Return _swaFull
        End Get
        Set(value As Boolean)
            SetProperty(_swaFull, value)
        End Set
    End Property
    
    ' Sampling Parameters
    Private _temperature As Double = 0.8
    Private _topP As Double = 0.9
    Private _topK As Integer = 40
    Private _minP As Double = 0.1
    
    Public Property Temperature As Double
        Get
            Return _temperature
        End Get
        Set(value As Double)
            SetProperty(_temperature, value)
        End Set
    End Property
    
    Public Property TopP As Double
        Get
            Return _topP
        End Get
        Set(value As Double)
            SetProperty(_topP, value)
        End Set
    End Property
    
    Public Property TopK As Integer
        Get
            Return _topK
        End Get
        Set(value As Integer)
            SetProperty(_topK, value)
        End Set
    End Property
    
    Public Property MinP As Double
        Get
            Return _minP
        End Get
        Set(value As Double)
            SetProperty(_minP, value)
        End Set
    End Property
    
    Private _topNSigma As Double = -1.0
    Private _xtcProbability As Double = 0.0
    Private _xtcThreshold As Double = 0.1
    Private _typical As Double = 1.0
    Private _repeatLastN As Integer = 64
    Private _repeatPenalty As Double = 1.0
    Private _presencePenalty As Double = 0.0
    Private _frequencyPenalty As Double = 0.0
    
    Public Property TopNSigma As Double
        Get
            Return _topNSigma
        End Get
        Set(value As Double)
            SetProperty(_topNSigma, value)
        End Set
    End Property
    
    Public Property XtcProbability As Double
        Get
            Return _xtcProbability
        End Get
        Set(value As Double)
            SetProperty(_xtcProbability, value)
        End Set
    End Property
    
    Public Property XtcThreshold As Double
        Get
            Return _xtcThreshold
        End Get
        Set(value As Double)
            SetProperty(_xtcThreshold, value)
        End Set
    End Property
    
    Public Property Typical As Double
        Get
            Return _typical
        End Get
        Set(value As Double)
            SetProperty(_typical, value)
        End Set
    End Property
    
    Public Property RepeatLastN As Integer
        Get
            Return _repeatLastN
        End Get
        Set(value As Integer)
            SetProperty(_repeatLastN, value)
        End Set
    End Property
    
    Public Property RepeatPenalty As Double
        Get
            Return _repeatPenalty
        End Get
        Set(value As Double)
            SetProperty(_repeatPenalty, value)
        End Set
    End Property
    
    Public Property PresencePenalty As Double
        Get
            Return _presencePenalty
        End Get
        Set(value As Double)
            SetProperty(_presencePenalty, value)
        End Set
    End Property
    
    Public Property FrequencyPenalty As Double
        Get
            Return _frequencyPenalty
        End Get
        Set(value As Double)
            SetProperty(_frequencyPenalty, value)
        End Set
    End Property
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
    Private _flashAttention As Boolean = False
    
    Public Property FlashAttention As Boolean
        Get
            Return _flashAttention
        End Get
        Set(value As Boolean)
            SetProperty(_flashAttention, value)
        End Set
    End Property
    
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
    Private _logDisable As Boolean = False
    Private _logFile As String = ""
    Private _logColors As Boolean = False
    Private _verbose As Boolean = False
    Private _offline As Boolean = False
    Private _verbosity As Integer = 0
    Private _logPrefix As Boolean = False
    Private _logTimestamps As Boolean = False
    Private _noPerf As Boolean = False
    Private _escape As Boolean = True
    Private _verbosePrompt As Boolean = False
    
    Public Property LogDisable As Boolean
        Get
            Return _logDisable
        End Get
        Set(value As Boolean)
            SetProperty(_logDisable, value)
        End Set
    End Property
    
    Public Property LogFile As String
        Get
            Return _logFile
        End Get
        Set(value As String)
            SetProperty(_logFile, value)
        End Set
    End Property
    
    Public Property LogColors As Boolean
        Get
            Return _logColors
        End Get
        Set(value As Boolean)
            SetProperty(_logColors, value)
        End Set
    End Property
    
    Public Property Verbose As Boolean
        Get
            Return _verbose
        End Get
        Set(value As Boolean)
            SetProperty(_verbose, value)
        End Set
    End Property
    
    Public Property Offline As Boolean
        Get
            Return _offline
        End Get
        Set(value As Boolean)
            SetProperty(_offline, value)
        End Set
    End Property
    
    Public Property Verbosity As Integer
        Get
            Return _verbosity
        End Get
        Set(value As Integer)
            SetProperty(_verbosity, value)
        End Set
    End Property
    
    Public Property LogPrefix As Boolean
        Get
            Return _logPrefix
        End Get
        Set(value As Boolean)
            SetProperty(_logPrefix, value)
        End Set
    End Property
    
    Public Property LogTimestamps As Boolean
        Get
            Return _logTimestamps
        End Get
        Set(value As Boolean)
            SetProperty(_logTimestamps, value)
        End Set
    End Property
    
    Public Property NoPerf As Boolean
        Get
            Return _noPerf
        End Get
        Set(value As Boolean)
            SetProperty(_noPerf, value)
        End Set
    End Property
    
    Public Property Escape As Boolean
        Get
            Return _escape
        End Get
        Set(value As Boolean)
            SetProperty(_escape, value)
        End Set
    End Property
    
    Public Property VerbosePrompt As Boolean
        Get
            Return _verbosePrompt
        End Get
        Set(value As Boolean)
            SetProperty(_verbosePrompt, value)
        End Set
    End Property
    
    ' Network Configuration
    Private _host As String = "127.0.0.1"
    Private _port As Integer = 8080
    Private _path As String = ""
    Private _apiPrefix As String = ""
    Private _timeout As Integer = 600
    
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
    
    Public Property Path As String
        Get
            Return _path
        End Get
        Set(value As String)
            SetProperty(_path, value)
        End Set
    End Property
    
    Public Property ApiPrefix As String
        Get
            Return _apiPrefix
        End Get
        Set(value As String)
            SetProperty(_apiPrefix, value)
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
    
    Private _threadsHttp As Integer = -1
    
    Public Property ThreadsHttp As Integer
        Get
            Return _threadsHttp
        End Get
        Set(value As Integer)
            SetProperty(_threadsHttp, value)
        End Set
    End Property
    
    ' Server Features
    Private _noWebui As Boolean = False
    Private _embeddings As Boolean = False
    Private _reranking As Boolean = False
    Private _apiKey As String = ""
    Private _apiKeyFile As String = ""
    Private _sslKeyFile As String = ""
    Private _sslCertFile As String = ""
    Private _contBatching As Boolean = True
    Private _noContBatching As Boolean = False
    Private _props As Boolean = False
    Private _metrics As Boolean = False
    Private _slots As Boolean = True
    Private _noSlots As Boolean = False
    
    Public Property NoWebui As Boolean
        Get
            Return _noWebui
        End Get
        Set(value As Boolean)
            SetProperty(_noWebui, value)
        End Set
    End Property
    
    Public Property Embeddings As Boolean
        Get
            Return _embeddings
        End Get
        Set(value As Boolean)
            SetProperty(_embeddings, value)
        End Set
    End Property
    
    Public Property Reranking As Boolean
        Get
            Return _reranking
        End Get
        Set(value As Boolean)
            SetProperty(_reranking, value)
        End Set
    End Property
    
    Public Property ApiKey As String
        Get
            Return _apiKey
        End Get
        Set(value As String)
            SetProperty(_apiKey, value)
        End Set
    End Property
    
    Public Property ApiKeyFile As String
        Get
            Return _apiKeyFile
        End Get
        Set(value As String)
            SetProperty(_apiKeyFile, value)
        End Set
    End Property
    
    Public Property SslKeyFile As String
        Get
            Return _sslKeyFile
        End Get
        Set(value As String)
            SetProperty(_sslKeyFile, value)
        End Set
    End Property
    
    Public Property SslCertFile As String
        Get
            Return _sslCertFile
        End Get
        Set(value As String)
            SetProperty(_sslCertFile, value)
        End Set
    End Property
    
    Public Property ContBatching As Boolean
        Get
            Return _contBatching
        End Get
        Set(value As Boolean)
            SetProperty(_contBatching, value)
        End Set
    End Property
    
    Public Property NoContBatching As Boolean
        Get
            Return _noContBatching
        End Get
        Set(value As Boolean)
            SetProperty(_noContBatching, value)
        End Set
    End Property
    
    Public Property Props As Boolean
        Get
            Return _props
        End Get
        Set(value As Boolean)
            SetProperty(_props, value)
        End Set
    End Property
    
    Public Property Metrics As Boolean
        Get
            Return _metrics
        End Get
        Set(value As Boolean)
            SetProperty(_metrics, value)
        End Set
    End Property
    
    Public Property Slots As Boolean
        Get
            Return _slots
        End Get
        Set(value As Boolean)
            SetProperty(_slots, value)
        End Set
    End Property
    
    Public Property NoSlots As Boolean
        Get
            Return _noSlots
        End Get
        Set(value As Boolean)
            SetProperty(_noSlots, value)
        End Set
    End Property
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