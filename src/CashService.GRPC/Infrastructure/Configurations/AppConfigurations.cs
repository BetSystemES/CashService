using CashService.BusinessLogic.Contracts.Services;
using CashService.BusinessLogic.Services;
using CashService.GRPC.Infrastructure.Mappings;
using FluentValidation;

namespace CashService.GRPC.Infrastructure.Configurations
{
    public static partial class AppConfigurations
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile<DataAccessProfile>();
            });
            services.AddScoped<ICashService, CashTransferService>();
            return services;
        }
    }
}