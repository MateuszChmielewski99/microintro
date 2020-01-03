using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Actio.Common.Events
{
    public class CreateUserRejected : IRejectedEvent
    {
        public string Reason { get; }

        public string Code { get; }

        public string Email { get; }

        public CreateUserRejected()
        {

        }

        public CreateUserRejected(string reason, string code, string email)
        {
            Reason = reason;
            Code = code;
            Email = email;
        }
    }
}