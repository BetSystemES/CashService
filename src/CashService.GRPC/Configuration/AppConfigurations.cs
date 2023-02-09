using CashService.BusinessLogic.Contracts.IServices;
using CashService.BusinessLogic.Services;

namespace CashService.GRPC.Configuration
{
    public static partial class AppConfiguration
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
