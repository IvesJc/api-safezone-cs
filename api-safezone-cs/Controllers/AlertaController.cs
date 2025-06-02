using api_safezone_cs.DTOs.Alerta;
using api_safezone_cs.Infra.HATEOAS.AlertaHateoas;
using api_safezone_cs.Mapper;
using api_safezone_cs.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace api_safezone_cs.Controllers;

[Route("api/[controller]")]
[EnableRateLimiting("default")]
[ApiController]
public class AlertaController(
    IAlertaService alertaService,
    LinkGenerator linkGenerator,
    IHttpContextAccessor contextAccessor) : ControllerBase
{
    [HttpGet(Name = "GetAlertas")]
    public async Task<IActionResult> GetAlertas()
    {
        var alertas = await alertaService.GetAllAlertasAsync();
        var alertaResponses = alertas.Select(alerta =>
            AlertaHateoasBuilder.Build(alerta.ToAlertaResponse(), linkGenerator, contextAccessor.HttpContext!)
        );

        return Ok(alertaResponses);
    }


    [HttpGet("{id:int}", Name = "GetAlertaById")]
    public async Task<IActionResult> GetAlertaById(int id)
    {
        var alerta = await alertaService.GetAlertaByIdAsync(id);
        if (alerta == null)
        {
            return NotFound();
        }

        var alertaResponse = alerta.ToAlertaResponse();
        var result = AlertaHateoasBuilder.Build(alertaResponse, linkGenerator, contextAccessor.HttpContext!);

        return Ok(result);
    }

    [HttpPost(Name = "CreateAlerta")]
    public async Task<IActionResult> CreateAlerta([FromBody] AlertaRequest alertaRequest)
    {
        var alerta = await alertaService.CreateAlertaAsync(alertaRequest);
        var alertaResponse = alerta.ToAlertaResponse();

        var result = AlertaHateoasBuilder.Build(alertaResponse, linkGenerator, contextAccessor.HttpContext!);
        return CreatedAtRoute("GetAlertaById", new { id = alertaResponse.Id }, result);
    }


    [HttpPut("{id:int}", Name = "UpdateAlerta")]
    public async Task<IActionResult> UpdateAlerta(int id, [FromBody] AlertaRequest alertaRequest)
    {
        var alerta = await alertaService.UpdateAlertaByAsync(id, alertaRequest);
        if (alerta == null)
        {
            return NotFound();
        }

        var alertaResponse = alerta.ToAlertaResponse();
        var result = AlertaHateoasBuilder.Build(alertaResponse, linkGenerator, contextAccessor.HttpContext!);
        return Ok(result);
    }


    [HttpDelete("{id:int}", Name = "DeleteAlerta")]
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
