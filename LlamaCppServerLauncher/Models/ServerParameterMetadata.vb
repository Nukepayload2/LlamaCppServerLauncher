Public Class ServerParameterMetadata
    Public Property Argument As String
    Public Property Explanation As String
    Public Property Category As String
    Public Property Editor As String
    Public Property DefaultValue As Object

    Public Shared ReadOnly Property AllParameters As New List(Of ServerParameterMetadata) From {
        New ServerParameterMetadata With {
            .Argument = "--server-path",
            .Explanation = "Server executable path (default: empty string, must be specified manually) | 服务器可执行文件路径。指定 llama.cpp HTTP 服务器的可执行文件位置，可以是绝对路径或相对路径。默认为空字符串，需要用户手动指定。支持 llama-server.exe 或其他兼容的服务器可执行文件。",
            .Category = "common",
            .Editor = "filepath",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--model",
            .Explanation = "Model path (default: `models/$filename` with filename from `--hf-file` or `--model-url` if set, otherwise models/7B/ggml-model-f16.gguf) (env: LLAMA_ARG_MODEL) | 模型文件路径。指定要加载的 GGUF 模型文件路径，支持本地文件路径或网络 URL。如果使用 --hf-repo 或 --model-url，此参数可选。默认根据 --hf-file 或 --model-url 设置为 models/$filename，否则为 models/7B/ggml-model-f16.gguf。",
            .Category = "common",
            .Editor = "filepath",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--threads",
            .Explanation = "Number of threads to use during generation (default: -1, -1 = auto detect) (env: LLAMA_ARG_THREADS) | 生成过程中使用的线程数。指定模型推理过程中使用的 CPU 线程数，直接影响推理速度和 CPU 使用率。建议设置为 CPU 逻辑核心数。默认为 -1 表示自动检测为系统可用的逻辑核心数。",
            .Category = "common",
            .Editor = "numberupdown",
            .DefaultValue = 4
        },
        New ServerParameterMetadata With {
            .Argument = "--ctx-size",
            .Explanation = "Size of the prompt context (default: 4096, 0 = loaded from model) (env: LLAMA_ARG_CTX_SIZE) | 上下文窗口大小。指定提示词上下文的最大 token 数量，影响模型能处理的输入长度。更大的上下文需要更多内存。默认为 4096，0 表示从模型加载。",
            .Category = "common",
            .Editor = "numberupdown",
            .DefaultValue = 4096
        },
        New ServerParameterMetadata With {
            .Argument = "--n-predict",
            .Explanation = "Number of tokens to predict (default: -1, -1 = infinity) (env: LLAMA_ARG_N_PREDICT) | 预测的 token 数量。控制每次生成输出的最大 token 数，防止无限生成。-1 表示无限制。适用于聊天、代码生成等任务。",
            .Category = "common",
            .Editor = "numberupdown",
            .DefaultValue = -1
        },
        New ServerParameterMetadata With {
            .Argument = "--batch-size",
            .Explanation = "Logical maximum batch size (default: 2048) (env: LLAMA_ARG_BATCH) | 逻辑最大批处理大小。影响同时处理的请求数量。较大的批处理可提高吞吐量但增加内存使用。适用于多用户并发场景。",
            .Category = "common",
            .Editor = "numberupdown",
            .DefaultValue = 2048
        },
        New ServerParameterMetadata With {
            .Argument = "--ubatch-size",
            .Explanation = "Physical maximum batch size (default: 512) (env: LLAMA_ARG_UBATCH) | 物理最大批处理大小。实际处理时的最小单位。较小的微批处理可提高内存效率，但可能略微降低性能。",
            .Category = "common",
            .Editor = "numberupdown",
            .DefaultValue = 512
        },
        New ServerParameterMetadata With {
            .Argument = "--threads-batch",
            .Explanation = "Number of threads to use during batch and prompt processing (default: same as --threads) | 批处理和提示词处理期间使用的线程数，专门用于批处理和提示词处理的线程数，通常与生成线程分开设置。默认与 --threads 相同。",
            .Category = "common",
            .Editor = "numberupdown",
            .DefaultValue = -1
        },
        New ServerParameterMetadata With {
            .Argument = "--n-gpu-layers",
            .Explanation = "Number of layers to store in VRAM (env: LLAMA_ARG_N_GPU_LAYERS) | GPU 层数。指定卸载到 VRAM 的神经网络层数，可显著加速推理。0 表示仅使用 CPU。设置为 -1 或足够大的数字可将整个模型移至 GPU。较高的层数可大幅提升推理速度，但需要足够的 GPU 显存。",
            .Category = "hardware",
            .Editor = "numberupdown",
            .DefaultValue = 0
        },
        New ServerParameterMetadata With {
            .Argument = "--main-gpu",
            .Explanation = "Main GPU to use | 主 GPU 设备。当 split-mode 为 none 时使用的 GPU，或当 split-mode 为 row 时存储中间结果和 KV 缓存的 GPU。默认为 0。多 GPU 系统中用于负载均衡。",
            .Category = "hardware",
            .Editor = "numberupdown",
            .DefaultValue = 0
        },
        New ServerParameterMetadata With {
            .Argument = "--tensor-split",
            .Explanation = "Tensor split configuration | 张量分割配置。指定将模型分配到多个 GPU 的比例，逗号分隔的比例列表。例如 '3,1' 表示 GPU 0 分配 75%，GPU 1 分配 25%。用于多 GPU 分布式推理。",
            .Category = "hardware",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--split-mode",
            .Explanation = "Split mode for tensors | 张量分割模式。控制如何在多个 GPU 间分割模型：none（单 GPU）、layer（默认，层分割）、row（行分割）。影响多 GPU 性能和内存使用。",
            .Category = "hardware",
            .Editor = "textbox",
            .DefaultValue = "none"
        },
        New ServerParameterMetadata With {
            .Argument = "--mlock",
            .Explanation = "Lock model in memory | 内存锁定模型。强制系统将模型保持在 RAM 中而非交换或压缩，提高性能但增加内存占用。适用于内存充足且需要稳定性能的场景。",
            .Category = "hardware",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--no-mmap",
            .Explanation = "Disable memory mapping | 禁用内存映射。不使用内存映射加载模型，加载速度较慢但可能减少页面交换。在不使用 mlock 时可用于优化内存管理。",
            .Category = "hardware",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--numa",
            .Explanation = "NUMA configuration | NUMA 配置。尝试在某些 NUMA 系统上进行优化：distribute（均匀分布）、isolate（仅限起始节点）、numactl（使用 numactl 提供的 CPU 映射）。适合多 CPU 架构优化。",
            .Category = "hardware",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--device",
            .Explanation = "Device to use | 使用的设备。逗号分隔的设备列表用于卸载（none 表示不卸载）。使用 --list-devices 查看可用设备列表。支持 CPU、GPU 等多种设备类型。",
            .Category = "hardware",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--cpu-mask",
            .Explanation = "CPU mask | CPU 掩码。CPU 亲和性的十六进制掩码，与 cpu-range 互补。任意长度的十六进制字符串，用于精确控制进程在特定 CPU 核心上运行。",
            .Category = "hardware",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--cpu-range",
            .Explanation = "CPU range | CPU 范围。CPU 亲和性的范围，格式为 lo-hi。与 --cpu-mask 互补，用于指定进程可使用的 CPU 核心范围。",
            .Category = "hardware",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--cpu-strict",
            .Explanation = "Strict CPU affinity | 严格 CPU 亲和性。使用严格的 CPU 放置，确保进程只在指定的 CPU 上运行。提高性能稳定性，但可能降低灵活性。",
            .Category = "hardware",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--prio",
            .Explanation = "Process priority | 进程优先级。设置进程/线程优先级：low(-1)、normal(0)、medium(1)、high(2)、realtime(3)。较高的优先级可获得更多 CPU 时间，但可能影响系统稳定性。",
            .Category = "hardware",
            .Editor = "numberupdown",
            .DefaultValue = 0
        },
        New ServerParameterMetadata With {
            .Argument = "--poll",
            .Explanation = "Poll interval | 轮询间隔。使用轮询级别等待工作（0 - 无轮询）。影响响应速度和 CPU 使用率的平衡。较高的轮询级别可提高响应速度但增加 CPU 使用。",
            .Category = "hardware",
            .Editor = "numberupdown",
            .DefaultValue = 50
        },
        New ServerParameterMetadata With {
            .Argument = "--cpu-mask-batch",
            .Explanation = "CPU mask for batch | 批处理 CPU 掩码。批处理操作的 CPU 亲和性掩码，与 --cpu-mask-batch 互补。默认与 --cpu-mask 相同，用于优化批处理性能。",
            .Category = "hardware",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--cpu-range-batch",
            .Explanation = "CPU range for batch | 批处理 CPU 范围。批处理操作的 CPU 亲和性范围，与 --cpu-mask-batch 互补。用于优化批处理操作的 CPU 分配。",
            .Category = "hardware",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--cpu-strict-batch",
            .Explanation = "Use strict CPU placement (default: same as --cpu-strict) | 批处理严格 CPU 亲和性。批处理操作的严格 CPU 放置，默认与 --cpu-strict 相同。确保批处理任务在指定的 CPU 上运行。",
            .Category = "hardware",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--prio-batch",
            .Explanation = "Set process/thread priority : 0-normal, 1-medium, 2-high, 3-realtime (default: 0) | 批处理进程优先级。批处理操作的进程/线程优先级：0-normal、1-medium、2-high、3-realtime。用于优化批处理任务的调度。",
            .Category = "hardware",
            .Editor = "numberupdown",
            .DefaultValue = 0
        },
        New ServerParameterMetadata With {
            .Argument = "--poll-batch",
            .Explanation = "Use polling to wait for work (default: same as --poll) | 批处理轮询间隔。批处理操作的轮询级别，默认与 --poll 相同。用于优化批处理任务的响应速度和 CPU 使用。",
            .Category = "hardware",
            .Editor = "numberupdown",
            .DefaultValue = 50
        },
        New ServerParameterMetadata With {
            .Argument = "--no-kv-offload",
            .Explanation = "Disable KV offload (default: false) | 禁用 KV 缓存卸载。阻止将 KV 缓存卸载到 GPU，强制所有 KV 缓存保留在 CPU 内存中。在 GPU 内存有限时可用，但可能影响推理速度。",
            .Category = "kv-cache",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--no-repack",
            .Explanation = "Disable weight repacking (default: false) | 禁用权重重新打包。不重新打包权重，可能在某些情况下加快加载速度，但可能影响后续推理性能。通常建议启用重新打包以获得最佳性能。",
            .Category = "kv-cache",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--cache-type-k",
            .Explanation = "KV cache data type for K. Allowed values: f32, f16, bf16, q8_0, q4_0, q4_1, iq4_nl, q5_0, q5_1 (default: f16) (env: LLAMA_ARG_CACHE_TYPE_K) | K 缓存类型。KV 缓存中 K 键的数据类型，可选值：f32、f16、bf16、q8_0、q4_0、q4_1、iq4_nl、q5_0、q5_1。影响内存占用和精度，f16 是较好的平衡。",
            .Category = "kv-cache",
            .Editor = "textbox",
            .DefaultValue = "f16"
        },
        New ServerParameterMetadata With {
            .Argument = "--cache-type-v",
            .Explanation = "KV cache data type for V. Allowed values: f32, f16, bf16, q8_0, q4_0, q4_1, iq4_nl, q5_0, q5_1 (default: f16) (env: LLAMA_ARG_CACHE_TYPE_V) | V 缓存类型。KV 缓存中 V 值的数据类型，可选值与 K 相同。通常与 K 设置相同类型以保持一致性，可根据精度需求选择。",
            .Category = "kv-cache",
            .Editor = "textbox",
            .DefaultValue = "f16"
        },
        New ServerParameterMetadata With {
            .Argument = "--defrag-thold",
            .Explanation = "KV cache defragmentation threshold (DEPRECATED) (env: LLAMA_ARG_DEFRAG_THOLD) | KV 缓存碎片整理阈值。当碎片超过此阈值时触发碎片整理，提高内存利用率。DEPRECATED 参数，可能在未来版本中被移除。",
            .Category = "kv-cache",
            .Editor = "numberupdown",
            .DefaultValue = 0.5
        },
        New ServerParameterMetadata With {
            .Argument = "--kv-unified",
            .Explanation = "Use single unified KV buffer for the KV cache of all sequences (default: false) [more info](https://github.com/ggml-org/llama.cpp/pull/14363) (env: LLAMA_ARG_KV_SPLIT) | 统一 KV 缓存。使用单一统一的 KV 缓冲区存储所有序列的 KV 缓存，而不是为每个序列分别分配。可提高内存效率，但可能在某些场景下影响性能。",
            .Category = "kv-cache",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--swa-full",
            .Explanation = "Use full-size SWA cache (default: false) [more info](https://github.com/ggml-org/llama.cpp/pull/13194#issuecomment-2868343055) (env: LLAMA_ARG_SWA_FULL) | 全尺寸滑动窗口注意力。使用完整大小的 SWA 缓存，提高长上下文处理的性能和内存效率。适用于处理超长文本或需要大量历史上下文的场景。",
            .Category = "kv-cache",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--temperature",
            .Explanation = "Temperature for sampling (default: 0.8) | 采样温度。控制生成文本的随机性，较低的值（如 0.2）使输出更确定性，较高的值（如 1.0）增加随机性和创造性。默认 0.8 提供良好的平衡。值越低输出越保守，越高越创造性但可能不连贯。",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 0.8
        },
        New ServerParameterMetadata With {
            .Argument = "--top-p",
            .Explanation = "Top-p sampling (default: 0.9, 1.0 = disabled) | Top-p 核心采样。累积概率达到 p 的最小 token 集合中进行采样，限制候选词数量。0.9 表示从累积概率 90% 的词中选择。1.0 表示禁用。适用于控制输出的多样性，是核采样方法。",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 0.9
        },
        New ServerParameterMetadata With {
            .Argument = "--top-k",
            .Explanation = "Top-k sampling (default: 40, 0 = disabled) | Top-k 采样。从概率最高的 k 个 token 中随机选择，限制候选词数量。较高的 k 值增加多样性，较低的 k 值使输出更集中。0 表示禁用，仅从最可能的词中选择。是传统而有效的采样方法。",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 40
        },
        New ServerParameterMetadata With {
            .Argument = "--min-p",
            .Explanation = "Min-p sampling (default: 0.1, 0.0 = disabled) | Min-p 采样。最小概率采样，过滤掉概率低于最高概率乘以 p 的 token。可防止低质量 token 被选中，提高输出质量。0.0 表示禁用。是一种较新的质量控制采样方法。",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 0.1
        },
        New ServerParameterMetadata With {
            .Argument = "--top-n-sigma",
            .Explanation = "Top-n-sigma sampling (default: -1.0, -1.0 = disabled) | Top-n sigma 采样。基于标准差的采样方法，考虑概率分布的标准差。负值表示禁用。提供另一种控制输出多样性的方法，适用于特定场景。使用统计方法进行候选token选择。",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = -1.0
        },
        New ServerParameterMetadata With {
            .Argument = "--xtc-probability",
            .Explanation = "XTC probability (default: 0.0, 0.0 = disabled) | XTC 概率。排除最常见 token 的概率，用于减少重复和常见短语。0.0 表示禁用，较高的值可以增加文本的原创性和多样性。XTC (Exclude Top Common) 是一种新颖的重复减少技术。",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 0.0
        },
        New ServerParameterMetadata With {
            .Argument = "--xtc-threshold",
            .Explanation = "XTC threshold (default: 0.1, 1.0 = disabled) | XTC 阈值。XTC 采样的阈值参数，控制排除 token 的严格程度。1.0 表示禁用。与 xtc-probability 配合使用，共同控制文本生成的特征。较低的阈值更严格地排除常见 token。",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 0.1
        },
        New ServerParameterMetadata With {
            .Argument = "--typical",
            .Explanation = "Locally typical sampling, parameter p (default: 1.0, 1.0 = disabled) | 典型采样。局部典型采样，基于 token 的典型性进行选择。参数 p 控制典型性阈值。1.0 表示禁用。适用于生成更加自然和预期的文本，减少非常规输出。",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 1.0
        },
        New ServerParameterMetadata With {
            .Argument = "--repeat-last-n",
            .Explanation = "Repeat last N tokens | 重复最后 N 个 token。考虑用于惩罚重复的最近 token 数量。0 表示禁用，-1 表示使用整个上下文大小。较大的值可以更好地防止重复，但可能影响自然重复。",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 64
        },
        New ServerParameterMetadata With {
            .Argument = "--repeat-penalty",
            .Explanation = "Penalize repeat sequence of tokens (default: 1.0, 1.0 = disabled) | 重复惩罚。惩罚重复的 token 序列，减少文本重复。1.0 表示禁用，大于 1.0 的值会降低重复 token 的概率。适用于减少重复性回答和提高文本多样性。典型值为 1.1-1.2。",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 1.0
        },
        New ServerParameterMetadata With {
            .Argument = "--presence-penalty",
            .Explanation = "Repeat alpha presence penalty (default: 0.0, 0.0 = disabled) | 存在惩罚。alpha 存在惩罚，基于 token 是否已经出现过进行惩罚。0.0 表示禁用。正数鼓励讨论新话题，负数鼓励重复。适用于保持对话的多样性和新鲜度。比频率惩罚更关注话题转换。",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 0.0
        },
        New ServerParameterMetadata With {
            .Argument = "--frequency-penalty",
            .Explanation = "Repeat alpha frequency penalty (default: 0.0, 0.0 = disabled) | 频率惩罚。alpha 频率惩罚，基于 token 出现的频率进行惩罚。0.0 表示禁用。比存在惩罚更直接地控制重复，适用于精细调整文本生成质量。惩罚强度与出现频率成正比。",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 0.0
        },
        New ServerParameterMetadata With {
            .Argument = "--dry-multiplier",
            .Explanation = "DRY multiplier | DRY 采样乘数。设置 DRY（Don't Repeat Yourself）采样的乘数，控制惩罚强度。0.0 表示禁用。高级重复检测方法，比传统重复惩罚更智能。",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 0.0
        },
        New ServerParameterMetadata With {
            .Argument = "--dry-base",
            .Explanation = "DRY base | DRY 基础值。设置 DRY 采样的基础值，影响惩罚计算的基准。与 dry-multiplier 配合使用，共同控制重复检测的敏感度。",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 1.75
        },
        New ServerParameterMetadata With {
            .Argument = "--dry-allowed-length",
            .Explanation = "DRY allowed length | DRY 允许长度。设置 DRY 采样允许的重复序列长度。较小的值限制更多，较大的值允许更多的自然重复。适用于控制文本的流畅度和原创性。",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 2
        },
        New ServerParameterMetadata With {
            .Argument = "--dry-penalty-last-n",
            .Explanation = "DRY penalty last N | DRY 惩罚最后 N。设置 DRY 惩罚考虑的最后 N 个 token，-1 表示使用上下文大小，0 表示禁用。控制重复检测的历史窗口大小。",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = -1
        },
        New ServerParameterMetadata With {
            .Argument = "--dry-sequence-breaker",
            .Explanation = "Add sequence breaker for DRY sampling, clearing out default breakers ('\n', ':', '""', '*') in the process; use ""none"" to not use any sequence breakers | DRY 序列分隔符。添加 DRY 采样的序列分隔符，清除默认分隔符（'\n', ':', '""', '*'）。使用 'none' 不使用任何分隔符。用于自定义重复检测的边界。",
            .Category = "sampling",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--dynatemp-range",
            .Explanation = "Dynamic temperature range (default: 0.0, 0.0 = disabled) | 动态温度范围。动态温度范围，根据生成上下文动态调整温度。0.0 表示禁用。可以根据上下文内容自动调整输出的随机性，提供更智能的生成控制。结合 dynatemp-exp 使用效果更佳。",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 0.0
        },
        New ServerParameterMetadata With {
            .Argument = "--dynatemp-exp",
            .Explanation = "Dynamic temperature exponent | 动态温度指数。动态温度的指数参数，影响温度调整的敏感度。与 dynatemp-range 配合使用，控制动态温度的行为模式。",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 1.0
        },
        New ServerParameterMetadata With {
            .Argument = "--mirostat",
            .Explanation = "Mirostat mode | Mirostat 模式。使用 Mirostat 采样算法，自动调节 perplexity 到目标水平。0=禁用，1=Mirostat，2=Mirostat 2.0。启用时忽略 Top K、Nucleus 和 Locally Typical 采样器。",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 0
        },
        New ServerParameterMetadata With {
            .Argument = "--mirostat-lr",
            .Explanation = "Mirostat learning rate | Mirostat 学习率。Mirostat 算法的学习率参数 eta，控制 perplexity 调节的速度。较高的值响应更快但可能不稳定，较低的值更稳定但响应较慢。",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 0.1
        },
        New ServerParameterMetadata With {
            .Argument = "--mirostat-ent",
            .Explanation = "Mirostat entropy | Mirostat 熵。Mirostat 算法的目标熵参数 tau，控制生成文本的不可预测性。较高的值产生更多样化的输出，较低的值产生更可预测的输出。",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 5.0
        },
        New ServerParameterMetadata With {
            .Argument = "--samplers",
            .Explanation = "Samplers configuration | 采样器配置。指定生成时使用的采样器序列，用分号分隔。默认序列：penalties;dry;top_n_sigma;top_k;typ_p;top_p;min_p;xtc;temperature。采样器的顺序影响最终生成结果。",
            .Category = "sampling",
            .Editor = "textbox",
            .DefaultValue = "penalties;dry;top_n_sigma;top_k;typ_p;top_p;min_p;xtc;temperature"
        },
        New ServerParameterMetadata With {
            .Argument = "--sampling-seq",
            .Explanation = "Sampling sequence | 采样序列。采样器的简化序列表示，每个字符对应一个采样器。默认 'edskypmxt'。提供更简洁的方式来配置采样器顺序，便于快速调整。",
            .Category = "sampling",
            .Editor = "textbox",
            .DefaultValue = "edskypmxt"
        },
        New ServerParameterMetadata With {
            .Argument = "--ignore-eos",
            .Explanation = "Ignore end of sequence token | 忽略序列结束 token。忽略 EOS（结束序列）token 并继续生成，意味着 --logit-bias EOS-inf。适用于需要强制生成更长的文本或不希望模型提前结束的场景。",
            .Category = "sampling",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--rope-scaling",
            .Explanation = "RoPE scaling type | RoPE 缩放类型。Rotary Position Embedding 频率缩放方法，可选值：none、linear、yarn。除非模型指定，否则默认为 linear。用于扩展模型上下文长度，提高长文本处理能力。",
            .Category = "rope",
            .Editor = "textbox",
            .DefaultValue = "none"
        },
        New ServerParameterMetadata With {
            .Argument = "--rope-scale",
            .Explanation = "RoPE context scaling factor, expands context by a factor of N (env: LLAMA_ARG_ROPE_SCALE) | RoPE 缩放因子。RoPE 上下文缩放因子，将上下文扩展 N 倍。例如 2.0 表示将上下文长度翻倍。适用于处理超出原始训练长度的长文本，但可能影响性能和准确性。",
            .Category = "rope",
            .Editor = "numberupdown",
            .DefaultValue = 1.0
        },
        New ServerParameterMetadata With {
            .Argument = "--rope-freq-base",
            .Explanation = "RoPE base frequency, used by NTK-aware scaling (default: loaded from model) (env: LLAMA_ARG_ROPE_FREQ_BASE) | RoPE 频率基础。RoPE 基础频率，用于 NTK 感知缩放。默认从模型加载。影响位置编码的频率分布，可调整以适应不同的上下文扩展需求。典型值在10000-100000范围内。",
            .Category = "rope",
            .Editor = "numberupdown",
            .DefaultValue = 0.0
        },
        New ServerParameterMetadata With {
            .Argument = "--rope-freq-scale",
            .Explanation = "RoPE frequency scaling factor, expands context by a factor of 1/N (env: LLAMA_ARG_ROPE_FREQ_SCALE) | RoPE 频率缩放。RoPE 频率缩放因子，将上下文扩展 1/N 倍。与 rope-scale 相反作用，用于精细调整位置编码的缩放比例。可用于实现更精确的上下文扩展控制。",
            .Category = "rope",
            .Editor = "numberupdown",
            .DefaultValue = 1.0
        },
        New ServerParameterMetadata With {
            .Argument = "--yarn-orig-ctx",
            .Explanation = "YaRN: original context size of model (default: 0 = model training context size) (env: LLAMA_ARG_YARN_ORIG_CTX) | YARN 原始上下文。YaRN：模型的原始上下文大小，0 表示模型训练上下文大小。用于 YaRN 扩展技术，确保位置编码的正确缩放。设置为0可自动从模型元数据中获取正确的上下文长度。",
            .Category = "rope",
            .Editor = "numberupdown",
            .DefaultValue = 0
        },
        New ServerParameterMetadata With {
            .Argument = "--yarn-ext-factor",
            .Explanation = "YaRN: extrapolation mix factor (default: -1.0, 0.0 = full interpolation) (env: LLAMA_ARG_YARN_EXT_FACTOR) | YARN 扩展因子。YaRN：外推混合因子，-1.0 表示默认，0.0 表示完全插值。控制 YaRN 如何处理超出原始上下文范围的内容，影响长文本处理质量。负值启用外推，0.0-1.0 之间控制插值强度。",
            .Category = "rope",
            .Editor = "numberupdown",
            .DefaultValue = -1.0
        },
        New ServerParameterMetadata With {
            .Argument = "--yarn-attn-factor",
            .Explanation = "YaRN: scale sqrt(t) or attention magnitude (default: 1.0) (env: LLAMA_ARG_YARN_ATTN_FACTOR) | YARN 注意力因子。YaRN：缩放 sqrt(t) 或注意力幅度。影响注意力机制在长距离上下文中的表现，控制注意力得分的缩放。调整此参数可以优化长距离依赖关系的建模效果。",
            .Category = "rope",
            .Editor = "numberupdown",
            .DefaultValue = 1.0
        },
        New ServerParameterMetadata With {
            .Argument = "--yarn-beta-slow",
            .Explanation = "YaRN: high correction dim or alpha (default: 1.0) (env: LLAMA_ARG_YARN_BETA_SLOW) | YARN 慢速 beta。YaRN：高校正维度或 alpha。控制 YaRN 中较慢变化的校正参数，影响位置编码的长期行为。较大的值增强长期位置编码的准确性，但可能影响短期精度。",
            .Category = "rope",
            .Editor = "numberupdown",
            .DefaultValue = 1.0
        },
        New ServerParameterMetadata With {
            .Argument = "--yarn-beta-fast",
            .Explanation = "YaRN: low correction dim or beta (default: 32.0) (env: LLAMA_ARG_YARN_BETA_FAST) | YARN 快速 beta。YaRN：低校正维度或 beta。控制 YaRN 中快速变化的校正参数，影响位置编码的短期精度。默认值 32.0 提供良好的短期精度平衡。",
            .Category = "rope",
            .Editor = "numberupdown",
            .DefaultValue = 32.0
        },
        New ServerParameterMetadata With {
            .Argument = "--flash-attn",
            .Explanation = "Enable Flash Attention (default: disabled) (env: LLAMA_ARG_FLASH_ATTN) | 启用 Flash Attention。启用 Flash Attention 优化算法，显著提高注意力计算效率，减少内存使用和计算时间。需要硬件支持，可大幅提升长文本处理性能。",
            .Category = "hardware",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--n-parallel",
            .Explanation = "Number of parallel sequences to decode (default: 1) (env: LLAMA_ARG_N_PARALLEL) | 并行进程数量。并行解码的序列数量，支持多用户同时使用。较高的值可提高并发性能，但增加内存和计算资源需求。适用于多用户聊天、批量处理等场景。",
            .Category = "common",
            .Editor = "numberupdown",
            .DefaultValue = 1
        },
        New ServerParameterMetadata With {
            .Argument = "--hf-repo",
            .Explanation = "Hugging Face repository | Hugging Face 仓库。Hugging Face 模型仓库，格式为 <user>/<model>[:quant]。quant 可选，不区分大小写，默认为 Q4_K_M，如果不存在则使用仓库中的第一个文件。示例：unsloth/phi-4-GGUF:q4_k_m",
            .Category = "model",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--hf-repo-draft",
            .Explanation = "Hugging Face draft repository | Hugging Face 草稿仓库。用于投机解码的草稿模型仓库，格式与 --hf-repo 相同。草稿模型通常较小，用于快速生成候选序列，提高整体推理速度。",
            .Category = "model",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--hf-file",
            .Explanation = "Hugging Face file | Hugging Face 文件。Hugging Face 模型文件，如果指定将覆盖 --hf-repo 中的 quant。用于精确指定要下载的模型文件，支持特定的量化版本。",
            .Category = "model",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--hf-token",
            .Explanation = "Hugging Face token | Hugging Face 令牌。Hugging Face 访问令牌，用于访问私有模型仓库或需要认证的模型。默认从 HF_TOKEN 环境变量获取。保护模型访问权限的重要安全措施。",
            .Category = "model",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--model-url",
            .Explanation = "Model URL | 模型 URL。模型下载 URL，支持从网络直接下载模型文件。提供另一种模型获取方式，无需手动下载。确保 URL 可访问且模型格式兼容。",
            .Category = "model",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--hf-repo-v",
            .Explanation = "Hugging Face repository for vision | 视觉模型仓库。用于多模态模型的视觉编码器仓库，支持图像理解和多模态推理。与主模型配合使用，实现图像-文本联合处理能力。",
            .Category = "model",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--hf-file-v",
            .Explanation = "Hugging Face file for vision | 视觉模型文件。多模态视觉编码器的具体文件路径，用于精确指定视觉组件。确保与语言模型版本兼容，支持高质量的图像理解功能。",
            .Category = "model",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--override-kv",
            .Explanation = "Override KV cache | 覆盖 KV 缓存。高级选项，按键覆盖模型元数据。可多次指定。类型：int、float、bool、str。示例：--override-kv tokenizer.ggml.add_bos_token=bool:false。用于自定义模型行为和配置。",
            .Category = "model",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--no-op-offload",
            .Explanation = "Disable offloading host tensor operations to device (default: false) | 禁用操作符卸载。禁用将主机张量操作卸载到设备的功能。在设备内存有限或遇到兼容性问题时使用。可能影响性能，但提高稳定性。",
            .Category = "model",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--keep",
            .Explanation = "Keep model in memory | 保留初始提示 token 数量。从初始提示中保留的 token 数量，0 表示不保留，-1 表示全部保留。用于维护对话上下文和状态，确保连贯的交互体验。",
            .Category = "model",
            .Editor = "numberupdown",
            .DefaultValue = 0
        },
        New ServerParameterMetadata With {
            .Argument = "--lora",
            .Explanation = "LoRA adapters | LoRA 适配器。LoRA（Low-Rank Adaptation）适配器路径，可用于多次指定以使用多个适配器。用于模型微调和个性化，在不修改原模型的情况下调整模型行为。支持角色扮演、风格调整等应用。",
            .Category = "lora",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--lora-scaled",
            .Explanation = "Scaled LoRA adapters | 缩放 LoRA 适配器。带有用户定义缩放因子的 LoRA 适配器，格式为路径:缩放值。可用于多次指定以使用多个缩放适配器。精确控制每个适配器的影响程度。",
            .Category = "lora",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--lora-init-without-apply",
            .Explanation = "Initialize LoRA without applying | 初始化 LoRA 但不应用。加载 LoRA 适配器但不立即应用（稍后通过 POST /lora-adapters 应用）。支持动态 LoRA 管理，可在运行时切换不同的适配器配置。",
            .Category = "lora",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--control-vector",
            .Explanation = "Control vectors | 控制向量。添加控制向量以调整模型行为。可重复添加多个控制向量。用于引导模型生成特定风格、内容或主题的文本，实现更精细的输出控制。",
            .Category = "control-vectors",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--control-vector-scaled",
            .Explanation = "Scaled control vectors | 缩放控制向量。添加带有用户定义缩放因子的控制向量，格式为路径:缩放值。精确控制每个向量对模型行为的影响程度。",
            .Category = "control-vectors",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--control-vector-layer-range",
            .Explanation = "Control vector layer range | 控制向量层范围。应用控制向量的层范围，开始和结束层都包含在内。指定控制向量影响的具体神经网络层，实现更精准的模型行为调整。",
            .Category = "control-vectors",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--cpu-moe",
            .Explanation = "CPU MoE | CPU 混合专家。将所有混合专家（MoE）权重保持在 CPU 中。对于 MoE 模型，强制所有专家网络在 CPU 上运行，可能影响性能但提高兼容性。",
            .Category = "moe",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--n-cpu-moe",
            .Explanation = "Number of CPU MoE | CPU MoE 数量。将前 N 层的混合专家（MoE）权重保持在 CPU 中。提供更细粒度的 MoE 控制，平衡性能和兼容性需求。",
            .Category = "moe",
            .Editor = "numberupdown",
            .DefaultValue = 0
        },
        New ServerParameterMetadata With {
            .Argument = "--cpu-moe-draft",
            .Explanation = "CPU MoE draft | CPU MoE 草稿。对于草稿模型，将所有混合专家（MoE）权重保持在 CPU 中。在投机解码场景中控制草稿模型的 MoE 行为。",
            .Category = "moe",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--n-cpu-moe-draft",
            .Explanation = "Keep the Mixture of Experts (MoE) weights of the first N layers in the CPU for the draft model (env: LLAMA_ARG_N_CPU_MOE_DRAFT) | CPU MoE 草稿数量。对于草稿模型，将前 N 层的混合专家（MoE）权重保持在 CPU 中。在投机解码中精细控制草稿模型的 MoE 行为。",
            .Category = "moe",
            .Editor = "numberupdown",
            .DefaultValue = 0
        },
        New ServerParameterMetadata With {
            .Argument = "--log-disable",
            .Explanation = "Disable logging | 禁用日志记录。完全禁用所有日志输出，减少控制台噪音和提高性能。在生产环境中可能有用，但会影响问题诊断和监控能力。",
            .Category = "logging",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--log-file",
            .Explanation = "Log file path | 日志文件路径。将日志输出重定向到指定文件，便于长期保存和分析日志。支持日志轮转和归档，适合生产环境部署和问题追踪。",
            .Category = "logging",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--log-colors",
            .Explanation = "Enable log colors | 启用日志颜色。为不同级别的日志消息添加颜色，提高可读性和快速识别问题。在支持颜色的终端中特别有用，便于快速定位错误和警告信息。",
            .Category = "logging",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--verbose",
            .Explanation = "Verbose logging | 详细日志记录。将详细级别设置为无限（即记录所有消息），适用于调试。提供最详细的内部状态信息，帮助诊断复杂问题。",
            .Category = "logging",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--offline",
            .Explanation = "Offline mode | 离线模式。强制使用缓存，防止网络访问。适用于网络受限环境或确保完全本地运行，避免意外下载或外部依赖。",
            .Category = "logging",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--verbosity",
            .Explanation = "Verbosity level | 详细级别。设置详细程度阈值，高于此级别的消息将被忽略。提供精细的日志级别控制，平衡信息量和性能影响。",
            .Category = "logging",
            .Editor = "numberupdown",
            .DefaultValue = 0
        },
        New ServerParameterMetadata With {
            .Argument = "--log-prefix",
            .Explanation = "Log prefix | 日志前缀。在日志消息中启用前缀，包含时间戳、级别等信息。提高日志的结构化和可读性，便于日志分析和过滤。",
            .Category = "logging",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--log-timestamps",
            .Explanation = "Log timestamps | 日志时间戳。在日志消息中启用时间戳，记录事件发生的精确时间。对于性能分析、问题追踪和审计日志非常重要。",
            .Category = "logging",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--no-perf",
            .Explanation = "Disable internal libllama performance timings (default: false) (env: LLAMA_ARG_NO_PERF) | 禁用性能指标。禁用内部 libllama 性能计时，减少开销但失去性能数据。在不需要性能分析的生产环境中可略微提高性能。",
            .Category = "logging",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--escape",
            .Explanation = "Process escapes sequences (\n, \r, \t, \', \"", \\) (default: true) | 转义特殊字符。处理转义序列（\n, \r, \t, \', \"", \\）。确保文本输出的正确性和安全性，防止格式问题和注入攻击。",
            .Category = "logging",
            .Editor = "checkbox",
            .DefaultValue = True
        },
        New ServerParameterMetadata With {
            .Argument = "--verbose-prompt",
            .Explanation = "Print a verbose prompt before generation (default: false) | 详细提示。在生成之前打印详细的提示信息，显示实际发送给模型的完整提示内容。用于调试和验证提示构建的正确性。",
            .Category = "logging",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--host",
            .Explanation = "Server host (default: 127.0.0.1) | 服务器主机。服务器监听的 IP 地址，或如果地址以 .sock 结尾则绑定到 UNIX 套接字。默认为 127.0.0.1（仅本地访问）。设置为 0.0.0.0 允许外部访问。生产环境中需要考虑网络安全配置。",
            .Category = "network",
            .Editor = "textbox",
            .DefaultValue = "127.0.0.1"
        },
        New ServerParameterMetadata With {
            .Argument = "--port",
            .Explanation = "Server port (default: 8080) | 服务器端口。服务器监听的端口号，默认为 8080。确保端口未被其他服务占用，且防火墙配置允许访问。支持 1-65535 范围内的端口号。生产环境中建议使用非特权端口（>1024）。",
            .Category = "network",
            .Editor = "numberupdown",
            .DefaultValue = 8080
        },
        New ServerParameterMetadata With {
            .Argument = "--path",
            .Explanation = "Server path | 服务器路径。提供静态文件的路径，用于 Web UI 和其他静态资源。空值表示不提供静态文件服务。支持相对路径和绝对路径。",
            .Category = "network",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--api-prefix",
            .Explanation = "API prefix | API 前缀。服务器提供 API 的前缀路径，不包含尾部斜杠。用于路径组织和反向代理配置，支持多个服务在同一域名下运行。",
            .Category = "network",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--timeout",
            .Explanation = "Timeout | 超时时间。服务器读/写超时时间（秒），默认为 600 秒（10分钟）。控制请求和响应的最大等待时间，防止长时间挂起的连接。",
            .Category = "network",
            .Editor = "numberupdown",
            .DefaultValue = 600
        },
        New ServerParameterMetadata With {
            .Argument = "--threads-http",
            .Explanation = "HTTP threads | HTTP 线程。用于处理 HTTP 请求的线程数，-1 表示自动检测。影响服务器的并发处理能力，较高的值支持更多同时连接但增加资源使用。",
            .Category = "network",
            .Editor = "numberupdown",
            .DefaultValue = -1
        },
        New ServerParameterMetadata With {
            .Argument = "--no-webui",
            .Explanation = "Disable web UI | 禁用 Web UI。禁用内置的 Web 用户界面，仅提供 API 服务。减少资源占用和攻击面，适用于纯 API 使用场景或与其他前端集成的部署。",
            .Category = "server",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--embeddings",
            .Explanation = "Enable embeddings | 启用嵌入。仅支持嵌入使用场景，仅适用于专用嵌入模型。提供文本嵌入向量生成功能，用于相似性搜索、聚类和语义理解等任务。",
            .Category = "server",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--reranking",
            .Explanation = "Enable reranking | 启用重排序。在服务器上启用重排序端点，用于改进搜索结果排序。提供文档重排序功能，提高检索系统的准确性和相关性。",
            .Category = "server",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--api-key",
            .Explanation = "API key | API 密钥。用于身份验证的 API 密钥，默认为无。保护服务器免受未授权访问的重要安全措施。建议在生产环境中设置强密钥。",
            .Category = "server",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--api-key-file",
            .Explanation = "API key file | API 密钥文件。包含 API 密钥的文件路径，支持多个密钥管理。提供更安全的密钥存储方式，避免在命令行中暴露敏感信息。",
            .Category = "server",
            .Editor = "filepath",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--ssl-key-file",
            .Explanation = "SSL key file | SSL 密钥文件。PEM 编码的 SSL 私钥文件路径。启用 HTTPS 加密通信，保护数据传输安全。需要与 SSL 证书文件配合使用。",
            .Category = "server",
            .Editor = "filepath",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--ssl-cert-file",
            .Explanation = "SSL certificate file | SSL 证书文件。PEM 编码的 SSL 证书文件路径。启用 HTTPS 安全连接，验证服务器身份。建议使用受信任 CA 签发的证书。",
            .Category = "server",
            .Editor = "filepath",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--cont-batching",
            .Explanation = "Enable continuous batching | 启用连续批处理。启用连续批处理（也称为动态批处理），提高多用户场景下的吞吐量和资源利用率。默认启用，是现代 LLM 服务的标准配置。",
            .Category = "server",
            .Editor = "checkbox",
            .DefaultValue = True
        },
        New ServerParameterMetadata With {
            .Argument = "--no-cont-batching",
            .Explanation = "Disable continuous batching | 禁用连续批处理。禁用连续批处理，回退到传统批处理模式。可能在某些特定场景下需要，但通常会降低整体性能。",
            .Category = "server",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--props",
            .Explanation = "Enable properties | 启用属性。启用通过 POST /props 更改全局属性的端点。允许运行时配置调整，提供动态参数管理能力。",
            .Category = "server",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--metrics",
            .Explanation = "Enable metrics | 启用指标。启用与 Prometheus 兼容的指标端点，提供性能监控数据。便于集成到监控系统中，实现实时性能跟踪和告警。",
            .Category = "server",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--slots",
            .Explanation = "Enable slots | 启用槽位。启用槽位监控端点，提供处理槽位的实时状态信息。默认启用，对于监控和调试多用户并发处理非常重要。",
            .Category = "server",
            .Editor = "checkbox",
            .DefaultValue = True
        },
        New ServerParameterMetadata With {
            .Argument = "--no-slots",
            .Explanation = "Disable slots | 禁用槽位。禁用槽位监控端点，减少监控开销但失去处理状态可见性。在不需要监控的简单部署中可考虑使用。",
            .Category = "server",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--slot-save-path",
            .Explanation = "Slot save path | 槽位保存路径。保存槽位 KV 缓存的路径，默认禁用。支持持久化处理状态，实现会话恢复和状态保持功能。",
            .Category = "server",
            .Editor = "directory",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--jinja",
            .Explanation = "Enable Jinja templating | 启用 Jinja 模板。使用 Jinja 模板进行聊天，提供更灵活的提示模板支持。允许自定义复杂的提示构建逻辑，满足特定对话需求。",
            .Category = "server",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--reasoning-format",
            .Explanation = "Reasoning format | 推理格式。控制是否允许和/或从响应中提取思考标签，以及返回格式：none（思想保留在 message.content 中）、deepseek（思想放在 message.reasoning_content 中）。默认为 auto。",
            .Category = "server",
            .Editor = "textbox",
            .DefaultValue = "auto"
        },
        New ServerParameterMetadata With {
            .Argument = "--reasoning-budget",
            .Explanation = "Reasoning budget | 推理预算。控制允许的思考量，目前只有 -1 表示无限制思考预算，或 0 表示禁用思考。默认为 -1。平衡思考深度和响应速度。",
            .Category = "server",
            .Editor = "numberupdown",
            .DefaultValue = -1
        },
        New ServerParameterMetadata With {
            .Argument = "--no-context-shift",
            .Explanation = "Disable context shift | 禁用上下文切换。在无限文本生成时禁用上下文切换，默认启用。可能在某些特定生成任务中需要保持严格的上下文连续性。",
            .Category = "server",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--context-shift",
            .Explanation = "Enable context shift | 启用上下文切换。在无限文本生成时启用上下文切换，默认禁用。允许处理超出上下文窗口的长文本，但可能丢失一些早期信息。",
            .Category = "server",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--chat-template",
            .Explanation = "Set custom jinja chat template | 设置自定义 jinja 聊天模板。指定自定义的 Jinja 聊天模板，用于格式化对话输入。如果指定了 suffix/prefix，模板将被禁用。除非在此标志前设置 --jinja，否则只接受常用的内置模板。内置模板列表：bailing, chatglm3, chatglm4, chatml, command-r, deepseek, deepseek2, deepseek3, exaone3, exaone4, falcon3, gemma, gigachat, glmedge, gpt-oss, granite, hunyuan-dense, hunyuan-moe, kimi-k2, llama2, llama2-sys, llama2-sys-bos, llama2-sys-strip, llama3, llama4, megrez, minicpm, mistral-v1, mistral-v3, mistral-v3-tekken, mistral-v7, mistral-v7-tekken, monarch, openchat, orion, phi3, phi4, rwkv-world, seed_oss, smolvlm, vicuna, vicuna-orca, yandex, zephyr",
            .Category = "chat",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--chat-template-file",
            .Explanation = "Set custom jinja chat template file | 设置自定义 jinja 聊天模板文件。从文件加载自定义的 Jinja 聊天模板，用于格式化对话输入。如果指定了 suffix/prefix，模板将被禁用。除非在此标志前设置 --jinja，否则只接受常用的内置模板。内置模板列表：bailing, chatglm3, chatglm4, chatml, command-r, deepseek, deepseek2, deepseek3, exaone3, exaone4, falcon3, gemma, gigachat, glmedge, gpt-oss, granite, hunyuan-dense, hunyuan-moe, kimi-k2, llama2, llama2-sys, llama2-sys-bos, llama2-sys-strip, llama3, llama4, megrez, minicpm, mistral-v1, mistral-v3, mistral-v3-tekken, mistral-v7, mistral-v7-tekken, monarch, openchat, orion, phi3, phi4, rwkv-world, seed_oss, smolvlm, vicuna, vicuna-orca, yandex, zephyr",
            .Category = "chat",
            .Editor = "filepath",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--chat-template-kwargs",
            .Explanation = "Set additional params for the json template parser | 为 json 模板解析器设置额外参数。为 Jinja 模板解析器提供额外的配置参数，用于自定义模板行为和渲染选项。允许调整模板的解析方式和变量处理。",
            .Category = "chat",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--no-prefill-assistant",
            .Explanation = "Whether to prefill the assistant's response if the last message is an assistant message | 是否在最后一条消息是助手消息时预填充助手的回复。默认启用预填充。设置此标志后，如果最后一条消息是助手消息，则将其视为完整消息而不进行预填充。适用于需要完全控制助手响应的场景。",
            .Category = "chat",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--model-alias",
            .Explanation = "Set alias for model name (to be used by REST API) | 为模型名称设置别名（供 REST API 使用）。为模型指定一个友好的别名，在 API 调用中使用此别名而不是模型文件名。便于模型管理和版本控制，支持在运行时动态切换模型。",
            .Category = "chat",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--reverse-prompt",
            .Explanation = "Halt generation at PROMPT, return control in interactive mode | 在指定提示符处停止生成，在交互模式下返回控制权。当模型生成到指定的提示字符串时自动停止，适用于对话系统、代码生成等需要精确控制输出长度的场景。",
            .Category = "chat",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--special",
            .Explanation = "Special tokens output enabled | 启用特殊 token 输出。控制是否在输出中包含特殊 token（如 BOS、EOS、PAD 等）。默认为 false。对于需要完整 token 级别控制的场景（如模型调试、数据预处理）很有用。",
            .Category = "chat",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--no-warmup",
            .Explanation = "Skip warming up the model with an empty run | 跳过使用空运行预热模型。在开始正式推理前跳过模型预热步骤，可加快启动速度但可能影响第一次推理的性能。适用于快速测试和开发环境。",
            .Category = "chat",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--spm-infill",
            .Explanation = "Use Suffix/Prefix/Middle pattern for infill (instead of Prefix/Suffix/Middle) as some models prefer this | 使用 Suffix/Prefix/Middle 模式进行填充（而不是 Prefix/Suffix/Middle），因为某些模型更倾向于这种模式。默认为禁用。适用于代码补全、文本修复等填充任务，不同的模型可能需要不同的填充模式。",
            .Category = "chat",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--mmproj",
            .Explanation = "Path to a multimodal projector file. see tools/mtmd/README.md. note: if -hf is used, this argument can be omitted | 多模态投影文件路径。参见 tools/mtmd/README.md。注意：如果使用 -hf 参数，此参数可以省略。多模态投影文件用于处理图像、视频等多模态输入，使模型能够理解和生成与视觉内容相关的文本响应。",
            .Category = "multimodal",
            .Editor = "filepath",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--mmproj-url",
            .Explanation = "URL to a multimodal projector file. see tools/mtmd/README.md | 多模态投影文件 URL。参见 tools/mtmd/README.md。从网络 URL 加载多模态投影文件，支持远程部署和动态下载多模态处理组件。确保 URL 可访问且文件格式兼容。",
            .Category = "multimodal",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--no-mmproj",
            .Explanation = "Explicitly disable multimodal projector, useful when using -hf | 显式禁用多模态投影器，在使用 -hf 时很有用。强制禁用多模态功能，即使在使用 Hugging Face 模型时也是如此。适用于纯文本推理场景，避免不必要的多模态组件加载。",
            .Category = "multimodal",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--no-mmproj-offload",
            .Explanation = "Do not offload multimodal projector to GPU (default: false) | 不将多模态投影器卸载到 GPU。强制多模态投影器在 CPU 上运行，避免 GPU 内存占用。在 GPU 内存有限或遇到兼容性问题时使用。可能影响性能，但提高系统稳定性。",
            .Category = "multimodal",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--pooling",
            .Explanation = "Pooling type for embeddings, use model default if unspecified | 嵌入的池化类型，如果未指定则使用模型默认值。池化类型影响如何将 token 级别的嵌入聚合为句子级别或文档级别的表示。可选值：none（不池化）、mean（平均值池化）、cls（CLS token 池化）、last（最后一个 token 池化）、rank（排序池化）。",
            .Category = "embedding",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--embd-bge-small-en-default",
            .Explanation = "Use default bge-small-en-v1.5 model (note: can download weights from the internet) | 使用默认的 bge-small-en-v1.5 模型（注意：可以从互联网下载权重）。BGE（BAAI General Embedding）是一个高质量的英文文本嵌入模型，适用于语义搜索、文本相似度计算等任务。启用后将自动下载所需模型文件。",
            .Category = "embedding",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--embd-e5-small-en-default",
            .Explanation = "Use default e5-small-v2 model (note: can download weights from the internet) | 使用默认的 e5-small-v2 模型（注意：可以从互联网下载权重）。E5 是一个基于对比学习的文本嵌入模型，在各种语义相似度任务中表现优异。特别适合短文本匹配和语义检索应用。",
            .Category = "embedding",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--embd-gte-small-default",
            .Explanation = "Use default gte-small model (note: can download weights from the internet) | 使用默认的 gte-small 模型（注意：可以从互联网下载权重）。GTE（General Text Embedding）是一个通用的文本嵌入模型，在多种语言和任务上都具有良好的性能，特别适合多语言应用场景。",
            .Category = "embedding",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--model-draft",
            .Explanation = "Draft model for speculative decoding (default: unused) | 用于投机解码的草稿模型（默认：未使用）。投机解码使用一个较小的草稿模型快速生成候选 token，然后由主模型验证，可显著提高推理速度。草稿模型应该比主模型小但质量相近，以确保较高的接受率。",
            .Category = "draft",
            .Editor = "filepath",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--ctx-size-draft",
            .Explanation = "Size of the prompt context for the draft model (default: 0, 0 = loaded from model) | 草稿模型的提示词上下文大小（默认：0，0 = 从模型加载）。草稿模型的上下文窗口大小，可以与主模型不同。设置为 0 将从模型元数据中加载默认值。较大的上下文可能提高草稿质量但增加内存使用。",
            .Category = "draft",
            .Editor = "numberupdown",
            .DefaultValue = 0
        },
        New ServerParameterMetadata With {
            .Argument = "--threads-draft",
            .Explanation = "Number of threads to use during generation (default: same as --threads) | 生成期间使用的线程数（默认：与 --threads 相同）。草稿模型推理时使用的 CPU 线程数，默认与主模型线程设置相同。可以根据草稿模型的特性和系统负载进行独立调整以优化性能。",
            .Category = "draft",
            .Editor = "numberupdown",
            .DefaultValue = -1
        },
        New ServerParameterMetadata With {
            .Argument = "--threads-batch-draft",
            .Explanation = "Number of threads to use during batch and prompt processing (default: same as --threads-draft) | 批处理和提示词处理期间使用的线程数（默认：与 --threads-draft 相同）。草稿模型的批处理专用线程数，默认与草稿生成线程相同。分开设置可以优化批处理和生成的性能平衡。",
            .Category = "draft",
            .Editor = "numberupdown",
            .DefaultValue = -1
        },
        New ServerParameterMetadata With {
            .Argument = "--device-draft",
            .Explanation = "Comma-separated list of devices to use for offloading the draft model (none = don't offload). Use --list-devices to see a list of available devices | 用于卸载草稿模型的逗号分隔设备列表（none = 不卸载）。使用 --list-devices 查看可用设备列表。草稿模型可以与主模型使用不同的设备配置，以优化资源利用和性能。",
            .Category = "draft",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--n-gpu-layers-draft",
            .Explanation = "Number of layers to store in VRAM for the draft model | 存储在草稿模型 VRAM 中的层数。草稿模型卸载到 GPU 的神经网络层数，可以与主模型的 GPU 层数设置不同。合理的设置可以在保证性能的同时最大化资源利用效率。",
            .Category = "draft",
            .Editor = "numberupdown",
            .DefaultValue = 0
        },
        New ServerParameterMetadata With {
            .Argument = "--draft-max",
            .Explanation = "Number of tokens to draft for speculative decoding (default: 16) | 投机解码的草稿 token 数量（默认：16）。草稿模型单次生成的最大 token 数，影响投机解码的效率。较大的值可能提高速度但增加草稿被拒绝的风险，需要根据模型质量和应用场景进行调整。",
            .Category = "draft",
            .Editor = "numberupdown",
            .DefaultValue = 16
        },
        New ServerParameterMetadata With {
            .Argument = "--draft-min",
            .Explanation = "Minimum number of draft tokens to use for speculative decoding (default: 0) | 投机解码使用的最小草稿 token 数量（默认：0）。当草稿生成质量较低时，实际生成的 token 数量可能低于此值。设置为 0 表示不限制最小值，允许灵活的投机策略。",
            .Category = "draft",
            .Editor = "numberupdown",
            .DefaultValue = 0
        },
        New ServerParameterMetadata With {
            .Argument = "--draft-p-min",
            .Explanation = "Minimum speculative decoding probability (greedy) (default: 0.8) | 最小投机解码概率（贪婪模式）（默认：0.8）。草稿 token 被接受的最小概率阈值，高于此值的 token 将被接受。较高的阈值提高输出质量但可能减少速度提升，较低的阈值增加速度但可能降低质量。",
            .Category = "draft",
            .Editor = "numberupdown",
            .DefaultValue = 0.8
        },
        New ServerParameterMetadata With {
            .Argument = "--cache-type-k-draft",
            .Explanation = "KV cache data type for K for the draft model. Allowed values: f32, f16, bf16, q8_0, q4_0, q4_1, iq4_nl, q5_0, q5_1 (default: f16) | 草稿模型的 KV 缓存 K 键数据类型。允许值：f32, f16, bf16, q8_0, q4_0, q4_1, iq4_nl, q5_0, q5_1（默认：f16）。草稿模型的 KV 缓存数据类型可以与主模型不同，允许在精度和内存效率间进行独立优化。",
            .Category = "draft",
            .Editor = "textbox",
            .DefaultValue = "f16"
        },
        New ServerParameterMetadata With {
            .Argument = "--cache-type-v-draft",
            .Explanation = "KV cache data type for V for the draft model. Allowed values: f32, f16, bf16, q8_0, q4_0, q4_1, iq4_nl, q5_0, q5_1 (default: f16) | 草稿模型的 KV 缓存 V 值数据类型。允许值：f32, f16, bf16, q8_0, q4_0, q4_1, iq4_nl, q5_0, q5_1（默认：f16）。通常与 K 设置相同类型以保持一致性，但可以根据草稿模型的特性进行独立配置。",
            .Category = "draft",
            .Editor = "textbox",
            .DefaultValue = "f16"
        },
        New ServerParameterMetadata With {
            .Argument = "--override-tensor-draft",
            .Explanation = "Override tensor buffer type for draft model | 覆盖草稿模型的张量缓冲区类型。允许为草稿模型指定不同的张量缓冲区类型，格式为 <张量名称模式>=<缓冲区类型>。可用于优化草稿模型的内存使用和计算效率，或解决兼容性问题。",
            .Category = "draft",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--spec-replace",
            .Explanation = "Translate the string in TARGET into DRAFT if the draft model and main model are not compatible | 如果草稿模型和主模型不兼容，将 TARGET 中的字符串翻译为 DRAFT。当草稿模型和主模型使用不同的 tokenizer 或词汇表时，此参数可用于处理 token 映射问题，确保投机解码的正确工作。",
            .Category = "draft",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--model-vocoder",
            .Explanation = "Vocoder model for audio generation (default: unused) | 用于音频生成的声码器模型（默认：未使用）。声码器模型将文本转换为语音，实现文本到语音（TTS）功能。需要与支持的语音合成模型配合使用，可生成自然流畅的语音输出。",
            .Category = "vocoder",
            .Editor = "filepath",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--tts-use-guide-tokens",
            .Explanation = "Use guide tokens to improve TTS word recall | 使用引导标记改善 TTS 词召回率。在文本到语音合成中使用引导标记来提高单词识别准确性。通过在文本中添加特殊的引导标记，帮助模型更好地理解单词边界和发音，从而改善语音合成的质量。",
            .Category = "vocoder",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--swa-checkpoints",
            .Explanation = "Max number of SWA checkpoints per slot to create (default: 3). See [more info](https://github.com/ggml-org/llama.cpp/pull/15293) | 每个 slot 创建的最大 SWA 检查点数（默认：3）。[更多信息](https://github.com/ggml-org/llama.cpp/pull/15293)。滑动窗口注意力（SWA）检查点用于管理长上下文处理中的状态，更多的检查点可以提高长文本处理的连续性但增加内存使用。",
            .Category = "checkpoint",
            .Editor = "numberupdown",
            .DefaultValue = 3
        },
        New ServerParameterMetadata With {
            .Argument = "--cache-reuse",
            .Explanation = "Min chunk size to attempt reusing from the cache via KV shifting (default: 0). See [card](https://ggml.ai/f0.png) | 通过 KV 移位尝试重用缓存的最小块大小（默认：0）。参见[卡片](https://ggml.ai/f0.png)。缓存重用机制可以通过移动 KV 缓存来重用之前计算的状态，提高连续推理的效率，特别是在相似的提示词序列中。",
            .Category = "functionality",
            .Editor = "numberupdown",
            .DefaultValue = 0
        },
        New ServerParameterMetadata With {
            .Argument = "--slot-prompt-similarity",
            .Explanation = "How much the prompt of a request must match the prompt of a slot in order to use that slot (default: 0.50, 0.0 = disabled) | 请求的提示词必须与 slot 的提示词匹配的程度才能使用该 slot（默认：0.50，0.0 = 禁用）。slot 相似度阈值用于决定是否重用现有的处理 slot，较高的值提高重用的准确性但可能降低命中率，较低的值增加重用机会但可能影响质量。",
            .Category = "functionality",
            .Editor = "numberupdown",
            .DefaultValue = 0.5
        },
        New ServerParameterMetadata With {
            .Argument = "--list-devices",
            .Explanation = "Print list of available devices and exit | 打印可用设备列表并退出。列出系统中所有可用于模型推理的设备（CPU、GPU 等），包括设备类型、内存信息和支持的功能。帮助用户了解可用的硬件资源和进行设备选择配置。",
            .Category = "functionality",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--help",
            .Explanation = "Print usage and exit | 打印使用说明并退出。显示完整的命令行参数列表和简短描述，帮助用户了解所有可用的配置选项。包含所有参数的基本用法、默认值和简短说明，是获取帮助信息的主要方式。",
            .Category = "help",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--usage",
            .Explanation = "Print usage and exit | 打印使用说明并退出。与 --help 功能相同，显示命令行的使用方法和参数说明。提供另一种获取帮助信息的方式，确保用户能够方便地访问使用文档。",
            .Category = "help",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--version",
            .Explanation = "Show version and build info | 显示版本和构建信息。输出当前软件的版本号、构建日期、提交信息等详细信息。用于版本确认、问题诊断和功能兼容性检查，确保用户了解正在使用的具体版本。",
            .Category = "help",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--completion-bash",
            .Explanation = "Print source-able bash completion script for llama.cpp | 打印可用于 source 的 llama.cpp bash 完成脚本。输出 bash shell 的命令行自动完成脚本，可用于配置 shell 自动完成功能。提供更便捷的命令行使用体验，减少手动输入参数的工作量。",
            .Category = "help",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--fim-qwen-1.5b-default",
            .Explanation = "Use default Qwen 2.5 Coder 1.5B (note: can download weights from the internet) | 使用默认的 Qwen 2.5 Coder 1.5B（注意：可以从互联网下载权重）。这是一个专门用于代码填充（Fill-in-the-Middle）的 1.5B 参数模型，适用于代码补全、代码修复等任务。启用后将自动下载所需模型文件。",
            .Category = "preset",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--fim-qwen-3b-default",
            .Explanation = "Use default Qwen 2.5 Coder 3B (note: can download weights from the internet) | 使用默认的 Qwen 2.5 Coder 3B（注意：可以从互联网下载权重）。这是一个 3B 参数的代码填充模型，在代码补全质量上优于 1.5B 版本，适用于更复杂的代码生成和修复任务。平衡了性能和资源使用。",
            .Category = "preset",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--fim-qwen-7b-default",
            .Explanation = "Use default Qwen 2.5 Coder 7B (note: can download weights from the internet) | 使用默认的 Qwen 2.5 Coder 7B（注意：可以从互联网下载权重）。这是一个 7B 参数的高质量代码填充模型，在代码理解、生成和修复方面具有出色的性能。适用于对代码质量要求较高的专业场景。",
            .Category = "preset",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--fim-qwen-7b-spec",
            .Explanation = "Use Qwen 2.5 Coder 7B + 0.5B draft for speculative decoding (note: can download weights from the internet) | 使用 Qwen 2.5 Coder 7B + 0.5B 草稿模型进行投机解码（注意：可以从互联网下载权重）。这是一个优化的代码填充配置，使用 7B 主模型配合 0.5B 草稿模型，通过投机解码技术显著提高推理速度，同时保持高质量的代码生成能力。",
            .Category = "preset",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--fim-qwen-14b-spec",
            .Explanation = "Use Qwen 2.5 Coder 14B + 0.5B draft for speculative decoding (note: can download weights from the internet) | 使用 Qwen 2.5 Coder 14B + 0.5B 草稿模型进行投机解码（注意：可以从互联网下载权重）。这是一个高性能的代码填充配置，使用 14B 主模型配合 0.5B 草稿模型，提供顶级的代码理解、生成和修复能力，同时通过投机解码实现快速的推理速度。",
            .Category = "preset",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--fim-qwen-30b-default",
            .Explanation = "Use default Qwen 3 Coder 30B A3B Instruct (note: can download weights from the internet) | 使用默认的 Qwen 3 Coder 30B A3B Instruct（注意：可以从互联网下载权重）。这是一个超大型的 30B 参数代码模型，具有最强的代码理解和生成能力，适用于最复杂的代码任务和专业的开发场景。需要较高的硬件配置才能有效运行。",
            .Category = "preset",
            .Editor = "checkbox",
            .DefaultValue = False
        }
    }

    Public Shared Function GetMetadataByArgument(argument As String) As ServerParameterMetadata
        Return AllParameters.FirstOrDefault(Function(p) p.Argument = argument)
    End Function
End Class
