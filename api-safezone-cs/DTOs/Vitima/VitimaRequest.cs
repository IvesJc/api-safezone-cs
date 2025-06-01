using api_safezone_cs.Domain.Enums;
using api_safezone_cs.Domain.Entities;

namespace api_safezone_cs.DTOs.Vitima;

public record VitimaRequest(
    string Nome,
    int Idade,
    Condicao Condicao,
    Domain.Entities.Localizacao Localizacao,
    int OcorrenciaId);