# LLaMA.cpp Server Launcher
[LLaMA.cpp HTTP 服务器](https://github.com/ggml-org/llama.cpp/tree/master/tools/server)的配置启动器

## 开发环境
- .NET SDK 8.0

## 运行环境
- Windows / Linux / macOS 桌面
- .NET Runtime 8.0
- 暂不支持 AOT 编译，如有需求请及时提出，并且暂时用自包含 R2R 编译

## 用法
- 下载 LLaMA.cpp 服务器的编译产物
- 下载 GGUF 模型文件
- 启动本程序
- 配置服务器可执行文件路径和模型文件
- 根据需要配置其他参数
- 点击启动服务器
- 访问 HTTP API 端点或网页界面

## 功能
- 完整的启动命令行参数配置
- 预览启动命令
- 记忆上次启动和关闭程序的配置
- 实时查看服务端的命令行输出
- 参数过滤和分类功能
- 保存和加载配置文件
- 一键打开浏览器访问网页界面
- 服务器状态监控和控制