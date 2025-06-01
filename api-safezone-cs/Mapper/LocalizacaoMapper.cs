using api_safezone_cs.Domain.Entities;
using api_safezone_cs.DTOs.Localizacao;

namespace api_safezone_cs.Mapper;

public static class LocalizacaoMapper
{
    public static LocalizacaoDto ToLocalizacaoDto(this Localizacao localizacao)
    {
        return new LocalizacaoDto(
            Latitude: localizacao.Latitude,
            Longitude: localizacao.Longitude
            );
    }

    public static Localizacao ToLocalizacao(this LocalizacaoDto localizacaoDto)
    {
        return new Localizacao
        {
            Latitude = localizacaoDto.Latitude,
            Longitude = localizacaoDto.Longitude
        };
    }
}