namespace Application.Contracts.UseCase;

/// <summary>
/// Represents the outcome of an operation, including success or failure information.
/// </summary>
/// <typeparam name="T">The type of the value returned on success.</typeparam>
public class Result<T>
{
    /// <summary>
    /// Indicates whether the operation was successful.
    /// </summary>
    public bool IsSuccess { get; private set; }
    
    /// <summary>
    /// Gets the value of the result if the operation was successful; otherwise, null.
    /// </summary>
    public T? Value { get; private set; }
    
    /// <summary>
    /// Gets the error message if the operation failed; otherwise, null.
    /// </summary>
    public string? ErrorMessage { get; private set; }
    
    /// <summary>
    /// Gets the exception that caused the failure, if any.
    /// </summary>
    public Exception? Exception { get; private set; }

    private Result(T value)
    {
        IsSuccess = true;
        Value = value;
    }

    private Result(string error, Exception exception)
    {
        IsSuccess = false;
        ErrorMessage = error;
        Exception = exception;
    }

    /// <summary>
    /// Creates a successful result wrapping the given value.
    /// </summary>
    public static Result<T> Success(T value) => new(value);
    
    /// <summary>
    /// Creates a failure result with an error message and optional exception.
    /// </summary>
    public static Result<T> Failure(string error, Exception exception) => new(error, exception);
}
