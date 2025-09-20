# ServerParameterMetadata English Explanation Update Plan

This document tracks the systematic update of English explanations in ServerParameterMetadata.vb to match the comprehensive detail level found in README.server.md.

## Status Legend
- [ ] Pending
- [x] Completed
- [🔄] In Progress

---

## Chunk 1: Common Parameters (Part 1) - Help & Basic Configuration
**Status: [ ] Pending**

1. **`-h, --help, --usage`**
   - Current: "Print usage and exit | 打印使用说明并退出"
   - README: "print usage and exit"
   - Target: "Print usage and exit | 打印使用说明并退出。显示完整的命令行参数列表和简短描述，帮助用户了解所有可用的配置选项。包含所有参数的基本用法、默认值和简短说明，是获取帮助信息的主要方式。"

2. **`--version`**
   - Current: "Show version and build info | 显示版本和构建信息"
   - README: "show version and build info"
   - Target: "Show version and build info | 显示版本和构建信息。输出当前软件的版本号、构建日期、提交信息等详细信息。用于版本确认、问题诊断和功能兼容性检查，确保用户了解正在使用的具体版本。"

3. **`--completion-bash`**
   - Current: "Print source-able bash completion script for llama.cpp | 打印可用于 source 的 llama.cpp bash 完成脚本"
   - README: "print source-able bash completion script for llama.cpp"
   - Target: "Print source-able bash completion script for llama.cpp | 打印可用于 source 的 llama.cpp bash 完成脚本。输出 bash shell 的命令行自动完成脚本，可用于配置 shell 自动完成功能。提供更便捷的命令行使用体验，减少手动输入参数的工作量。"

4. **`--verbose-prompt`**
   - Current: "Verbose prompt | 详细提示"
   - README: "print a verbose prompt before generation (default: false)"
   - Target: "Print a verbose prompt before generation (default: false) | 在生成之前打印详细的提示信息，显示实际发送给模型的完整提示内容。用于调试和验证提示构建的正确性。"

5. **`-t, --threads N`**
   - Current: "Number of threads to use | 使用的线程数量"
   - README: "number of threads to use during generation (default: -1)<br/>(env: LLAMA_ARG_THREADS)"
   - Target: "Number of threads to use during generation (default: -1) (env: LLAMA_ARG_THREADS) | 生成过程中使用的线程数。指定模型推理过程中使用的 CPU 线程数，直接影响推理速度和 CPU 使用率。建议设置为 CPU 逻辑核心数。默认为 -1 表示自动检测。"

6. **`-tb, --threads-batch N`**
   - Current: "Threads for batch processing | 批处理线程数"
   - README: "number of threads to use during batch and prompt processing (default: same as --threads)"
   - Target: "Number of threads to use during batch and prompt processing (default: same as --threads) | 批处理和提示词处理期间使用的线程数，专门用于批处理和提示词处理的线程数，通常与生成线程分开设置。默认与 --threads 相同。"

7. **`-C, --cpu-mask M`**
   - Current: "CPU mask | CPU 掩码"
   - README: "CPU affinity mask: arbitrarily long hex. Complements cpu-range (default: "")"
   - Target: "CPU affinity mask: arbitrarily long hex. Complements cpu-range (default: "") | CPU 亲和性的十六进制掩码，与 cpu-range 互补。任意长度的十六进制字符串，用于精确控制进程在特定 CPU 核心上运行。"

8. **`-Cr, --cpu-range lo-hi`**
   - Current: "CPU range | CPU 范围"
   - README: "range of CPUs for affinity. Complements --cpu-mask"
   - Target: "Range of CPUs for affinity. Complements --cpu-mask | CPU 亲和性的范围，格式为 lo-hi。与 --cpu-mask 互补，用于指定进程可使用的 CPU 核心范围。"

9. **`--cpu-strict <0|1>`**
   - Current: "Strict CPU affinity | 严格 CPU 亲和性"
   - README: "use strict CPU placement (default: 0)<br/>"
   - Target: "Use strict CPU placement (default: 0) | 使用严格的 CPU 放置，确保进程只在指定的 CPU 上运行。提高性能稳定性，但可能降低灵活性。"

10. **`--prio N`**
    - Current: "Process priority | 进程优先级"
    - README: "set process/thread priority : low(-1), normal(0), medium(1), high(2), realtime(3) (default: 0)<br/>"
    - Target: "Set process/thread priority : low(-1), normal(0), medium(1), high(2), realtime(3) (default: 0) | 设置进程/线程优先级：low(-1)、normal(0)、medium(1)、high(2)、realtime(3)。较高的优先级可获得更多 CPU 时间，但可能影响系统稳定性。"

11. **`--poll <0...100>`**
    - Current: "Poll interval | 轮询间隔"
    - README: "use polling level to wait for work (0 - no polling, default: 50)<br/>"
    - Target: "Use polling level to wait for work (0 - no polling, default: 50) | 使用轮询级别等待工作（0 - 无轮询）。影响响应速度和 CPU 使用率的平衡。较高的轮询级别可提高响应速度但增加 CPU 使用。"

12. **`-Cb, --cpu-mask-batch M`**
    - Current: "CPU mask for batch | 批处理 CPU 掩码"
    - README: "CPU affinity mask: arbitrarily long hex. Complements cpu-range-batch (default: same as --cpu-mask)"
    - Target: "CPU affinity mask: arbitrarily long hex. Complements cpu-range-batch (default: same as --cpu-mask) | 批处理操作的 CPU 亲和性掩码，与 --cpu-mask-batch 互补。默认与 --cpu-mask 相同，用于优化批处理性能。"

13. **`-Crb, --cpu-range-batch lo-hi`**
    - Current: "CPU range for batch | 批处理 CPU 范围"
    - README: "ranges of CPUs for affinity. Complements --cpu-mask-batch"
    - Target: "Ranges of CPUs for affinity. Complements --cpu-mask-batch | 批处理操作的 CPU 亲和性范围，与 --cpu-mask-batch 互补。用于优化批处理操作的 CPU 分配。"

14. **`--cpu-strict-batch <0|1>`**
    - Current: "Strict CPU affinity for batch | 批处理严格 CPU 亲和性"
    - README: "use strict CPU placement (default: same as --cpu-strict)"
    - Target: "Use strict CPU placement (default: same as --cpu-strict) | 批处理操作的严格 CPU 放置，默认与 --cpu-strict 相同。确保批处理任务在指定的 CPU 上运行。"

15. **`--prio-batch N`**
    - Current: "Process priority for batch | 批处理进程优先级"
    - README: "set process/thread priority : 0-normal, 1-medium, 2-high, 3-realtime (default: 0)<br/>"
    - Target: "Set process/thread priority : 0-normal, 1-medium, 2-high, 3-realtime (default: 0) | 批处理操作的进程/线程优先级：0-normal、1-medium、2-high、3-realtime。用于优化批处理任务的调度。"

16. **`--poll-batch <0|1>`**
    - Current: "Poll interval for batch | 批处理轮询间隔"
    - README: "use polling to wait for work (default: same as --poll)"
    - Target: "Use polling to wait for work (default: same as --poll) | 批处理操作的轮询级别，默认与 --poll 相同。用于优化批处理任务的响应速度和 CPU 使用。"

17. **`-c, --ctx-size N`**
    - Current: "Context size | 上下文窗口大小"
    - README: "size of the prompt context (default: 4096, 0 = loaded from model)<br/>(env: LLAMA_ARG_CTX_SIZE)"
    - Target: "Size of the prompt context (default: 4096, 0 = loaded from model) (env: LLAMA_ARG_CTX_SIZE) | 上下文窗口大小。指定提示词上下文的最大 token 数量，影响模型能处理的输入长度。更大的上下文需要更多内存。默认为 4096，0 表示从模型加载。"

18. **`-n, --predict, --n-predict N`**
    - Current: "Number of tokens to predict | 预测的 token 数量"
    - README: "number of tokens to predict (default: -1, -1 = infinity)<br/>(env: LLAMA_ARG_N_PREDICT)"
    - Target: "Number of tokens to predict (default: -1, -1 = infinity) (env: LLAMA_ARG_N_PREDICT) | 预测的 token 数量。控制每次生成输出的最大 token 数，防止无限生成。-1 表示无限制。适用于聊天、代码生成等任务。"

19. **`-b, --batch-size N`**
    - Current: "Batch size for processing | 批处理大小"
    - README: "logical maximum batch size (default: 2048)<br/>(env: LLAMA_ARG_BATCH)"
    - Target: "Logical maximum batch size (default: 2048) (env: LLAMA_ARG_BATCH) | 逻辑最大批处理大小。影响同时处理的请求数量。较大的批处理可提高吞吐量但增加内存使用。适用于多用户并发场景。"

20. **`-ub, --ubatch-size N`**
    - Current: "Micro batch size | 微批处理大小"
    - README: "physical maximum batch size (default: 512)<br/>(env: LLAMA_ARG_UBATCH)"
    - Target: "Physical maximum batch size (default: 512) (env: LLAMA_ARG_UBATCH) | 物理最大批处理大小。实际处理时的最小单位。较小的微批处理可提高内存效率，但可能略微降低性能。"

---

## Chunk 2: Common Parameters (Part 2) - Memory & Cache Management
**Status: [ ] Pending**

1. **`--keep N`**
   - Current: "Keep model in memory | 保留初始提示 token 数量"
   - README: "number of tokens to keep from the initial prompt (default: 0, -1 = all)"
   - Target: "Number of tokens to keep from the initial prompt (default: 0, -1 = all) | 从初始提示中保留的 token 数量，0 表示不保留，-1 表示全部保留。用于维护对话上下文和状态，确保连贯的交互体验。"

2. **`--swa-full`**
   - Current: "Sliding window attention full | 全尺寸滑动窗口注意力"
   - README: "use full-size SWA cache (default: false)<br/>[(more info)](https://github.com/ggml-org/llama.cpp/pull/13194#issuecomment-2868343055)<br/>(env: LLAMA_ARG_SWA_FULL)"
   - Target: "Use full-size SWA cache (default: false) (env: LLAMA_ARG_SWA_FULL) | 使用完整大小的 SWA 缓存，提高长上下文处理的性能和内存效率。适用于处理超长文本或需要大量历史上下文的场景。"

3. **`--kv-unified, -kvu`**
   - Current: "Unified KV cache | 统一 KV 缓存"
   - README: "use single unified KV buffer for the KV cache of all sequences (default: false)<br/>[(more info)](https://github.com/ggml-org/llama.cpp/pull/14363)<br/>(env: LLAMA_ARG_KV_SPLIT)"
   - Target: "Use single unified KV buffer for the KV cache of all sequences (default: false) (env: LLAMA_ARG_KV_SPLIT) | 使用单一统一的 KV 缓冲区存储所有序列的 KV 缓存，而不是为每个序列分别分配。可提高内存效率，但可能在某些场景下影响性能。"

4. **`-fa, --flash-attn`**
   - Current: "Enable flash attention | 启用 Flash Attention"
   - README: "enable Flash Attention (default: disabled)<br/>(env: LLAMA_ARG_FLASH_ATTN)"
   - Target: "Enable Flash Attention (default: disabled) (env: LLAMA_ARG_FLASH_ATTN) | 启用 Flash Attention 优化算法，显著提高注意力计算效率，减少内存使用和计算时间。需要硬件支持，可大幅提升长文本处理性能。"

5. **`--no-perf`**
   - Current: "Disable performance metrics | 禁用性能指标"
   - README: "disable internal libllama performance timings (default: false)<br/>(env: LLAMA_ARG_NO_PERF)"
   - Target: "Disable internal libllama performance timings (default: false) (env: LLAMA_ARG_NO_PERF) | 禁用内部 libllama 性能计时，减少开销但失去性能数据。在不需要性能分析的生产环境中可略微提高性能。"

