using api_safezone_cs.Domain.Entities;
using api_safezone_cs.DTOs.Ocorrencia;

namespace api_safezone_cs.Repositories.Interfaces;

public interface IOcorrenciaRepository
{
    Task<List<Ocorrencia>> GetAllOcorrenciasAsync();
    Task<Ocorrencia?> GetOcorrenciaByIdAsync(int id);
    Task<Ocorrencia> CreateOcorrenciaAsync(Ocorrencia ocorrencia);
    Task<Ocorrencia?> UpdateOcorrenciaByAsync(int id, OcorrenciaRequest ocorrenciaRequest);
    Task<Ocorrencia?> DeleteOcorrenciaByAsync(int id);
}