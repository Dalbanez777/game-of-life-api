using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using GameOfLife.Services;

namespace GameOfLife
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add environment config support
            builder.Configuration
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                   .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                   .AddEnvironmentVariables();

            // Services
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<GameOfLifeService>();

            var env = builder.Environment;
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

builder.Logging.Configure(options =>
{
    Console.WriteLine($"🌱 Running in: {env.EnvironmentName}");
});

            var app = builder.Build();

            // Error handling
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";

                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (errorFeature != null)
                    {
                        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
                        logger.LogError(errorFeature.Error, "Unhandled exception");

                        await context.Response.WriteAsJsonAsync(new { error = "An unexpected error occurred. Please try again later." });
                    }
                });
            });

            app.UseSwagger();
            app.UseSwaggerUI();
            app.MapControllers();

            app.Run();
        }
    }
}