using api_safezone_cs.Domain.Entities;
using api_safezone_cs.DTOs.Vitima;

namespace api_safezone_cs.Services.Interfaces;

public interface IVitimaService
{
    Task<List<Vitima>> GetAllVitimasAsync();
    Task<Vitima?> GetVitimaByIdAsync(int id);
    Task<Vitima> CreateVitimaAsync(VitimaRequest vitimaRequest);
    Task<Vitima?> UpdateVitimaByAsync(int id, VitimaRequest vitimaRequest);
    Task<bool> DeleteVitimaByAsync(int id);
}