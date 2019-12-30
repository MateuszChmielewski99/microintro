﻿using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Common.RabbitMq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using RawRabbit;
using System;
using System.Threading.Tasks;

namespace Actio.Common.Services
{
    public class ServiceHost : IServiceHost
    {
        private readonly IWebHost _webHost;

        public ServiceHost(IWebHost webHost)
        {
            _webHost = webHost;
        }

        public void Run() => _webHost.Run();

        public static HostBuilder Create<TStartup>(string[] args) where TStartup : class
        {
            Console.Title = typeof(TStartup).Namespace;
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            var webHostBuilder = WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(config)
                .UseStartup<TStartup>();

            return new HostBuilder(webHostBuilder.Build());
        }

        public abstract class BuilderBase 
        {
            public abstract ServiceHost Build();
        }

        public class HostBuilder : BuilderBase
        {
            private readonly IWebHost _webHost;
            private RawRabbit.IBusClient _bus;

            public HostBuilder(IWebHost webHost)
            {
                _webHost = webHost;
            }

            public BusBuilder UseRabbitMq() 
            {
                _bus = _webHost.Services.GetService(typeof(RawRabbit.IBusClient)) as RawRabbit.IBusClient;

                return new BusBuilder(_webHost, _bus);
            }

            public override ServiceHost Build()
            {
                return new ServiceHost(_webHost);
            }
        }

        public class BusBuilder : BuilderBase
        {
            private readonly IWebHost _webHost;
            private RawRabbit.IBusClient _bus;

            public BusBuilder(IWebHost webHost, IBusClient bus)
            {
                _webHost = webHost;
                _bus = bus;
            }

            public async Task<BusBuilder> SubscribeToCommand<TCommand>() where TCommand : ICommand 
            {
                var handler = _webHost.Services.GetService(typeof(ICommandHandler<TCommand>)) as ICommandHandler<TCommand>;

                await _bus.WithCommandHandlerAsync(handler);

                return this;
            }

            public async Task<BusBuilder> SubscribeToEvent<TEvent>() where TEvent : IEvent
            {
                var handler = _webHost.Services.GetService(typeof(IEventHandler<TEvent>)) as IEventHandler<TEvent>;

                await _bus.WithEventHandlerAsync(handler);

                return this;
            }

            public override ServiceHost Build()
            {
                throw new NotImplementedException();
            }
        }

    }
}
