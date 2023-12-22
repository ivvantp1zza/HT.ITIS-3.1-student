using Dotnet.Homeworks.Mailing.API.Configuration;
using Dotnet.Homeworks.Mailing.API.Consumers;
using MassTransit;

namespace Dotnet.Homeworks.Mailing.API.ServicesExtensions;

public static class AddMasstransitRabbitMqExtensions
{
    public static IServiceCollection AddMasstransitRabbitMq(this IServiceCollection services,
        IConfiguration configuration)
    {
        var rabbitMqConfig = configuration.GetSection("RabbitMqConfig");
        services.AddMassTransit(c =>
        {
            var assembly = typeof(Program).Assembly;
            c.AddConsumers(assembly);
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

