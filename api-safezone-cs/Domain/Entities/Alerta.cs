using System.ComponentModel.DataAnnotations;
using api_safezone_cs.Domain.Enums;

namespace api_safezone_cs.Domain.Entities;

public class Alerta
{
    [Key]
    public int Id { get; set; }
    public TipoAlerta Tipo { get; set; }
    [MaxLength(100)]
    public string AreaAfetada { get; set; } = null!;
    public Severidade Severidade { get; set; } 
    public Status Status { get; set; } 
    public DateTime DataHora { get; set; } = DateTime.UtcNow;
}