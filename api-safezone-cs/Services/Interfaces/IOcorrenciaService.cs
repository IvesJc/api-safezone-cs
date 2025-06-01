using api_safezone_cs.Domain.Entities;
using api_safezone_cs.DTOs.Ocorrencia;

namespace api_safezone_cs.Services.Interfaces;

public interface IOcorrenciaService
{
    Task<List<Ocorrencia>> GetAllOcorrenciasAsync();
    Task<Ocorrencia?> GetOcorrenciaByIdAsync(int id);
    Task<Ocorrencia> CreateOcorrenciaAsync(OcorrenciaRequest ocorrenciaRequest);
    Task<Ocorrencia?> UpdateOcorrenciaByAsync(int id, OcorrenciaRequest ocorrenciaRequest);
    Task<bool> DeleteOcorrenciaByAsync(int id);
}