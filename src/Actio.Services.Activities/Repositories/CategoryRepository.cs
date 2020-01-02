using Actio.Common.Domain.Models;
using Actio.Common.Domain.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;

namespace Actio.Services.Activities.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MongoDB.Driver.IMongoDatabase _database;

        public CategoryRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task AddAsync(Category category) => await Collection.InsertOneAsync(category);


        public async Task<IEnumerable<Category>> BrowseAsync() => await Collection.AsQueryable().ToListAsync();

        public async Task<Category> GetAsync(string name) => await Collection
            .AsQueryable()
            .FirstOrDefaultAsync(s => s.Name == name.ToLowerInvariant());

        private IMongoCollection<Category> Collection => _database.GetCollection<Category>("Categories");
    }
}
