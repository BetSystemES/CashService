using CashService.BusinessLogic.Contracts.IServices;
using CashService.BusinessLogic.Services;
using FluentValidation;

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

            services.AddValidatorsFromAssembly(typeof(Program).Assembly);

            return services;
        }

    }
}
