using Microsoft.AspNetCore.Server.Kestrel.Core;
using ProductC.Models;
using ProductC.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<FirebaseService>();

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Firebase service instead of MongoDB
builder.Services.AddSingleton<FirebaseService>();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.WithOrigins(
                    "http://localhost:5173",    // Add HTTP for development
                    "https://localhost:5173",   // Keep HTTPS
                    "https://localhost:56143"   // Other environments
                  )
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();          // If using cookies/auth
        });
});

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = 10 * 1024 * 1024; // 10 MB
});

builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = int.MaxValue;
});

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = long.MaxValue;
});

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection(); // Only force HTTPS in production
}
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.Run();