6. **`-e, --escape`**
   - Current: "Escape special characters | 转义特殊字符"
   - README: "process escapes sequences (\n, \r, \t, \', \", \\) (default: true)"
   - Target: "Process escapes sequences (\n, \r, \t, \', \", \\) (default: true) | 处理转义序列。确保文本输出的正确性和安全性，防止格式问题和注入攻击。"

7. **`--no-escape`**
   - Current: "Do not process escape sequences"
   - README: "do not process escape sequences"
   - Target: "Do not process escape sequences | 不处理转义序列。在某些需要原始文本输出的场景中使用。"

8. **`--rope-scaling {none,linear,yarn}`**
   - Current: "RoPE scaling type | RoPE 缩放类型"
   - README: "RoPE frequency scaling method, defaults to linear unless specified by the model<br/>(env: LLAMA_ARG_ROPE_SCALING_TYPE)"
   - Target: "RoPE frequency scaling method, defaults to linear unless specified by the model (env: LLAMA_ARG_ROPE_SCALING_TYPE) | Rotary Position Embedding 频率缩放方法，可选值：none、linear、yarn。用于扩展模型上下文长度，提高长文本处理能力。"

9. **`--rope-scale N`**
   - Current: "RoPE scale factor | RoPE 缩放因子"
   - README: "RoPE context scaling factor, expands context by a factor of N<br/>(env: LLAMA_ARG_ROPE_SCALE)"
   - Target: "RoPE context scaling factor, expands context by a factor of N (env: LLAMA_ARG_ROPE_SCALE) | RoPE 上下文缩放因子，将上下文扩展 N 倍。例如 2.0 表示将上下文长度翻倍。适用于处理超出原始训练长度的长文本。"

10. **`--rope-freq-base N`**
    - Current: "RoPE frequency base | RoPE 频率基础"
    - README: "RoPE base frequency, used by NTK-aware scaling (default: loaded from model)<br/>(env: LLAMA_ARG_ROPE_FREQ_BASE)"
    - Target: "RoPE base frequency, used by NTK-aware scaling (default: loaded from model) (env: LLAMA_ARG_ROPE_FREQ_BASE) | RoPE 基础频率，用于 NTK 感知缩放。影响位置编码的频率分布，可调整以适应不同的上下文扩展需求。"

11. **`--rope-freq-scale N`**
    - Current: "RoPE frequency scale | RoPE 频率缩放"
    - README: "RoPE frequency scaling factor, expands context by a factor of 1/N<br/>(env: LLAMA_ARG_ROPE_FREQ_SCALE)"
    - Target: "RoPE frequency scaling factor, expands context by a factor of 1/N (env: LLAMA_ARG_ROPE_FREQ_SCALE) | RoPE 频率缩放因子，将上下文扩展 1/N 倍。与 rope-scale 相反作用，用于精细调整位置编码的缩放比例。"

12. **`--yarn-orig-ctx N`**
    - Current: "YARN original context | YARN 原始上下文"
    - README: "YaRN: original context size of model (default: 0 = model training context size)<br/>(env: LLAMA_ARG_YARN_ORIG_CTX)"
    - Target: "YaRN: original context size of model (default: 0 = model training context size) (env: LLAMA_ARG_YARN_ORIG_CTX) | YaRN：模型的原始上下文大小，0 表示模型训练上下文大小。用于 YaRN 扩展技术，确保位置编码的正确缩放。"

13. **`--yarn-ext-factor N`**
    - Current: "YARN extension factor | YARN 扩展因子"
    - README: "YaRN: extrapolation mix factor (default: -1.0, 0.0 = full interpolation)<br/>(env: LLAMA_ARG_YARN_EXT_FACTOR)"
    - Target: "YaRN: extrapolation mix factor (default: -1.0, 0.0 = full interpolation) (env: LLAMA_ARG_YARN_EXT_FACTOR) | YaRN：外推混合因子，-1.0 表示默认，0.0 表示完全插值。控制 YaRN 如何处理超出原始上下文范围的内容，影响长文本处理质量。"

14. **`--yarn-attn-factor N`**
    - Current: "YARN attention factor | YARN 注意力因子"
    - README: "YaRN: scale sqrt(t) or attention magnitude (default: 1.0)<br/>(env: LLAMA_ARG_YARN_ATTN_FACTOR)"
    - Target: "YaRN: scale sqrt(t) or attention magnitude (default: 1.0) (env: LLAMA_ARG_YARN_ATTN_FACTOR) | YaRN：缩放 sqrt(t) 或注意力幅度。影响注意力机制在长距离上下文中的表现，控制注意力得分的缩放。"

15. **`--yarn-beta-slow N`**
    - Current: "YARN beta slow | YARN 慢速 beta"
    - README: "YaRN: high correction dim or alpha (default: 1.0)<br/>(env: LLAMA_ARG_YARN_BETA_SLOW)"
    - Target: "YaRN: high correction dim or alpha (default: 1.0) (env: LLAMA_ARG_YARN_BETA_SLOW) | YaRN：高校正维度或 alpha。控制 YaRN 中较慢变化的校正参数，影响位置编码的长期行为。"

16. **`--yarn-beta-fast N`**
    - Current: "YARN beta fast | YARN 快速 beta"
    - README: "YaRN: low correction dim or beta (default: 32.0)<br/>(env: LLAMA_ARG_YARN_BETA_FAST)"
    - Target: "YaRN: low correction dim or beta (default: 32.0) (env: LLAMA_ARG_YARN_BETA_FAST) | YaRN：低校正维度或 beta。控制 YaRN 中快速变化的校正参数，影响位置编码的短期精度。"

17. **`-nkvo, --no-kv-offload`**
    - Current: "Disable KV cache offload | 禁用 KV 缓存卸载"
    - README: "disable KV offload<br/>(env: LLAMA_ARG_NO_KV_OFFLOAD)"
    - Target: "Disable KV offload (env: LLAMA_ARG_NO_KV_OFFLOAD) | 禁用 KV 缓存卸载。阻止将 KV 缓存卸载到 GPU，强制所有 KV 缓存保留在 CPU 内存中。在 GPU 内存有限时可用，但可能影响推理速度。"

18. **`-nr, --no-repack`**
    - Current: "Disable repacking | 禁用权重重新打包"
    - README: "disable weight repacking<br/>(env: LLAMA_ARG_NO_REPACK)"
    - Target: "Disable weight repacking (env: LLAMA_ARG_NO_REPACK) | 禁用权重重新打包。不重新打包权重，可能在某些情况下加快加载速度，但可能影响后续推理性能。通常建议启用重新打包以获得最佳性能。"

19. **`-ctk, --cache-type-k TYPE`**
    - Current: "Cache type for K | K 缓存类型"
    - README: "KV cache data type for K<br/>allowed values: f32, f16, bf16, q8_0, q4_0, q4_1, iq4_nl, q5_0, q5_1<br/>(default: f16)<br/>(env: LLAMA_ARG_CACHE_TYPE_K)"
    - Target: "KV cache data type for K (env: LLAMA_ARG_CACHE_TYPE_K) | KV 缓存中 K 键的数据类型，可选值：f32、f16、bf16、q8_0、q4_0、q4_1、iq4_nl、q5_0、q5_1。影响内存占用和精度，f16 是较好的平衡。"

20. **`-ctv, --cache-type-v TYPE`**
    - Current: "Cache type for V | V 缓存类型"
    - README: "KV cache data type for V<br/>allowed values: f32, f16, bf16, q8_0, q4_0, q4_1, iq4_nl, q5_0, q5_1<br/>(default: f16)<br/>(env: LLAMA_ARG_CACHE_TYPE_V)"
    - Target: "KV cache data type for V (env: LLAMA_ARG_CACHE_TYPE_V) | KV 缓存中 V 值的数据类型，可选值与 K 相同。通常与 K 设置相同类型以保持一致性，可根据精度需求选择。"

---

## Chunk 3: Common Parameters (Part 3) - Advanced Configuration
**Status: [ ] Pending**

1. **`-dt, --defrag-thold N`**
   - Current: "Defragmentation threshold | KV 缓存碎片整理阈值"
   - README: "KV cache defragmentation threshold (DEPRECATED)<br/>(env: LLAMA_ARG_DEFRAG_THOLD)"
   - Target: "KV cache defragmentation threshold (DEPRECATED) (env: LLAMA_ARG_DEFRAG_THOLD) | KV 缓存碎片整理阈值。当碎片超过此阈值时触发碎片整理，提高内存利用率。DEPRECATED 参数，可能在未来版本中被移除。"

2. **`-np, --parallel N`**
   - Current: "Number of parallel processes | 并行进程数量"
   - README: "number of parallel sequences to decode (default: 1)<br/>(env: LLAMA_ARG_N_PARALLEL)"
   - Target: "Number of parallel sequences to decode (default: 1) (env: LLAMA_ARG_N_PARALLEL) | 并行解码的序列数量，支持多用户同时使用。较高的值可提高并发性能，但增加内存和计算资源需求。"

3. **`--mlock`**
   - Current: "Lock model in memory | 内存锁定模型"
   - README: "force system to keep model in RAM rather than swapping or compressing<br/>(env: LLAMA_ARG_MLOCK)"
   - Target: "Force system to keep model in RAM rather than swapping or compressing (env: LLAMA_ARG_MLOCK) | 强制系统将模型保持在 RAM 中而非交换或压缩，提高性能但增加内存占用。适用于内存充足且需要稳定性能的场景。"

4. **`--no-mmap`**
   - Current: "Disable memory mapping | 禁用内存映射"
   - README: "do not memory-map model (slower load but may reduce pageouts if not using mlock)<br/>(env: LLAMA_ARG_NO_MMAP)"
   - Target: "Do not memory-map model (slower load but may reduce pageouts if not using mlock) (env: LLAMA_ARG_NO_MMAP) | 不使用内存映射加载模型，加载速度较慢但可能减少页面交换。在不使用 mlock 时可用于优化内存管理。"

5. **`--numa TYPE`**
   - Current: "NUMA configuration | NUMA 配置"
   - README: "attempt optimizations that help on some NUMA systems<br/>- distribute: spread execution evenly over all nodes<br/>- isolate: only spawn threads on CPUs on the node that execution started on<br/>- numactl: use the CPU map provided by numactl<br/>if run without this previously, it is recommended to drop the system page cache before using this<br/>see https://github.com/ggml-org/llama.cpp/issues/1437<br/>(env: LLAMA_ARG_NUMA)"
   - Target: "Attempt optimizations that help on some NUMA systems (env: LLAMA_ARG_NUMA) | NUMA 配置。尝试在某些 NUMA 系统上进行优化：distribute（均匀分布）、isolate（仅限起始节点）、numactl（使用 numactl 提供的 CPU 映射）。适合多 CPU 架构优化。"

6. **`-dev, --device <dev1,dev2,..>`**
   - Current: "Device to use | 使用的设备"
   - README: "comma-separated list of devices to use for offloading (none = don't offload)<br/>use --list-devices to see a list of available devices<br/>(env: LLAMA_ARG_DEVICE)"
   - Target: "Comma-separated list of devices to use for offloading (none = don't offload) (env: LLAMA_ARG_DEVICE) | 使用的设备。逗号分隔的设备列表用于卸载（none 表示不卸载）。使用 --list-devices 查看可用设备列表。支持 CPU、GPU 等多种设备类型。"

7. **`--list-devices`**
   - Current: "Print list of available devices and exit"
   - README: "print list of available devices and exit"
   - Target: "Print list of available devices and exit | 打印可用设备列表并退出。列出系统中所有可用于模型推理的设备（CPU、GPU 等），包括设备类型、内存信息和支持的功能。帮助用户了解可用的硬件资源和进行设备选择配置。"

