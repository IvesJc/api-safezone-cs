using MassTransit;
using notification_service.Models;

namespace notification_service.Consumer;

public class OcorrenciaConsumer : IConsumer<OcorrenciaNotificacaoDto>
{
    private readonly ILogger<OcorrenciaConsumer> _logger;

    public OcorrenciaConsumer(ILogger<OcorrenciaConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<OcorrenciaNotificacaoDto> context)
    {
        var msg = context.Message;

        _logger.LogInformation("📥 Nova ocorrência recebida: Tipo: {Tipo}, Status: {Status}, Data: {DataHora}",
            msg.Tipo, msg.Status, msg.DataHora);

        return Task.CompletedTask;
    }
}