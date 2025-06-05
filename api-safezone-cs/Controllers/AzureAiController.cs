using api_safezone_cs.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace api_safezone_cs.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AzureAiController(IAzureAiService azureAiService) : ControllerBase
{
    [HttpPost("ml/pergunta-ai")]
    public async Task<IActionResult> PerguntarSobreAcidente([FromBody] string pergunta)
    {
        var resposta = await azureAiService.PerguntarSobreAcidentesAsync(pergunta);
        return Ok(resposta);
    }
}