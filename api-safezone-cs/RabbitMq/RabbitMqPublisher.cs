// using System.Text;
// using System.Text.Json;
// using Microsoft.EntityFrameworkCore.Metadata;
// using RabbitMQ.Client;
// using RabbitMQ.Client.Exceptions;
//
// namespace api_safezone_cs.RabbitMq;
//
//
// public class RabbitMqPublisher(IConfiguration _configuration, ILogger<RabbitMqPublisher> _logger)
// {
//     public async Task PublishAsync<T>(T message, string queueName)
//     {
//         try
//         {
//             var factory = new ConnectionFactory
//             {
//                 HostName = _configuration["RabbitMQ:HostName"],
//                 UserName = _configuration["RabbitMQ:UserName"],
//                 Password = _configuration["RabbitMQ:Password"]
//             };
//
//             await using var connection = await factory.CreateConnectionAsync();
//             await using var channel = await connection.CreateChannelAsync();
//
//             await channel.QueueDeclareAsync(queue: queueName,
//                 durable: true,
//                 exclusive: false,
//                 autoDelete: false,
//                 arguments: null);
//
//             var json = JsonSerializer.Serialize(message);
//             var body = Encoding.UTF8.GetBytes(json);
//
//             var properties = await channel.CreateBasicPropertiesAsync();
//             properties.DeliveryMode = 2; // 2 = Persistent
//
//             await channel.BasicPublishAsync(new BasicPublishArgs
//             {
//                 Exchange = "",
//                 RoutingKey = queueName,
//                 BasicProperties = properties,
//                 Body = body
//             });
//
//             _logger.LogInformation("Mensagem publicada com sucesso na fila '{Queue}'", queueName);
//         }
//         catch (BrokerUnreachableException ex)
//         {
//             _logger.LogError(ex, "Não foi possível se conectar ao broker RabbitMQ.");
//             throw;
//         }
//         catch (Exception ex)
//         {
//             _logger.LogError(ex, "Erro ao publicar mensagem no RabbitMQ.");
//             throw;
//         }
//     }}