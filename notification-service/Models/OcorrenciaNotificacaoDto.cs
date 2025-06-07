namespace notification_service.Models;

public class OcorrenciaNotificacaoDto
{
    public string Tipo { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime DataHora { get; set; }
}