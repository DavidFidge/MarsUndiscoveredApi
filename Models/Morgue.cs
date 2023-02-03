using MongoDB.Bson.Serialization.Attributes;

namespace MarsUndiscoveredApi.Models;

public class Morgue
{
    [BsonId]
    public Guid Id { get; set; }

    public string Username { get; set; } = null!;
}