using System.Text;
using System.Text.Json;
using api_safezone_cs.Services.Interfaces;

namespace api_safezone_cs.Services.Service;

public class AzureAiService : IAzureAiService
{
    private readonly HttpClient _httpClient;
    private readonly string _endpoint;
    private readonly string _apiKey;

    public AzureAiService(IConfiguration config)
    {
        _httpClient = new HttpClient();
        _endpoint = config["AzureAI:Endpoint"];
        _apiKey = config["AzureAI:ApiKey"];
    }

    public async Task<string> PerguntarSobreAcidentesAsync(string prompt)
    {
        var request = new
        {
            messages = new[]
            {
                new { role = "system", content = "Você é um especialista em desastres naturais." },
                new { role = "user", content = prompt }
            }
        };

        var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("api-key", _apiKey);

        var response = await _httpClient.PostAsync(_endpoint, content);
        var responseString = await response.Content.ReadAsStringAsync();

        // Extrai apenas o content
        var document = JsonDocument.Parse(responseString);
        var respostaDaIa = document
            .RootElement
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString();

        return respostaDaIa ?? "Não foi possível obter uma resposta da IA.";
    }
}