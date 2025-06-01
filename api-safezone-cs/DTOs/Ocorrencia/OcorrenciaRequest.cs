using api_safezone_cs.Domain.Entities;
using api_safezone_cs.Domain.Enums;
using api_safezone_cs.DTOs.Vitima;

namespace api_safezone_cs.DTOs.Ocorrencia;

public record OcorrenciaRequest(
    string Local,
    TipoOcorrencia Tipo,
    Status Status,
    Prioridade Prioridade,
    DateTime DataHora,
    ICollection<VitimaRequest> Vitimas);