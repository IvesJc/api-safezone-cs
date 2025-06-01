using api_safezone_cs.Domain.Enums;

namespace api_safezone_cs.DTOs.Vitima;

public record VitimaResponse(
    int Id,
    string Nome,
    int Idade,
    Condicao Condicao,
    Domain.Entities.Localizacao Localizacao,
    int OcorrenciaId,
    Domain.Entities.Ocorrencia Ocorrencia);