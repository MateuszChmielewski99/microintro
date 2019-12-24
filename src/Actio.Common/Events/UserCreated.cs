﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Actio.Common.Events
{
    class UserCreated : IEvent
    {
        public string Email { get;}
        public string Name { get; }

        protected UserCreated() { }

        public UserCreated(string email, string name)
        {
            this.Email = email;
            this.Name = name;
        }
    }
}