8. **`--override-tensor, -ot <tensor name pattern>=<buffer type>,...`**
   - Current: "Override tensor buffer type"
   - README: "override tensor buffer type"
   - Target: "Override tensor buffer type | 覆盖张量缓冲区类型。允许为张量指定不同的缓冲区类型，格式为 <张量名称模式>=<缓冲区类型>。可用于优化内存使用和计算效率，或解决兼容性问题。"

9. **`--cpu-moe, -cmoe`**
   - Current: "CPU MoE | CPU 混合专家"
   - README: "keep all Mixture of Experts (MoE) weights in the CPU<br/>(env: LLAMA_ARG_CPU_MOE)"
   - Target: "Keep all Mixture of Experts (MoE) weights in the CPU (env: LLAMA_ARG_CPU_MOE) | CPU 混合专家。将所有混合专家（MoE）权重保持在 CPU 中。对于 MoE 模型，强制所有专家网络在 CPU 上运行，可能影响性能但提高兼容性。"

10. **`--n-cpu-moe, -ncmoe N`**
    - Current: "Number of CPU MoE | CPU MoE 数量"
    - README: "keep the Mixture of Experts (MoE) weights of the first N layers in the CPU<br/>(env: LLAMA_ARG_N_CPU_MOE)"
    - Target: "Keep the Mixture of Experts (MoE) weights of the first N layers in the CPU (env: LLAMA_ARG_N_CPU_MOE) | CPU MoE 数量。将前 N 层的混合专家（MoE）权重保持在 CPU 中。提供更细粒度的 MoE 控制，平衡性能和兼容性需求。"

11. **`-ngl, --gpu-layers, --n-gpu-layers N`**
    - Current: "Number of GPU layers | GPU 层数"
    - README: "number of layers to store in VRAM<br/>(env: LLAMA_ARG_N_GPU_LAYERS)"
    - Target: "Number of layers to store in VRAM (env: LLAMA_ARG_N_GPU_LAYERS) | GPU 层数。指定卸载到 VRAM 的神经网络层数，可显著加速推理。0 表示仅使用 CPU。设置为 -1 或足够大的数字可将整个模型移至 GPU。"

12. **`-sm, --split-mode {none,layer,row}`**
    - Current: "Split mode for tensors | 张量分割模式"
    - README: "how to split the model across multiple GPUs, one of:<br/>- none: use one GPU only<br/>- layer (default): split layers and KV across GPUs<br/>- row: split rows across GPUs<br/>(env: LLAMA_ARG_SPLIT_MODE)"
    - Target: "How to split the model across multiple GPUs (env: LLAMA_ARG_SPLIT_MODE) | 张量分割模式。控制如何在多个 GPU 间分割模型：none（单 GPU）、layer（默认，层分割）、row（行分割）。影响多 GPU 性能和内存使用。"

13. **`-ts, --tensor-split N0,N1,N2,...`**
    - Current: "Tensor split configuration | 张量分割配置"
    - README: "fraction of the model to offload to each GPU, comma-separated list of proportions, e.g. 3,1<br/>(env: LLAMA_ARG_TENSOR_SPLIT)"
    - Target: "Fraction of the model to offload to each GPU, comma-separated list of proportions, e.g. 3,1 (env: LLAMA_ARG_TENSOR_SPLIT) | 张量分割配置。指定将模型分配到多个 GPU 的比例，逗号分隔的比例列表。例如 '3,1' 表示 GPU 0 分配 75%，GPU 1 分配 25%。用于多 GPU 分布式推理。"

14. **`-mg, --main-gpu INDEX`**
    - Current: "Main GPU to use | 主 GPU 设备"
    - README: "the GPU to use for the model (with split-mode = none), or for intermediate results and KV (with split-mode = row) (default: 0)<br/>(env: LLAMA_ARG_MAIN_GPU)"
    - Target: "The GPU to use for the model (with split-mode = none), or for intermediate results and KV (with split-mode = row) (default: 0) (env: LLAMA_ARG_MAIN_GPU) | 主 GPU 设备。当 split-mode 为 none 时使用的 GPU，或当 split-mode 为 row 时存储中间结果和 KV 缓存的 GPU。默认为 0。多 GPU 系统中用于负载均衡。"

15. **`--check-tensors`**
    - Current: "Check model tensor data for invalid values"
    - README: "check model tensor data for invalid values (default: false)"
    - Target: "Check model tensor data for invalid values (default: false) | 检查模型张量数据中的无效值。用于模型验证和调试，帮助识别损坏的模型文件或内存问题。"

16. **`--override-kv KEY=TYPE:VALUE`**
    - Current: "Override KV cache | 覆盖 KV 缓存"
    - README: "advanced option to override model metadata by key. may be specified multiple times.<br/>types: int, float, bool, str. example: --override-kv tokenizer.ggml.add_bos_token=bool:false"
    - Target: "Advanced option to override model metadata by key. May be specified multiple times. Types: int, float, bool, str. Example: --override-kv tokenizer.ggml.add_bos_token=bool:false | 覆盖模型元数据。高级选项，按键覆盖模型元数据。可多次指定。用于自定义模型行为和配置。"

17. **`--no-op-offload`**
    - Current: "Disable operator offload | 禁用操作符卸载"
    - README: "disable offloading host tensor operations to device (default: false)"
    - Target: "Disable offloading host tensor operations to device (default: false) | 禁用操作符卸载。禁用将主机张量操作卸载到设备的功能。在设备内存有限或遇到兼容性问题时使用。可能影响性能，但提高稳定性。"

18. **`--lora FNAME`**
    - Current: "LoRA adapters | LoRA 适配器"
    - README: "path to LoRA adapter (can be repeated to use multiple adapters)"
    - Target: "Path to LoRA adapter (can be repeated to use multiple adapters) | LoRA 适配器。LoRA（Low-Rank Adaptation）适配器路径，可用于多次指定以使用多个适配器。用于模型微调和个性化，在不修改原模型的情况下调整模型行为。"

19. **`--lora-scaled FNAME SCALE`**
    - Current: "Scaled LoRA adapters | 缩放 LoRA 适配器"
    - README: "path to LoRA adapter with user defined scaling (can be repeated to use multiple adapters)"
    - Target: "Path to LoRA adapter with user defined scaling (can be repeated to use multiple adapters) | 缩放 LoRA 适配器。带有用户定义缩放因子的 LoRA 适配器，格式为路径:缩放值。可用于多次指定以使用多个缩放适配器。精确控制每个适配器的影响程度。"

20. **`--control-vector FNAME`**
    - Current: "Control vectors | 控制向量"
    - README: "add a control vector<br/>note: this argument can be repeated to add multiple control vectors"
    - Target: "Add a control vector (can be repeated to add multiple control vectors) | 控制向量。添加控制向量以调整模型行为。可重复添加多个控制向量。用于引导模型生成特定风格、内容或主题的文本，实现更精细的输出控制。"

---

## Chunk 4: Sampling Parameters
**Status: [ ] Pending**

1. **`--control-vector-scaled FNAME SCALE`**
   - Current: "Scaled control vectors | 缩放控制向量"
   - README: "add a control vector with user defined scaling SCALE<br/>note: this argument can be repeated to add multiple scaled control vectors"
   - Target: "Add a control vector with user defined scaling SCALE (can be repeated to add multiple scaled control vectors) | 缩放控制向量。添加带有用户定义缩放因子的控制向量，格式为路径:缩放值。精确控制每个向量对模型行为的影响程度。"

2. **`--control-vector-layer-range START END`**
   - Current: "Control vector layer range | 控制向量层范围"
   - README: "layer range to apply the control vector(s) to, start and end inclusive"
   - Target: "Layer range to apply the control vector(s) to, start and end inclusive | 控制向量层范围。应用控制向量的层范围，开始和结束层都包含在内。指定控制向量影响的具体神经网络层，实现更精准的模型行为调整。"

3. **`-m, --model FNAME`**
   - Current: "Model file path | 模型文件路径"
   - README: "model path (default: `models/$filename` with filename from `--hf-file` or `--model-url` if set, otherwise models/7B/ggml-model-f16.gguf)<br/>(env: LLAMA_ARG_MODEL)"
   - Target: "Model path (default: `models/$filename` with filename from `--hf-file` or `--model-url` if set, otherwise models/7B/ggml-model-f16.gguf) (env: LLAMA_ARG_MODEL) | 模型文件路径。指定要加载的 GGUF 模型文件路径，支持本地文件路径或网络 URL。如果使用 --hf-repo 或 --model-url，此参数可选。"

4. **`-mu, --model-url MODEL_URL`**
   - Current: "Model URL | 模型 URL"
   - README: "model download url (default: unused)<br/>(env: LLAMA_ARG_MODEL_URL)"
   - Target: "Model download url (default: unused) (env: LLAMA_ARG_MODEL_URL) | 模型下载 URL。支持从网络直接下载模型文件。提供另一种模型获取方式，无需手动下载。确保 URL 可访问且模型格式兼容。"

5. **`-hf, -hfr, --hf-repo <user>/<model>[:quant]`**
   - Current: "Hugging Face repository | Hugging Face 仓库"
   - README: "Hugging Face model repository; quant is optional, case-insensitive, default to Q4_K_M, or falls back to the first file in the repo if Q4_K_M doesn't exist.<br/>mmproj is also downloaded automatically if available. to disable, add --no-mmproj<br/>example: unsloth/phi-4-GGUF:q4_k_m<br/>(default: unused)<br/>(env: LLAMA_ARG_HF_REPO)"
   - Target: "Hugging Face model repository; quant is optional, case-insensitive, default to Q4_K_M, or falls back to the first file in the repo if Q4_K_M doesn't exist. mmproj is also downloaded automatically if available. to disable, add --no-mmproj. Example: unsloth/phi-4-GGUF:q4_k_m (env: LLAMA_ARG_HF_REPO) | Hugging Face 模型仓库。Hugging Face 模型仓库，格式为 <user>/<model>[:quant]。quant 可选，不区分大小写，默认为 Q4_K_M。示例：unsloth/phi-4-GGUF:q4_k_m"

6. **`-hfd, -hfrd, --hf-repo-draft <user>/<model>[:quant]`**
   - Current: "Hugging Face draft repository | Hugging Face 草稿仓库"
   - README: "Same as --hf-repo, but for the draft model (default: unused)<br/>(env: LLAMA_ARG_HFD_REPO)"
   - Target: "Same as --hf-repo, but for the draft model (default: unused) (env: LLAMA_ARG_HFD_REPO) | Hugging Face 草稿仓库。用于投机解码的草稿模型仓库，格式与 --hf-repo 相同。草稿模型通常较小，用于快速生成候选序列，提高整体推理速度。"

7. **`-hff, --hf-file FILE`**
   - Current: "Hugging Face file | Hugging Face 文件"
   - README: "Hugging Face model file. If specified, it will override the quant in --hf-repo (default: unused)<br/>(env: LLAMA_ARG_HF_FILE)"
   - Target: "Hugging Face model file. If specified, it will override the quant in --hf-repo (default: unused) (env: LLAMA_ARG_HF_FILE) | Hugging Face 文件。Hugging Face 模型文件，如果指定将覆盖 --hf-repo 中的 quant。用于精确指定要下载的模型文件，支持特定的量化版本。"

8. **`-hfv, -hfrv, --hf-repo-v <user>/<model>[:quant]`**
   - Current: "Hugging Face repository for vision | 视觉模型仓库"
   - README: "Hugging Face model repository for the vocoder model (default: unused)<br/>(env: LLAMA_ARG_HF_REPO_V)"
   - Target: "Hugging Face model repository for the vocoder model (default: unused) (env: LLAMA_ARG_HF_REPO_V) | 视觉模型仓库。用于多模态模型的视觉编码器仓库，支持图像理解和多模态推理。与主模型配合使用，实现图像-文本联合处理能力。"

