Public Class ServerParameterMetadata
    Public Property Argument As String
    Public Property Explanation As String
    Public Property Category As String
    Public Property Editor As String
    Public Property DefaultValue As Object

    Public Shared ReadOnly Property AllParameters As New List(Of ServerParameterMetadata) From {
        New ServerParameterMetadata With {
            .Argument = "--server-path",
            .Explanation = "Server executable path | 服务器可执行文件路径。指定 llama.cpp HTTP 服务器的可执行文件位置，可以是绝对路径或相对路径。默认为空的字符串，需要用户手动指定。",
            .Category = "common",
            .Editor = "filepath",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--model",
            .Explanation = "Model file path | 模型文件路径。指定要加载的 GGUF 模型文件路径，支持本地文件路径或网络 URL。如果使用 --hf-repo 或 --model-url，此参数可选。默认为 models/$filename 格式。",
            .Category = "common",
            .Editor = "filepath",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--threads",
            .Explanation = "Number of threads to use | 使用的线程数量。指定模型推理过程中使用的 CPU 线程数，直接影响推理速度和 CPU 使用率。建议设置为 CPU 逻辑核心数。默认为 -1 表示自动检测。",
            .Category = "common",
            .Editor = "numberupdown",
            .DefaultValue = 4
        },
        New ServerParameterMetadata With {
            .Argument = "--ctx-size",
            .Explanation = "Context size | 上下文窗口大小。指定提示词上下文的最大 token 数量，影响模型能处理的输入长度。更大的上下文需要更多内存。默认为 4096，0 表示从模型加载。",
            .Category = "common",
            .Editor = "numberupdown",
            .DefaultValue = 4096
        },
        New ServerParameterMetadata With {
            .Argument = "--n-predict",
            .Explanation = "Number of tokens to predict | 预测的 token 数量。控制每次生成输出的最大 token 数，防止无限生成。-1 表示无限制。适用于聊天、代码生成等任务。",
            .Category = "common",
            .Editor = "numberupdown",
            .DefaultValue = -1
        },
        New ServerParameterMetadata With {
            .Argument = "--batch-size",
            .Explanation = "Batch size for processing | 批处理大小。逻辑最大批处理大小，影响同时处理的请求数量。较大的批处理可提高吞吐量但增加内存使用。适用于多用户并发场景。",
            .Category = "common",
            .Editor = "numberupdown",
            .DefaultValue = 2048
        },
        New ServerParameterMetadata With {
            .Argument = "--ubatch-size",
            .Explanation = "Micro batch size | 微批处理大小。物理最大批处理大小，实际处理时的最小单位。较小的微批处理可提高内存效率，但可能略微降低性能。",
            .Category = "common",
            .Editor = "numberupdown",
            .DefaultValue = 512
        },
        New ServerParameterMetadata With {
            .Argument = "--threads-batch",
            .Explanation = "Threads for batch processing | 批处理线程数。专门用于批处理和提示词处理的线程数，通常与生成线程分开设置。默认与 --threads 相同。",
            .Category = "common",
            .Editor = "numberupdown",
            .DefaultValue = -1
        },
        New ServerParameterMetadata With {
            .Argument = "--n-gpu-layers",
            .Explanation = "Number of GPU layers | GPU 层数。指定卸载到 VRAM 的神经网络层数，可显著加速推理。0 表示仅使用 CPU。设置为 -1 或足够大的数字可将整个模型移至 GPU。",
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
            .Explanation = "Strict CPU affinity for batch | 批处理严格 CPU 亲和性。批处理操作的严格 CPU 放置，默认与 --cpu-strict 相同。确保批处理任务在指定的 CPU 上运行。",
            .Category = "hardware",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--prio-batch",
            .Explanation = "Process priority for batch | 批处理进程优先级。批处理操作的进程/线程优先级：0-normal、1-medium、2-high、3-realtime。用于优化批处理任务的调度。",
            .Category = "hardware",
            .Editor = "numberupdown",
            .DefaultValue = 0
        },
        New ServerParameterMetadata With {
            .Argument = "--poll-batch",
            .Explanation = "Poll interval for batch | 批处理轮询间隔。批处理操作的轮询级别，默认与 --poll 相同。用于优化批处理任务的响应速度和 CPU 使用。",
            .Category = "hardware",
            .Editor = "numberupdown",
            .DefaultValue = 50
        },
        New ServerParameterMetadata With {
            .Argument = "--no-kv-offload",
            .Explanation = "Disable KV cache offload | 禁用 KV 缓存卸载。阻止将 KV 缓存卸载到 GPU，强制所有 KV 缓存保留在 CPU 内存中。在 GPU 内存有限时可用，但可能影响推理速度。",
            .Category = "kv-cache",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--no-repack",
            .Explanation = "Disable repacking | 禁用权重重新打包。不重新打包权重，可能在某些情况下加快加载速度，但可能影响后续推理性能。通常建议启用重新打包以获得最佳性能。",
            .Category = "kv-cache",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--cache-type-k",
            .Explanation = "Cache type for K | K 缓存类型。KV 缓存中 K 键的数据类型，可选值：f32、f16、bf16、q8_0、q4_0、q4_1、iq4_nl、q5_0、q5_1。影响内存占用和精度，f16 是较好的平衡。",
            .Category = "kv-cache",
            .Editor = "textbox",
            .DefaultValue = "f16"
        },
        New ServerParameterMetadata With {
            .Argument = "--cache-type-v",
            .Explanation = "Cache type for V | V 缓存类型。KV 缓存中 V 值的数据类型，可选值与 K 相同。通常与 K 设置相同类型以保持一致性，可根据精度需求选择。",
            .Category = "kv-cache",
            .Editor = "textbox",
            .DefaultValue = "f16"
        },
        New ServerParameterMetadata With {
            .Argument = "--defrag-thold",
            .Explanation = "Defragmentation threshold | KV 缓存碎片整理阈值。当碎片超过此阈值时触发碎片整理，提高内存利用率。DEPRECATED 参数，可能在未来版本中被移除。",
            .Category = "kv-cache",
            .Editor = "numberupdown",
            .DefaultValue = 0.5
        },
        New ServerParameterMetadata With {
            .Argument = "--kv-unified",
            .Explanation = "Unified KV cache | 统一 KV 缓存。使用单一统一的 KV 缓冲区存储所有序列的 KV 缓存，而不是为每个序列分别分配。可提高内存效率，但可能在某些场景下影响性能。",
            .Category = "kv-cache",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--swa-full",
            .Explanation = "Sliding window attention full | 全尺寸滑动窗口注意力。使用完整大小的 SWA 缓存，提高长上下文处理的性能和内存效率。适用于处理超长文本或需要大量历史上下文的场景。",
            .Category = "kv-cache",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--temperature",
            .Explanation = "Temperature for sampling | 采样温度。控制生成文本的随机性，较低的值（如 0.2）使输出更确定性，较高的值（如 1.0）增加随机性和创造性。默认 0.8 提供良好的平衡。",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 0.8
        },
        New ServerParameterMetadata With {
            .Argument = "--top-p",
            .Explanation = "Top-p sampling | Top-p 核心采样。累积概率达到 p 的最小 token 集合中进行采样，限制候选词数量。0.9 表示从累积概率 90% 的词中选择。1.0 表示禁用。适用于控制输出的多样性。",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 0.9
        },
        New ServerParameterMetadata With {
            .Argument = "--top-k",
            .Explanation = "Top-k sampling | Top-k 采样。从概率最高的 k 个 token 中随机选择，限制候选词数量。较高的 k 值增加多样性，较低的 k 值使输出更集中。0 表示禁用，仅从最可能的词中选择。",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 40
        },
        New ServerParameterMetadata With {
            .Argument = "--min-p",
            .Explanation = "Min-p sampling | Min-p 采样。最小概率采样，过滤掉概率低于最高概率乘以 p 的 token。可防止低质量 token 被选中，提高输出质量。0.0 表示禁用。",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 0.1
        },
        New ServerParameterMetadata With {
            .Argument = "--top-n-sigma",
            .Explanation = "Top-n sigma sampling | Top-n sigma 采样。基于标准差的采样方法，考虑概率分布的标准差。负值表示禁用。提供另一种控制输出多样性的方法，适用于特定场景。",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = -1.0
        },
        New ServerParameterMetadata With {
            .Argument = "--xtc-probability",
            .Explanation = "XTC probability | XTC 概率。排除最常见 token 的概率，用于减少重复和常见短语。0.0 表示禁用，较高的值可以增加文本的原创性和多样性。",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 0.0
        },
        New ServerParameterMetadata With {
            .Argument = "--xtc-threshold",
            .Explanation = "XTC threshold | XTC 阈值。XTC 采样的阈值参数，控制排除 token 的严格程度。1.0 表示禁用。与 xtc-probability 配合使用，共同控制文本生成的特征。",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 0.1
        },
        New ServerParameterMetadata With {
            .Argument = "--typical",
            .Explanation = "Typical sampling | 典型采样。局部典型采样，基于 token 的典型性进行选择。参数 p 控制典型性阈值。1.0 表示禁用。适用于生成更加自然和预期的文本。",
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
            .Explanation = "Repeat penalty | 重复惩罚。惩罚重复的 token 序列，减少文本重复。1.0 表示禁用，大于 1.0 的值会降低重复 token 的概率。适用于减少重复性回答和提高文本多样性。",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 1.0
        },
        New ServerParameterMetadata With {
            .Argument = "--presence-penalty",
            .Explanation = "Presence penalty | 存在惩罚。alpha 存在惩罚，基于 token 是否已经出现过进行惩罚。0.0 表示禁用。正数鼓励讨论新话题，负数鼓励重复。适用于保持对话的多样性和新鲜度。",
            .Category = "sampling",
            .Editor = "numberupdown",
            .DefaultValue = 0.0
        },
        New ServerParameterMetadata With {
            .Argument = "--frequency-penalty",
            .Explanation = "Frequency penalty | 频率惩罚。alpha 频率惩罚，基于 token 出现的频率进行惩罚。0.0 表示禁用。比存在惩罚更直接地控制重复，适用于精细调整文本生成质量。",
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
            .Explanation = "DRY sequence breaker | DRY 序列分隔符。添加 DRY 采样的序列分隔符，清除默认分隔符（'\n', ':', '""', '*'）。使用 'none' 不使用任何分隔符。用于自定义重复检测的边界。",
            .Category = "sampling",
            .Editor = "textbox",
            .DefaultValue = ""
        },
        New ServerParameterMetadata With {
            .Argument = "--dynatemp-range",
            .Explanation = "Dynamic temperature range | 动态温度范围。动态温度范围，根据生成上下文动态调整温度。0.0 表示禁用。可以根据上下文内容自动调整输出的随机性，提供更智能的生成控制。",
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
            .Explanation = "RoPE scale factor | RoPE 缩放因子。RoPE 上下文缩放因子，将上下文扩展 N 倍。例如 2.0 表示将上下文长度翻倍。适用于处理超出原始训练长度的长文本，但可能影响性能。",
            .Category = "rope",
            .Editor = "numberupdown",
            .DefaultValue = 1.0
        },
        New ServerParameterMetadata With {
            .Argument = "--rope-freq-base",
            .Explanation = "RoPE frequency base | RoPE 频率基础。RoPE 基础频率，用于 NTK 感知缩放。默认从模型加载。影响位置编码的频率分布，可调整以适应不同的上下文扩展需求。",
            .Category = "rope",
            .Editor = "numberupdown",
            .DefaultValue = 0.0
        },
        New ServerParameterMetadata With {
            .Argument = "--rope-freq-scale",
            .Explanation = "RoPE frequency scale | RoPE 频率缩放。RoPE 频率缩放因子，将上下文扩展 1/N 倍。与 rope-scale 相反作用，用于精细调整位置编码的缩放比例。",
            .Category = "rope",
            .Editor = "numberupdown",
            .DefaultValue = 1.0
        },
        New ServerParameterMetadata With {
            .Argument = "--yarn-orig-ctx",
            .Explanation = "YARN original context | YARN 原始上下文。YaRN：模型的原始上下文大小，0 表示模型训练上下文大小。用于 YaRN 扩展技术，确保位置编码的正确缩放。",
            .Category = "rope",
            .Editor = "numberupdown",
            .DefaultValue = 0
        },
        New ServerParameterMetadata With {
            .Argument = "--yarn-ext-factor",
            .Explanation = "YARN extension factor | YARN 扩展因子。YaRN：外推混合因子，-1.0 表示默认，0.0 表示完全插值。控制 YaRN 如何处理超出原始上下文范围的内容，影响长文本处理质量。",
            .Category = "rope",
            .Editor = "numberupdown",
            .DefaultValue = -1.0
        },
        New ServerParameterMetadata With {
            .Argument = "--yarn-attn-factor",
            .Explanation = "YARN attention factor | YARN 注意力因子。YaRN：缩放 sqrt(t) 或注意力幅度。影响注意力机制在长距离上下文中的表现，控制注意力得分的缩放。",
            .Category = "rope",
            .Editor = "numberupdown",
            .DefaultValue = 1.0
        },
        New ServerParameterMetadata With {
            .Argument = "--yarn-beta-slow",
            .Explanation = "YARN beta slow | YARN 慢速 beta。YaRN：高校正维度或 alpha。控制 YaRN 中较慢变化的校正参数，影响位置编码的长期行为。",
            .Category = "rope",
            .Editor = "numberupdown",
            .DefaultValue = 1.0
        },
        New ServerParameterMetadata With {
            .Argument = "--yarn-beta-fast",
            .Explanation = "YARN beta fast | YARN 快速 beta。YaRN：低校正维度或 beta。控制 YaRN 中快速变化的校正参数，影响位置编码的短期精度。",
            .Category = "rope",
            .Editor = "numberupdown",
            .DefaultValue = 32.0
        },
        New ServerParameterMetadata With {
            .Argument = "--flash-attn on",
            .Explanation = "Enable flash attention | 启用 Flash Attention。启用 Flash Attention 优化算法，显著提高注意力计算效率，减少内存使用和计算时间。需要硬件支持，可大幅提升长文本处理性能。",
            .Category = "hardware",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--n-parallel",
            .Explanation = "Number of parallel processes | 并行进程数量。并行解码的序列数量，支持多用户同时使用。较高的值可提高并发性能，但增加内存和计算资源需求。适用于多用户聊天、批量处理等场景。",
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
            .Explanation = "Disable operator offload | 禁用操作符卸载。禁用将主机张量操作卸载到设备的功能。在设备内存有限或遇到兼容性问题时使用。可能影响性能，但提高稳定性。",
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
            .Explanation = "Number of CPU MoE draft | CPU MoE 草稿数量。对于草稿模型，将前 N 层的混合专家（MoE）权重保持在 CPU 中。在投机解码中精细控制草稿模型的 MoE 行为。",
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
            .Explanation = "Disable performance metrics | 禁用性能指标。禁用内部 libllama 性能计时，减少开销但失去性能数据。在不需要性能分析的生产环境中可略微提高性能。",
            .Category = "logging",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--escape",
            .Explanation = "Escape special characters | 转义特殊字符。处理转义序列（\n, \r, \t, \', \"", \\）。确保文本输出的正确性和安全性，防止格式问题和注入攻击。",
            .Category = "logging",
            .Editor = "checkbox",
            .DefaultValue = True
        },
        New ServerParameterMetadata With {
            .Argument = "--verbose-prompt",
            .Explanation = "Verbose prompt | 详细提示。在生成之前打印详细的提示信息，显示实际发送给模型的完整提示内容。用于调试和验证提示构建的正确性。",
            .Category = "logging",
            .Editor = "checkbox",
            .DefaultValue = False
        },
        New ServerParameterMetadata With {
            .Argument = "--host",
            .Explanation = "Server host | 服务器主机。服务器监听的 IP 地址，或如果地址以 .sock 结尾则绑定到 UNIX 套接字。默认为 127.0.0.1（仅本地访问）。设置为 0.0.0.0 允许外部访问。",
            .Category = "network",
            .Editor = "textbox",
            .DefaultValue = "127.0.0.1"
        },
        New ServerParameterMetadata With {
            .Argument = "--port",
            .Explanation = "Server port | 服务器端口。服务器监听的端口号，默认为 8080。确保端口未被其他服务占用，且防火墙配置允许访问。支持 1-65535 范围内的端口号。",
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