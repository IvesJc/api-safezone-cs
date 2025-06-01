using System.ComponentModel.DataAnnotations;

namespace api_safezone_cs.DTOs.Localizacao;

public record LocalizacaoDto(
    [Required] [MaxLength(11)] string Latitude,
    [Required] [MaxLength(12)] string Longitude
);