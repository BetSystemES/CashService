using CashService.DataAccess.Extensions;
using CashService.GRPC.Infrastructure.Configurations;
using CashService.GRPC.Interceptors;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args)
    .AddAppSettings()
    .AddSerialLogger();

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

var connectionString = builder.Configuration.GetConnectionString("CashDb");

builder.Services.AddPostgreSqlContext(options =>
{
    options.UseNpgsql(connectionString, opt => opt.EnableRetryOnFailure(3));
});

// Add services to the container.
builder.Services
    .AddRepositories()
    .AddProviders()     
    .AddInfrastructureServices()
    .AddFluentValidation()
    .AddGrpc(options =>
    {
        options.Interceptors.Add<ErrorHandlingInterceptor>();
        options.Interceptors.Add<ValidationInterceptor>();
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<CashService.GRPC.Services.CashService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();

namespace CashService.Grpc
{
    public partial class Program { }
}