9. **`-hffv, --hf-file-v FILE`**
   - Current: "Hugging Face file for vision | 视觉模型文件"
   - README: "Hugging Face model file for the vocoder model (default: unused)<br/>(env: LLAMA_ARG_HF_FILE_V)"
   - Target: "Hugging Face model file for the vocoder model (default: unused) (env: LLAMA_ARG_HF_FILE_V) | 视觉模型文件。多模态视觉编码器的具体文件路径，用于精确指定视觉组件。确保与语言模型版本兼容，支持高质量的图像理解功能。"

10. **`-hft, --hf-token TOKEN`**
    - Current: "Hugging Face token | Hugging Face 令牌"
    - README: "Hugging Face access token (default: value from HF_TOKEN environment variable)<br/>(env: HF_TOKEN)"
    - Target: "Hugging Face access token (default: value from HF_TOKEN environment variable) (env: HF_TOKEN) | Hugging Face 令牌。Hugging Face 访问令牌，用于访问私有模型仓库或需要认证的模型。默认从 HF_TOKEN 环境变量获取。保护模型访问权限的重要安全措施。"

11. **`--log-disable`**
    - Current: "Disable logging | 禁用日志记录"
    - README: "Log disable"
    - Target: "Log disable | 禁用日志记录。完全禁用所有日志输出，减少控制台噪音和提高性能。在生产环境中可能有用，但会影响问题诊断和监控能力。"

12. **`--log-file FNAME`**
    - Current: "Log file path | 日志文件路径"
    - README: "Log to file"
    - Target: "Log to file | 日志文件路径。将日志输出重定向到指定文件，便于长期保存和分析日志。支持日志轮转和归档，适合生产环境部署和问题追踪。"

13. **`--log-colors`**
    - Current: "Enable log colors | 启用日志颜色"
    - README: "Enable colored logging<br/>(env: LLAMA_LOG_COLORS)"
    - Target: "Enable colored logging (env: LLAMA_LOG_COLORS) | 启用日志颜色。为不同级别的日志消息添加颜色，提高可读性和快速识别问题。在支持颜色的终端中特别有用，便于快速定位错误和警告信息。"

14. **`-v, --verbose, --log-verbose`**
    - Current: "Verbose logging | 详细日志记录"
    - README: "Set verbosity level to infinity (i.e. log all messages, useful for debugging)"
    - Target: "Set verbosity level to infinity (i.e. log all messages, useful for debugging) | 详细日志记录。将详细级别设置为无限（即记录所有消息），适用于调试。提供最详细的内部状态信息，帮助诊断复杂问题。"

15. **`--offline`**
    - Current: "Offline mode | 离线模式"
    - README: "Offline mode: forces use of cache, prevents network access<br/>(env: LLAMA_OFFLINE)"
    - Target: "Offline mode: forces use of cache, prevents network access (env: LLAMA_OFFLINE) | 离线模式。强制使用缓存，防止网络访问。适用于网络受限环境或确保完全本地运行，避免意外下载或外部依赖。"

16. **`-lv, --verbosity, --log-verbosity N`**
    - Current: "Verbosity level | 详细级别"
    - README: "Set the verbosity threshold. Messages with a higher verbosity will be ignored.<br/>(env: LLAMA_LOG_VERBOSITY)"
    - Target: "Set the verbosity threshold. Messages with a higher verbosity will be ignored. (env: LLAMA_LOG_VERBOSITY) | 详细级别。设置详细程度阈值，高于此级别的消息将被忽略。提供精细的日志级别控制，平衡信息量和性能影响。"

17. **`--log-prefix`**
    - Current: "Log prefix | 日志前缀"
    - README: "Enable prefix in log messages<br/>(env: LLAMA_LOG_PREFIX)"
    - Target: "Enable prefix in log messages (env: LLAMA_LOG_PREFIX) | 日志前缀。在日志消息中启用前缀，包含时间戳、级别等信息。提高日志的结构化和可读性，便于日志分析和过滤。"

18. **`--log-timestamps`**
    - Current: "Log timestamps | 日志时间戳"
    - README: "Enable timestamps in log messages<br/>(env: LLAMA_LOG_TIMESTAMPS)"
    - Target: "Enable timestamps in log messages (env: LLAMA_LOG_TIMESTAMPS) | 日志时间戳。在日志消息中启用时间戳，记录事件发生的精确时间。对于性能分析、问题追踪和审计日志非常重要。"

19. **`-ctkd, --cache-type-k-draft TYPE`**
    - Current: "Cache type for K for the draft model"
    - README: "KV cache data type for K for the draft model<br/>allowed values: f32, f16, bf16, q8_0, q4_0, q4_1, iq4_nl, q5_0, q5_1<br/>(default: f16)<br/>(env: LLAMA_ARG_CACHE_TYPE_K_DRAFT)"
    - Target: "KV cache data type for K for the draft model (env: LLAMA_ARG_CACHE_TYPE_K_DRAFT) | 草稿模型的 KV 缓存 K 键数据类型。允许值：f32, f16, bf16, q8_0, q4_0, q4_1, iq4_nl, q5_0, q5_1（默认：f16）。草稿模型的 KV 缓存数据类型可以与主模型不同，允许在精度和内存效率间进行独立优化。"

20. **`-ctvd, --cache-type-v-draft TYPE`**
    - Current: "Cache type for V for the draft model"
    - README: "KV cache data type for V for the draft model<br/>allowed values: f32, f16, bf16, q8_0, q4_0, q4_1, iq4_nl, q5_0, q5_1<br/>(default: f16)<br/>(env: LLAMA_ARG_CACHE_TYPE_V_DRAFT)"
    - Target: "KV cache data type for V for the draft model (env: LLAMA_ARG_CACHE_TYPE_V_DRAFT) | 草稿模型的 KV 缓存 V 值数据类型。允许值：f32, f16, bf16, q8_0, q4_0, q4_1, iq4_nl, q5_0, q5_1（默认：f16）。通常与 K 设置相同类型以保持一致性，但可以根据草稿模型的特性进行独立配置。"

---

## Chunk 5: Example-Specific Parameters (Server & Network)
**Status: [ ] Pending**

1. **`--swa-checkpoints N`**
   - Current: "Max number of SWA checkpoints per slot to create"
   - README: "max number of SWA checkpoints per slot to create (default: 3)<br/>[(more info)](https://github.com/ggml-org/llama.cpp/pull/15293)<br/>(env: LLAMA_ARG_SWA_CHECKPOINTS)"
   - Target: "Max number of SWA checkpoints per slot to create (default: 3) (env: LLAMA_ARG_SWA_CHECKPOINTS) | 每个 slot 创建的最大 SWA 检查点数。滑动窗口注意力（SWA）检查点用于管理长上下文处理中的状态，更多的检查点可以提高长文本处理的连续性但增加内存使用。"

2. **`--no-context-shift`**
   - Current: "Disables context shift on infinite text generation"
   - README: "disables context shift on infinite text generation (default: enabled)<br/>(env: LLAMA_ARG_NO_CONTEXT_SHIFT)"
   - Target: "Disables context shift on infinite text generation (default: enabled) (env: LLAMA_ARG_NO_CONTEXT_SHIFT) | 禁用上下文切换。在无限文本生成时禁用上下文切换，默认启用。可能在某些特定生成任务中需要保持严格的上下文连续性。"

3. **`--context-shift`**
   - Current: "Enables context shift on infinite text generation"
   - README: "enables context shift on infinite text generation (default: disabled)<br/>(env: LLAMA_ARG_CONTEXT_SHIFT)"
   - Target: "Enables context shift on infinite text generation (default: disabled) (env: LLAMA_ARG_CONTEXT_SHIFT) | 启用上下文切换。在无限文本生成时启用上下文切换，默认禁用。允许处理超出上下文窗口的长文本，但可能丢失一些早期信息。"

4. **`-r, --reverse-prompt PROMPT`**
   - Current: "Halt generation at PROMPT, return control in interactive mode"
   - README: "halt generation at PROMPT, return control in interactive mode<br/>"
   - Target: "Halt generation at PROMPT, return control in interactive mode | 在指定提示符处停止生成，在交互模式下返回控制权。当模型生成到指定的提示字符串时自动停止，适用于对话系统、代码生成等需要精确控制输出长度的场景。"

5. **`-sp, --special`**
   - Current: "Special tokens output enabled"
   - README: "special tokens output enabled (default: false)"
   - Target: "Special tokens output enabled (default: false) | 启用特殊 token 输出。控制是否在输出中包含特殊 token（如 BOS、EOS、PAD 等）。对于需要完整 token 级别控制的场景（如模型调试、数据预处理）很有用。"

6. **`--no-warmup`**
   - Current: "Skip warming up the model with an empty run"
   - README: "skip warming up the model with an empty run"
   - Target: "Skip warming up the model with an empty run | 跳过模型预热。在开始正式推理前跳过模型预热步骤，可加快启动速度但可能影响第一次推理的性能。适用于快速测试和开发环境。"

7. **`--spm-infill`**
   - Current: "Use Suffix/Prefix/Middle pattern for infill"
   - README: "use Suffix/Prefix/Middle pattern for infill (instead of Prefix/Suffix/Middle) as some models prefer this. (default: disabled)"
   - Target: "Use Suffix/Prefix/Middle pattern for infill (instead of Prefix/Suffix/Middle) as some models prefer this (default: disabled) | 使用 Suffix/Prefix/Middle 模式进行填充。使用 Suffix/Prefix/Middle 模式进行填充（而不是 Prefix/Suffix/Middle），因为某些模型更倾向于这种模式。适用于代码补全、文本修复等填充任务。"

8. **`--pooling {none,mean,cls,last,rank}`**
   - Current: "Pooling type for embeddings, use model default if unspecified"
   - README: "pooling type for embeddings, use model default if unspecified<br/>(env: LLAMA_ARG_POOLING)"
   - Target: "Pooling type for embeddings, use model default if unspecified (env: LLAMA_ARG_POOLING) | 嵌入的池化类型。池化类型影响如何将 token 级别的嵌入聚合为句子级别或文档级别的表示。可选值：none（不池化）、mean（平均值池化）、cls（CLS token 池化）、last（最后一个 token 池化）、rank（排序池化）。"

9. **`-cb, --cont-batching`**
   - Current: "Enable continuous batching"
   - README: "enable continuous batching (a.k.a dynamic batching) (default: enabled)<br/>(env: LLAMA_ARG_CONT_BATCHING)"
   - Target: "Enable continuous batching (a.k.a dynamic batching) (default: enabled) (env: LLAMA_ARG_CONT_BATCHING) | 启用连续批处理。启用连续批处理（也称为动态批处理），提高多用户场景下的吞吐量和资源利用率。默认启用，是现代 LLM 服务的标准配置。"

10. **`-nocb, --no-cont-batching`**
    - Current: "Disable continuous batching"
    - README: "disable continuous batching<br/>(env: LLAMA_ARG_NO_CONT_BATCHING)"
    - Target: "Disable continuous batching (env: LLAMA_ARG_NO_CONT_BATCHING) | 禁用连续批处理。禁用连续批处理，回退到传统批处理模式。可能在某些特定场景下需要，但通常会降低整体性能。"

11. **`--mmproj FILE`**
    - Current: "Path to a multimodal projector file. see tools/mtmd/README.md"
    - README: "path to a multimodal projector file. see tools/mtmd/README.md<br/>note: if -hf is used, this argument can be omitted<br/>(env: LLAMA_ARG_MMPROJ)"
    - Target: "Path to a multimodal projector file. see tools/mtmd/README.md (env: LLAMA_ARG_MMPROJ) | 多模态投影文件路径。多模态投影文件用于处理图像、视频等多模态输入，使模型能够理解和生成与视觉内容相关的文本响应。如果使用 -hf 参数，此参数可以省略。"

