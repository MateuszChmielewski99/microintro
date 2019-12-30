using Actio.Common.Commands;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RawRabbit.Common;

namespace Actio.Api.Controllers
{   
    [Route("[controller]")]
    public class ActivitiesController : Controller
    {   
        private readonly IBusClient _busClient;
        public ActivitiesController(IBusClient client) 
        {
            this._busClient = client;
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] CreateAction command)
        {
            command.Id = Guid.NewGuid();
            command.CreatedAt = DateTime.UtcNow;

            await _busClient.PublishAsync(command);

            return Accepted($"activities/{command.Id}");
        }
    }
}
