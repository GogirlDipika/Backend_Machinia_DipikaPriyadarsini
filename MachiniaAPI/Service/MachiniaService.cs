using MachiniaAPI.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MachiniaAPI.Service
{
    public class MachiniaService
    {
        private readonly IMongoCollection<TrainingCenter> _trainingCenter;

        public MachiniaService(IOptions<MachiniaDatabaseSettings> options) 
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);

            _trainingCenter = mongoClient.GetDatabase(options.Value.DatabaseName).GetCollection<TrainingCenter>(options.Value.CollectionName);
        }

        public async Task<List<TrainingCenter>> Get() =>
            await _trainingCenter.Find(_ => true).ToListAsync();

        public async Task<TrainingCenter> Get(string centercode) =>
            await _trainingCenter.Find(m => m.CenterCode== centercode).FirstOrDefaultAsync();

        public async Task Create(TrainingCenter newTrainingCenter) =>
            await _trainingCenter.InsertOneAsync(newTrainingCenter);

    }
}
