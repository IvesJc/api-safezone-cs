using System.ComponentModel.DataAnnotations;

namespace api_safezone_cs.Domain.Entities;

public class Localizacao
{
    [Required]
    [MaxLength(11)]
    public required string Latitude { get; set; }
    [Required]
    [MaxLength(12)]
    public required string Longitude { get; set; }
}