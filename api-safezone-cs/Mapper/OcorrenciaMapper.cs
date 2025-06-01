using api_safezone_cs.Domain.Entities;
using api_safezone_cs.DTOs.Ocorrencia;

namespace api_safezone_cs.Mapper;

public static class OcorrenciaMapper
{
    public static OcorrenciaRequest ToOcorrenciaRequest(this Ocorrencia ocorrencia)
    {
        return new OcorrenciaRequest(
            Local: ocorrencia.Local,
            Tipo: ocorrencia.Tipo,
            Status: ocorrencia.Status,
            Prioridade: ocorrencia.Prioridade,
            DataHora: ocorrencia.DataHora,
            Vitimas: ocorrencia.Vitimas.Select(v => v.ToVitimaRequest()).ToList()
        );
    }

    public static OcorrenciaResponse ToOcorrenciaResponse(this Ocorrencia ocorrencia)
    {
        return new OcorrenciaResponse(
            Id: ocorrencia.Id,
            Local: ocorrencia.Local,
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
            Local = ocorrenciaRequest.Local,
            Status = ocorrenciaRequest.Status,
            Tipo = ocorrenciaRequest.Tipo,
            Vitimas = ocorrenciaRequest.Vitimas.Select(v => v.ToVitimaFromRequest()).ToList()
        };
    }
    public static Ocorrencia ToOcorrenciaFromResponse(this OcorrenciaResponse ocorrenciaResponse)
    {
        return new Ocorrencia
        {
            Id = ocorrenciaResponse.Id,
            Prioridade = ocorrenciaResponse.Prioridade,
            DataHora = ocorrenciaResponse.DataHora,
            Local = ocorrenciaResponse.Local,
            Status = ocorrenciaResponse.Status,
            Tipo = ocorrenciaResponse.Tipo,
            Vitimas = ocorrenciaResponse.Vitimas.Select(v => v.ToVitimaFromResponse()).ToList()
        };
    }
}