12. **`--mmproj-url URL`**
    - Current: "URL to a multimodal projector file. see tools/mtmd/README.md"
    - README: "URL to a multimodal projector file. see tools/mtmd/README.md<br/>(env: LLAMA_ARG_MMPROJ_URL)"
    - Target: "URL to a multimodal projector file. see tools/mtmd/README.md (env: LLAMA_ARG_MMPROJ_URL) | 多模态投影文件 URL。从网络 URL 加载多模态投影文件，支持远程部署和动态下载多模态处理组件。确保 URL 可访问且文件格式兼容。"

13. **`--no-mmproj`**
    - Current: "Explicitly disable multimodal projector, useful when using -hf"
    - README: "explicitly disable multimodal projector, useful when using -hf<br/>(env: LLAMA_ARG_NO_MMPROJ)"
    - Target: "Explicitly disable multimodal projector, useful when using -hf (env: LLAMA_ARG_NO_MMPROJ) | 显式禁用多模态投影器。强制禁用多模态功能，即使在使用 Hugging Face 模型时也是如此。适用于纯文本推理场景，避免不必要的多模态组件加载。"

14. **`--no-mmproj-offload`**
    - Current: "Do not offload multimodal projector to GPU"
    - README: "do not offload multimodal projector to GPU<br/>(env: LLAMA_ARG_NO_MMPROJ_OFFLOAD)"
    - Target: "Do not offload multimodal projector to GPU (env: LLAMA_ARG_NO_MMPROJ_OFFLOAD) | 不将多模态投影器卸载到 GPU。强制多模态投影器在 CPU 上运行，避免 GPU 内存占用。在 GPU 内存有限或遇到兼容性问题时使用。"

15. **`--override-tensor-draft, -otd <tensor name pattern>=<buffer type>,...`**
    - Current: "Override tensor buffer type for draft model"
    - README: "override tensor buffer type for draft model"
    - Target: "Override tensor buffer type for draft model | 覆盖草稿模型的张量缓冲区类型。允许为草稿模型指定不同的张量缓冲区类型，格式为 <张量名称模式>=<缓冲区类型>。可用于优化草稿模型的内存使用和计算效率，或解决兼容性问题。"

16. **`--cpu-moe-draft, -cmoed`**
    - Current: "CPU MoE draft"
    - README: "keep all Mixture of Experts (MoE) weights in the CPU for the draft model<br/>(env: LLAMA_ARG_CPU_MOE_DRAFT)"
    - Target: "Keep all Mixture of Experts (MoE) weights in the CPU for the draft model (env: LLAMA_ARG_CPU_MOE_DRAFT) | CPU MoE 草稿。对于草稿模型，将所有混合专家（MoE）权重保持在 CPU 中。在投机解码场景中控制草稿模型的 MoE 行为。"

17. **`--n-cpu-moe-draft, -ncmoed N`**
    - Current: "Number of CPU MoE draft"
    - README: "keep the Mixture of Experts (MoE) weights of the first N layers in the CPU for the draft model<br/>(env: LLAMA_ARG_N_CPU_MOE_DRAFT)"
    - Target: "Keep the Mixture of Experts (MoE) weights of the first N layers in the CPU for the draft model (env: LLAMA_ARG_N_CPU_MOE_DRAFT) | CPU MoE 草稿数量。对于草稿模型，将前 N 层的混合专家（MoE）权重保持在 CPU 中。在投机解码中精细控制草稿模型的 MoE 行为。"

18. **`-a, --alias STRING`**
    - Current: "Set alias for model name (to be used by REST API)"
    - README: "set alias for model name (to be used by REST API)<br/>(env: LLAMA_ARG_ALIAS)"
    - Target: "Set alias for model name (to be used by REST API) (env: LLAMA_ARG_ALIAS) | 为模型名称设置别名。为模型指定一个友好的别名，在 API 调用中使用此别名而不是模型文件名。便于模型管理和版本控制，支持在运行时动态切换模型。"

19. **`--host HOST`**
    - Current: "Server host"
    - README: "ip address to listen, or bind to an UNIX socket if the address ends with .sock (default: 127.0.0.1)<br/>(env: LLAMA_ARG_HOST)"
    - Target: "IP address to listen, or bind to an UNIX socket if the address ends with .sock (default: 127.0.0.1) (env: LLAMA_ARG_HOST) | 服务器主机。服务器监听的 IP 地址，或如果地址以 .sock 结尾则绑定到 UNIX 套接字。默认为 127.0.0.1（仅本地访问）。设置为 0.0.0.0 允许外部访问。"

20. **`--port PORT`**
    - Current: "Server port"
    - README: "port to listen (default: 8080)<br/>(env: LLAMA_ARG_PORT)"
    - Target: "Port to listen (default: 8080) (env: LLAMA_ARG_PORT) | 服务器端口。服务器监听的端口号，默认为 8080。确保端口未被其他服务占用，且防火墙配置允许访问。支持 1-65535 范围内的端口号。"

---

## Chunk 6: Remaining Specialized Parameters
**Status: [ ] Pending**

1. **`--path PATH`**
   - Current: "Server path"
   - README: "path to serve static files from (default: )<br/>(env: LLAMA_ARG_STATIC_PATH)"
   - Target: "Path to serve static files from (default: ) (env: LLAMA_ARG_STATIC_PATH) | 服务器路径。提供静态文件的路径，用于 Web UI 和其他静态资源。空值表示不提供静态文件服务。支持相对路径和绝对路径。"

2. **`--api-prefix PREFIX`**
   - Current: "API prefix"
   - README: "prefix path the server serves from, without the trailing slash (default: )<br/>(env: LLAMA_ARG_API_PREFIX)"
   - Target: "Prefix path the server serves from, without the trailing slash (default: ) (env: LLAMA_ARG_API_PREFIX) | API 前缀。服务器提供 API 的前缀路径，不包含尾部斜杠。用于路径组织和反向代理配置，支持多个服务在同一域名下运行。"

3. **`--no-webui`**
   - Current: "Disable web UI"
   - README: "Disable the Web UI (default: enabled)<br/>(env: LLAMA_ARG_NO_WEBUI)"
   - Target: "Disable the Web UI (default: enabled) (env: LLAMA_ARG_NO_WEBUI) | 禁用 Web UI。禁用内置的 Web 用户界面，仅提供 API 服务。减少资源占用和攻击面，适用于纯 API 使用场景或与其他前端集成的部署。"

4. **`--embedding, --embeddings`**
   - Current: "Enable embeddings"
   - README: "restrict to only support embedding use case; use only with dedicated embedding models (default: disabled)<br/>(env: LLAMA_ARG_EMBEDDINGS)"
   - Target: "Restrict to only support embedding use case; use only with dedicated embedding models (default: disabled) (env: LLAMA_ARG_EMBEDDINGS) | 启用嵌入。仅支持嵌入使用场景，仅适用于专用嵌入模型。提供文本嵌入向量生成功能，用于相似性搜索、聚类和语义理解等任务。"

5. **`--reranking, --rerank`**
   - Current: "Enable reranking"
   - README: "enable reranking endpoint on server (default: disabled)<br/>(env: LLAMA_ARG_RERANKING)"
   - Target: "Enable reranking endpoint on server (default: disabled) (env: LLAMA_ARG_RERANKING) | 启用重排序。在服务器上启用重排序端点，用于改进搜索结果排序。提供文档重排序功能，提高检索系统的准确性和相关性。"

6. **`--api-key KEY`**
   - Current: "API key"
   - README: "API key to use for authentication (default: none)<br/>(env: LLAMA_API_KEY)"
   - Target: "API key to use for authentication (default: none) (env: LLAMA_API_KEY) | API 密钥。用于身份验证的 API 密钥，默认为无。保护服务器免受未授权访问的重要安全措施。建议在生产环境中设置强密钥。"

7. **`--api-key-file FNAME`**
   - Current: "API key file"
   - README: "path to file containing API keys (default: none)"
   - Target: "Path to file containing API keys (default: none) | API 密钥文件。包含 API 密钥的文件路径，支持多个密钥管理。提供更安全的密钥存储方式，避免在命令行中暴露敏感信息。"

8. **`--ssl-key-file FNAME`**
   - Current: "SSL key file"
   - README: "path to file a PEM-encoded SSL private key<br/>(env: LLAMA_ARG_SSL_KEY_FILE)"
   - Target: "Path to file a PEM-encoded SSL private key (env: LLAMA_ARG_SSL_KEY_FILE) | SSL 密钥文件。PEM 编码的 SSL 私钥文件路径。启用 HTTPS 加密通信，保护数据传输安全。需要与 SSL 证书文件配合使用。"

9. **`--ssl-cert-file FNAME`**
   - Current: "SSL certificate file"
   - README: "path to file a PEM-encoded SSL certificate<br/>(env: LLAMA_ARG_SSL_CERT_FILE)"
   - Target: "Path to file a PEM-encoded SSL certificate (env: LLAMA_ARG_SSL_CERT_FILE) | SSL 证书文件。PEM 编码的 SSL 证书文件路径。启用 HTTPS 安全连接，验证服务器身份。建议使用受信任 CA 签发的证书。"

10. **`--chat-template-kwargs STRING`**
    - Current: "Set additional params for the json template parser"
    - README: "sets additional params for the json template parser<br/>(env: LLAMA_CHAT_TEMPLATE_KWARGS)"
    - Target: "Sets additional params for the json template parser (env: LLAMA_CHAT_TEMPLATE_KWARGS) | 为 json 模板解析器设置额外参数。为 Jinja 模板解析器提供额外的配置参数，用于自定义模板行为和渲染选项。允许调整模板的解析方式和变量处理。"

11. **`-to, --timeout N`**
    - Current: "Timeout"
    - README: "server read/write timeout in seconds (default: 600)<br/>(env: LLAMA_ARG_TIMEOUT)"
    - Target: "Server read/write timeout in seconds (default: 600) (env: LLAMA_ARG_TIMEOUT) | 超时时间。服务器读/写超时时间（秒），默认为 600 秒（10分钟）。控制请求和响应的最大等待时间，防止长时间挂起的连接。"

12. **`--threads-http N`**
    - Current: "HTTP threads"
    - README: "number of threads used to process HTTP requests (default: -1)<br/>(env: LLAMA_ARG_THREADS_HTTP)"
    - Target: "Number of threads used to process HTTP requests (default: -1) (env: LLAMA_ARG_THREADS_HTTP) | HTTP 线程。用于处理 HTTP 请求的线程数，-1 表示自动检测。影响服务器的并发处理能力，较高的值支持更多同时连接但增加资源使用。"

13. **`--cache-reuse N`**
    - Current: "Min chunk size to attempt reusing from the cache via KV shifting"
    - README: "min chunk size to attempt reusing from the cache via KV shifting (default: 0)<br/>[(card)](https://ggml.ai/f0.png)<br/>(env: LLAMA_ARG_CACHE_REUSE)"
    - Target: "Min chunk size to attempt reusing from the cache via KV shifting (default: 0) (env: LLAMA_ARG_CACHE_REUSE) | 缓存重用。通过 KV 移位尝试重用缓存的最小块大小。缓存重用机制可以通过移动 KV 缓存来重用之前计算的状态，提高连续推理的效率，特别是在相似的提示词序列中。"

14. **`--metrics`**
    - Current: "Enable metrics"
    - README: "enable prometheus compatible metrics endpoint (default: disabled)<br/>(env: LLAMA_ARG_ENDPOINT_METRICS)"
    - Target: "Enable prometheus compatible metrics endpoint (default: disabled) (env: LLAMA_ARG_ENDPOINT_METRICS) | 启用指标。启用与 Prometheus 兼容的指标端点，提供性能监控数据。便于集成到监控系统中，实现实时性能跟踪和告警。"

