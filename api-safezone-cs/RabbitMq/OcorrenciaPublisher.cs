// using System.Text;
// using System.Text.Json;
// using RabbitMQ.Client;
//
// namespace api_safezone_cs.RabbitMq;
//
// public class OcorrenciaPublisher
// {
//     private readonly string _hostname = "localhost";
//     private readonly string _queueName = "ocorrencias";
//     private readonly string _username = "guest";
//     private readonly string _password = "guest";
//
//     public async void  PublicarNovaOcorrencia(object ocorrencia)
//     {
//         var factory = new ConnectionFactory()
//         {
//             HostName = _hostname,
//             UserName = _username,
//             Password = _password
//         };
//
//         using var connection = await factory.CreateConnectionAsync();
//         using var channel = connection.CreateModel();
//
//         // Garante que a fila existe
//         channel.QueueDeclare(queue: _queueName,
//             durable: false,
//             exclusive: false,
//             autoDelete: false,
//             arguments: null);
//
//         var json = JsonSerializer.Serialize(ocorrencia);
//         var body = Encoding.UTF8.GetBytes(json);
//
//         channel.BasicPublish(exchange: "",
//             routingKey: _queueName,
//             basicProperties: null,
//             body: body);
//     }
// }