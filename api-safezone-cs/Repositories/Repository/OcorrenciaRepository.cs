using api_safezone_cs.Data.AppData;
using api_safezone_cs.Domain.Entities;
using api_safezone_cs.DTOs.Ocorrencia;
using api_safezone_cs.Mapper;
using api_safezone_cs.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api_safezone_cs.Repositories.Repository;

public class OcorrenciaRepository(AppDbContext dbContext) : IOcorrenciaRepository
{
    public async Task<List<Ocorrencia>> GetAllOcorrenciasAsync()
    {
        return await dbContext.Ocorrencias.Include(o => o.Vitimas).ToListAsync();
    }

    public async Task<Ocorrencia?> GetOcorrenciaByIdAsync(int id)
    {
        return await dbContext.Ocorrencias.Include(ocorrencia => ocorrencia.Vitimas).FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<Ocorrencia> CreateOcorrenciaAsync(Ocorrencia ocorrencia)
    {
        await dbContext.AddAsync(ocorrencia);
        await dbContext.SaveChangesAsync();
        return ocorrencia;
    }

    public async Task<Ocorrencia?> UpdateOcorrenciaByAsync(int id, OcorrenciaRequest ocorrenciaRequest)
    {
        var ocorrencia = await dbContext.Ocorrencias.FirstOrDefaultAsync(o => o.Id == id);
        if (ocorrencia != null)
        {
            return null;
        }

        ocorrenciaRequest.ToOcorrenciaFromRequest();
        await dbContext.SaveChangesAsync();
        return ocorrencia;
    }

    public async Task<Ocorrencia?> DeleteOcorrenciaByAsync(int id)
    {
        var ocorrencia = await dbContext.Ocorrencias.FirstOrDefaultAsync(o => o.Id == id);
        if (ocorrencia != null)
        {
            return null;
        }

        dbContext.Remove(id);
        await dbContext.SaveChangesAsync();
        return ocorrencia;
    }
}