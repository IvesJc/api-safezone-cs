using api_safezone_cs.Domain.Entities;
using api_safezone_cs.DTOs.Ocorrencia;

namespace api_safezone_cs.Mapper;

public static class OcorrenciaMapper
{
    public static OcorrenciaRequest ToOcorrenciaRequest(this Ocorrencia ocorrencia)
    {
        return new OcorrenciaRequest(
            Localizacao: ocorrencia.Localizacao,
            Tipo: ocorrencia.Tipo,
            Status: ocorrencia.Status,
            Prioridade: ocorrencia.Prioridade,
            DataHora: ocorrencia.DataHora
        );
    }

    public static OcorrenciaResponse ToOcorrenciaResponse(this Ocorrencia ocorrencia)
    {
        return new OcorrenciaResponse(
            Id: ocorrencia.Id,
            Localizacao: ocorrencia.Localizacao,
            Tipo: ocorrencia.Tipo,
            Status: ocorrencia.Status,
            Prioridade: ocorrencia.Prioridade,
            DataHora: ocorrencia.DataHora,
            Vitimas: ocorrencia.Vitimas.Select(v => v.ToVitimaResponse()).ToList()
        );
    }

    public static Ocorrencia ToOcorrenciaFromRequest(this OcorrenciaRequest ocorrenciaRequest)
    {
        return new Ocorrencia
        {
            Prioridade = ocorrenciaRequest.Prioridade,
            DataHora = ocorrenciaRequest.DataHora,
            Localizacao = ocorrenciaRequest.Localizacao,
            Status = ocorrenciaRequest.Status,
            Tipo = ocorrenciaRequest.Tipo,
        };
    }
    public static Ocorrencia ToOcorrenciaFromResponse(this OcorrenciaResponse ocorrenciaResponse)
    {
        return new Ocorrencia
        {
            Id = ocorrenciaResponse.Id,
            Prioridade = ocorrenciaResponse.Prioridade,
            DataHora = ocorrenciaResponse.DataHora,
            Localizacao = ocorrenciaResponse.Localizacao,
            Status = ocorrenciaResponse.Status,
            Tipo = ocorrenciaResponse.Tipo,
            Vitimas = ocorrenciaResponse.Vitimas.Select(v => v.ToVitimaFromResponse()).ToList()
        };
    }
}