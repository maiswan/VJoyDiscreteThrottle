using Maiswan.vJoyThrottle;
using Maiswan.vJoyThrottleServer;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Read config
const string configPath = "./configurations.json";
string rawJson = File.ReadAllText(configPath);
Configuration config = JsonSerializer.Deserialize<Configuration>(rawJson) ?? new();

// Initialize virtual throttle and conduct DI
using DiscreteThrottle throttle = new(config.Notches.MaxBrake, config.Notches.MaxPower);
builder.Services.AddSingleton<DiscreteThrottle>(throttle);

// Add services to the container.
builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
