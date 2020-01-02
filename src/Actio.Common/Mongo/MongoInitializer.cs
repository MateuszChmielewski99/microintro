using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Actio.Common.Mongo
{
    public class MongoInitializer : IDatabaseInitializer
    {
        private bool _initialized;
        private readonly MongoDB.Driver.IMongoDatabase _database;
        private readonly IDatabaseSeeder _seeder;
        private readonly bool _seed;

        public MongoInitializer(IMongoDatabase database, IOptions<MongoOptions> options, IDatabaseSeeder seeder)
        {
            _database = database;
            _seed = options.Value.Seed;
            _seeder = seeder;
        }

        public async Task InitializeAsync()
        {
            if (_initialized) { return; }

            RegisterConventions();

            _initialized = true;

            if (!_seed) { return; }

            await _seeder.SeedAsync();
        }

        private void RegisterConventions()
        {
            MongoDB.Bson.Serialization.Conventions.ConventionRegistry.Register("ActioConventions", new MongoConventions(), x => true);
        }

        private class MongoConventions : MongoDB.Bson.Serialization.Conventions.IConventionPack
        {
            public IEnumerable<IConvention> Conventions => new List<IConvention>
            {
                new IgnoreExtraElementsConvention(true),
                new EnumRepresentationConvention(MongoDB.Bson.BsonType.String),
                new CamelCaseElementNameConvention()
            };
        }
    }
}
