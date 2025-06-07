// Program.cs
using MassTransit;
using notification_service.Consumer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OcorrenciaConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("ocorrencia-notify", e =>
        {
            e.ConfigureConsumer<OcorrenciaConsumer>(context);
        });
    });
});

var app = builder.Build();
app.MapControllers();
app.Run();