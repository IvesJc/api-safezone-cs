using api_safezone_cs.Domain.Entities;
using api_safezone_cs.DTOs.Localizacao;

namespace api_safezone_cs.Repositories.Interfaces;

public interface ILocalizacaoRepository
{
    Task<List<Localizacao>> GetAllLocalizacaosAsync();
    Task<Localizacao?> GetLocalizacaoByIdAsync(int id);
    Task<Localizacao> CreateLocalizacaoAsync(Localizacao localizacao);
    Task<Localizacao?> UpdateLocalizacaoByAsync(int id, LocalizacaoDto localizacaoDto);
    Task<Localizacao?> DeleteLocalizacaoByAsync(int id);
}