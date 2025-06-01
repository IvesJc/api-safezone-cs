using api_safezone_cs.Domain.Entities;
using api_safezone_cs.DTOs.Vitima;
using api_safezone_cs.Mapper;
using api_safezone_cs.Repositories.Interfaces;
using api_safezone_cs.Services.Interfaces;

namespace api_safezone_cs.Services;

public class VitimaService(IVitimaRepository vitimaRepository) : IVitimaService
{
    public async Task<List<Vitima>> GetAllVitimasAsync()
    {
        return await vitimaRepository.GetAllVitimasAsync();
    }

    public async Task<Vitima?> GetVitimaByIdAsync(int id)
    {
        return await vitimaRepository.GetVitimaByIdAsync(id);
    }

    public async Task<Vitima> CreateVitimaAsync(VitimaRequest vitimaRequest)
    {
        var vitima = vitimaRequest.ToVitimaFromRequest();
        await vitimaRepository.CreateVitimaAsync(vitima);
        return vitima;
    }

    public async Task<Vitima?> UpdateVitimaByAsync(int id, VitimaRequest vitimaRequest)
    {
        return await vitimaRepository.UpdateVitimaByAsync(id, vitimaRequest);
    }

    public async Task<bool> DeleteVitimaByAsync(int id)
    {
        var vitima = await vitimaRepository.DeleteVitimaByAsync(id);
        return vitima != null;
    }
}