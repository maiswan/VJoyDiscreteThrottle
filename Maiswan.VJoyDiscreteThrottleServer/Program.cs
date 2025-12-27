using Maiswan.vJoyThrottle;
using Maiswan.vJoyThrottleServer;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Read config
const string configPath = "./configurations.json";
string rawJson = File.ReadAllText(configPath);
Configuration config = JsonSerializer.Deserialize<Configuration>(rawJson)
    ?? throw new InvalidOperationException("Invalid configuration");

// Initialize virtual throttle and conduct DI
using DiscreteThrottle throttle = new(config);
builder.Services.AddSingleton(throttle);

// Add services to the container.
builder.Services.AddControllers();
var app = builder.Build();

// Home route
app.MapGet("/", () => "maiswan/VJoyDiscreteThrottle");

// Configure the HTTP request pipeline.
app.UseAuthorization();
app.MapControllers();
app.Run($"http://localhost:{config.ServerPort}");
