using api_safezone_cs.Data.AppData;
using api_safezone_cs.DTOs.Alerta;
using api_safezone_cs.Mapper;
using api_safezone_cs.Repositories.Interfaces;
using api_safezone_cs.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api_safezone_cs.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AlertaController(IAlertaService alertaService, AppDbContext dbContext) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAlertas()
    {
        var alertas = await alertaService.GetAllAlertasAsync();
        return Ok(alertas);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAlertaById(int id)
    {
        var alerta = await alertaService.GetAlertaByIdAsync(id);
        if (alerta == null)
        {
            return NotFound();
        }

        return Ok(alerta);
    }

    [HttpPost]
    public async Task<IActionResult> CreteAlerta([FromBody]AlertaRequest alertaRequest)
    {
        var alerta = await alertaService.CreateAlertaAsync(alertaRequest);
        return CreatedAtAction(nameof(GetAlertaById), new { id = alerta.Id }, alerta);
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAlerta(int id, [FromBody]AlertaRequest alertaRequest)
    {
        var alerta = await alertaService.UpdateAlertaByAsync(id, alertaRequest);
        if (alerta == null)
        {
            return NotFound();
        }

        return Ok(alerta.ToAlertaResponse());
    }    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAlerta(int id)
    {
        var alerta = await alertaService.DeleteAlertaByAsync(id);
        if (!alerta)
        {
            return NotFound();
        }

        return NoContent();
    }
}