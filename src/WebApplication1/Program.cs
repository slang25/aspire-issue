using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

var resourceBuilder = ResourceBuilder.CreateDefault()
    .AddService("myapp")
    ;

builder.Services
    .AddOpenTelemetry()
    .WithMetrics(metricsBuilder =>
    {
        metricsBuilder.SetResourceBuilder(resourceBuilder)
            .AddAspNetCoreInstrumentation()
            .AddRuntimeInstrumentation();
    })
    .WithTracing(tracingBuilder =>
    {
        tracingBuilder.SetResourceBuilder(resourceBuilder)
            .AddAspNetCoreInstrumentation();
    });

var useOtlpExporter = !string.IsNullOrWhiteSpace(
    builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]);

if (useOtlpExporter)
{
    builder.Services.AddOpenTelemetry().UseOtlpExporter();
}

var app = builder.Build();
app.MapGet("/hi", () => "Hello");

app.Run();