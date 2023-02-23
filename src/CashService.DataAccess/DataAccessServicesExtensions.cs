using CashService.BusinessLogic.Contracts.IProviders;
using CashService.BusinessLogic.Contracts.IRepositories;
using CashService.EntityModels.Models;
using CashService.DataAccess.EF;
using CashService.DataAccess.Providers;
using CashService.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CashService.DataAccess
{
    public static class DataAccessServicesExtensions
    {
        /// <summary>Register the PostgreSql context.</summary>
        /// <param name="services">The services collection.</param>
        /// <returns>
        ///   The services collection.
        /// </returns>
        public static IServiceCollection AddPostgreSqlContext(this IServiceCollection services,
            Action<DbContextOptionsBuilder> options)
        {
            services.AddDbContextPool<CashDbContext>(options);

            services.AddScoped<IDataContext, CashDataContext>();

            services.AddScoped(serviceProvider =>
                serviceProvider.GetRequiredService<CashDbContext>()
                    .Set<TransactionEntity>());
            services.AddScoped(serviceProvider =>
                serviceProvider.GetRequiredService<CashDbContext>()
                    .Set<TransactionProfileEntity>());

            return services;
        }

        /// <summary>Register repositories in service collection.</summary>
        /// <param name="services">The services collection.</param>
        /// <returns>
        ///   The services collection.
        /// </returns>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services
                .AddScoped<IRepository<TransactionProfileEntity>, TransactionProfileRepositiry>()
                .AddScoped<IRepository<TransactionEntity>, TransactionRepositiry>();
            return services;
        }

        /// <summary>Register providers in service collection.</summary>
        /// <param name="services">The services collection.</param>
        /// <returns>
        ///   The services collection.
        /// </returns>
        public static IServiceCollection AddProviders(this IServiceCollection services)
        {
            services.AddScoped<ICashProvider, CashProvider>();

            return services;
        }


    }
}
