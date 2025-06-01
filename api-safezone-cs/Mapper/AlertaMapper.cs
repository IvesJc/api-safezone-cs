using api_safezone_cs.Domain.Entities;
using api_safezone_cs.DTOs.Alerta;

namespace api_safezone_cs.Mapper;

public static class AlertaMapper
{
    public static AlertaRequest ToAlertaRequest(this Alerta alerta)
    {
        return new AlertaRequest(
            Severidade: alerta.Severidade,
            Status: alerta.Status,
            Tipo: alerta.Tipo,
            AreaAfetada: alerta.AreaAfetada,
            DataHora: alerta.DataHora
        );
    }

    public static AlertaResponse ToAlertaResponse(this Alerta alerta)
    {
        return new AlertaResponse(
            Id: alerta.Id,
            Severidade: alerta.Severidade,
            Status: alerta.Status,
            Tipo: alerta.Tipo,
            AreaAfetada: alerta.AreaAfetada,
            DataHora: alerta.DataHora
        );
    }

    public static Alerta ToAlertaFromRequest(this AlertaRequest alertaRequest)
    {
        return new Alerta
        {
            Severidade = alertaRequest.Severidade,
            Status = alertaRequest.Status,
            Tipo = alertaRequest.Tipo,
            AreaAfetada = alertaRequest.AreaAfetada,
            DataHora = alertaRequest.DataHora
        };
    }
    
    public static Alerta ToAlertaFromResponse(this AlertaResponse alertaResponse)
    {
        return new Alerta
        {
           Id = alertaResponse.Id,
           Severidade = alertaResponse.Severidade,
           Status = alertaResponse.Status,
           Tipo = alertaResponse.Tipo,
           AreaAfetada = alertaResponse.AreaAfetada,
           DataHora = alertaResponse.DataHora
        };
    }
}