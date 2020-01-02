using Actio.Common.Domain.Models;
using Actio.Common.Domain.Repositories;
using MongoDB.Driver;
using System;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;

namespace Actio.Services.Activities.Repositories
{
    public class ActivityRepository : IActivityRepository
    {

        private readonly MongoDB.Driver.IMongoDatabase _database;

        public ActivityRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task AddAsync(Activity activity) => await Collection.InsertOneAsync(activity);

        public async Task<Activity> GetAsync(Guid id) => await Collection
            .AsQueryable()
            .FirstOrDefaultAsync(s => s.Id == id);

        private MongoDB.Driver.IMongoCollection<Activity> Collection => _database.GetCollection<Activity>("Activities");
    }
}
