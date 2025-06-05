using System.Text;
using System.Text.Json;
using notification_service.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace notification_service.Services;

public class NotificationConsumer : BackgroundService
{
    private readonly ILogger<NotificationConsumer> _logger;

    public NotificationConsumer(ILogger<NotificationConsumer> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = "rabbitmq"
        };

        var connection = await factory.CreateConnectionAsync(stoppingToken); // novo método
        var channel = await connection.CreateChannelAsync();    // novo método

        await channel.QueueDeclareAsync(
            queue: "ocorrencias",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            cancellationToken: stoppingToken);

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var messageJson = Encoding.UTF8.GetString(body);

            var ocorrencia = JsonSerializer.Deserialize<OcorrenciaMessage>(messageJson);

            Console.WriteLine($"[Notificação recebida] Tipo: {ocorrencia?.Tipo}, Prioridade: {ocorrencia?.Prioridade}");

            await Task.Yield(); // Simula algo assíncrono real
        };

        await channel.BasicConsumeAsync(queue: "ocorrencias", autoAck: true, consumer: consumer, cancellationToken: stoppingToken);

        await Task.Delay(Timeout.Infinite, stoppingToken); // mantém vivo
    }
}