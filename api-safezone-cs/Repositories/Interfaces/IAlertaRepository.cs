using api_safezone_cs.Domain.Entities;
using api_safezone_cs.DTOs.Alerta;

namespace api_safezone_cs.Repositories.Interfaces;

public interface IAlertaRepository
{
    Task<List<Alerta>> GetAllAlertasAsync();
    Task<Alerta?> GetAlertaByIdAsync(int id);
    Task<Alerta> CreateAlertaAsync(Alerta alerta);
    Task<Alerta?> UpdateAlertaByAsync(int id, AlertaRequest alertaRequest);
    Task<Alerta?> DeleteAlertaByAsync(int id);
}