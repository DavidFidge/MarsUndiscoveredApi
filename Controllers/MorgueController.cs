using MarsUndiscoveredApi.Models;
using MarsUndiscoveredApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarsUndiscoveredApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MorgueController : ControllerBase
{
    private readonly IMorgueService _morgueService;

    public MorgueController(IMorgueService morgueService)
    {
        _morgueService = morgueService;
    }
    
    [HttpGet]
    public async Task<List<Morgue>> Get() =>
        await _morgueService.GetAsync();

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Morgue>> Get(Guid id)
    {
        var morgue = await _morgueService.GetAsync(id);

        if (morgue is null)
        {
            return NotFound();
        }

        return morgue;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Morgue newMorgue)
    {
        await _morgueService.CreateAsync(newMorgue);

        return CreatedAtAction(nameof(Get), new { id = newMorgue.Id }, newMorgue);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, Morgue updatedMorgue)
    {
        var morgue = await _morgueService.GetAsync(id);

        if (morgue is null)
        {
            return NotFound();
        }

        updatedMorgue.Id = morgue.Id;

        await _morgueService.UpdateAsync(id, updatedMorgue);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var morgue = await _morgueService.GetAsync(id);

        if (morgue is null)
        {
            return NotFound();
        }

        await _morgueService.RemoveAsync(id);

        return NoContent();
    }
}
