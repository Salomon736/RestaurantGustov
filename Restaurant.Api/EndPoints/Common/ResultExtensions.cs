using System.Net;
using Restaurant.Domain.Responses;

namespace Restaurant.Api.EndPoints.Common;

public static class ResultExtensions
{
    public static async Task<IResult> ToApiResult<T>(this Task<Result<T>> resultTask)
    {
        var result = await resultTask;
        if (!result.IsSuccess && (result.StatusCode == HttpStatusCode.BadRequest || result.StatusCode == HttpStatusCode.NotFound))
        {
            return Results.Json(result, statusCode: StatusCodes.Status200OK);
        }
        return Results.Json(result, statusCode: (int)result.StatusCode);
    }
}