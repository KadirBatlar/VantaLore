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
    public IActionResult Search(string query)
    {
        var result = _service.Search(query);
        return Ok(result);
    }

    [HttpGet("ask")]
    public IActionResult Ask(string question)
    {
        var result = _service.Ask(question);

        return Ok(new
        {
            Question = question,
            Result = result
        });
    }
}