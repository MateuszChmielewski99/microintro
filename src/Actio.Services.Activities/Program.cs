using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Common.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Actio.Services.Activities
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = await ServiceHost.Create<Startup>(args)
               .UseRabbitMq()
               .SubscribeToCommand<CreateAction>();

            builder.Build().Run();
        }

    }
}
