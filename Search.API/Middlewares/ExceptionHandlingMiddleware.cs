using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Search.Application.Enums;
using Search.Domain.Dto;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Search.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        public ExceptionHandlingMiddleware(RequestDelegate requestDelegate, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
        }

        /// <summary>
        /// This method handles the exception and if there is no exception it will go to the next request.
        /// </summary>
        /// <param name="context">context object is passed to get the http header values</param>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception Message :{ex.Message}, StackTrace: {ex.StackTrace}");
                await HandleException(context, ex);
            }
        }

        /// <summary>
        /// Handles Exception which is thrown at runtime
        /// </summary>
        /// <param name="context">http context parameter for assigning user details</param>
        /// <param name="exception">exception parameter to get exception details</param>
        /// <returns>Response with custom message for exception</returns>
        private Task HandleException(HttpContext context, Exception exception)
        {
            string innerException = string.Empty;
            int statusCode;
            var type = exception.GetType();
            ErrorDetails errorDetails = new();
            if (exception.InnerException != null)
            {
                innerException = exception.InnerException.ToString();
            }
            switch (exception)
            {
                case UnauthorizedAccessException ex:
                    statusCode = StatusCodes.Status401Unauthorized;
                    errorDetails.ErrorCode = ErrorCode.Unauthorized.ToString();
                    errorDetails.ErrorDescription = $"Type: {type}, Message: {ex.Message}, StackTrace: {ex.StackTrace}.\n InnerException : {innerException}";
                    break;
                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    errorDetails.ErrorCode = ErrorCode.SystemError.ToString();
                    errorDetails.ErrorDescription = $"Type: {type}, Message: {exception.Message}, StackTrace: {exception.StackTrace} .\n InnerException : {innerException}";
                    break;
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(errorDetails));
        }
    }
}
