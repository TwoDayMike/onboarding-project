using Application;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using Web;


var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.Debug()
            .CreateBootstrapLogger();

try
{
    builder.Host.ConfigureHostOptions(options =>
    {
        options.ShutdownTimeout = TimeSpan.FromSeconds(30);
    });

    builder.Host.UseSerilog((hostBuilderContext, loggerConfiguration) =>
    {
        loggerConfiguration
        .MinimumLevel.Information()
        .WriteTo.Console(LogEventLevel.Information)
        .WriteTo.Debug(LogEventLevel.Information);

        string? IntrumentationKey = Environment.GetEnvironmentVariable("APPLICATIONINSIGHTS__INSTRUMENTATIONKEY");
        if (IntrumentationKey is not null && builder.Environment.IsProduction())
        {
            var insightsConnectionString = $"InstrumentationKey={IntrumentationKey}";
            var telemetryConfiguration = TelemetryConfiguration.CreateDefault();
            Log.Logger.Information("Setting up insights logging");
            telemetryConfiguration.ConnectionString = insightsConnectionString;
            loggerConfiguration
                .WriteTo.ApplicationInsights(telemetryConfiguration, TelemetryConverter.Events, LogEventLevel.Information);

        }
        else
        {
            Log.Logger.Information("Missing IntrumentationKey");
            loggerConfiguration.WriteTo.Logger(lc =>
                lc
                .WriteTo.File("./Logs/log-.txt", restrictedToMinimumLevel: LogEventLevel.Information,
                rollingInterval: RollingInterval.Day,
                retainedFileTimeLimit: TimeSpan.FromDays(30),
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] ({SourceContext}) {Message}{NewLine}{Exception}")
                );
        }
    });


    //Use keyvault if KEYVAULT_ENDPOINT is set on prod.
    if (builder.Environment.IsProduction())
    {
        string? keyVaultEndpoint = Environment.GetEnvironmentVariable("KEYVAULT__ENDPOINT");
        if (keyVaultEndpoint is not null)
        {
            var secretClient = new SecretClient(new(keyVaultEndpoint), new DefaultAzureCredential());
            builder.Configuration.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());
        }
    }

    builder.Services.AddHealthChecks();

    ApplicationServiceConfiguration.ConfigureServices(builder.Services);
    InfrastructureServiceConfiguration.ConfigureServices(builder.Services, builder.Configuration, builder.Environment);
    WebServiceConfiguration.ConfigureServices(builder.Services, builder.Configuration);

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    if (app.Environment.EnvironmentName != "NSwag")
    {
        if (app.Environment.IsProduction())
        {
            await using var scope = app.Services.CreateAsyncScope();
            using var db = scope.ServiceProvider.GetService<ApplicationDbContext>();
            //Only migrate database if it exists.
            if (db is not null && db.Database.CanConnect())
                await db.Database.MigrateAsync();
        }
    }



    app.UseCors("DefaultCors");

    app.UseSerilogRequestLogging();
    app.UseHealthChecks("/health");
    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseSwaggerUi3();
    app.UseOpenApi();

    app.UseRouting();

    //TODO add auth.
    //app.UseAuthentication();
    //app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}");

    app.Run();
}
catch (Exception e)
{
    Log.Logger.Fatal(e.Message);
}