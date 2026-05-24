using Microsoft.AspNetCore.Mvc;
using VantaLore.Application.Services;

namespace VantaLore.Api.Controllers;

[ApiController]
[Route("api/lore")]
public class LoreController : ControllerBase
{
    private readonly LoreQueryService _service;

    public LoreController(LoreQueryService service)
    {
        _service = service;
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search(string query)
    {
        var result = await _service.Search(query);
        return Ok(result);
    }

    [HttpPost("ask")]
    public async Task<IActionResult> Ask(string question)
    {
        var result = await _service.Ask(question);

        return Ok(new
        {
            Question = question,
            Result = result
        });
    }
}