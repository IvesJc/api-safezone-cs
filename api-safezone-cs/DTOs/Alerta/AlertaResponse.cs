using System.ComponentModel.DataAnnotations;
using api_safezone_cs.Domain.Enums;

namespace api_safezone_cs.DTOs.Alerta;

public record AlertaResponse(
    int Id,
    TipoAlerta Tipo,
    [MaxLength(100)] string AreaAfetada,
    Severidade Severidade,
    Status Status,
    DateTime DataHora
);