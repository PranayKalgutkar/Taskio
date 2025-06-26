using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Taskio.App.IRepository;
using Taskio.Domain.Entities;

namespace Taskio.Persist.Repositories
{
    public class LoggerRepository : ILoggerRepository
    {
        private readonly AppDbContext _dbContext;

        public LoggerRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task LogException(ExceptionLog exception)
        {
            _dbContext.ExceptionLogs.Add(exception);
            await _dbContext.SaveChangesAsync();
        }
    }
}