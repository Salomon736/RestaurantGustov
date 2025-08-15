namespace Restaurant.Domain.Responses;
using  System.Net;
public class Result<T>
{
    public T Data { get; private set; }
    public bool IsSuccess { get; private set; }
    public string Message { get; private set; }
    public HttpStatusCode StatusCode { get; private set; }
    public List<string> Errors { get; private set; }

    private Result(T data, bool isSuccess, string message, List<string> errors, HttpStatusCode statusCode)
    {
        Data = data;
        IsSuccess = isSuccess;
        Message = message;
        Errors = errors ?? new List<string>();
        StatusCode = statusCode;
    }

    public static Result<T> Success(T data, HttpStatusCode statusCode)
        => new Result<T>(data, true,StatusMensages.GetMessage((int)statusCode), new List<string>(), statusCode);

    public static Result<T> Failure(List<string> errors, HttpStatusCode statusCode)
        => new Result<T>(default, false, StatusMensages.GetMessage((int)statusCode), errors, statusCode);
}