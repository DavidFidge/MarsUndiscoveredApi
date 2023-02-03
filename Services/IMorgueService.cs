using MarsUndiscoveredApi.Models;

namespace MarsUndiscoveredApi.Services;

public interface IMorgueService
{
    Task<List<Morgue>> GetAsync();
    Task<Morgue?> GetAsync(Guid id);
    Task CreateAsync(Morgue newMorgue);
    Task UpdateAsync(Guid id, Morgue updatedMorgue);
    Task RemoveAsync(Guid id);
}