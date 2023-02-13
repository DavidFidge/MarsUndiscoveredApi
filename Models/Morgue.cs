using MongoDB.Bson.Serialization.Attributes;

namespace MarsUndiscoveredApi.Models;

public class Morgue
{
    [BsonId]
    public Guid Id { get; set; }
    public string GameVersion { get; set; }
    public int Version { get; set; }
    public string Seed { get; set; }
    public string Username { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Dictionary<string, int> EnemiesDefeated { get; set; } = new();
    public List<string> FinalInventory { get; set; } = new();
    public bool IsVictorious { get; set; }
    public string GameEndStatus { get; set; }
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public string TextReport { get; set; }
}