using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using api_safezone_cs.Domain.Enums;

namespace api_safezone_cs.Domain.Entities;

public class Vitima
{
    [Key]
    public int Id { get; set; }
    
    [MaxLength(100)]
    public string Nome { get; set; } = null!;
    
    [Range(0, 120)]
    public int Idade { get; set; }
    
    public Condicao Condicao { get; set; }
    
    [Required]
    public required Localizacao Localizacao { get; set; } 
    
    public int OcorrenciaId { get; set; }
    [ForeignKey("OcorrenciaId")]
    [JsonIgnore]
    public Ocorrencia Ocorrencia { get; set; } = null!;
}