# LLaMA.cpp Server Launcher

Configuration launcher for [LLaMA.cpp HTTP server](https://github.com/ggml-org/llama.cpp/tree/master/tools/server)

[中文说明](ReadMe.zh-CN.md)

## Development Environment
- .NET SDK 8.0

## Runtime Environment
- Windows / Linux / macOS desktop
- .NET Runtime 8.0
- AOT compilation is not currently supported. Please raise an issue if needed, and use self-contained R2R compilation for now.

## Usage
- Download the compiled artifacts of the LLaMA.cpp server
- Download the GGUF model files
- Start this program
- Configure the server executable path and model file
- Configure additional parameters as needed
- Click "Start Server"
- Access the HTTP API endpoints or web interface

## Features
- Complete startup command-line parameter configuration
- Preview startup command
- Remember configuration from last startup and shutdown
- View server command-line output in real-time
- Parameter filtering and categorization
- Save and load configuration files
- One-click browser opening for web interface
- Server status monitoring and control