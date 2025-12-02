
using Azure.Monitor.OpenTelemetry.AspNetCore;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Data;
using ExpenseTracker.Service;
using ExpenseTracker.Web.Middlewares;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Context;
using Serilog.Templates;
using System.Net;

namespace ExpenseTracker.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Debug(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.Console()
                .Enrich.FromLogContext()
                .WriteTo.File("logs/expense-tracker-.log", rollingInterval: RollingInterval.Day)
                .CreateBootstrapLogger();
            try
            {
                Log.Information("Starting up the Expense Tracker API");
                var builder = WebApplication.CreateBuilder(args);

                builder.Host.UseSerilog((context, services, configuration) => configuration
                                   .ReadFrom.Configuration(context.Configuration)
                                   .ReadFrom.Services(services)
                                   .WriteTo.Console(new ExpressionTemplate(
                                       "[{@t:HH:mm:ss } {@l:u3}]{#if @tr is not null} ({substring(@tr,0,4)}:{substring(@sp,0,4)}){#end} {@m}\n{@x}"))
                                   .Enrich.FromLogContext(), writeToProviders: true);
                Log.Information("Starting up the Expense Tracker API Services");
                builder.Services.AddOpenTelemetry().UseAzureMonitor();

                // Add services to the container.

                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();
                builder.Services.AddDbContext<ExpenseTrackerDbContext>(options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
                        , providerOptions => providerOptions.EnableRetryOnFailure()
                        ).EnableSensitiveDataLogging(true);
                });

                builder.Services.AddScoped<IUserRepository, UserRepository>();
                builder.Services.AddScoped<IUserService, UserService>();
                builder.Services.AddScoped<IExpenseService, ExpenseService>();
                builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();

                var app = builder.Build();

                app.Use(async (context, next) =>
                {
                    using (LogContext.PushProperty("RequestPath", context.Request.Path))
                    using (LogContext.PushProperty("RequestId", context.TraceIdentifier))
                    using (LogContext.PushProperty("Method", context.Request.Method))
                    using (LogContext.PushProperty("Query", context.Request.QueryString.Value))
                    {
                        await next();
                    }
                });

                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";

                        var exceptionHandlerPathFeature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();
                        if (exceptionHandlerPathFeature?.Error != null)
                        {
                            // Log the exception with Serilog
                            Log.Error(exceptionHandlerPathFeature.Error, "Unhandled exception occurred while processing {Path}", exceptionHandlerPathFeature.Path);
                            Console.WriteLine(exceptionHandlerPathFeature.Error.ToString());
                        }

                        await context.Response.WriteAsync("{\"error\":\"An unexpected error occurred.\"}");
                    });
                });

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();

                app.UseAuthorization();

                app.UseMiddleware<RequestResponseLoggingMiddleware>();
                app.MapControllers();

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
