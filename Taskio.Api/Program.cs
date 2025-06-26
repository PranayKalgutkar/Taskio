using Microsoft.EntityFrameworkCore;
using Taskio.App.IRepository;
using Taskio.App.IServices;
using Taskio.Infra.Services;
using Taskio.Persist;
using Taskio.Persist.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Path.Combine(builder.Environment.ContentRootPath, "Taskio.Api"))
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

builder.Services.AddControllers(); // Add this line to register controllers
// Register EF Core with PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("TaskioCon")));


// Add services to the container.
builder.Services.AddScoped<IUserService, UserService>();           // App Service
builder.Services.AddScoped<IUserRepository, UserRepository>();     // Data Access
builder.Services.AddScoped<IEmailService, EmailService>();         // External Service

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