15. **`--props`**
    - Current: "Enable properties"
    - README: "enable changing global properties via POST /props (default: disabled)<br/>(env: LLAMA_ARG_ENDPOINT_PROPS)"
    - Target: "Enable changing global properties via POST /props (default: disabled) (env: LLAMA_ARG_ENDPOINT_PROPS) | 启用属性。启用通过 POST /props 更改全局属性的端点。允许运行时配置调整，提供动态参数管理能力。"

16. **`--slots`**
    - Current: "Enable slots"
    - README: "enable slots monitoring endpoint (default: enabled)<br/>(env: LLAMA_ARG_ENDPOINT_SLOTS)"
    - Target: "Enable slots monitoring endpoint (default: enabled) (env: LLAMA_ARG_ENDPOINT_SLOTS) | 启用槽位。启用槽位监控端点，提供处理槽位的实时状态信息。默认启用，对于监控和调试多用户并发处理非常重要。"

17. **`--no-slots`**
    - Current: "Disable slots"
    - README: "disables slots monitoring endpoint<br/>(env: LLAMA_ARG_NO_ENDPOINT_SLOTS)"
    - Target: "Disables slots monitoring endpoint (env: LLAMA_ARG_NO_ENDPOINT_SLOTS) | 禁用槽位。禁用槽位监控端点，减少监控开销但失去处理状态可见性。在不需要监控的简单部署中可考虑使用。"

18. **`--slot-save-path PATH`**
    - Current: "Slot save path"
    - README: "path to save slot kv cache (default: disabled)"
    - Target: "Path to save slot kv cache (default: disabled) | 槽位保存路径。保存槽位 KV 缓存的路径，默认禁用。支持持久化处理状态，实现会话恢复和状态保持功能。"

19. **`--jinja`**
    - Current: "Use Jinja templating"
    - README: "use jinja template for chat (default: disabled)<br/>(env: LLAMA_ARG_JINJA)"
    - Target: "Use jinja template for chat (default: disabled) (env: LLAMA_ARG_JINJA) | 启用 Jinja 模板。使用 Jinja 模板进行聊天，提供更灵活的提示模板支持。允许自定义复杂的提示构建逻辑，满足特定对话需求。"

20. **`--reasoning-format FORMAT`**
    - Current: "Reasoning format"
    - README: "controls whether thought tags are allowed and/or extracted from the response, and in which format they're returned; one of:<br/>- none: leaves thoughts unparsed in `message.content`<br/>- deepseek: puts thoughts in `message.reasoning_content` (except in streaming mode, which behaves as `none`)<br/>(default: auto)<br/>(env: LLAMA_ARG_THINK)"
    - Target: "Controls whether thought tags are allowed and/or extracted from the response, and in which format they're returned; one of: none, deepseek (default: auto) (env: LLAMA_ARG_THINK) | 推理格式。控制是否允许和/或从响应中提取思考标签，以及返回格式：none（思想保留在 message.content 中）、deepseek（思想放在 message.reasoning_content 中）。默认为 auto。"

---

## Additional Parameters to Complete

21. **`--reasoning-budget N`**
    - Current: "Reasoning budget"
    - README: "controls the amount of thinking allowed; currently only one of: -1 for unrestricted thinking budget, or 0 to disable thinking (default: -1)<br/>(env: LLAMA_ARG_THINK_BUDGET)"
    - Target: "Controls the amount of thinking allowed; currently only one of: -1 for unrestricted thinking budget, or 0 to disable thinking (default: -1) (env: LLAMA_ARG_THINK_BUDGET) | 推理预算。控制允许的思考量，目前只有 -1 表示无限制思考预算，或 0 表示禁用思考。默认为 -1。平衡思考深度和响应速度。"

22. **`--chat-template JINJA_TEMPLATE`**
    - Current: "Set custom jinja chat template"
    - README: "set custom jinja chat template (default: template taken from model's metadata)<br/>if suffix/prefix are specified, template will be disabled<br/>only commonly used templates are accepted (unless --jinja is set before this flag):<br/>list of built-in templates:<br/>bailing, chatglm3, chatglm4, chatml, command-r, deepseek, deepseek2, deepseek3, exaone3, exaone4, falcon3, gemma, gigachat, glmedge, gpt-oss, granite, hunyuan-dense, hunyuan-moe, kimi-k2, llama2, llama2-sys, llama2-sys-bos, llama2-sys-strip, llama3, llama4, megrez, minicpm, mistral-v1, mistral-v3, mistral-v3-tekken, mistral-v7, mistral-v7-tekken, monarch, openchat, orion, phi3, phi4, rwkv-world, seed_oss, smolvlm, vicuna, vicuna-orca, yandex, zephyr<br/>(env: LLAMA_ARG_CHAT_TEMPLATE)"
    - Target: "Set custom jinja chat template (env: LLAMA_ARG_CHAT_TEMPLATE) | 设置自定义 jinja 聊天模板。指定自定义的 Jinja 聊天模板，用于格式化对话输入。内置模板列表：bailing, chatglm3, chatglm4, chatml, command-r, deepseek, deepseek2, deepseek3, exaone3, exaone4, falcon3, gemma, gigachat, glmedge, gpt-oss, granite, hunyuan-dense, hunyuan-moe, kimi-k2, llama2, llama2-sys, llama2-sys-bos, llama2-sys-strip, llama3, llama4, megrez, minicpm, mistral-v1, mistral-v3, mistral-v3-tekken, mistral-v7, mistral-v7-tekken, monarch, openchat, orion, phi3, phi4, rwkv-world, seed_oss, smolvlm, vicuna, vicuna-orca, yandex, zephyr"

23. **`--chat-template-file JINJA_TEMPLATE_FILE`**
    - Current: "Set custom jinja chat template file"
    - README: "set custom jinja chat template file (default: template taken from model's metadata)<br/>if suffix/prefix are specified, template will be disabled<br/>only commonly used templates are accepted (unless --jinja is set before this flag):<br/>list of built-in templates:<br/>bailing, chatglm3, chatglm4, chatml, command-r, deepseek, deepseek2, deepseek3, exaone3, exaone4, falcon3, gemma, gigachat, glmedge, gpt-oss, granite, hunyuan-dense, hunyuan-moe, kimi-k2, llama2, llama2-sys, llama2-sys-bos, llama2-sys-strip, llama3, llama4, megrez, minicpm, mistral-v1, mistral-v3, mistral-v3-tekken, mistral-v7, mistral-v7-tekken, monarch, openchat, orion, phi3, phi4, rwkv-world, seed_oss, smolvlm, vicuna, vicuna-orca, yandex, zephyr<br/>(env: LLAMA_ARG_CHAT_TEMPLATE_FILE)"
    - Target: "Set custom jinja chat template file (env: LLAMA_ARG_CHAT_TEMPLATE_FILE) | 设置自定义 jinja 聊天模板文件。从文件加载自定义的 Jinja 聊天模板，用于格式化对话输入。内置模板列表与 --chat-template 相同。"

24. **`--no-prefill-assistant`**
    - Current: "Whether to prefill the assistant's response if the last message is an assistant message"
    - README: "whether to prefill the assistant's response if the last message is an assistant message (default: prefill enabled)<br/>when this flag is set, if the last message is an assistant message then it will be treated as a full message and not prefilled<br/><br/>(env: LLAMA_ARG_NO_PREFILL_ASSISTANT)"
    - Target: "Whether to prefill the assistant's response if the last message is an assistant message (default: prefill enabled) (env: LLAMA_ARG_NO_PREFILL_ASSISTANT) | 是否预填充助手响应。控制当最后一条消息是助手消息时是否预填充助手的回复。默认启用预填充。设置此标志后，如果最后一条消息是助手消息，则将其视为完整消息而不进行预填充。"

25. **`-sps, --slot-prompt-similarity SIMILARITY`**
    - Current: "How much the prompt of a request must match the prompt of a slot in order to use that slot"
    - README: "how much the prompt of a request must match the prompt of a slot in order to use that slot (default: 0.50, 0.0 = disabled)<br/>"
    - Target: "How much the prompt of a request must match the prompt of a slot in order to use that slot (default: 0.50, 0.0 = disabled) (env: LLAMA_ARG_SLOT_PROMPT_SIMILARITY) | 槽位提示相似度。请求的提示词必须与 slot 的提示词匹配的程度才能使用该 slot。较高的值提高重用的准确性但可能降低命中率，较低的值增加重用机会但可能影响质量。"

26. **`--lora-init-without-apply`**
    - Current: "Initialize LoRA without applying"
    - README: "load LoRA adapters without applying them (apply later via POST /lora-adapters) (default: disabled)"
    - Target: "Load LoRA adapters without applying them (apply later via POST /lora-adapters) (default: disabled) (env: LLAMA_ARG_LORA_INIT_WITHOUT_APPLY) | 初始化 LoRA 但不应用。加载 LoRA 适配器但不立即应用（稍后通过 POST /lora-adapters 应用）。支持动态 LoRA 管理，可在运行时切换不同的适配器配置。"

27. **`--draft-max, --draft, --draft-n N`**
    - Current: "Number of tokens to draft for speculative decoding"
    - README: "number of tokens to draft for speculative decoding (default: 16)<br/>(env: LLAMA_ARG_DRAFT_MAX)"
    - Target: "Number of tokens to draft for speculative decoding (default: 16) (env: LLAMA_ARG_DRAFT_MAX) | 投机解码的草稿 token 数量。草稿模型单次生成的最大 token 数，影响投机解码的效率。较大的值可能提高速度但增加草稿被拒绝的风险，需要根据模型质量和应用场景进行调整。"

28. **`--draft-min, --draft-n-min N`**
    - Current: "Minimum number of draft tokens to use for speculative decoding"
    - README: "minimum number of draft tokens to use for speculative decoding (default: 0)<br/>(env: LLAMA_ARG_DRAFT_MIN)"
    - Target: "Minimum number of draft tokens to use for speculative decoding (default: 0) (env: LLAMA_ARG_DRAFT_MIN) | 投机解码使用的最小草稿 token 数量。当草稿生成质量较低时，实际生成的 token 数量可能低于此值。设置为 0 表示不限制最小值，允许灵活的投机策略。"

29. **`--draft-p-min P`**
    - Current: "Minimum speculative decoding probability"
    - README: "minimum speculative decoding probability (greedy) (default: 0.8)<br/>(env: LLAMA_ARG_DRAFT_P_MIN)"
    - Target: "Minimum speculative decoding probability (greedy) (default: 0.8) (env: LLAMA_ARG_DRAFT_P_MIN) | 最小投机解码概率。草稿 token 被接受的最小概率阈值，高于此值的 token 将被接受。较高的阈值提高输出质量但可能减少速度提升，较低的阈值增加速度但可能降低质量。"

30. **`-cd, --ctx-size-draft N`**
    - Current: "Size of the prompt context for the draft model"
    - README: "size of the prompt context for the draft model (default: 0, 0 = loaded from model)<br/>(env: LLAMA_ARG_CTX_SIZE_DRAFT)"
    - Target: "Size of the prompt context for the draft model (default: 0, 0 = loaded from model) (env: LLAMA_ARG_CTX_SIZE_DRAFT) | 草稿模型的提示词上下文大小。草稿模型的上下文窗口大小，可以与主模型不同。设置为 0 将从模型元数据中加载默认值。较大的上下文可能提高草稿质量但增加内存使用。"

31. **`-devd, --device-draft <dev1,dev2,..>`**
    - Current: "Comma-separated list of devices to use for offloading the draft model"
    - README: "comma-separated list of devices to use for offloading the draft model (none = don't offload). use --list-devices to see a list of available devices"
    - Target: "Comma-separated list of devices to use for offloading the draft model (none = don't offload). Use --list-devices to see a list of available devices (env: LLAMA_ARG_DEVICE_DRAFT) | 草稿模型设备。用于卸载草稿模型的逗号分隔设备列表（none = 不卸载）。草稿模型可以与主模型使用不同的设备配置，以优化资源利用和性能。"

