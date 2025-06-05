using api_safezone_cs.ML;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var trainer = new OcorrenciaModelTrainer();
trainer.TreinarModelo("ML/Csv/ocorrencias_extremas.csv", "ML/Model/ocorrencias_modelo.zip");

Console.WriteLine("Modelo treinado e salvo com sucesso!");
app.MapGet("/", () => "Hello World!");

app.Run();