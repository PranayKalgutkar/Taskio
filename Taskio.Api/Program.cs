using Microsoft.EntityFrameworkCore;
using Taskio.Api.Filters;
using Taskio.Api.Middlewares;
using Taskio.App.IRepository;
using Taskio.App.IServices;
using Taskio.Infra.Services;
using Taskio.Persist;
using Taskio.Persist.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ResponseFilter>();
});
// Register EF Core with PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("TaskioCon")));

//Register CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("Taskio-ui", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add services to the container.
builder.Services.AddScoped<IUserService, UserService>();           // App Service
builder.Services.AddScoped<IUserRepository, UserRepository>();     // Data Access
builder.Services.AddScoped<ILoggerService, LoggerService>();
builder.Services.AddScoped<ILoggerRepository, LoggerRepository>();

builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();


builder.Services.AddScoped<IEmailService, EmailService>();         // External Service

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable CORS
app.UseCors("Taskio-ui");

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

