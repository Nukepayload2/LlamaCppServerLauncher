Public Class ServerParameterMetadata
    Public Property Argument As String
    Public Property Explanation As String
    Public Property Category As String
    Public Property Editor As String
    Public Property DefaultValue As Object
    
    Public Shared ReadOnly Property AllParameters As New List(Of ServerParameterMetadata) From {
        New ServerParameterMetadata With {
            .Argument = "--server-path",
            .Explanation = "Server executable path",
            .Category = "common",
            .Editor = "filepath",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--model",
            .Explanation = "Model file path",
            .Category = "common",
            .Editor = "filepath",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--threads",
            .Explanation = "Number of threads to use",
            .Category = "common",
            .Editor = "numberupdown",
            .DefaultValue = 4
        },
        New ServerParameterMetadata With {
            .Argument = "--ctx-size",
            .Explanation = "Context size",
            .Category = "common",
            .Editor = "numberupdown",
            .DefaultValue = 4096
        },
        New ServerParameterMetadata With {
            .Argument = "--n-predict",
            .Explanation = "Number of tokens to predict",
            .Category = "common",
            .Editor = "numberupdown",
            .DefaultValue = -1
        },
        New ServerParameterMetadata With {
            .Argument = "--batch-size",
            .Explanation = "Batch size for processing",
            .Category = "common",
            .Editor = "numberupdown",
            .DefaultValue = 2048
        },
        New ServerParameterMetadata With {
            .Argument = "--ubatch-size",
            .Explanation = "Micro batch size",
            .Category = "common",
            .Editor = "numberupdown",
            .DefaultValue = 512
        },
        New ServerParameterMetadata With {
            .Argument = "--threads-batch",
            .Explanation = "Threads for batch processing",
            .Category = "common",
            .Editor = "numberupdown",
            .DefaultValue = -1
        },
        New ServerParameterMetadata With {
            .Argument = "--n-gpu-layers",
            .Explanation = "Number of GPU layers",
            .Category = "hardware",
            .Editor = "numberupdown",
            .DefaultValue = 0
        },
        New ServerParameterMetadata With {
            .Argument = "--main-gpu",
            .Explanation = "Main GPU to use",
            .Category = "hardware",
            .Editor = "numberupdown",
            .DefaultValue = 0
        },
        New ServerParameterMetadata With {
            .Argument = "--tensor-split",
            .Explanation = "Tensor split configuration",
            .Category = "hardware",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--split-mode",
            .Explanation = "Split mode for tensors",
            .Category = "hardware",
            .Editor = "textbox",
            .DefaultValue = "none"
        },
        New ServerParameterMetadata With {
            .Argument = "--mlock",
            .Explanation = "Lock model in memory",
            .Category = "hardware",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--no-mmap",
            .Explanation = "Disable memory mapping",
            .Category = "hardware",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--numa",
            .Explanation = "NUMA configuration",
            .Category = "hardware",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--device",
            .Explanation = "Device to use",
            .Category = "hardware",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--cpu-mask",
            .Explanation = "CPU mask",
            .Category = "hardware",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--cpu-range",
            .Explanation = "CPU range",
            .Category = "hardware",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--cpu-strict",
            .Explanation = "Strict CPU affinity",
            .Category = "hardware",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--prio",
            .Explanation = "Process priority",
            .Category = "hardware",
            .Editor = "numberupdown",
            .DefaultValue = 0
        },
        New ServerParameterMetadata With {
            .Argument = "--poll",
            .Explanation = "Poll interval",
            .Category = "hardware",
            .Editor = "numberupdown",
            .DefaultValue = 50
        },
        New ServerParameterMetadata With {
            .Argument = "--cpu-mask-batch",
            .Explanation = "CPU mask for batch",
            .Category = "hardware",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--cpu-range-batch",
            .Explanation = "CPU range for batch",
            .Category = "hardware",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--cpu-strict-batch",
            .Explanation = "Strict CPU affinity for batch",
            .Category = "hardware",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--prio-batch",
            .Explanation = "Process priority for batch",
            .Category = "hardware",
            .Editor = "numberupdown",
            .DefaultValue = 0
        },
        New ServerParameterMetadata With {
            .Argument = "--poll-batch",
            .Explanation = "Poll interval for batch",
            .Category = "hardware",
            .Editor = "numberupdown",
            .DefaultValue = 50
        },
        New ServerParameterMetadata With {
            .Argument = "--no-kv-offload",
            .Explanation = "Disable KV cache offload",
            .Category = "kv-cache",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--no-repack",
            .Explanation = "Disable repacking",
            .Category = "kv-cache",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--cache-type-k",
            .Explanation = "Cache type for K",
            .Category = "kv-cache",
            .Editor = "textbox",
            .DefaultValue = "f16"
        },
        New ServerParameterMetadata With {
            .Argument = "--cache-type-v",
            .Explanation = "Cache type for V",
            .Category = "kv-cache",
            .Editor = "textbox",
            .DefaultValue = "f16"
        },
        New ServerParameterMetadata With {
            .Argument = "--defrag-thold",
            .Explanation = "Defragmentation threshold",
            .Category = "kv-cache",
            .Editor = "numberupdown",
            .DefaultValue = 0.5
        },
        New ServerParameterMetadata With {
            .Argument = "--kv-unified",
            .Explanation = "Unified KV cache",
            .Category = "kv-cache",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--swa-full",
            .Explanation = "Sliding window attention full",
            .Category = "kv-cache",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--temperature",
            .Explanation = "Temperature for sampling",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 0.8
        },
        New ServerParameterMetadata With {
            .Argument = "--top-p",
            .Explanation = "Top-p sampling",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 0.9
        },
        New ServerParameterMetadata With {
            .Argument = "--top-k",
            .Explanation = "Top-k sampling",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 40
        },
        New ServerParameterMetadata With {
            .Argument = "--min-p",
            .Explanation = "Min-p sampling",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 0.1
        },
        New ServerParameterMetadata With {
            .Argument = "--top-n-sigma",
            .Explanation = "Top-n sigma sampling",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = -1.0
        },
        New ServerParameterMetadata With {
            .Argument = "--xtc-probability",
            .Explanation = "XTC probability",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 0.0
        },
        New ServerParameterMetadata With {
            .Argument = "--xtc-threshold",
            .Explanation = "XTC threshold",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 0.1
        },
        New ServerParameterMetadata With {
            .Argument = "--typical",
            .Explanation = "Typical sampling",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 1.0
        },
        New ServerParameterMetadata With {
            .Argument = "--repeat-last-n",
            .Explanation = "Repeat last N tokens",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 64
        },
        New ServerParameterMetadata With {
            .Argument = "--repeat-penalty",
            .Explanation = "Repeat penalty",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 1.0
        },
        New ServerParameterMetadata With {
            .Argument = "--presence-penalty",
            .Explanation = "Presence penalty",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 0.0
        },
        New ServerParameterMetadata With {
            .Argument = "--frequency-penalty",
            .Explanation = "Frequency penalty",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 0.0
        },
        New ServerParameterMetadata With {
            .Argument = "--dry-multiplier",
            .Explanation = "DRY multiplier",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 0.0
        },
        New ServerParameterMetadata With {
            .Argument = "--dry-base",
            .Explanation = "DRY base",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 1.75
        },
        New ServerParameterMetadata With {
            .Argument = "--dry-allowed-length",
            .Explanation = "DRY allowed length",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 2
        },
        New ServerParameterMetadata With {
            .Argument = "--dry-penalty-last-n",
            .Explanation = "DRY penalty last N",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = -1
        },
        New ServerParameterMetadata With {
            .Argument = "--dry-sequence-breaker",
            .Explanation = "DRY sequence breaker",
            .Category = "sampling",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--dynatemp-range",
            .Explanation = "Dynamic temperature range",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 0.0
        },
        New ServerParameterMetadata With {
            .Argument = "--dynatemp-exp",
            .Explanation = "Dynamic temperature exponent",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 1.0
        },
        New ServerParameterMetadata With {
            .Argument = "--mirostat",
            .Explanation = "Mirostat mode",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 0
        },
        New ServerParameterMetadata With {
            .Argument = "--mirostat-lr",
            .Explanation = "Mirostat learning rate",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 0.1
        },
        New ServerParameterMetadata With {
            .Argument = "--mirostat-ent",
            .Explanation = "Mirostat entropy",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 5.0
        },
        New ServerParameterMetadata With {
            .Argument = "--samplers",
            .Explanation = "Samplers configuration",
            .Category = "sampling",
            .Editor = "textbox",
            .DefaultValue = "penalties;dry;top_n_sigma;top_k;typ_p;top_p;min_p;xtc;temperature"
        },
        New ServerParameterMetadata With {
            .Argument = "--sampling-seq",
            .Explanation = "Sampling sequence",
            .Category = "sampling",
            .Editor = "textbox",
            .DefaultValue = "edskypmxt"
        },
        New ServerParameterMetadata With {
            .Argument = "--ignore-eos",
            .Explanation = "Ignore end of sequence token",
            .Category = "sampling",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--rope-scaling",
            .Explanation = "RoPE scaling type",
            .Category = "rope",
            .Editor = "textbox",
            .DefaultValue = "none"
        },
        New ServerParameterMetadata With {
            .Argument = "--rope-scale",
            .Explanation = "RoPE scale factor",
            .Category = "rope",
            .Editor = "numberupdown",
            .DefaultValue = 1.0
        },
        New ServerParameterMetadata With {
            .Argument = "--rope-freq-base",
            .Explanation = "RoPE frequency base",
            .Category = "rope",
            .Editor = "numberupdown",
            .DefaultValue = 0.0
        },
        New ServerParameterMetadata With {
            .Argument = "--rope-freq-scale",
            .Explanation = "RoPE frequency scale",
            .Category = "rope",
            .Editor = "numberupdown",
            .DefaultValue = 1.0
        },
        New ServerParameterMetadata With {
            .Argument = "--yarn-orig-ctx",
            .Explanation = "YARN original context",
            .Category = "rope",
            .Editor = "numberupdown",
            .DefaultValue = 0
        },
        New ServerParameterMetadata With {
            .Argument = "--yarn-ext-factor",
            .Explanation = "YARN extension factor",
            .Category = "rope",
            .Editor = "numberupdown",
            .DefaultValue = -1.0
        },
        New ServerParameterMetadata With {
            .Argument = "--yarn-attn-factor",
            .Explanation = "YARN attention factor",
            .Category = "rope",
            .Editor = "numberupdown",
            .DefaultValue = 1.0
        },
        New ServerParameterMetadata With {
            .Argument = "--yarn-beta-slow",
            .Explanation = "YARN beta slow",
            .Category = "rope",
            .Editor = "numberupdown",
            .DefaultValue = 1.0
        },
        New ServerParameterMetadata With {
            .Argument = "--yarn-beta-fast",
            .Explanation = "YARN beta fast",
            .Category = "rope",
            .Editor = "numberupdown",
            .DefaultValue = 32.0
        },
        New ServerParameterMetadata With {
            .Argument = "--flash-attn",
            .Explanation = "Enable flash attention",
            .Category = "hardware",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--n-parallel",
            .Explanation = "Number of parallel processes",
            .Category = "common",
            .Editor = "numberupdown",
            .DefaultValue = 1
        },
        New ServerParameterMetadata With {
            .Argument = "--hf-repo",
            .Explanation = "Hugging Face repository",
            .Category = "model",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--hf-repo-draft",
            .Explanation = "Hugging Face draft repository",
            .Category = "model",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--hf-file",
            .Explanation = "Hugging Face file",
            .Category = "model",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--hf-token",
            .Explanation = "Hugging Face token",
            .Category = "model",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--model-url",
            .Explanation = "Model URL",
            .Category = "model",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--hf-repo-v",
            .Explanation = "Hugging Face repository for vision",
            .Category = "model",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--hf-file-v",
            .Explanation = "Hugging Face file for vision",
            .Category = "model",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--override-kv",
            .Explanation = "Override KV cache",
            .Category = "model",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--no-op-offload",
            .Explanation = "Disable operator offload",
            .Category = "model",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--keep",
            .Explanation = "Keep model in memory",
            .Category = "model",
            .Editor = "numberupdown",
            .DefaultValue = 0
        },
        New ServerParameterMetadata With {
            .Argument = "--lora",
            .Explanation = "LoRA adapters",
            .Category = "lora",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--lora-scaled",
            .Explanation = "Scaled LoRA adapters",
            .Category = "lora",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--lora-init-without-apply",
            .Explanation = "Initialize LoRA without applying",
            .Category = "lora",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--control-vector",
            .Explanation = "Control vectors",
            .Category = "control-vectors",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--control-vector-scaled",
            .Explanation = "Scaled control vectors",
            .Category = "control-vectors",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--control-vector-layer-range",
            .Explanation = "Control vector layer range",
            .Category = "control-vectors",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--cpu-moe",
            .Explanation = "CPU MoE",
            .Category = "moe",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--n-cpu-moe",
            .Explanation = "Number of CPU MoE",
            .Category = "moe",
            .Editor = "numberupdown",
            .DefaultValue = 0
        },
        New ServerParameterMetadata With {
            .Argument = "--cpu-moe-draft",
            .Explanation = "CPU MoE draft",
            .Category = "moe",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--n-cpu-moe-draft",
            .Explanation = "Number of CPU MoE draft",
            .Category = "moe",
            .Editor = "numberupdown",
            .DefaultValue = 0
        },
        New ServerParameterMetadata With {
            .Argument = "--log-disable",
            .Explanation = "Disable logging",
            .Category = "logging",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--log-file",
            .Explanation = "Log file path",
            .Category = "logging",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--log-colors",
            .Explanation = "Enable log colors",
            .Category = "logging",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--verbose",
            .Explanation = "Verbose logging",
            .Category = "logging",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--offline",
            .Explanation = "Offline mode",
            .Category = "logging",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--verbosity",
            .Explanation = "Verbosity level",
            .Category = "logging",
            .Editor = "numberupdown",
            .DefaultValue = 0
        },
        New ServerParameterMetadata With {
            .Argument = "--log-prefix",
            .Explanation = "Log prefix",
            .Category = "logging",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--log-timestamps",
            .Explanation = "Log timestamps",
            .Category = "logging",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--no-perf",
            .Explanation = "Disable performance metrics",
            .Category = "logging",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--escape",
            .Explanation = "Escape special characters",
            .Category = "logging",
            .Editor = "checkbox",
            .DefaultValue = True
        },
        New ServerParameterMetadata With {
            .Argument = "--verbose-prompt",
            .Explanation = "Verbose prompt",
            .Category = "logging",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--host",
            .Explanation = "Server host",
            .Category = "network",
            .Editor = "textbox",
            .DefaultValue = "127.0.0.1"
        },
        New ServerParameterMetadata With {
            .Argument = "--port",
            .Explanation = "Server port",
            .Category = "network",
            .Editor = "numberupdown",
            .DefaultValue = 8080
        },
        New ServerParameterMetadata With {
            .Argument = "--path",
            .Explanation = "Server path",
            .Category = "network",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--api-prefix",
            .Explanation = "API prefix",
            .Category = "network",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--timeout",
            .Explanation = "Timeout",
            .Category = "network",
            .Editor = "numberupdown",
            .DefaultValue = 600
        },
        New ServerParameterMetadata With {
            .Argument = "--threads-http",
            .Explanation = "HTTP threads",
            .Category = "network",
            .Editor = "numberupdown",
            .DefaultValue = -1
        },
        New ServerParameterMetadata With {
            .Argument = "--no-webui",
            .Explanation = "Disable web UI",
            .Category = "server",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--embeddings",
            .Explanation = "Enable embeddings",
            .Category = "server",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--reranking",
            .Explanation = "Enable reranking",
            .Category = "server",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--api-key",
            .Explanation = "API key",
            .Category = "server",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--api-key-file",
            .Explanation = "API key file",
            .Category = "server",
            .Editor = "filepath",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--ssl-key-file",
            .Explanation = "SSL key file",
            .Category = "server",
            .Editor = "filepath",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--ssl-cert-file",
            .Explanation = "SSL certificate file",
            .Category = "server",
            .Editor = "filepath",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--cont-batching",
            .Explanation = "Enable continuous batching",
            .Category = "server",
            .Editor = "checkbox",
            .DefaultValue = True
        },
        New ServerParameterMetadata With {
            .Argument = "--no-cont-batching",
            .Explanation = "Disable continuous batching",
            .Category = "server",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--props",
            .Explanation = "Enable properties",
            .Category = "server",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--metrics",
            .Explanation = "Enable metrics",
            .Category = "server",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--slots",
            .Explanation = "Enable slots",
            .Category = "server",
            .Editor = "checkbox",
            .DefaultValue = True
        },
        New ServerParameterMetadata With {
            .Argument = "--no-slots",
            .Explanation = "Disable slots",
            .Category = "server",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--slot-save-path",
            .Explanation = "Slot save path",
            .Category = "server",
            .Editor = "directory",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--jinja",
            .Explanation = "Enable Jinja templating",
            .Category = "server",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--reasoning-format",
            .Explanation = "Reasoning format",
            .Category = "server",
            .Editor = "textbox",
            .DefaultValue = "auto"
        },
        New ServerParameterMetadata With {
            .Argument = "--reasoning-budget",
            .Explanation = "Reasoning budget",
            .Category = "server",
            .Editor = "numberupdown",
            .DefaultValue = -1
        },
        New ServerParameterMetadata With {
            .Argument = "--no-context-shift",
            .Explanation = "Disable context shift",
            .Category = "server",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--context-shift",
            .Explanation = "Enable context shift",
            .Category = "server",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--chat-template",
            .Explanation = "Chat template",
            .Category = "chat",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--chat-template-file",
            .Explanation = "Chat template file",
            .Category = "chat",
            .Editor = "filepath",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--chat-template-kwargs",
            .Explanation = "Chat template kwargs",
            .Category = "chat",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--no-prefill-assistant",
            .Explanation = "Disable prefill assistant",
            .Category = "chat",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--model-alias",
            .Explanation = "Model alias",
            .Category = "chat",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--reverse-prompt",
            .Explanation = "Reverse prompt",
            .Category = "chat",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--special",
            .Explanation = "Special tokens",
            .Category = "chat",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--no-warmup",
            .Explanation = "Disable warmup",
            .Category = "chat",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--spm-infill",
            .Explanation = "SPM infill",
            .Category = "chat",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--mmproj",
            .Explanation = "Multimodal projection",
            .Category = "multimodal",
            .Editor = "filepath",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--mmproj-url",
            .Explanation = "Multimodal projection URL",
            .Category = "multimodal",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--no-mmproj",
            .Explanation = "Disable multimodal projection",
            .Category = "multimodal",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--no-mmproj-offload",
            .Explanation = "Disable multimodal projection offload",
            .Category = "multimodal",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--pooling",
            .Explanation = "Pooling method",
            .Category = "embedding",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "embd-bge-small-en-default",
            .Explanation = "BGE small EN default",
            .Category = "embedding",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "embd-e5-small-en-default",
            .Explanation = "E5 small EN default",
            .Category = "embedding",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "embd-gte-small-default",
            .Explanation = "GTE small default",
            .Category = "embedding",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--model-draft",
            .Explanation = "Draft model",
            .Category = "draft",
            .Editor = "filepath",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--ctx-size-draft",
            .Explanation = "Draft context size",
            .Category = "draft",
            .Editor = "numberupdown",
            .DefaultValue = 0
        },
        New ServerParameterMetadata With {
            .Argument = "--threads-draft",
            .Explanation = "Draft threads",
            .Category = "draft",
            .Editor = "numberupdown",
            .DefaultValue = -1
        },
        New ServerParameterMetadata With {
            .Argument = "--threads-batch-draft",
            .Explanation = "Draft batch threads",
            .Category = "draft",
            .Editor = "numberupdown",
            .DefaultValue = -1
        },
        New ServerParameterMetadata With {
            .Argument = "--device-draft",
            .Explanation = "Draft device",
            .Category = "draft",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--n-gpu-layers-draft",
            .Explanation = "Draft GPU layers",
            .Category = "draft",
            .Editor = "numberupdown",
            .DefaultValue = 0
        },
        New ServerParameterMetadata With {
            .Argument = "--draft-max",
            .Explanation = "Draft max",
            .Category = "draft",
            .Editor = "numberupdown",
            .DefaultValue = 16
        },
        New ServerParameterMetadata With {
            .Argument = "--draft-min",
            .Explanation = "Draft min",
            .Category = "draft",
            .Editor = "numberupdown",
            .DefaultValue = 0
        },
        New ServerParameterMetadata With {
            .Argument = "--draft-p-min",
            .Explanation = "Draft p min",
            .Category = "draft",
            .Editor = "numberupdown",
            .DefaultValue = 0.8
        },
        New ServerParameterMetadata With {
            .Argument = "--cache-type-k-draft",
            .Explanation = "Draft cache type K",
            .Category = "draft",
            .Editor = "textbox",
            .DefaultValue = "f16"
        },
        New ServerParameterMetadata With {
            .Argument = "--cache-type-v-draft",
            .Explanation = "Draft cache type V",
            .Category = "draft",
            .Editor = "textbox",
            .DefaultValue = "f16"
        },
        New ServerParameterMetadata With {
            .Argument = "--override-tensor-draft",
            .Explanation = "Draft override tensor",
            .Category = "draft",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--spec-replace",
            .Explanation = "Speculative replace",
            .Category = "draft",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--model-vocoder",
            .Explanation = "Vocoder model",
            .Category = "vocoder",
            .Editor = "filepath",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--tts-use-guide-tokens",
            .Explanation = "TTS use guide tokens",
            .Category = "vocoder",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--swa-checkpoints",
            .Explanation = "SWA checkpoints",
            .Category = "checkpoint",
            .Editor = "numberupdown",
            .DefaultValue = 3
        },
        New ServerParameterMetadata With {
            .Argument = "--cache-reuse",
            .Explanation = "Cache reuse",
            .Category = "functionality",
            .Editor = "numberupdown",
            .DefaultValue = 0
        },
        New ServerParameterMetadata With {
            .Argument = "--slot-prompt-similarity",
            .Explanation = "Slot prompt similarity",
            .Category = "functionality",
            .Editor = "numberupdown",
            .DefaultValue = 0.5
        },
        New ServerParameterMetadata With {
            .Argument = "--list-devices",
            .Explanation = "List devices",
            .Category = "functionality",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--help",
            .Explanation = "Show help",
            .Category = "help",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--usage",
            .Explanation = "Show usage",
            .Category = "help",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--version",
            .Explanation = "Show version",
            .Category = "help",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--completion-bash",
            .Explanation = "Bash completion",
            .Category = "help",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "fim-qwen1.5b-default",
            .Explanation = "FIM Qwen 1.5B default",
            .Category = "preset",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "fim-qwen3b-default",
            .Explanation = "FIM Qwen 3B default",
            .Category = "preset",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "fim-qwen7b-default",
            .Explanation = "FIM Qwen 7B default",
            .Category = "preset",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "fim-qwen7b-spec",
            .Explanation = "FIM Qwen 7B spec",
            .Category = "preset",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "fim-qwen14b-spec",
            .Explanation = "FIM Qwen 14B spec",
            .Category = "preset",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "fim-qwen30b-default",
            .Explanation = "FIM Qwen 30B default",
            .Category = "preset",
            .Editor = "checkbox",
            .DefaultValue = False
        }
    }
    
    Public Shared Function GetMetadataByArgument(argument As String) As ServerParameterMetadata
        Return AllParameters.FirstOrDefault(Function(p) p.Argument = argument)
    End Function
End Class