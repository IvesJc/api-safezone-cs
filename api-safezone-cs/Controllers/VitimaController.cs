using api_safezone_cs.DTOs.Vitima;
using api_safezone_cs.Infra.HATEOAS.VitimaHateoas;
using api_safezone_cs.Mapper;
using api_safezone_cs.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace api_safezone_cs.Controllers;

[Route("api/[controller]")]
[EnableRateLimiting("default")]
[ApiController]
public class VitimaController(
    IVitimaService vitimaService,
    LinkGenerator linkGenerator,
    IHttpContextAccessor contextAccessor) : ControllerBase
{
    [HttpGet(Name = "GetVitimas")]
    public async Task<IActionResult> GetVitimas()
    {
        var vitimas = await vitimaService.GetAllVitimasAsync();

        var vitimasComLinks = vitimas.Select(v =>
            VitimaHateoasBuilder.Build(v.ToVitimaResponseSemOcorrencia(), linkGenerator, contextAccessor.HttpContext!)
        );

        return Ok(vitimasComLinks);
    }

    [HttpGet("{id:int}", Name = "GetVitimaById")]
    public async Task<IActionResult> GetVitimaById(int id)
    {
        var vitima = await vitimaService.GetVitimaByIdAsync(id);
        if (vitima == null)
        {
            return NotFound();
        }

        var response = vitima.ToVitimaResponseSemOcorrencia();
        var result = VitimaHateoasBuilder.Build(response, linkGenerator, contextAccessor.HttpContext!);

        return Ok(result);
    }

    [HttpPost(Name = "CreateVitima")]
    public async Task<IActionResult> CreteVitima([FromBody] VitimaRequest vitimaRequest)
    {
        var vitima = await vitimaService.CreateVitimaAsync(vitimaRequest);
        var response = vitima.ToVitimaResponseSemOcorrencia();

        var result = VitimaHateoasBuilder.Build(response, linkGenerator, contextAccessor.HttpContext!);
        return CreatedAtRoute("GetVitimaById", new { id = response.Id }, result);
    }

    [HttpPut("{id:int}", Name = "UpdateVitima")]
    public async Task<IActionResult> UpdateVitima(int id, [FromBody] VitimaRequest vitimaRequest)
    {
        var vitima = await vitimaService.UpdateVitimaByAsync(id, vitimaRequest);
        if (vitima == null)
        {
            return NotFound();
        }

        var response = vitima.ToVitimaResponseSemOcorrencia();
        var result = VitimaHateoasBuilder.Build(response, linkGenerator, contextAccessor.HttpContext!);
        return Ok(result);
    }

    [HttpDelete("{id:int}", Name = "DeleteVitima")]
    public async Task<IActionResult> DeleteVitima(int id)
    {
        var sucesso = await vitimaService.DeleteVitimaByAsync(id);
        if (!sucesso)
        {
            return NotFound();
        }

        return NoContent();
    }
}
