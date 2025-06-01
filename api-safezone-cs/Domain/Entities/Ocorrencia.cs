using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api_safezone_cs.Domain.Enums;

namespace api_safezone_cs.Domain.Entities;

public class Ocorrencia
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Local { get; set; } = null!;
    
    public TipoOcorrencia Tipo { get; set; }
    
    public Status Status { get; set; } 
    public Prioridade Prioridade { get; set; }
    public DateTime DataHora { get; set; } = DateTime.UtcNow;

    [ForeignKey("Vitima")]
    public ICollection<Vitima> Vitimas { get; set; } = new List<Vitima>();
}

