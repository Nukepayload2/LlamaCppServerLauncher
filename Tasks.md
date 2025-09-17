# LlamaCppServerLauncher 任务清单

## 项目现状分析

### 已完成的功能
- ✅ **AppSettings.vb** 中定义了221+个完整的llama.cpp server参数
- ✅ **MainWindow.axaml** 实现了基础UI界面（约30%的参数）
- ✅ 完整的参数管理系统和JSON配置支持
- ✅ 命令行参数生成功能
- ✅ 服务器进程管理
- ✅ 国际化支持（中英文）

### UI中遗漏的重要参数

虽然AppSettings.vb中定义了完整的参数，但UI中只实现了约30%的参数。以下是按优先级分类的遗漏参数：

## 🎯 第一阶段：硬件和性能参数（高优先级）

### 1.1 多GPU配置 (5个参数)
- `MainGpu` - 主GPU选择 (NumericUpDown: 0-N)
- `TensorSplit` - 多GPU张量分割 (TextBox: "3,1"格式)
- `SplitMode` - GPU分割模式 (ComboBox: none/layer/row)
- `Device` - 设备选择列表 (TextBox: "0,1,2")
- `Numa` - NUMA优化 (ComboBox: distribute/isolate/numactl)

### 1.2 CPU优化 (8个参数)
- `CpuMask` - CPU亲和性掩码 (TextBox: 十六进制格式)
- `CpuRange` - CPU范围 (TextBox: "0-7"格式)
- `Prio` - 进程优先级 (ComboBox: low/normal/medium/high/realtime)
- `Poll` - 轮询级别 (NumericUpDown: 0-100)
- `CpuMaskBatch` - 批处理CPU掩码
- `CpuRangeBatch` - 批处理CPU范围
- `CpuStrict` - 严格CPU放置 (CheckBox)
- `CpuStrictBatch` - 批处理严格CPU放置 (CheckBox)

### 1.3 批处理配置 (3个参数)
- `BatchSize` - 逻辑批处理大小 (NumericUpDown: 512-8192)
- `UbatchSize` - 物理批处理大小 (NumericUpDown: 128-2048)
- `NParallel` - 并行序列数 (NumericUpDown: 1-16)

## 🎯 第二阶段：高级采样参数（高优先级）

### 2.1 采样器配置 (2个参数)
- `Samplers` - 采样器顺序 (TextBox: "penalties;dry;top_k;...")
- `SamplingSeq` - 简化采样序列 (TextBox: "edskypmxt")

### 2.2 DRY采样 (6个参数)
- `DryMultiplier` - DRY采样乘数 (NumericUpDown: 0.0-5.0)
- `DryBase` - DRY采样基础值 (NumericUpDown: 1.0-3.0)
- `DryAllowedLength` - 允许长度 (NumericUpDown: 1-10)
- `DryPenaltyLastN` - 惩罚范围 (NumericUpDown: -1-128)
- `DrySequenceBreaker` - 序列分隔符 (TextBox: "\n,:,\"")
- `IgnoreEOS` - 忽略结束标记 (CheckBox)

### 2.3 Mirostat采样 (3个参数)
- `Mirostat` - Mirostat模式 (ComboBox: 0/1/2)
- `MirostatLr` - 学习率 (NumericUpDown: 0.01-1.0)
- `MirostatEnt` - 目标熵 (NumericUpDown: 1.0-10.0)

### 2.4 动态温度 (2个参数)
- `DynatempRange` - 动态温度范围 (NumericUpDown: 0.0-2.0)
- `DynatempExp` - 动态温度指数 (NumericUpDown: 0.1-5.0)

### 2.5 高级采样选项 (4个参数)
- `Typical` - 典型采样 (NumericUpDown: 0.1-2.0)
- `TopNSigma` - Top-N Sigma (NumericUpDown: -1.0-5.0)
- `XtcProbability` - XTC概率 (NumericUpDown: 0.0-1.0)
- `XtcThreshold` - XTC阈值 (NumericUpDown: 0.0-1.0)
- `RepeatLastN` - 重复惩罚范围 (NumericUpDown: -1-256)

## 🎯 第三阶段：网络和安全参数（中优先级）

### 3.1 API认证 (3个参数)
- `ApiKey` - API密钥 (TextBox)
- `ApiKeyFile` - API密钥文件 (TextBox + Browse按钮)
- `NoWebui` - 禁用Web UI (CheckBox)

