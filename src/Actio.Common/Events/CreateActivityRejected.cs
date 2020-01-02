using System;

namespace Actio.Common.Events
{
    public class CreateActivityRejected: IRejectedEvent
    {
        public string Code { get; }
        public Guid Id { get; }
        public string Reason { get; }

        public CreateActivityRejected()
        {
        }

        public CreateActivityRejected(string code, Guid id, string reason)
        {
            Code = code;
            Id = id;
            Reason = reason;
        }
    }
}