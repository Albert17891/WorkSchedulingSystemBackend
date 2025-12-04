namespace WorkSchedulingSystem.Application.Common;

public class ApiResult<T>
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public string? ErrorMessage { get; set; }
    public List<string>? Errors { get; set; }

    private ApiResult(bool isSuccess, T? data, string errorMessage, List<string>? errors = null)
    {
        IsSuccess = isSuccess;
        Data = data;
        ErrorMessage = errorMessage;
        Errors = errors ?? new List<string>();
    }

    public static ApiResult<T> Success(T data)
    {
        return new ApiResult<T>(true, data, string.Empty);
    }

    public static ApiResult<T> Failure(string errorMessage)
    {
        return new ApiResult<T>(false, default, errorMessage);
    }

    public static ApiResult<T> Failure(List<string> errors)
    {
        return new ApiResult<T>(false, default, string.Join(", ", errors), errors);
    }
}
