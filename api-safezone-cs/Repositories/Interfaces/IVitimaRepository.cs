using api_safezone_cs.Domain.Entities;
using api_safezone_cs.DTOs.Vitima;

namespace api_safezone_cs.Repositories.Interfaces;

public interface IVitimaRepository
{
    Task<List<Vitima>> GetAllVitimasAsync();
    Task<Vitima?> GetVitimaByIdAsync(int id);
    Task<Vitima> CreateVitimaAsync(Vitima vitima);
    Task<Vitima?> UpdateVitimaByAsync(int id, VitimaRequest vitimaRequest);
    Task<Vitima?> DeleteVitimaByAsync(int id);
}