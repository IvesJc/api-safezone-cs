using api_safezone_cs.Domain.Entities;
using api_safezone_cs.DTOs.Alerta;
using api_safezone_cs.Mapper;
using api_safezone_cs.Repositories.Interfaces;
using api_safezone_cs.Services.Interfaces;

namespace api_safezone_cs.Services.Service;

public class AlertaService(IAlertaRepository alertaRepository) : IAlertaService
{
    public async Task<List<Alerta>> GetAllAlertasAsync()
    {
        return await alertaRepository.GetAllAlertasAsync();
    }

    public async Task<Alerta?> GetAlertaByIdAsync(int id)
    {
        return await alertaRepository.GetAlertaByIdAsync(id);
    }

    public async Task<Alerta> CreateAlertaAsync(AlertaRequest alertaRequest)
    {
        var alerta = alertaRequest.ToAlertaFromRequest();
        await alertaRepository.CreateAlertaAsync(alerta);
        return alerta;
    }

    public async Task<Alerta?> UpdateAlertaByAsync(int id, AlertaRequest alertaRequest)
    {
        return await alertaRepository.UpdateAlertaByAsync(id, alertaRequest);
    }

    public async Task<bool> DeleteAlertaByAsync(int id)
    {
        var alerta = await alertaRepository.DeleteAlertaByAsync(id);
        return alerta != null;
    }
}