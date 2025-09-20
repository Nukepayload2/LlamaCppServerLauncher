Imports System.Windows.Input
Imports System.Threading

Public Class RelayCommand
    Implements ICommand

    Private ReadOnly _execute As Action(Of Object)
    Private ReadOnly _executeAsync As Func(Of Object, CancellationToken, Task)
    Private ReadOnly _canExecute As Func(Of Object, Boolean)
    Private ReadOnly _onException As Action(Of Exception)

    Private _isExecuting As Boolean
    Private ReadOnly _executionLock As New Object()
    Private _cancellationTokenSource As New CancellationTokenSource()

    Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

    Sub New(execute As Action(Of Object),
            Optional canExecute As Func(Of Object, Boolean) = Nothing,
            Optional onException As Action(Of Exception) = Nothing)
        ArgumentNullException.ThrowIfNull(execute)
        _execute = execute
        _canExecute = canExecute
        _onException = onException
    End Sub

    Sub New(execute As Func(Of Object, CancellationToken, Task),
            Optional canExecute As Func(Of Object, Boolean) = Nothing,
            Optional onException As Action(Of Exception) = Nothing)
        ArgumentNullException.ThrowIfNull(execute)
        _executeAsync = execute
        _canExecute = canExecute
        _onException = onException
    End Sub

    Public Sub Execute(parameter As Object) Implements ICommand.Execute
        Try
            If _executeAsync IsNot Nothing Then
                ' 异步执行 - 使用 Fire-and-forget 模式但有状态管理
                ExecuteAsyncInternal(parameter)
            ElseIf _execute IsNot Nothing Then
                ' 同步执行
                _execute(parameter)
            End If
        Catch ex As Exception
            HandleException(ex)
        End Try
    End Sub

    Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
        ' 检查是否正在执行异步操作
        SyncLock _executionLock
            If _isExecuting Then
                Return False
            End If
        End SyncLock

        ' 检查自定义条件
        Return _canExecute Is Nothing OrElse _canExecute(parameter)
    End Function

    Public Sub RaiseCanExecuteChanged()
        RaiseEvent CanExecuteChanged(Me, EventArgs.Empty)
    End Sub

    ''' <summary>
    ''' 取消当前正在执行的异步操作
    ''' </summary>
    Public Sub Cancel()
        Try
            _cancellationTokenSource.Cancel()
        Catch
            ' 取消操作失败时忽略异常
        End Try
    End Sub

    ''' <summary>
    ''' 获取当前是否正在执行异步操作
    ''' </summary>
    Public ReadOnly Property IsExecuting As Boolean
        Get
            SyncLock _executionLock
                Return _isExecuting
            End SyncLock
        End Get
    End Property

    ''' <summary>
    ''' 异步执行内部实现，包含状态管理和异常处理
    ''' </summary>
    Private Async Sub ExecuteAsyncInternal(parameter As Object)
        Dim cancellationToken As CancellationToken
        
        ' 原子性地设置执行状态
        SyncLock _executionLock
            If _isExecuting Then
                Return ' 已经在执行中，防止重复执行
            End If
            _isExecuting = True
            cancellationToken = _cancellationTokenSource.Token
        End SyncLock

        ' 通知 UI 状态变更
        RaiseCanExecuteChanged()

        Try
            ' 执行异步操作
            Await _executeAsync(parameter, cancellationToken)
        Catch ex As OperationCanceledException
            ' 取消操作是正常行为，不处理
        Catch ex As Exception
            ' 处理其他异常
            HandleException(ex)
        Finally
            ' 重置执行状态
            SyncLock _executionLock
                _isExecuting = False
                ' 重置取消令牌
                If _cancellationTokenSource.IsCancellationRequested Then
                    _cancellationTokenSource.Dispose()
                    _cancellationTokenSource = New CancellationTokenSource
                End If
            End SyncLock

            ' 通知 UI 状态恢复
            RaiseCanExecuteChanged()
        End Try
    End Sub

    ''' <summary>
    ''' 异常处理方法
    ''' </summary>
    Private Sub HandleException(ex As Exception)
        If _onException IsNot Nothing Then
            Try
                _onException(ex)
            Catch
                ' 异常处理器本身出错时静默处理
            End Try
        Else
            ' 如果没有自定义异常处理器，至少记录异常
            Debug.WriteLine($"RelayCommand 执行异常: {ex.GetType().Name} - {ex.Message}")
        End If
    End Sub

End Class