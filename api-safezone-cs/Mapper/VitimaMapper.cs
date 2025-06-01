using api_safezone_cs.Domain.Entities;
using api_safezone_cs.Domain.Enums;
using api_safezone_cs.DTOs.Vitima;

namespace api_safezone_cs.Mapper;

public static class VitimaMapper
{
    public static VitimaRequest ToVitimaRequest(this Vitima vitima)
    {
        return new VitimaRequest(
            Nome: vitima.Nome,
            Idade: vitima.Idade,
            Condicao: vitima.Condicao,
            Localizacao: vitima.Localizacao,
            OcorrenciaId: vitima.OcorrenciaId,
            Ocorrencia: vitima.Ocorrencia
        );
    }

    public static VitimaResponse ToVitimaResponse(this Vitima vitima)
    {
        return new VitimaResponse(
            Id: vitima.Id,
            Nome: vitima.Nome,
            Idade: vitima.Idade,
            Condicao: vitima.Condicao,
            Localizacao: vitima.Localizacao,
            OcorrenciaId: vitima.OcorrenciaId,
            Ocorrencia: vitima.Ocorrencia
        );
    }

    public static Vitima ToVitimaFromRequest(this VitimaRequest vitimaRequest)
    {
        return new Vitima
        {
            Nome = vitimaRequest.Nome,
            Idade = vitimaRequest.Idade,
            Condicao = vitimaRequest.Condicao,
            Localizacao = vitimaRequest.Localizacao,
            OcorrenciaId = vitimaRequest.OcorrenciaId,
            Ocorrencia = vitimaRequest.Ocorrencia
        };
    }
    public static Vitima ToVitimaFromResponse(this VitimaResponse vitimaResponse)
    {
        return new Vitima
        {
            Id = vitimaResponse.Id,
            Nome = vitimaResponse.Nome,
            Idade = vitimaResponse.Idade,
            Condicao = vitimaResponse.Condicao,
            Localizacao = vitimaResponse.Localizacao,
            OcorrenciaId = vitimaResponse.OcorrenciaId,
            Ocorrencia = vitimaResponse.Ocorrencia
        };
    }
}