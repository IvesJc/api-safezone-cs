using api_safezone_cs.DTOs.Ocorrencia;
using api_safezone_cs.Infra.HATEOAS.OcorrenciaHateoas;
using api_safezone_cs.Mapper;
using api_safezone_cs.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace api_safezone_cs.Controllers;

[Route("api/[controller]")]
[EnableRateLimiting("default")]
[ApiController]
public class OcorrenciaController(
    IOcorrenciaService ocorrenciaService,
    LinkGenerator linkGenerator,
    IHttpContextAccessor contextAccessor) : ControllerBase
{
    [HttpGet(Name = "GetOcorrencias")]
    public async Task<IActionResult> GetOcorrencias()
    {
        var ocorrencias = await ocorrenciaService.GetAllOcorrenciasAsync();

        var ocorrenciasComLinks = ocorrencias.Select(o =>
            OcorrenciaHateoasBuilder.Build(o.ToOcorrenciaResponse(), linkGenerator, contextAccessor.HttpContext!)
        );

        return Ok(ocorrenciasComLinks);
    }

    [HttpGet("{id:int}", Name = "GetOcorrenciaById")]
    public async Task<IActionResult> GetOcorrenciaById(int id)
    {
        var ocorrencia = await ocorrenciaService.GetOcorrenciaByIdAsync(id);
        if (ocorrencia == null)
        {
            return NotFound();
        }

        var ocorrenciaResponse = ocorrencia.ToOcorrenciaResponse();
        var result = OcorrenciaHateoasBuilder.Build(ocorrenciaResponse, linkGenerator, contextAccessor.HttpContext!);

        return Ok(result);
    }

    [HttpPost(Name = "CreateOcorrencia")]
    public async Task<IActionResult> CreteOcorrencia([FromBody] OcorrenciaRequest ocorrenciaRequest)
    {
        var ocorrencia = await ocorrenciaService.CreateOcorrenciaAsync(ocorrenciaRequest);
        var response = ocorrencia.ToOcorrenciaResponse();

        var result = OcorrenciaHateoasBuilder.Build(response, linkGenerator, contextAccessor.HttpContext!);
        return CreatedAtRoute("GetOcorrenciaById", new { id = response.Id }, result);
    }

    [HttpPut("{id:int}", Name = "UpdateOcorrencia")]
    public async Task<IActionResult> UpdateOcorrencia(int id, [FromBody] OcorrenciaRequest ocorrenciaRequest)
    {
        var ocorrencia = await ocorrenciaService.UpdateOcorrenciaByAsync(id, ocorrenciaRequest);
        if (ocorrencia == null)
        {
            return NotFound();
        }

        var response = ocorrencia.ToOcorrenciaResponse();
        var result = OcorrenciaHateoasBuilder.Build(response, linkGenerator, contextAccessor.HttpContext!);
        return Ok(result);
    }

    [HttpDelete("{id:int}", Name = "DeleteOcorrencia")]
    public async Task<IActionResult> DeleteOcorrencia(int id)
    {
        var sucesso = await ocorrenciaService.DeleteOcorrenciaByAsync(id);
        if (!sucesso)
        {
            return NotFound();
        }

        return NoContent();
    }
}
