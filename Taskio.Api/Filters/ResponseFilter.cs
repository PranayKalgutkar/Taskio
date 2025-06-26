using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Taskio.Api.Filters
{
    public class ResponseFilter : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var correlationId = context.HttpContext.Items["CorrelationId"]?.ToString();

            if (context.Result is ObjectResult objectResult &&
                objectResult.StatusCode >= 200 && objectResult.StatusCode < 300)
            {
                var value = objectResult.Value;
                string status = "success";

                // If value is a plain string and indicates a failure reason, change status to "failed"
                if (value is string message &&
                    (message.Contains("already exists", StringComparison.OrdinalIgnoreCase) ||
                     message.Contains("not found", StringComparison.OrdinalIgnoreCase) ||
                     message.Contains("failed", StringComparison.OrdinalIgnoreCase)))
                {
                    status = "failed";
                }

                var wrapped = new
                {
                    status,
                    correlationId = correlationId,
                    data = objectResult.Value
                };

                context.Result = new ObjectResult(wrapped)
                {
                    StatusCode = objectResult.StatusCode
                };
            }
            await next();
        }
    }
}