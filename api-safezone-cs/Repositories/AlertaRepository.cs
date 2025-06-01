using api_safezone_cs.Data.AppData;
using api_safezone_cs.Domain.Entities;
using api_safezone_cs.DTOs.Alerta;
using api_safezone_cs.Mapper;
using api_safezone_cs.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api_safezone_cs.Repositories;

public class AlertaRepository(AppDbContext dbContext) : IAlertaRepository
{
    public async Task<List<Alerta>> GetAllAlertasAsync()
    {
        return await dbContext.Alertas.ToListAsync();
    }

    public async Task<Alerta?> GetAlertaByIdAsync(int id)
    {
        return await dbContext.Alertas.FindAsync(id);
    }

    public async Task<Alerta> CreateAlertaAsync(Alerta alerta)
    {
        await dbContext.AddAsync(alerta);
        await dbContext.SaveChangesAsync();
        return alerta;
    }

    public async Task<Alerta?> UpdateAlertaByAsync(int id, AlertaRequest alertaRequest)
    {
        var alerta = await dbContext.Alertas.FirstOrDefaultAsync(a => a.Id == id);
        if (alerta != null)
        {
            return null;
        }

        alertaRequest.ToAlertaFromRequest();
        await dbContext.SaveChangesAsync();
        return alerta;
    }

    public async Task<Alerta?> DeleteAlertaByAsync(int id)
    {
        var alerta = await dbContext.Alertas.FirstOrDefaultAsync(a => a.Id == id);
        if (alerta != null)
        {
            return null;
        }

        dbContext.Remove(id);
        await dbContext.SaveChangesAsync();
        return alerta;
    }
}