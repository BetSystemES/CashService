﻿namespace CashService.GRPC.Infrastructure.Configurations
{
    /// <summary>
    ///   Add environment variables to <seealso cref="WebApplicationBuilder"/>
    /// </summary>
    public static partial class AppConfigurations
    {
        /// <summary>Adds the application settings.</summary>
        /// <param name="applicationBuilder">The application builder.</param>
        /// <returns>
        ///   <seealso cref="WebApplicationBuilder"/>
        /// </returns>
        public static WebApplicationBuilder AddAppSettings(this WebApplicationBuilder applicationBuilder)
        {
            applicationBuilder.Host.ConfigureAppConfiguration(config =>
            {
                var prefix = "CashService_";
                config.AddEnvironmentVariables(prefix);
                config.Build();
            });
            return applicationBuilder;
        }
    }
}