32. **`-ngld, --gpu-layers-draft, --n-gpu-layers-draft N`**
    - Current: "Number of layers to store in VRAM for the draft model"
    - README: "number of layers to store in VRAM for the draft model<br/>(env: LLAMA_ARG_N_GPU_LAYERS_DRAFT)"
    - Target: "Number of layers to store in VRAM for the draft model (env: LLAMA_ARG_N_GPU_LAYERS_DRAFT) | 草稿模型 GPU 层数。存储在草稿模型 VRAM 中的层数。草稿模型卸载到 GPU 的神经网络层数，可以与主模型的 GPU 层数设置不同。合理的设置可以在保证性能的同时最大化资源利用效率。"

33. **`-md, --model-draft FNAME`**
    - Current: "Draft model for speculative decoding"
    - README: "draft model for speculative decoding (default: unused)<br/>(env: LLAMA_ARG_MODEL_DRAFT)"
    - Target: "Draft model for speculative decoding (default: unused) (env: LLAMA_ARG_MODEL_DRAFT) | 草稿模型。用于投机解码的草稿模型（默认：未使用）。投机解码使用一个较小的草稿模型快速生成候选 token，然后由主模型验证，可显著提高推理速度。草稿模型应该比主模型小但质量相近，以确保较高的接受率。"

34. **`--spec-replace TARGET DRAFT`**
    - Current: "Translate the string in TARGET into DRAFT if the draft model and main model are not compatible"
    - README: "translate the string in TARGET into DRAFT if the draft model and main model are not compatible"
    - Target: "Translate the string in TARGET into DRAFT if the draft model and main model are not compatible (env: LLAMA_ARG_SPEC_REPLACE) | 规格替换。如果草稿模型和主模型不兼容，将 TARGET 中的字符串翻译为 DRAFT。当草稿模型和主模型使用不同的 tokenizer 或词汇表时，此参数可用于处理 token 映射问题，确保投机解码的正确工作。"

35. **`-mv, --model-vocoder FNAME`**
    - Current: "Vocoder model for audio generation"
    - README: "vocoder model for audio generation (default: unused)"
    - Target: "Vocoder model for audio generation (default: unused) (env: LLAMA_ARG_MODEL_VOCODER) | 声码器模型。用于音频生成的声码器模型（默认：未使用）。声码器模型将文本转换为语音，实现文本到语音（TTS）功能。需要与支持的语音合成模型配合使用，可生成自然流畅的语音输出。"

36. **`--tts-use-guide-tokens`**
    - Current: "Use guide tokens to improve TTS word recall"
    - README: "Use guide tokens to improve TTS word recall"
    - Target: "Use guide tokens to improve TTS word recall (env: LLAMA_ARG_TTS_USE_GUIDE_TOKENS) | 使用引导标记改善 TTS 词召回率。在文本到语音合成中使用引导标记来提高单词识别准确性。通过在文本中添加特殊的引导标记，帮助模型更好地理解单词边界和发音，从而改善语音合成的质量。"

37. **`--embd-bge-small-en-default`**
    - Current: "Use default bge-small-en-v1.5 model"
    - README: "use default bge-small-en-v1.5 model (note: can download weights from the internet)"
    - Target: "Use default bge-small-en-v1.5 model (note: can download weights from the internet) (env: LLAMA_ARG_EMBD_BGE_SMALL_EN_DEFAULT) | 使用默认的 bge-small-en-v1.5 模型。BGE（BAAI General Embedding）是一个高质量的英文文本嵌入模型，适用于语义搜索、文本相似度计算等任务。启用后将自动下载所需模型文件。"

38. **`--embd-e5-small-en-default`**
    - Current: "Use default e5-small-v2 model"
    - README: "use default e5-small-v2 model (note: can download weights from the internet)"
    - Target: "Use default e5-small-v2 model (note: can download weights from the internet) (env: LLAMA_ARG_EMBD_E5_SMALL_EN_DEFAULT) | 使用默认的 e5-small-v2 模型。E5 是一个基于对比学习的文本嵌入模型，在各种语义相似度任务中表现优异。特别适合短文本匹配和语义检索应用。"

39. **`--embd-gte-small-default`**
    - Current: "Use default gte-small model"
    - README: "use default gte-small model (note: can download weights from the internet)"
    - Target: "Use default gte-small model (note: can download weights from the internet) (env: LLAMA_ARG_EMBD_GTE_SMALL_DEFAULT) | 使用默认的 gte-small 模型。GTE（General Text Embedding）是一个通用的文本嵌入模型，在多种语言和任务上都具有良好的性能，特别适合多语言应用场景。"

40. **`fim-qwen1.5b-default`**
    - Current: "Use default Qwen 2.5 Coder 1.5B"
    - README: "use default Qwen 2.5 Coder 1.5B (note: can download weights from the internet)"
    - Target: "Use default Qwen 2.5 Coder 1.5B (note: can download weights from the internet) (env: LLAMA_ARG_FIM_QWEN1_5B_DEFAULT) | 使用默认的 Qwen 2.5 Coder 1.5B。这是一个专门用于代码填充（Fill-in-the-Middle）的 1.5B 参数模型，适用于代码补全、代码修复等任务。启用后将自动下载所需模型文件。"

41. **`fim-qwen3b-default`**
    - Current: "Use default Qwen 2.5 Coder 3B"
    - README: "use default Qwen 2.5 Coder 3B (note: can download weights from the internet)"
    - Target: "Use default Qwen 2.5 Coder 3B (note: can download weights from the internet) (env: LLAMA_ARG_FIM_QWEN3B_DEFAULT) | 使用默认的 Qwen 2.5 Coder 3B。这是一个 3B 参数的代码填充模型，在代码补全质量上优于 1.5B 版本，适用于更复杂的代码生成和修复任务。平衡了性能和资源使用。"

42. **`fim-qwen7b-default`**
    - Current: "Use default Qwen 2.5 Coder 7B"
    - README: "use default Qwen 2.5 Coder 7B (note: can download weights from the internet)"
    - Target: "Use default Qwen 2.5 Coder 7B (note: can download weights from the internet) (env: LLAMA_ARG_FIM_QWEN7B_DEFAULT) | 使用默认的 Qwen 2.5 Coder 7B。这是一个 7B 参数的高质量代码填充模型，在代码理解、生成和修复方面具有出色的性能。适用于对代码质量要求较高的专业场景。"

43. **`fim-qwen7b-spec`**
    - Current: "Use Qwen 2.5 Coder 7B + 0.5B draft for speculative decoding"
    - README: "use Qwen 2.5 Coder 7B + 0.5B draft for speculative decoding (note: can download weights from the internet)"
    - Target: "Use Qwen 2.5 Coder 7B + 0.5B draft for speculative decoding (note: can download weights from the internet) (env: LLAMA_ARG_FIM_QWEN7B_SPEC) | 使用 Qwen 2.5 Coder 7B + 0.5B 草稿模型。这是一个优化的代码填充配置，使用 7B 主模型配合 0.5B 草稿模型，通过投机解码技术显著提高推理速度，同时保持高质量的代码生成能力。"

44. **`fim-qwen14b-spec`**
    - Current: "Use Qwen 2.5 Coder 14B + 0.5B draft for speculative decoding"
    - README: "use Qwen 2.5 Coder 14B + 0.5B draft for speculative decoding (note: can download weights from the internet)"
    - Target: "Use Qwen 2.5 Coder 14B + 0.5B draft for speculative decoding (note: can download weights from the internet) (env: LLAMA_ARG_FIM_QWEN14B_SPEC) | 使用 Qwen 2.5 Coder 14B + 0.5B 草稿模型。这是一个高性能的代码填充配置，使用 14B 主模型配合 0.5B 草稿模型，提供顶级的代码理解、生成和修复能力，同时通过投机解码实现快速的推理速度。"

45. **`fim-qwen30b-default`**
    - Current: "Use default Qwen 3 Coder 30B A3B Instruct"
    - README: "use default Qwen 3 Coder 30B A3B Instruct (note: can download weights from the internet)"
    - Target: "Use default Qwen 3 Coder 30B A3B Instruct (note: can download weights from the internet) (env: LLAMA_ARG_FIM_QWEN30B_DEFAULT) | 使用默认的 Qwen 3 Coder 30B A3B Instruct。这是一个超大型的 30B 参数代码模型，具有最强的代码理解和生成能力，适用于最复杂的代码任务和专业的开发场景。需要较高的硬件配置才能有效运行。"

---

## Sampling Parameters (Full List)

1. **`--samplers SAMPLERS`**
   - Current: "Samplers configuration"
   - README: "samplers that will be used for generation in the order, separated by ';'<br/>(default: penalties;dry;top_n_sigma;top_k;typ_p;top_p;min_p;xtc;temperature)"
   - Target: "Samplers that will be used for generation in the order, separated by ';' (default: penalties;dry;top_n_sigma;top_k;typ_p;top_p;min_p;xtc;temperature) | 采样器配置。指定生成时使用的采样器序列，用分号分隔。默认序列：penalties;dry;top_n_sigma;top_k;typ_p;top_p;min_p;xtc;temperature。采样器的顺序影响最终生成结果。"

2. **`-s, --seed SEED`**
   - Current: "RNG seed"
   - README: "RNG seed (default: -1, use random seed for -1)"
   - Target: "RNG seed (default: -1, use random seed for -1) | RNG 种子。随机数生成器种子，控制生成过程的随机性。-1 表示使用随机种子。相同的种子会产生相同的输出，用于结果重现和调试。"

3. **`--sampling-seq, --sampler-seq SEQUENCE`**
   - Current: "Sampling sequence"
   - README: "simplified sequence for samplers that will be used (default: edskypmxt)"
   - Target: "Simplified sequence for samplers that will be used (default: edskypmxt) | 采样序列。采样器的简化序列表示，每个字符对应一个采样器。默认 'edskypmxt'。提供更简洁的方式来配置采样器顺序，便于快速调整。"

4. **`--ignore-eos`**
   - Current: "Ignore end of sequence token"
   - README: "ignore end of stream token and continue generating (implies --logit-bias EOS-inf)"
   - Target: "Ignore end of stream token and continue generating (implies --logit-bias EOS-inf) | 忽略序列结束 token。忽略 EOS（结束序列）token 并继续生成，意味着 --logit-bias EOS-inf。适用于需要强制生成更长的文本或不希望模型提前结束的场景。"

5. **`--temp N`**
   - Current: "Temperature for sampling"
   - README: "temperature (default: 0.8)"
   - Target: "Temperature (default: 0.8) | 采样温度。控制生成文本的随机性，较低的值（如 0.2）使输出更确定性，较高的值（如 1.0）增加随机性和创造性。默认 0.8 提供良好的平衡。"

6. **`--top-k N`**
   - Current: "Top-k sampling"
   - README: "top-k sampling (default: 40, 0 = disabled)"
   - Target: "Top-k sampling (default: 40, 0 = disabled) | Top-k 采样。从概率最高的 k 个 token 中随机选择，限制候选词数量。较高的 k 值增加多样性，较低的 k 值使输出更集中。0 表示禁用，仅从最可能的词中选择。"

7. **`--top-p N`**
   - Current: "Top-p sampling"
   - README: "top-p sampling (default: 0.9, 1.0 = disabled)"
   - Target: "Top-p sampling (default: 0.9, 1.0 = disabled) | Top-p 核心采样。累积概率达到 p 的最小 token 集合中进行采样，限制候选词数量。0.9 表示从累积概率 90% 的词中选择。1.0 表示禁用。适用于控制输出的多样性。"

8. **`--min-p N`**
   - Current: "Min-p sampling"
   - README: "min-p sampling (default: 0.1, 0.0 = disabled)"
   - Target: "Min-p sampling (default: 0.1, 0.0 = disabled) | Min-p 采样。最小概率采样，过滤掉概率低于最高概率乘以 p 的 token。可防止低质量 token 被选中，提高输出质量。0.0 表示禁用。"

