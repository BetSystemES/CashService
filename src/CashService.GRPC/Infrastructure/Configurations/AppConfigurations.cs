using CashService.BusinessLogic.Contracts;
using CashService.BusinessLogic.Contracts.Services;
using CashService.BusinessLogic.Services;
using CashService.DataAccess;
using CashService.GRPC.Infrastructure.Mappings;

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
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IResilientService, ResilientService>();
            return services;
        }
    }
}