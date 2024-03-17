using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Search.API.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate requestDelegate, ILogger<LoggingMiddleware> logger)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            string requestLog = $"Request: {context.Request.Method}, Path: {context.Request.Path}";
            _logger.LogInformation($"{requestLog}");
            WriteToFile(requestLog);

            //capture request body
            context.Request.EnableBuffering();
            var requestBody = await RequestFormatting(context.Request);
            string requestBodyLog = $"Request Body: {requestBody}";
            _logger.LogInformation(requestBodyLog);
            WriteToFile(requestBodyLog);

            //capture response body
            var originalStreamBody = context.Response.Body;
            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;
                await _requestDelegate(context);
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                var response = await new StreamReader(context.Response.Body).ReadToEndAsync();
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                string responseLog = $"Response : {response}";
                _logger.LogInformation(responseLog);
                WriteToFile(responseLog);
                await responseBody.CopyToAsync(originalStreamBody);
            }
        }

        //request formatting
        private async Task<string> RequestFormatting(HttpRequest request)
        {
            request.EnableBuffering();
            var content = new Byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(content, 0, content.Length);
            var requestBody = Encoding.UTF8.GetString(content);
            request.Body.Seek(0, SeekOrigin.Begin);
            return $"{requestBody}";
        }

        //writing to local file
        private void WriteToFile(string message)
        {
            string logFilePath = "Logs";
            string logFileName = $"log_{DateTime.Now:yyyyMMdd}.txt";
            string fullLogPath = Path.Combine(logFilePath, logFileName);
            using (StreamWriter streamWriter = new(fullLogPath, true))
            {
                streamWriter.WriteLine(message);
            }
        }
    }
}