9. **`--top-nsigma N`**
   - Current: "Top-n sigma sampling"
   - README: "top-n-sigma sampling (default: -1.0, -1.0 = disabled)"
   - Target: "Top-n-sigma sampling (default: -1.0, -1.0 = disabled) | Top-n sigma 采样。基于标准差的采样方法，考虑概率分布的标准差。负值表示禁用。提供另一种控制输出多样性的方法，适用于特定场景。"

10. **`--xtc-probability N`**
    - Current: "XTC probability"
    - README: "xtc probability (default: 0.0, 0.0 = disabled)"
    - Target: "XTC probability (default: 0.0, 0.0 = disabled) | XTC 概率。排除最常见 token 的概率，用于减少重复和常见短语。0.0 表示禁用，较高的值可以增加文本的原创性和多样性。"

11. **`--xtc-threshold N`**
    - Current: "XTC threshold"
    - README: "xtc threshold (default: 0.1, 1.0 = disabled)"
    - Target: "XTC threshold (default: 0.1, 1.0 = disabled) | XTC 阈值。XTC 采样的阈值参数，控制排除 token 的严格程度。1.0 表示禁用。与 xtc-probability 配合使用，共同控制文本生成的特征。"

12. **`--typical N`**
    - Current: "Typical sampling"
    - README: "locally typical sampling, parameter p (default: 1.0, 1.0 = disabled)"
    - Target: "Locally typical sampling, parameter p (default: 1.0, 1.0 = disabled) | 典型采样。局部典型采样，基于 token 的典型性进行选择。参数 p 控制典型性阈值。1.0 表示禁用。适用于生成更加自然和预期的文本。"

13. **`--repeat-last-n N`**
    - Current: "Repeat last N tokens"
    - README: "last n tokens to consider for penalize (default: 64, 0 = disabled, -1 = ctx_size)"
    - Target: "Last n tokens to consider for penalize (default: 64, 0 = disabled, -1 = ctx_size) | 重复最后 N 个 token。考虑用于惩罚重复的最近 token 数量。0 表示禁用，-1 表示使用整个上下文大小。较大的值可以更好地防止重复，但可能影响自然重复。"

14. **`--repeat-penalty N`**
    - Current: "Repeat penalty"
    - README: "penalize repeat sequence of tokens (default: 1.0, 1.0 = disabled)"
    - Target: "Penalize repeat sequence of tokens (default: 1.0, 1.0 = disabled) | 重复惩罚。惩罚重复的 token 序列，减少文本重复。1.0 表示禁用，大于 1.0 的值会降低重复 token 的概率。适用于减少重复性回答和提高文本多样性。"

15. **`--presence-penalty N`**
    - Current: "Presence penalty"
    - README: "repeat alpha presence penalty (default: 0.0, 0.0 = disabled)"
    - Target: "Repeat alpha presence penalty (default: 0.0, 0.0 = disabled) | 存在惩罚。alpha 存在惩罚，基于 token 是否已经出现过进行惩罚。0.0 表示禁用。正数鼓励讨论新话题，负数鼓励重复。适用于保持对话的多样性和新鲜度。"

16. **`--frequency-penalty N`**
    - Current: "Frequency penalty"
    - README: "repeat alpha frequency penalty (default: 0.0, 0.0 = disabled)"
    - Target: "Repeat alpha frequency penalty (default: 0.0, 0.0 = disabled) | 频率惩罚。alpha 频率惩罚，基于 token 出现的频率进行惩罚。0.0 表示禁用。比存在惩罚更直接地控制重复，适用于精细调整文本生成质量。"

17. **`--dry-multiplier N`**
    - Current: "DRY multiplier"
    - README: "set DRY sampling multiplier (default: 0.0, 0.0 = disabled)"
    - Target: "Set DRY sampling multiplier (default: 0.0, 0.0 = disabled) | DRY 采样乘数。设置 DRY（Don't Repeat Yourself）采样的乘数，控制惩罚强度。0.0 表示禁用。高级重复检测方法，比传统重复惩罚更智能。"

18. **`--dry-base N`**
    - Current: "DRY base"
    - README: "set DRY sampling base value (default: 1.75)"
    - Target: "Set DRY sampling base value (default: 1.75) | DRY 基础值。设置 DRY 采样的基础值，影响惩罚计算的基准。与 dry-multiplier 配合使用，共同控制重复检测的敏感度。"

19. **`--dry-allowed-length N`**
    - Current: "DRY allowed length"
    - README: "set allowed length for DRY sampling (default: 2)"
    - Target: "Set allowed length for DRY sampling (default: 2) | DRY 允许长度。设置 DRY 采样允许的重复序列长度。较小的值限制更多，较大的值允许更多的自然重复。适用于控制文本的流畅度和原创性。"

20. **`--dry-penalty-last-n N`**
    - Current: "DRY penalty last N"
    - README: "set DRY penalty for the last n tokens (default: -1, 0 = disable, -1 = context size)"
    - Target: "Set DRY penalty for the last n tokens (default: -1, 0 = disable, -1 = context size) | DRY 惩罚最后 N。设置 DRY 惩罚考虑的最后 N 个 token，-1 表示使用上下文大小，0 表示禁用。控制重复检测的历史窗口大小。"

21. **`--dry-sequence-breaker STRING`**
    - Current: "DRY sequence breaker"
    - README: "add sequence breaker for DRY sampling, clearing out default breakers ('\n', ':', '\"', '*') in the process; use \"none\" to not use any sequence breakers<br/>"
    - Target: "Add sequence breaker for DRY sampling, clearing out default breakers ('\n', ':', '\"', '*') in the process; use \"none\" to not use any sequence breakers | DRY 序列分隔符。添加 DRY 采样的序列分隔符，清除默认分隔符。使用 'none' 不使用任何分隔符。用于自定义重复检测的边界。"

22. **`--dynatemp-range N`**
    - Current: "Dynamic temperature range"
    - README: "dynamic temperature range (default: 0.0, 0.0 = disabled)"
    - Target: "Dynamic temperature range (default: 0.0, 0.0 = disabled) | 动态温度范围。动态温度范围，根据生成上下文动态调整温度。0.0 表示禁用。可以根据上下文内容自动调整输出的随机性，提供更智能的生成控制。"

23. **`--dynatemp-exp N`**
    - Current: "Dynamic temperature exponent"
    - README: "dynamic temperature exponent (default: 1.0)"
    - Target: "Dynamic temperature exponent (default: 1.0) | 动态温度指数。动态温度的指数参数，影响温度调整的敏感度。与 dynatemp-range 配合使用，控制动态温度的行为模式。"

24. **`--mirostat N`**
    - Current: "Mirostat mode"
    - README: "use Mirostat sampling.<br/>Top K, Nucleus and Locally Typical samplers are ignored if used.<br/>(default: 0, 0 = disabled, 1 = Mirostat, 2 = Mirostat 2.0)"
    - Target: "Use Mirostat sampling. Top K, Nucleus and Locally Typical samplers are ignored if used (default: 0, 0 = disabled, 1 = Mirostat, 2 = Mirostat 2.0) | Mirostat 模式。使用 Mirostat 采样算法，自动调节 perplexity 到目标水平。0=禁用，1=Mirostat，2=Mirostat 2.0。启用时忽略 Top K、Nucleus 和 Locally Typical 采样器。"

25. **`--mirostat-lr N`**
    - Current: "Mirostat learning rate"
    - README: "Mirostat learning rate, parameter eta (default: 0.1)"
    - Target: "Mirostat learning rate, parameter eta (default: 0.1) | Mirostat 学习率。Mirostat 算法的学习率参数 eta，控制 perplexity 调节的速度。较高的值响应更快但可能不稳定，较低的值更稳定但响应较慢。"

26. **`--mirostat-ent N`**
    - Current: "Mirostat entropy"
    - README: "Mirostat target entropy, parameter tau (default: 5.0)"
    - Target: "Mirostat target entropy, parameter tau (default: 5.0) | Mirostat 熵。Mirostat 算法的目标熵参数 tau，控制生成文本的不可预测性。较高的值产生更多样化的输出，较低的值产生更可预测的输出。"

27. **`-l, --logit-bias TOKEN_ID(+/-)BIAS`**
    - Current: "Logit bias"
    - README: "modifies the likelihood of token appearing in the completion,<br/>i.e. `--logit-bias 15043+1` to increase likelihood of token ' Hello',<br/>or `--logit-bias 15043-1` to decrease likelihood of token ' Hello'"
    - Target: "Modifies the likelihood of token appearing in the completion, i.e. `--logit-bias 15043+1` to increase likelihood of token ' Hello', or `--logit-bias 15043-1` to decrease likelihood of token ' Hello' | Logit 偏置。修改 token 在完成中出现的可能性，例如 `--logit-bias 15043+1` 增加 'Hello' token 的可能性，或 `--logit-bias 15043-1` 减少 'Hello' token 的可能性。用于精确控制生成内容的特征。"

28. **`--grammar GRAMMAR`**
    - Current: "Grammar"
    - README: "BNF-like grammar to constrain generations (see samples in grammars/ dir) (default: '')"
    - Target: "BNF-like grammar to constrain generations (see samples in grammars/ dir) (default: '') | 语法。BNF 类语法来约束生成，确保输出符合特定格式或结构（参见 grammars/ 目录中的示例）。适用于生成结构化数据、代码或其他需要严格格式控制的场景。"

29. **`--grammar-file FNAME`**
    - Current: "Grammar file"
    - README: "file to read grammar from"
    - Target: "File to read grammar from | 语法文件。从文件读取语法约束。支持从外部文件加载复杂的语法规则，便于管理和重用语法定义。"

30. **`-j, --json-schema SCHEMA`**
    - Current: "JSON schema"
    - README: "JSON schema to constrain generations (https://json-schema.org/), e.g. `{}` for any JSON object<br/>For schemas w/ external $refs, use --grammar + example/json_schema_to_grammar.py instead"
    - Target: "JSON schema to constrain generations (https://json-schema.org/), e.g. `{}` for any JSON object. For schemas w/ external $refs, use --grammar + example/json_schema_to_grammar.py instead | JSON 架构。JSON 架构来约束生成，确保输出符合 JSON 结构（例如 `{}` 表示任何 JSON 对象）。对于包含外部 $refs 的架构，请使用 --grammar + example/json_schema_to_grammar.py。适用于生成 JSON 格式的结构化数据。"

31. **`-jf, --json-schema-file FILE`**
    - Current: "JSON schema file"
    - README: "File containing a JSON schema to constrain generations (https://json-schema.org/), e.g. `{}` for any JSON object<br/>For schemas w/ external $refs, use --grammar + example/json_schema_to_grammar.py instead"
    - Target: "File containing a JSON schema to constrain generations (https://json-schema.org/), e.g. `{}` for any JSON object. For schemas w/ external $refs, use --grammar + example/json_schema_to_grammar.py instead | JSON 架构文件。包含 JSON 架构的文件，用于约束生成，确保输出符合 JSON 结构。支持从文件加载复杂的 JSON 架构定义。"

---

## Progress Tracking

### Completed Chunks:
- [ ] Chunk 1: Common Parameters (Part 1)
- [ ] Chunk 2: Common Parameters (Part 2) 
- [ ] Chunk 3: Common Parameters (Part 3)
- [ ] Chunk 4: Sampling Parameters
- [ ] Chunk 5: Example-Specific Parameters
- [ ] Chunk 6: Remaining Specialized Parameters

### Total Parameters: ~200+
### Updated Parameters: 0
### Remaining Parameters: ~200+

## Notes:
- Maintain the bilingual format: English | Chinese
- Preserve existing Chinese translations
- Expand English explanations with comprehensive details from README
- Include default values, ranges, environment variables where applicable
- Add use cases and performance implications
- Group related parameters logically for better organization