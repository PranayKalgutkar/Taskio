using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taskio.App.IRepository;
using Taskio.App.IServices;
using Taskio.Domain.Entities;

namespace Taskio.Infra.Services
{
    public class LoggerService : ILoggerService
    {
        private readonly ILoggerRepository _loggerRepository;

        public LoggerService(ILoggerRepository loggerRepository)
        {
            _loggerRepository = loggerRepository;
        }
        public async Task LogException(Exception exception, string? path, string? correlationId)
        {
            var log = new ExceptionLog
            {
                CorrelationId = correlationId,
                Message = exception.Message,
                StackTrace = exception.StackTrace,
                Source = exception.Source,
                Path = path
            };
            await _loggerRepository.LogException(log);
            
            throw new NotImplementedException();
        }
    }
}