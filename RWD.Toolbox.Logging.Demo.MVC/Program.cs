using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using RWD.Toolbox.Logging.Infrastructure.Filters;
using RWD.Toolbox.Logging.Demo.Communication;

var builder = WebApplication.CreateBuilder(args);

// setup logging
var name = Assembly.GetExecutingAssembly().GetName();
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    //.MinimumLevel.Information()
    //.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    //.Enrich.WithAspnetcoreHttpcontext(serviceProvider)
    //.Enrich.FromLogContext()
    //.Enrich.WithExceptionDetails()
    //.Enrich.WithMachineName()
    //.Enrich.WithEnvironmentName()
    //.Enrich.WithEnvironmentUserName()
    .Enrich.WithProperty("Assembly", $"{name.Name}")
    .Enrich.WithProperty("Version", $"{name.Version}")
    .WriteTo.File(new RenderedCompactJsonFormatter(), @"E:\Testing\error.json", shared: true)
    // .WriteTo.MSSqlServer(connectionString: AppSettings.ConnString,
    //                      sinkOptions: new MSSqlServerSinkOptions { TableName = "Log_Error", AutoCreateSqlTable = true, BatchPostingLimit = 1 },
    //                      columnOptions: Logger.GetSqlColumnOptions())
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);


// register for DI
builder.Services.AddTransient<ICommunicationAgent, CommunicationAgent>();
builder.Services.AddHttpContextAccessor();


builder.Services.AddMvc(options =>
{
    options.Filters.Add(typeof(TrackActionPerformanceFilter));
    options.Filters.Add(typeof(TrackActionUsageFilter));
});

var app = builder.Build();


//if (!app.Environment.IsDevelopment())
//{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
//}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();


