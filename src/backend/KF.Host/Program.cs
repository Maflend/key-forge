using KF.Host;
using KF.Host.Features.Accounts;
using KF.Host.Features.Payments;
using KF.Host.OTel;
using KF.Host.Persistence;
using KF.Host.RestApiConfigurations;
using KF.Host.Settings;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi(OpenApiConfiguration.Configure);

builder.ConfigureOpenTelemetry();

builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy(), ["live"]);

builder.Services.AddServiceDiscovery();

builder.Services.ConfigureHttpClientDefaults(http =>
{
    http.AddStandardResilienceHandler();
    http.AddServiceDiscovery();
});

builder.Services.AddLogging();
builder.Services.AddHttpLogging();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddSettings(builder.Configuration);

var app = builder.Build();

app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(ScalarCustomConfiguration.Configure);
    // All health checks must pass for app to be considered ready to accept traffic after starting
    app.MapHealthChecks(Paths.HealthEndpointPath);

    // Only health checks tagged with the "live" tag must pass for app to be considered alive
    app.MapHealthChecks(Paths.AlivenessEndpointPath, new HealthCheckOptions
    {
        Predicate = r => r.Tags.Contains("live")
    });
}

app.UseHttpsRedirection();
app.MapAccountsEndpoints();
app.MapPaymentsEndpoints();

app.Run();