namespace notification_service.Models;

public class OcorrenciaMessage
{
    public int Id { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Prioridade { get; set; } = string.Empty;
    public DateTime DataHora { get; set; }
}