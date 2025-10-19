

public class OperationResult<T>
{
    public bool Success { get; init; }
    public string Message { get; init; } = string.Empty;
    public T? Data { get; init; }

    public static OperationResult<T> Ok(T? data = default, string message = "OK")
        => new() { Success = true, Message = message, Data = data };

    public static OperationResult<T> Fail(string message)
        => new() { Success = false, Message = message, Data = default };
}
