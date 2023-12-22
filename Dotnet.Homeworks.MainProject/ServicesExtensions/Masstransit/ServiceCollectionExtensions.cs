using Dotnet.Homeworks.MainProject.Configuration;
using MassTransit;

namespace Dotnet.Homeworks.MainProject.ServicesExtensions.Masstransit;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMasstransitRabbitMq(this IServiceCollection services,
        IConfiguration configuration)
    {
        var rabbitMqConfig = configuration.GetSection("RabbitMqConfig");
        services.AddMassTransit(c =>
        {
            c.UsingRabbitMq((ctx, cfg) =>
            {
                var uri = new Uri($"amqp://{rabbitMqConfig["Username"]}:{rabbitMqConfig["Password"]}@{rabbitMqConfig["Hostname"]}:{rabbitMqConfig["Port"]}");
                cfg.Host(uri);
                cfg.ConfigureEndpoints(ctx);
            });
        });

        return services;
    }
}