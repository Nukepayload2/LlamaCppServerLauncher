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

    Private _dryMultiplier As Double = 0.0
    Private _dryBase As Double = 1.75
    Private _dryAllowedLength As Integer = 2
    Private _dryPenaltyLastN As Integer = -1
    Private _drySequenceBreaker As String = ""
    Private _dynatempRange As Double = 0.0
    Private _dynatempExp As Double = 1.0
    Private _mirostat As Integer = 0
    Private _mirostatLr As Double = 0.1
    Private _mirostatEnt As Double = 5.0
    Private _samplers As String = "penalties;dry;top_n_sigma;top_k;typ_p;top_p;min_p;xtc;temperature"
    Private _samplingSeq As String = "edskypmxt"
    Private _ignoreEOS As Boolean = False

    Public Property DryMultiplier As Double
        Get
            Return _dryMultiplier
        End Get
        Set(value As Double)
            SetProperty(_dryMultiplier, value)
        End Set
    End Property

    Public Property DryBase As Double
        Get
            Return _dryBase
        End Get
        Set(value As Double)
            SetProperty(_dryBase, value)
        End Set
    End Property

    Public Property DryAllowedLength As Integer
        Get
            Return _dryAllowedLength
        End Get
        Set(value As Integer)
            SetProperty(_dryAllowedLength, value)
        End Set
    End Property

    Public Property DryPenaltyLastN As Integer
        Get
            Return _dryPenaltyLastN
        End Get
        Set(value As Integer)
            SetProperty(_dryPenaltyLastN, value)
        End Set
    End Property

    Public Property DrySequenceBreaker As String
        Get
            Return _drySequenceBreaker
        End Get
        Set(value As String)
            SetProperty(_drySequenceBreaker, value)
        End Set
    End Property

    Public Property DynatempRange As Double
        Get
            Return _dynatempRange
        End Get
        Set(value As Double)
            SetProperty(_dynatempRange, value)
        End Set
    End Property

    Public Property DynatempExp As Double
        Get
            Return _dynatempExp
        End Get
        Set(value As Double)
            SetProperty(_dynatempExp, value)
        End Set
    End Property

    Public Property Mirostat As Integer
        Get
            Return _mirostat
        End Get
        Set(value As Integer)
            SetProperty(_mirostat, value)
        End Set
    End Property

    Public Property MirostatLr As Double
        Get
            Return _mirostatLr
        End Get
        Set(value As Double)
            SetProperty(_mirostatLr, value)
        End Set
    End Property

    Public Property MirostatEnt As Double
        Get
            Return _mirostatEnt
        End Get
        Set(value As Double)
            SetProperty(_mirostatEnt, value)
        End Set
    End Property

    Public Property Samplers As String
        Get
            Return _samplers
        End Get
        Set(value As String)
            SetProperty(_samplers, value)
        End Set
    End Property

    Public Property SamplingSeq As String
        Get
            Return _samplingSeq
        End Get
        Set(value As String)
            SetProperty(_samplingSeq, value)
        End Set
    End Property

    Public Property IgnoreEOS As Boolean
        Get
            Return _ignoreEOS
        End Get
        Set(value As Boolean)
            SetProperty(_ignoreEOS, value)
        End Set
    End Property

    ' Rope Configuration
    Private _ropeScaling As String = "none"
    Private _ropeScale As Double = 1.0
    Private _ropeFreqBase As Double = 0.0
    Private _ropeFreqScale As Double = 1.0
    Private _yarnOrigCtx As Integer = 0
    Private _yarnExtFactor As Double = -1.0
    Private _yarnAttnFactor As Double = 1.0
    Private _yarnBetaSlow As Double = 1.0
    Private _yarnBetaFast As Double = 32.0

    Public Property RopeScaling As String
        Get
            Return _ropeScaling
        End Get
        Set(value As String)
            SetProperty(_ropeScaling, value)
        End Set
    End Property

    Public Property RopeScale As Double
        Get
            Return _ropeScale
        End Get
        Set(value As Double)
            SetProperty(_ropeScale, value)
        End Set
    End Property

    Public Property RopeFreqBase As Double
        Get
            Return _ropeFreqBase
        End Get
        Set(value As Double)
            SetProperty(_ropeFreqBase, value)
        End Set
    End Property

    Public Property RopeFreqScale As Double
        Get
            Return _ropeFreqScale
        End Get
        Set(value As Double)
            SetProperty(_ropeFreqScale, value)
        End Set
    End Property

    Public Property YarnOrigCtx As Integer
        Get
            Return _yarnOrigCtx
        End Get
        Set(value As Integer)
            SetProperty(_yarnOrigCtx, value)
        End Set
    End Property

    Public Property YarnExtFactor As Double
        Get
            Return _yarnExtFactor
        End Get
        Set(value As Double)
            SetProperty(_yarnExtFactor, value)
        End Set
    End Property

    Public Property YarnAttnFactor As Double
        Get
            Return _yarnAttnFactor
        End Get
        Set(value As Double)
            SetProperty(_yarnAttnFactor, value)
        End Set
    End Property

    Public Property YarnBetaSlow As Double
        Get
            Return _yarnBetaSlow
        End Get
        Set(value As Double)
            SetProperty(_yarnBetaSlow, value)
        End Set
    End Property

    Public Property YarnBetaFast As Double
        Get
            Return _yarnBetaFast
        End Get
        Set(value As Double)
            SetProperty(_yarnBetaFast, value)
        End Set
    End Property

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
    Private _nParallel As Integer = 1

    Public Property NParallel As Integer
        Get
            Return _nParallel
        End Get
        Set(value As Integer)
            SetProperty(_nParallel, value)
        End Set
    End Property

    ' Model Configuration
    Private _hfRepo As String = ""
    Private _hfRepoDraft As String = ""
    Private _hfFile As String = ""
    Private _hfToken As String = ""
    Private _modelUrl As String = ""
    Private _hfRepoV As String = ""
    Private _hfFileV As String = ""
    Private _overrideKV As String = ""
    Private _noOpOffload As Boolean = False
    Private _keep As Integer = 0

    Public Property HfRepo As String
        Get
            Return _hfRepo
        End Get
        Set(value As String)
            SetProperty(_hfRepo, value)
        End Set
    End Property

    Public Property HfRepoDraft As String
        Get
            Return _hfRepoDraft
        End Get
        Set(value As String)
            SetProperty(_hfRepoDraft, value)
        End Set
    End Property

    Public Property HfFile As String
        Get
            Return _hfFile
        End Get
        Set(value As String)
            SetProperty(_hfFile, value)
        End Set
    End Property

    Public Property HfToken As String
        Get
            Return _hfToken
        End Get
        Set(value As String)
            SetProperty(_hfToken, value)
        End Set
    End Property

    Public Property ModelUrl As String
        Get
            Return _modelUrl
        End Get
        Set(value As String)
            SetProperty(_modelUrl, value)
        End Set
    End Property

    Public Property HfRepoV As String
        Get
            Return _hfRepoV
        End Get
        Set(value As String)
            SetProperty(_hfRepoV, value)
        End Set
    End Property

    Public Property HfFileV As String
        Get
            Return _hfFileV
        End Get
        Set(value As String)
            SetProperty(_hfFileV, value)
        End Set
    End Property

    Public Property OverrideKV As String
        Get
            Return _overrideKV
        End Get
        Set(value As String)
            SetProperty(_overrideKV, value)
        End Set
    End Property

    Public Property NoOpOffload As Boolean
        Get
            Return _noOpOffload
        End Get
        Set(value As Boolean)
            SetProperty(_noOpOffload, value)
        End Set
    End Property

    Public Property Keep As Integer
        Get
            Return _keep
        End Get
        Set(value As Integer)
            SetProperty(_keep, value)
        End Set
    End Property

    ' LoRA Configuration
    Private _lora As New List(Of String)
    Private _loraScaled As New List(Of LoraScaledConfig)
    Private _loraInitWithoutApply As Boolean = False

    Public Property Lora As List(Of String)
        Get
            Return _lora
        End Get
        Set(value As List(Of String))
            SetProperty(_lora, value)
        End Set
    End Property

    Public Property LoraScaled As List(Of LoraScaledConfig)
        Get
            Return _loraScaled
        End Get
        Set(value As List(Of LoraScaledConfig))
            SetProperty(_loraScaled, value)
        End Set
    End Property

    Public Property LoraInitWithoutApply As Boolean
        Get
            Return _loraInitWithoutApply
        End Get
        Set(value As Boolean)
            SetProperty(_loraInitWithoutApply, value)
        End Set
    End Property

    ' Control Vectors
    Private _controlVector As New List(Of String)
    Private _controlVectorScaled As New List(Of ControlVectorScaledConfig)
    Private _controlVectorLayerRange As String = ""

    Public Property ControlVector As List(Of String)
        Get
            Return _controlVector
        End Get
        Set(value As List(Of String))
            SetProperty(_controlVector, value)
        End Set
    End Property

    Public Property ControlVectorScaled As List(Of ControlVectorScaledConfig)
        Get
            Return _controlVectorScaled
        End Get
        Set(value As List(Of ControlVectorScaledConfig))
            SetProperty(_controlVectorScaled, value)
        End Set
    End Property

    Public Property ControlVectorLayerRange As String
        Get
            Return _controlVectorLayerRange
        End Get
        Set(value As String)
            SetProperty(_controlVectorLayerRange, value)
        End Set
    End Property

    ' Mixture of Experts
    Private _cpuMoe As Boolean = False
    Private _nCpuMoe As Integer = 0
    Private _cpuMoeDraft As Boolean = False
    Private _nCpuMoeDraft As Integer = 0

    Public Property CpuMoe As Boolean
        Get
            Return _cpuMoe
        End Get
        Set(value As Boolean)
            SetProperty(_cpuMoe, value)
        End Set
    End Property

    Public Property NCpuMoe As Integer
        Get
            Return _nCpuMoe
        End Get
        Set(value As Integer)
            SetProperty(_nCpuMoe, value)
        End Set
    End Property

    Public Property CpuMoeDraft As Boolean
        Get
            Return _cpuMoeDraft
        End Get
        Set(value As Boolean)
            SetProperty(_cpuMoeDraft, value)
        End Set
    End Property

    Public Property NCpuMoeDraft As Integer
        Get
            Return _nCpuMoeDraft
        End Get
        Set(value As Integer)
            SetProperty(_nCpuMoeDraft, value)
        End Set
    End Property

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

    Private _slotSavePath As String = ""
    Private _jinja As Boolean = False
    Private _reasoningFormat As String = "auto"
    Private _reasoningBudget As Integer = -1
    Private _noContextShift As Boolean = False
    Private _contextShift As Boolean = False

    Public Property SlotSavePath As String
        Get
            Return _slotSavePath
        End Get
        Set(value As String)
            SetProperty(_slotSavePath, value)
        End Set
    End Property

    Public Property Jinja As Boolean
        Get
            Return _jinja
        End Get
        Set(value As Boolean)
            SetProperty(_jinja, value)
        End Set
    End Property

    Public Property ReasoningFormat As String
        Get
            Return _reasoningFormat
        End Get
        Set(value As String)
            SetProperty(_reasoningFormat, value)
        End Set
    End Property

    Public Property ReasoningBudget As Integer
        Get
            Return _reasoningBudget
        End Get
        Set(value As Integer)
            SetProperty(_reasoningBudget, value)
        End Set
    End Property

    Public Property NoContextShift As Boolean
        Get
            Return _noContextShift
        End Get
        Set(value As Boolean)
            SetProperty(_noContextShift, value)
        End Set
    End Property

    Public Property ContextShift As Boolean
        Get
            Return _contextShift
        End Get
        Set(value As Boolean)
            SetProperty(_contextShift, value)
        End Set
    End Property

    ' Chat Configuration
    Private _chatTemplate As String = ""
    Private _chatTemplateFile As String = ""
    Private _chatTemplateKwargs As String = ""
    Private _noPrefillAssistant As Boolean = False
    Private _modelAlias As String = ""
    Private _reversePrompt As String = ""
    Private _special As Boolean = False
    Private _noWarmup As Boolean = False
    Private _spmInfill As Boolean = False

    Public Property ChatTemplate As String
        Get
            Return _chatTemplate
        End Get
        Set(value As String)
            SetProperty(_chatTemplate, value)
        End Set
    End Property

    Public Property ChatTemplateFile As String
        Get
            Return _chatTemplateFile
        End Get
        Set(value As String)
            SetProperty(_chatTemplateFile, value)
        End Set
    End Property

    Public Property ChatTemplateKwargs As String
        Get
            Return _chatTemplateKwargs
        End Get
        Set(value As String)
            SetProperty(_chatTemplateKwargs, value)
        End Set
    End Property

    Public Property NoPrefillAssistant As Boolean
        Get
            Return _noPrefillAssistant
        End Get
        Set(value As Boolean)
            SetProperty(_noPrefillAssistant, value)
        End Set
    End Property

    Public Property ModelAlias As String
        Get
            Return _modelAlias
        End Get
        Set(value As String)
            SetProperty(_modelAlias, value)
        End Set
    End Property

    Public Property ReversePrompt As String
        Get
            Return _reversePrompt
        End Get
        Set(value As String)
            SetProperty(_reversePrompt, value)
        End Set
    End Property

    Public Property Special As Boolean
        Get
            Return _special
        End Get
        Set(value As Boolean)
            SetProperty(_special, value)
        End Set
    End Property

    Public Property NoWarmup As Boolean
        Get
            Return _noWarmup
        End Get
        Set(value As Boolean)
            SetProperty(_noWarmup, value)
        End Set
    End Property

    Public Property SpmInfill As Boolean
        Get
            Return _spmInfill
        End Get
        Set(value As Boolean)
            SetProperty(_spmInfill, value)
        End Set
    End Property

    ' Multimodal Configuration
    Private _mmproj As String = ""
    Private _mmprojUrl As String = ""
    Private _noMmproj As Boolean = False
    Private _noMmprojOffload As Boolean = False

    Public Property Mmproj As String
        Get
            Return _mmproj
        End Get
        Set(value As String)
            SetProperty(_mmproj, value)
        End Set
    End Property

    Public Property MmprojUrl As String
        Get
            Return _mmprojUrl
        End Get
        Set(value As String)
            SetProperty(_mmprojUrl, value)
        End Set
    End Property

    Public Property NoMmproj As Boolean
        Get
            Return _noMmproj
        End Get
        Set(value As Boolean)
            SetProperty(_noMmproj, value)
        End Set
    End Property

    Public Property NoMmprojOffload As Boolean
        Get
            Return _noMmprojOffload
        End Get
        Set(value As Boolean)
            SetProperty(_noMmprojOffload, value)
        End Set
    End Property

    ' Embedding Configuration
    Private _pooling As String = ""
    Private _embdBgeSmallEnDefault As Boolean = False
    Private _embdE5SmallEnDefault As Boolean = False
    Private _embdGteSmallDefault As Boolean = False

    Public Property Pooling As String
        Get
            Return _pooling
        End Get
        Set(value As String)
            SetProperty(_pooling, value)
        End Set
    End Property

    Public Property EmbdBgeSmallEnDefault As Boolean
        Get
            Return _embdBgeSmallEnDefault
        End Get
        Set(value As Boolean)
            SetProperty(_embdBgeSmallEnDefault, value)
        End Set
    End Property

    Public Property EmbdE5SmallEnDefault As Boolean
        Get
            Return _embdE5SmallEnDefault
        End Get
        Set(value As Boolean)
            SetProperty(_embdE5SmallEnDefault, value)
        End Set
    End Property

    Public Property EmbdGteSmallDefault As Boolean
        Get
            Return _embdGteSmallDefault
        End Get
        Set(value As Boolean)
            SetProperty(_embdGteSmallDefault, value)
        End Set
    End Property

    ' Draft Model Configuration
    Private _modelDraft As String = ""
    Private _ctxSizeDraft As Integer = 0
    Private _threadsDraft As Integer = -1
    Private _threadsBatchDraft As Integer = -1
    Private _deviceDraft As String = ""
    Private _nGpuLayersDraft As Integer = 0
    Private _draftMax As Integer = 16
    Private _draftMin As Integer = 0
    Private _draftPMin As Double = 0.8
    Private _cacheTypeKDraft As String = "f16"
    Private _cacheTypeVDraft As String = "f16"
    Private _overrideTensorDraft As String = ""
    Private _specReplace As String = ""

    Public Property ModelDraft As String
        Get
            Return _modelDraft
        End Get
        Set(value As String)
            SetProperty(_modelDraft, value)
        End Set
    End Property

    Public Property CtxSizeDraft As Integer
        Get
            Return _ctxSizeDraft
        End Get
        Set(value As Integer)
            SetProperty(_ctxSizeDraft, value)
        End Set
    End Property

    Public Property ThreadsDraft As Integer
        Get
            Return _threadsDraft
        End Get
        Set(value As Integer)
            SetProperty(_threadsDraft, value)
        End Set
    End Property

    Public Property ThreadsBatchDraft As Integer
        Get
            Return _threadsBatchDraft
        End Get
        Set(value As Integer)
            SetProperty(_threadsBatchDraft, value)
        End Set
    End Property

    Public Property DeviceDraft As String
        Get
            Return _deviceDraft
        End Get
        Set(value As String)
            SetProperty(_deviceDraft, value)
        End Set
    End Property

    Public Property NGpuLayersDraft As Integer
        Get
            Return _nGpuLayersDraft
        End Get
        Set(value As Integer)
            SetProperty(_nGpuLayersDraft, value)
        End Set
    End Property

    Public Property DraftMax As Integer
        Get
            Return _draftMax
        End Get
        Set(value As Integer)
            SetProperty(_draftMax, value)
        End Set
    End Property

    Public Property DraftMin As Integer
        Get
            Return _draftMin
        End Get
        Set(value As Integer)
            SetProperty(_draftMin, value)
        End Set
    End Property

    Public Property DraftPMin As Double
        Get
            Return _draftPMin
        End Get
        Set(value As Double)
            SetProperty(_draftPMin, value)
        End Set
    End Property

    Public Property CacheTypeKDraft As String
        Get
            Return _cacheTypeKDraft
        End Get
        Set(value As String)
            SetProperty(_cacheTypeKDraft, value)
        End Set
    End Property

    Public Property CacheTypeVDraft As String
        Get
            Return _cacheTypeVDraft
        End Get
        Set(value As String)
            SetProperty(_cacheTypeVDraft, value)
        End Set
    End Property

    Public Property OverrideTensorDraft As String
        Get
            Return _overrideTensorDraft
        End Get
        Set(value As String)
            SetProperty(_overrideTensorDraft, value)
        End Set
    End Property

    Public Property SpecReplace As String
        Get
            Return _specReplace
        End Get
        Set(value As String)
            SetProperty(_specReplace, value)
        End Set
    End Property

    ' Vocoder Configuration
    Private _modelVocoder As String = ""
    Private _ttsUseGuideTokens As Boolean = False

    Public Property ModelVocoder As String
        Get
            Return _modelVocoder
        End Get
        Set(value As String)
            SetProperty(_modelVocoder, value)
        End Set
    End Property

    Public Property TtsUseGuideTokens As Boolean
        Get
            Return _ttsUseGuideTokens
        End Get
        Set(value As Boolean)
            SetProperty(_ttsUseGuideTokens, value)
        End Set
    End Property

    ' Checkpoint Configuration
    Private _sWACheckpoints As Integer = 3

    Public Property SWACheckpoints As Integer
        Get
            Return _sWACheckpoints
        End Get
        Set(value As Integer)
            SetProperty(_sWACheckpoints, value)
        End Set
    End Property

    ' Functionality Configuration
    Private _cacheReuse As Integer = 0
    Private _slotPromptSimilarity As Double = 0.5

    Public Property CacheReuse As Integer
        Get
            Return _cacheReuse
        End Get
        Set(value As Integer)
            SetProperty(_cacheReuse, value)
        End Set
    End Property

    Public Property SlotPromptSimilarity As Double
        Get
            Return _slotPromptSimilarity
        End Get
        Set(value As Double)
            SetProperty(_slotPromptSimilarity, value)
        End Set
    End Property

    ' List Devices
    Private _listDevices As Boolean = False

    Public Property ListDevices As Boolean
        Get
            Return _listDevices
        End Get
        Set(value As Boolean)
            SetProperty(_listDevices, value)
        End Set
    End Property

    ' Completion and Help
    Private _help As Boolean = False
    Private _usage As Boolean = False
    Private _version As Boolean = False
    Private _completionBash As Boolean = False

    Public Property Help As Boolean
        Get
            Return _help
        End Get
        Set(value As Boolean)
            SetProperty(_help, value)
        End Set
    End Property

    Public Property Usage As Boolean
        Get
            Return _usage
        End Get
        Set(value As Boolean)
            SetProperty(_usage, value)
        End Set
    End Property

    Public Property Version As Boolean
        Get
            Return _version
        End Get
        Set(value As Boolean)
            SetProperty(_version, value)
        End Set
    End Property

    Public Property CompletionBash As Boolean
        Get
            Return _completionBash
        End Get
        Set(value As Boolean)
            SetProperty(_completionBash, value)
        End Set
    End Property

    ' Preset Models
    Private _fimQwen15BDefault As Boolean = False
    Private _fimQwen3BDefault As Boolean = False
    Private _fimQwen7BDefault As Boolean = False
    Private _fimQwen7BSpec As Boolean = False
    Private _fimQwen14BSpec As Boolean = False
    Private _fimQwen30BDefault As Boolean = False

    Public Property FimQwen15BDefault As Boolean
        Get
            Return _fimQwen15BDefault
        End Get
        Set(value As Boolean)
            SetProperty(_fimQwen15BDefault, value)
        End Set
    End Property

    Public Property FimQwen3BDefault As Boolean
        Get
            Return _fimQwen3BDefault
        End Get
        Set(value As Boolean)
            SetProperty(_fimQwen3BDefault, value)
        End Set
    End Property

    Public Property FimQwen7BDefault As Boolean
        Get
            Return _fimQwen7BDefault
        End Get
        Set(value As Boolean)
            SetProperty(_fimQwen7BDefault, value)
        End Set
    End Property

    Public Property FimQwen7BSpec As Boolean
        Get
            Return _fimQwen7BSpec
        End Get
        Set(value As Boolean)
            SetProperty(_fimQwen7BSpec, value)
        End Set
    End Property

    Public Property FimQwen14BSpec As Boolean
        Get
            Return _fimQwen14BSpec
        End Get
        Set(value As Boolean)
            SetProperty(_fimQwen14BSpec, value)
        End Set
    End Property

    Public Property FimQwen30BDefault As Boolean
        Get
            Return _fimQwen30BDefault
        End Get
        Set(value As Boolean)
            SetProperty(_fimQwen30BDefault, value)
        End Set
    End Property

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