using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentGateway.Context.Interface;

namespace PaymentGateway.Context
{
    public static class ServiceProvider
    {
        public static IServiceCollection AddPaymentGatewayContext(this IServiceCollection services, IConfiguration configuration, int poolSize = 128)
        {
            var connectionString = configuration.GetConnectionString("PaymentGateway");
            var readOnlyConnectionString = configuration.GetConnectionString("PaymentGatewayReadOnly");
            services.AddEntityFrameworkNpgsql();
            services.AddDbContextPool<IPaymentGatewayContext, PaymentGatewayContext>((serviceProvider, optionsBuilder) =>
            {
                optionsBuilder
                    .UseNpgsql(
                    connectionString,
                    npgsqlOptons =>
                    {
                        npgsqlOptons.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
                        npgsqlOptons.EnableRetryOnFailure();
                    })
                    .UseInternalServiceProvider(serviceProvider);
#if DEBUG
                optionsBuilder.EnableDetailedErrors();
                optionsBuilder.EnableSensitiveDataLogging();
#endif
            }, poolSize);


            services.AddDbContextPool<IReadOnlyPaymentGatewayContext, ReadOnlyPaymentGatewayContext>((serviceProvider, optionsBuilder) =>
            {
                optionsBuilder
                    .UseNpgsql(
                    readOnlyConnectionString,
                    npgsqlOptons =>
                    {
                        npgsqlOptons.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
                        npgsqlOptons.EnableRetryOnFailure();
                    })
                    .UseInternalServiceProvider(serviceProvider)
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
#if DEBUG
                optionsBuilder.EnableDetailedErrors();
                optionsBuilder.EnableSensitiveDataLogging();
#endif
            }, poolSize);

            return services;
        }
    }
}