### 3.2 SSL配置 (2个参数)
- `SslKeyFile` - SSL密钥文件 (TextBox + Browse按钮)
- `SslCertFile` - SSL证书文件 (TextBox + Browse按钮)

### 3.3 服务配置 (4个参数)
- `Path` - 静态文件路径 (TextBox + Browse按钮)
- `ApiPrefix` - API前缀路径 (TextBox)
- `ThreadsHttp` - HTTP线程数 (NumericUpDown: -1-64)
- `ContBatching` - 连续批处理 (CheckBox)
- `NoContBatching` - 禁用连续批处理 (CheckBox)
- `Props` - 属性修改端点 (CheckBox)

### 3.4 服务器功能 (3个参数)
- `Embeddings` - 嵌入端点 (CheckBox)
- `Reranking` - 重排端点 (CheckBox)
- `Metrics` - 指标端点 (CheckBox)

## 🎯 第四阶段：模型和适配器参数（中优先级）

### 4.1 LoRA适配器 (3个参数)
- `Lora` - LoRA适配器路径列表 (ListBox + 添加/删除按钮)
- `LoraScaled` - 带缩放的LoRA适配器 (ListView with Scale column)
- `LoraInitWithoutApply` - 加载但不应用 (CheckBox)

### 4.2 控制向量 (3个参数)
- `ControlVector` - 控制向量路径列表 (ListBox + 添加/删除按钮)
- `ControlVectorScaled` - 带缩放的控制向量 (ListView with Scale column)
- `ControlVectorLayerRange` - 层范围 (TextBox: "0,32")

### 4.3 Hugging Face集成 (4个参数)
- `HfRepo` - HF仓库 (TextBox: "user/model")
- `HfFile` - HF文件 (TextBox)
- `HfToken` - HF令牌 (PasswordBox)
- `ModelUrl` - 模型URL (TextBox)
- `HfRepoDraft` - 草稿模型HF仓库 (TextBox)

### 4.4 模型配置 (4个参数)
- `OverrideKV` - 覆盖元数据 (TextBox: "key=type:value")
- `NoOpOffload` - 禁用操作卸载 (CheckBox)
- `Keep` - 保留token数 (NumericUpDown: 0-2048)
- `NPredict` - 预测token数 (NumericUpDown: -1-4096)

## 🎯 第五阶段：草稿模型参数（中优先级）

### 5.1 草稿模型基础 (6个参数)
- `ModelDraft` - 草稿模型路径 (TextBox + Browse按钮)
- `CtxSizeDraft` - 草稿上下文大小 (NumericUpDown: 0-8192)
- `NGpuLayersDraft` - 草稿GPU层数 (NumericUpDown: 0-100)
- `ThreadsDraft` - 草稿线程数 (NumericUpDown: -1-64)
- `ThreadsBatchDraft` - 草稿批处理线程 (NumericUpDown: -1-64)
- `DeviceDraft` - 草稿设备 (TextBox)

### 5.2 投机解码 (3个参数)
- `DraftMax` - 最大草稿token数 (NumericUpDown: 1-64)
- `DraftMin` - 最小草稿token数 (NumericUpDown: 0-32)
- `DraftPMin` - 最小概率 (NumericUpDown: 0.0-1.0)

### 5.3 草稿模型缓存 (2个参数)
- `CacheTypeKDraft` - 草稿K缓存类型 (ComboBox: f32/f16/bf16/q8_0等)
- `CacheTypeVDraft` - 草稿V缓存类型 (ComboBox: f32/f16/bf16/q8_0等)

## 🎯 第六阶段：RoPE和上下文参数（中优先级）

### 6.1 RoPE配置 (4个参数)
- `RopeScaling` - RoPE缩放方法 (ComboBox: none/linear/yarn)
- `RopeScale` - RoPE缩放因子 (NumericUpDown: 0.1-10.0)
- `RopeFreqBase` - RoPE频率基数 (NumericUpDown: 0.0-1000000.0)
- `RopeFreqScale` - RoPE频率缩放 (NumericUpDown: 0.1-10.0)

### 6.2 YaRN配置 (5个参数)
- `YarnOrigCtx` - 原始上下文大小 (NumericUpDown: 0-8192)
- `YarnExtFactor` - 外推因子 (NumericUpDown: -1.0-10.0)
- `YarnAttnFactor` - 注意力因子 (NumericUpDown: 0.1-10.0)
- `YarnBetaSlow` - Beta慢 (NumericUpDown: 0.1-100.0)
- `YarnBetaFast` - Beta快 (NumericUpDown: 1.0-1000.0)

