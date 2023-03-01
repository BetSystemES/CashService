using CashService.BusinessLogic.Contracts.IServices;
using CashService.BusinessLogic.Services;
using FluentValidation;

namespace CashService.GRPC.Configuration
{
    // TODO: Rename class from AppConfiguration to AppConfigurations
    // TODO: Change file location to CashService.Grpc.Infrastructure.Configurations
    public static partial class AppConfiguration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile<DataAccessProfile>();
            });

            services.AddScoped<ICashService, CashTransferService>();

            // TODO: Add new AppConfigurations partial class for fluent validation
            services.AddValidatorsFromAssembly(typeof(Program).Assembly);

            return services;
        }

    }
}
