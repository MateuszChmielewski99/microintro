using Actio.Services.Identity.Domain.Models;
using Actio.Services.Identity.Domain.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;

namespace Actio.Services.Identity.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoDatabase MongoDatabase;

        public UserRepository(IMongoDatabase mongoDatabase)
        {
            MongoDatabase = mongoDatabase;
        }
        public async Task AddAsync(User user)
            => await Collection.InsertOneAsync(user);

        public async Task<User> GetAsync(Guid id)
            => await Collection.AsQueryable()
                .FirstOrDefaultAsync(s => s.Id.Equals(id));


        public async Task<User> GetAsync(string email)
            => await Collection.AsQueryable()
            .FirstOrDefaultAsync(s => s.Email.Equals(email.ToLowerInvariant()));

        private IMongoCollection<User> Collection
            => MongoDatabase.GetCollection<User>("Users");
    }
}
