using FluentValidation;

namespace CashService.GRPC.Infrastructure.Configurations
{
    public static partial class AppConfigurations
    {
        public static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(Program).Assembly);
            return services;
        }
    }
}