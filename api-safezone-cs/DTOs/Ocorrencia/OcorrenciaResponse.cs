using api_safezone_cs.Domain.Enums;
using api_safezone_cs.DTOs.Vitima;

namespace api_safezone_cs.DTOs.Ocorrencia;

public record OcorrenciaResponse(
    int Id,
    Domain.Entities.Localizacao Localizacao,
    TipoOcorrencia Tipo,
    Status Status,
    Prioridade Prioridade,
    DateTime DataHora,
    ICollection<VitimaResponse> Vitimas
);