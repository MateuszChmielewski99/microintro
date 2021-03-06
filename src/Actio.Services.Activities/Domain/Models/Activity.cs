﻿using System;
using System.Collections.Generic;
using System.Text;
using Actio.Common.Exceptions;

namespace Actio.Common.Domain.Models
{
    public class Activity
    {
        public Guid Id { get; protected set; }

        public Guid UserId { get; protected set; }

        public string Name { get; protected set; }

        public string Categoty { get; protected set; }

        public string Description { get; protected set; }

        public DateTime CreatedAt { get; protected set; }

        protected Activity() { }

        public Activity(Guid id, Guid userId, string name, string categoty, string description, DateTime createdAt)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ActioException("empty_name_field", "Filed name cannot be empty");
            }

            Id = id;
            UserId = userId;
            Name = name;
            Categoty = categoty;
            Description = description;
            CreatedAt = createdAt;
        }
    }
}
