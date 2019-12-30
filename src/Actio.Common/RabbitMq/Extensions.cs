using Actio.Common.Commands;
using RawRabbit;
using System.Threading.Tasks;
using RawRabbit.Pipe;
using System.Reflection;
using Actio.Common.Events;

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

    }
}
