using api_safezone_cs.Data.AppData;
using api_safezone_cs.Domain.Entities;
using api_safezone_cs.DTOs.Alerta;
using api_safezone_cs.Mapper;
using api_safezone_cs.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api_safezone_cs.Repositories.Repository;

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
        if (alerta == null)
        {
            return null;
        }

        alerta.Severidade = alertaRequest.Severidade;
        alerta.Status = alertaRequest.Status;
        alerta.Tipo = alertaRequest.Tipo;
        alerta.AreaAfetada = alertaRequest.AreaAfetada;
        alerta.DataHora = alertaRequest.DataHora;
        await dbContext.SaveChangesAsync();
        return alerta;
    }

    public async Task<Alerta?> DeleteAlertaByAsync(int id)
    {
        var alerta = await dbContext.Alertas.FirstOrDefaultAsync(a => a.Id == id);
        if (alerta == null)
        {
            return null;
        }

        dbContext.Remove(alerta);
        await dbContext.SaveChangesAsync();
        return alerta;
    }
}