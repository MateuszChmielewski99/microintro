using System;
using System.Collections.Generic;
using System.Text;

namespace Actio.Common.Domain.Models
{
    public class Category
    {
        public Guid Id { get; protected set; }

        public string Name { get; protected set; }

        protected Category() { }

        public Category(string name)
        {
            Name = name;
        }
    }
}
