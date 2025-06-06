using api_safezone_cs.Data.AppData;
using api_safezone_cs.Domain.Entities;
using api_safezone_cs.DTOs.Vitima;
using api_safezone_cs.Mapper;
using api_safezone_cs.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api_safezone_cs.Repositories.Repository;

public class VitimaRepository(AppDbContext dbContext) : IVitimaRepository
{
    public async Task<List<Vitima>> GetAllVitimasAsync()
    {
        return await dbContext.Vitimas.Include(vitima => vitima.Ocorrencia).ToListAsync();
    }

    public async Task<Vitima?> GetVitimaByIdAsync(int id)
    {
        return await dbContext.Vitimas.FindAsync(id);
    }

    public async Task<Vitima> CreateVitimaAsync(Vitima vitima)
    {
        await dbContext.AddAsync(vitima);
        await dbContext.SaveChangesAsync();
        return vitima;
    }

    public async Task<Vitima?> UpdateVitimaByAsync(int id, VitimaRequest vitimaRequest)
    {
        var vitima = await dbContext.Vitimas.FirstOrDefaultAsync(v => v.Id == id);
        if (vitima == null)
        {
            return null;
        }

        vitima.Condicao = vitimaRequest.Condicao;
        vitima.Idade = vitimaRequest.Idade;
        vitima.Localizacao = vitimaRequest.Localizacao;
        vitima.Nome = vitimaRequest.Nome;
        vitima.OcorrenciaId = vitimaRequest.OcorrenciaId;
        await dbContext.SaveChangesAsync();
        return vitima;
    }

    public async Task<Vitima?> DeleteVitimaByAsync(int id)
    {
        var vitima = await dbContext.Vitimas.FirstOrDefaultAsync(v => v.Id == id);
        if (vitima == null)
        {
            return null;
        }

        dbContext.Remove(vitima);
        await dbContext.SaveChangesAsync();
        return vitima;
    }
}