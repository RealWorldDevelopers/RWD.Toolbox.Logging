using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RWD.Toolbox.Logging.Demo.ClassLibrary;
using RWD.Toolbox.Logging.Infrastructure.Filters;
using RWD.Toolbox.Logging.Infrastructure.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// setup logging
var name = Assembly.GetExecutingAssembly().GetName();
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    //.MinimumLevel.Debug() 
    //.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    //.Enrich.WithAspnetcoreHttpcontext(serviceProvider)
    //.Enrich.FromLogContext()
    //.Enrich.WithExceptionDetails()
    //.Enrich.WithMachineName()
    //.Enrich.WithEnvironmentName()
    //.Enrich.WithEnvironmentUserName()
    .Enrich.WithProperty("Assembly", $"{name.Name}")
    .Enrich.WithProperty("Version", $"{name.Version}")
    //.WriteTo.File(new RenderedCompactJsonFormatter(), @"E:\Testing\error.json", shared: true)
    // .WriteTo.MSSqlServer(connectionString: AppSettings.ConnString,
    //                      sinkOptions: new MSSqlServerSinkOptions { TableName = "Log_Error", AutoCreateSqlTable = true, BatchPostingLimit = 1 },
    //                      columnOptions: Logger.GetSqlColumnOptions())
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);


// add cors settings
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        bldr =>
        {
            bldr
                .WithOrigins("https://localhost:7052", "https://www.winemakerssoftware.com")
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});


// Register for DI
builder.Services.AddTransient<IWeatherData, WeatherData>();


// Global Performance and Usage Tracking
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(TrackActionPerformanceFilter));
    options.Filters.Add(typeof(TrackActionUsageFilter));
});

// misc services needed
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Demo API",
        Version = "v1",
        Description = "Description for the API goes here.",
        Contact = new OpenApiContact
        {
            Name = "Ankush Jain",
            Email = string.Empty,
            Url = new Uri("https://coderjony.com/"),
        },
    });
});


// Configure the HTTP request pipeline.
var app = builder.Build();

// development only
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo API V1"); });
}

// Return custom safe API errors in production
app.UseApiExceptionHandler(options =>
{
    options.AddResponseDetails = UpdateApiErrorResponse;
    options.DetermineLogLevel = DetermineLogLevel;
});
app.UseHsts();

app.UseCors();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();



// Determine how to classify error
LogLevel DetermineLogLevel(Exception ex)
{
    if (ex.Message.StartsWith("cannot open database", StringComparison.InvariantCultureIgnoreCase) ||
        ex.Message.StartsWith("a network-related", StringComparison.InvariantCultureIgnoreCase))
    {
        return LogLevel.Critical;
    }

    return LogLevel.Error;
}


// Add Custom Notes to Errors
void UpdateApiErrorResponse(HttpContext context, Exception ex, ApiError error)
{
    if (ex.GetType().Name == nameof(SqlException))
    {
        error.Detail = "Exception was a database exception!";
    }
}
