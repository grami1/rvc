using Microsoft.AspNetCore.Mvc;
using RvcApp.Models;
using RvcApp.Services;

namespace RvcApp.Controllers;

[ApiController]
[Route("tibber-developer-test")]
public class RvcController : ControllerBase
{
    private readonly IRvcService _rvcService;

    public RvcController(IRvcService rvcService)
    {
        _rvcService = rvcService;
    }

    [HttpPost("enter-path")]
    public IActionResult RunRvc([FromBody] RvcPath rvcPath)
    {
        var result = _rvcService.RunRvc(rvcPath);
        return Ok(result);
    }
}