using api_safezone_cs.Domain.Enums;

namespace api_safezone_cs.DTOs.Ocorrencia;

public record OcorrenciaRequest(
    Domain.Entities.Localizacao Localizacao,
    TipoOcorrencia Tipo,
    Status Status,
    Prioridade Prioridade,
    DateTime DataHora);