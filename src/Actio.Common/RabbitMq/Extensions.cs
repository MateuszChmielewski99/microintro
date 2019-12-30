using Actio.Common.Commands;
using RawRabbit;
using System.Threading.Tasks;
using System.Reflection;
using Actio.Common.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using RawRabbit.Instantiation;

namespace Actio.Common.RabbitMq
{
    public static class Extensions
    {
        public static async Task WithCommandHandlerAsync<TCommand>(this IBusClient bus, ICommandHandler<TCommand> handler)
            where TCommand : ICommand
        => await
            bus.SubscribeAsync<TCommand>(msg => handler.HandleAsync(msg),
            ctx => ctx.UseSubscribeConfiguration(cfg =>
            cfg.FromDeclaredQueue(q => q.WithName(GetQueueName<TCommand>()))));

        public static async Task WithEventHandlerAsync<TEvent>(this IBusClient bus, IEventHandler<TEvent> handler)
            where TEvent : IEvent
        => await
            bus.SubscribeAsync<TEvent>(msg => handler.HandleAsync(msg),
                ctx => ctx.UseSubscribeConfiguration(cfg =>
                cfg.FromDeclaredQueue(q => q.WithName(GetQueueName<TEvent>()))));

        private static string GetQueueName<T>() => $"{Assembly.GetEntryAssembly().GetName()}/{typeof(T).Name}";

        public static void AddRabbitMq(this IServiceCollection service, IConfiguration configuration) 
        {
            var options = new RawRabbit.Configuration.RawRabbitConfiguration();
            var section = configuration.GetSection("rabbitmq");
            section.Bind(options);

            var client = RawRabbitFactory.CreateSingleton(new RawRabbitOptions
            {
                ClientConfiguration = options
            });

            service.AddSingleton<IBusClient>(_ => client);
        }
    }
}
