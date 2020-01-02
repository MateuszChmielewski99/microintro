using Actio.Common.Commands;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Actio.Common.Events;
using Actio.Common.Exceptions;
using Actio.Services.Activities.Services;
using Microsoft.Extensions.Logging;

namespace Actio.Services.Activities.Handlers
{
    public class CreateActivityHandler : ICommandHandler<CreateAction>
    {
        private readonly IBusClient _busClient;
        private readonly IActivityService _activityService;
        private ILogger _logger;

        public CreateActivityHandler(IBusClient busClient, IActivityService activityService, ILogger logger)
        {
            _busClient = busClient;
            _activityService = activityService;
            _logger = logger;
        }

        public async Task HandleAsync(CreateAction command)
        {
            try
            {
                await _activityService.AddAsync(command.Id, command.UserId, command.Category, command.Name,
                    command.Description, command.CreatedAt);
            }
            catch (ActioException e)
            {
                await _busClient.PublishAsync(new CreateActivityRejected(e.Code, command.Id, e.Message));
                _logger.LogError(e.Message);
            }
            catch (Exception e)
            {
                await _busClient.PublishAsync(new CreateActivityRejected("error", command.Id, e.Message));
                _logger.LogError(e.Message);
            }
        }
    }
}