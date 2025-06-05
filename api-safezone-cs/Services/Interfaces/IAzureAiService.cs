namespace api_safezone_cs.Services.Interfaces;

public interface IAzureAiService
{
    Task<string> PerguntarSobreAcidentesAsync(string pergunta);
}