using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Common.Exceptions;
using Actio.Services.Identity.Services;
using Microsoft.Extensions.Logging;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Services.Identity.Handlers
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private readonly IBusClient _busClient;
        private ILogger _logger;
        private readonly IUserService _userService;

        public CreateUserHandler(IBusClient busClient, ILogger logger, IUserService userService)
        {
            _busClient = busClient;
            _logger = logger;
            _userService = userService;
        }

        public async Task HandleAsync(CreateUser command)
        {
            try
            {
                await _userService.RegisterAsync(command.Email, command.Name, command.Password);
                await _busClient.PublishAsync(new UserCreated(command.Email, command.Name));

                return;
            }
            catch (ActioException ex) 
            {
                await _busClient.PublishAsync(new CreateUserRejected("error",ex.Code,command.Email));
            }
        }
    }
}
