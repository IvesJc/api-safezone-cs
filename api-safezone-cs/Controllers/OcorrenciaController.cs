using api_safezone_cs.DTOs.Ocorrencia;
using api_safezone_cs.Mapper;
using api_safezone_cs.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api_safezone_cs.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OcorrenciaController(IOcorrenciaService ocorrenciaService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetOcorrencias()
    {
        var ocorrencias = await ocorrenciaService.GetAllOcorrenciasAsync();
        return Ok(ocorrencias);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOcorrenciaById(int id)
    {
        var ocorrencia = await ocorrenciaService.GetOcorrenciaByIdAsync(id);
        if (ocorrencia == null)
        {
            return NotFound();
        }

        return Ok(ocorrencia);
    }

    [HttpPost]
    public async Task<IActionResult> CreteOcorrencia([FromBody]OcorrenciaRequest ocorrenciaRequest)
    {
        var ocorrencia = await ocorrenciaService.CreateOcorrenciaAsync(ocorrenciaRequest);
        return CreatedAtAction(nameof(GetOcorrenciaById), new { id = ocorrencia.Id }, ocorrencia);
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateOcorrencia(int id, [FromBody]OcorrenciaRequest ocorrenciaRequest)
    {
        var ocorrencia = await ocorrenciaService.UpdateOcorrenciaByAsync(id, ocorrenciaRequest);
        if (ocorrencia == null)
        {
            return NotFound();
        }

        return Ok(ocorrencia.ToOcorrenciaResponse());
    }    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteOcorrencia(int id)
    {
        var ocorrencia = await ocorrenciaService.DeleteOcorrenciaByAsync(id);
        if (!ocorrencia)
        {
            return NotFound();
        }

        return NoContent();
    }
}