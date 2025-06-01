using api_safezone_cs.DTOs.Vitima;
using api_safezone_cs.Mapper;
using api_safezone_cs.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api_safezone_cs.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VitimaController(IVitimaService vitimaService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetVitimas()
    {
        var vitimas = await vitimaService.GetAllVitimasAsync();
        return Ok(vitimas);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetVitimaById(int id)
    {
        var vitima = await vitimaService.GetVitimaByIdAsync(id);
        if (vitima == null)
        {
            return NotFound();
        }

        return Ok(vitima);
    }

    [HttpPost]
    public async Task<IActionResult> CreteVitima([FromBody]VitimaRequest vitimaRequest)
    {
        var vitima = await vitimaService.CreateVitimaAsync(vitimaRequest);
        return CreatedAtAction(nameof(GetVitimaById), new { id = vitima.Id }, vitima);
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateVitima(int id, [FromBody]VitimaRequest vitimaRequest)
    {
        var vitima = await vitimaService.UpdateVitimaByAsync(id, vitimaRequest);
        if (vitima == null)
        {
            return NotFound();
        }

        return Ok(vitima.ToVitimaResponse());
    }    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteVitima(int id)
    {
        var vitima = await vitimaService.DeleteVitimaByAsync(id);
        if (!vitima)
        {
            return NotFound();
        }

        return NoContent();
    }
}