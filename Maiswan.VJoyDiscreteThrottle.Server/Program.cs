using Maiswan.VJoyDiscreteThrottle.Server;
using Maiswan.VJoyDiscreteThrottle.Throttle;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Use first available config
string[] configPaths = [
    "./configurations.dev.json",
    "./configurations.prod.json",
    "./configurations.json",
];
string configPath = configPaths.First(File.Exists);
string rawJson = File.ReadAllText(configPath);
ServerConfiguration config = JsonSerializer.Deserialize<ServerConfiguration>(rawJson)
    ?? throw new InvalidOperationException("Invalid configuration");

// Initialize virtual throttle and conduct DI
builder.Services.AddSingleton<Configuration>(config);
builder.Services.AddSingleton<DiscreteThrottle>();
builder.Services.AddSingleton<SseExtensions>();

// Add services to the container.
builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthorization();
app.MapControllers();
app.Run($"http://localhost:{config.ServerPort}");
