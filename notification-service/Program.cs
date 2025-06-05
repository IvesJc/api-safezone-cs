using notification_service.Services;

var builder = WebApplication.CreateBuilder(args);

// Registra o BackgroundService
builder.Services.AddHostedService<NotificationConsumer>();

var app = builder.Build();
app.Run();