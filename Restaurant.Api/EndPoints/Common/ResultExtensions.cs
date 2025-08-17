using Restaurant.Domain.Responses;

namespace Restaurant.Api.EndPoints.Common;

public static class ResultExtensions
{
    public static async Task<IResult> ToApiResult<T>(this Task<Result<T>> resultTask)
    {
        var result = await resultTask;
        return Results.Json(result, statusCode: (int)result.StatusCode);
    }
}