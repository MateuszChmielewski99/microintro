using Actio.Common.Domain.Repositories;
using Actio.Common.Mongo;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Services.Activities.Services
{
    public class CustomMongoSeeder : DatabaseSeeder
    {
        private ICategoryRepository _categoryRepository;
        public CustomMongoSeeder(IMongoDatabase database, ICategoryRepository categoryRepository) : base(database)
        {
            _categoryRepository = categoryRepository;
        }

        protected override async Task CustomSeedAsync()
        {
            var categories = new List<string> { "work", "sport", "hobby" };
            await Task.WhenAll(categories.Select(x => _categoryRepository.AddAsync(new Common.Domain.Models.Category(x))));
        }
    }
}
