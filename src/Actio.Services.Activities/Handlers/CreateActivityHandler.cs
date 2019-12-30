using Actio.Common.Commands;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Services.Activities.Handlers
{
    public class CreateActivityHandler : ICommandHandler<CreateAction>
    {
        private readonly IBusClient _busClient;

        public CreateActivityHandler(IBusClient busClient)
        {
            _busClient = busClient;
        }

        public async Task HandleAsync(CreateAction command)
        {
            Console.WriteLine($"Created: {command.Name}");
            await _busClient.PublishAsync(new Actio.Common.Events.ActivityCreated(command.Id, command.UserId,
                command.Category,command.Name,command.Description,command.CreatedAt));
        }
    }
}
