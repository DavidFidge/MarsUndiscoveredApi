using MarsUndiscoveredApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MarsUndiscoveredApi.Services;

public class MorgueService : IMorgueService
{
    private readonly IMongoCollection<Morgue> _morgueCollection;

    public MorgueService(DatabaseSettings databaseSettings)
    {
        var mongoClient = new MongoClient(
            databaseSettings.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            databaseSettings.DatabaseName);

        _morgueCollection = mongoDatabase.GetCollection<Morgue>(
            databaseSettings.MorgueCollectionName);
    }

    public async Task<List<Morgue>> GetAsync() =>
        await _morgueCollection
            .Find(m => m.Version == 1)
            .SortByDescending(m => m.EndDate)
            .ToListAsync();

    public async Task<Morgue?> GetAsync(Guid id) =>
        await _morgueCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Morgue newMorgue) =>
        await _morgueCollection.InsertOneAsync(newMorgue);

    public async Task UpdateAsync(Guid id, Morgue updatedMorgue) =>
        await _morgueCollection.ReplaceOneAsync(x => x.Id == id, updatedMorgue);

    public async Task RemoveAsync(Guid id) =>
        await _morgueCollection.DeleteOneAsync(x => x.Id == id);
}