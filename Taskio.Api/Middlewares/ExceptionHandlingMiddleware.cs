using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taskio.App.IServices;

namespace Taskio.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Resolve ILoggerService manually within the scope of the request
                var loggerService = context.RequestServices.GetService<ILoggerService>();

                var correlationId = context.Items["CorrelationId"]?.ToString();
                var path = context.Request.Path;

                if (loggerService != null)
                {
                    await loggerService.LogException(ex, path, correlationId);
                }

                context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";

                    var errorResponse = new
                    {
                        status = "error",
                        correlationId = correlationId,
                        data = "Internal Error Occurred"
                    };

                await context.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }
}