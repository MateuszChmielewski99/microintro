﻿using System;
namespace Actio.Common.Commands
{
    public class CreateAction : IAuthenticatedCommand
    {
        public Guid Id { get; set; }

        Guid IAuthenticatedCommand.UserId { get; set; }

        public string Category { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    
        public DateTime CreatedAt { get; set; }
    }
}
