using System.Net;
using System.Text.Json;
using Microsoft.Data.SqlClient;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Responses;

namespace Restaurant.Api.Middleware
{
    public class MiddlewareException
    {
        private readonly RequestDelegate _next;

        public MiddlewareException(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var statusCode = HttpStatusCode.InternalServerError;

            var result = CreateErrorResponse(exception, ref statusCode);

            context.Response.StatusCode = (int)statusCode;
            var jsonResponse = JsonSerializer.Serialize(result);

            return context.Response.WriteAsync(jsonResponse);
        }

        private Result<object> CreateErrorResponse(Exception exception, ref HttpStatusCode statusCode)
        {
            var errors = new List<string>();
            CollectErrorMessages(exception, errors);
            if (exception is DomainException)
            {
                statusCode = HttpStatusCode.BadRequest;
            }
            else if (exception is ArgumentException || exception is ArgumentNullException)
            {
                statusCode = HttpStatusCode.BadRequest;
            }
            else if (exception is UnauthorizedAccessException)
            {
                statusCode = HttpStatusCode.Unauthorized;
            }
            else if (exception is NotImplementedException)
            {
                statusCode = HttpStatusCode.NotImplemented;
            }
            else if (exception is SqlException) 
            {
                statusCode = HttpStatusCode.ServiceUnavailable;
                errors.Add("Error de base de datos: " + exception.Message);
            }
            else
            {
                statusCode = HttpStatusCode.InternalServerError; 
            }

            return Result<object>.Failure(errors, statusCode);
        }


        private void CollectErrorMessages(Exception exception, List<string> errorList)
        {
            if (exception == null)
                return;

            if (exception is AggregateException aggregateException)
            {
                foreach (var innerException in aggregateException.InnerExceptions)
                {
                    CollectErrorMessages(innerException, errorList);
                }
            }
            else
            {
                errorList.Add(exception.Message);
                CollectErrorMessages(exception.InnerException, errorList);
            }
        }
    }
}