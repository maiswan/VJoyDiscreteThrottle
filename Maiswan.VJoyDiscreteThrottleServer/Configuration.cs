namespace Maiswan.vJoyThrottle;

internal record Notches
{
	public int MaxBrake { get; init; } = 0;
	public int MaxPower { get; init; } = 0;
}

internal class Configuration
{
	public Notches Notches { get; init; } = new();
}
