using api_safezone_cs.Domain.Entities;
using api_safezone_cs.DTOs.Alerta;

namespace api_safezone_cs.Services.Interfaces;

public interface IAlertaService
{
    Task<List<Alerta>> GetAllAlertasAsync();
    Task<Alerta?> GetAlertaByIdAsync(int id);
    Task<Alerta> CreateAlertaAsync(AlertaRequest alertaRequest);
    Task<Alerta?> UpdateAlertaByAsync(int id, AlertaRequest alertaRequest);
    Task<bool> DeleteAlertaByAsync(int id);
}