## 🎯 第七阶段：多模态和嵌入参数（低优先级）

### 7.1 多模态配置 (4个参数)
- `Mmproj` - 多模态投影器路径 (TextBox + Browse按钮)
- `MmprojUrl` - 多模态投影器URL (TextBox)
- `NoMmproj` - 禁用多模态投影器 (CheckBox)
- `NoMmprojOffload` - 禁用投影器卸载 (CheckBox)

### 7.2 嵌入配置 (4个参数)
- `Pooling` - 池化类型 (ComboBox: none/mean/cls/last/rank)
- `EmbdBgeSmallEnDefault` - 默认BGE模型 (CheckBox)
- `EmbdE5SmallEnDefault` - 默认E5模型 (CheckBox)
- `EmbdGteSmallDefault` - 默认GTE模型 (CheckBox)

## 🎯 第八阶段：日志和监控参数（低优先级）

### 8.1 日志配置 (6个参数)
- `LogFile` - 日志文件路径 (TextBox + Browse按钮)
- `Verbosity` - 日志详细程度 (NumericUpDown: 0-5)
- `LogPrefix` - 日志前缀 (CheckBox)
- `NoPerf` - 禁用性能计时 (CheckBox)
- `Escape` - 转义序列 (CheckBox)
- `VerbosePrompt` - 详细提示 (CheckBox)

### 8.2 监控功能 (4个参数)
- `SlotSavePath` - 槽位保存路径 (TextBox + Browse按钮)
- `SWACheckpoints` - SWA检查点数 (NumericUpDown: 1-10)
- `CacheReuse` - 缓存重用大小 (NumericUpDown: 0-1024)
- `SlotPromptSimilarity` - 提示相似度 (NumericUpDown: 0.0-1.0)

## 🎯 第九阶段：聊天和功能参数（低优先级）

### 9.1 聊天配置 (5个参数)
- `ChatTemplate` - 聊天模板 (ComboBox: 内置模板列表)
- `ChatTemplateFile` - 聊天模板文件 (TextBox + Browse按钮)
- `ChatTemplateKwargs` - 模板参数 (TextBox)
- `ModelAlias` - 模型别名 (TextBox)
- `NoPrefillAssistant` - 禁用助手预填充 (CheckBox)

### 9.2 功能配置 (5个参数)
- `ReversePrompt` - 反向提示 (TextBox)
- `ReasoningFormat` - 推理格式 (ComboBox: auto/deepseek/none)
- `ReasoningBudget` - 推理预算 (NumericUpDown: -1-1000)
- `Special` - 特殊标记输出 (CheckBox)
- `NoWarmup` - 跳过预热 (CheckBox)
- `SpmInfill` - SPM填充模式 (CheckBox)

## 🎯 第十阶段：其他功能参数（低优先级）

### 10.1 MoE配置 (2个参数)
- `CpuMoe` - MoE权重保持在CPU (CheckBox)
- `NCpuMoe` - MoE CPU层数 (NumericUpDown: 0-100)
- `CpuMoeDraft` - 草稿模型MoE (CheckBox)
- `NCpuMoeDraft` - 草稿模型MoE层数 (NumericUpDown: 0-100)

### 10.2 其他参数 (6个参数)
- `SWAFull` - 全尺寸SWA缓存 (CheckBox)
- `NParallel` - 并行处理数 (NumericUpDown: 1-16)
- `Offline` - 离线模式 (CheckBox)
- `NoContextShift` - 禁用上下文转移 (CheckBox)
- `ContextShift` - 启用上下文转移 (CheckBox)
- `Jinja` - Jinja模板 (CheckBox)

## 实现建议

### UI设计原则
1. **分组组织**：使用Expander控件按功能分组
2. **合理布局**：保持现有Grid布局风格
3. **用户体验**：常用参数放在前面，高级参数可折叠
4. **工具提示**：为每个参数添加说明性工具提示
5. **实时预览**：保持命令预览功能实时更新

### 技术实现
1. **数据绑定**：使用Avalonia的双向绑定
2. **验证逻辑**：添加参数范围和格式验证
3. **错误处理**：友好的错误提示
4. **国际化**：所有新参数添加中英文资源
5. **响应式设计**：确保在不同屏幕尺寸下正常显示

## 预期成果

实现这个计划后，将达到：
- **95%+的参数UI覆盖**
- **完整的功能支持**
- **优秀的用户体验**
- **生产环境就绪**