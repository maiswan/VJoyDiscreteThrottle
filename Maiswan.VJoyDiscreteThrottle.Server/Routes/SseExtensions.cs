using Maiswan.VJoyDiscreteThrottle.Throttle;
using System.Text.Json;

namespace Maiswan.VJoyDiscreteThrottle.Server;

public class SseExtensions(DiscreteThrottle throttle)
{
    private readonly DiscreteThrottle throttle = throttle;

    public async Task NewStreamAsync(HttpContext context, HttpResponse response)
    {
        response.Headers.Append("Content-Type", "text/event-stream");
        response.Headers.Append("Cache-Control", "no-cache");
        response.Headers.Append("Connection", "keep-alive");

        CancellationToken token = context.RequestAborted;

        async void OnThrottleChanged(object? sender, ThrottleChangedEventArgs e)
        {
            await WriteAsync(response, e.ThrottleState, token);
        }
        throttle.OnThrottleChanged += OnThrottleChanged;

        try
        {
            // Write initial state
            await WriteAsync(response, throttle.ThrottleState, token);
            await Task.Delay(Timeout.Infinite, token);
        }
        catch (TaskCanceledException)
        {
            // client disconnected
        }
        finally
        {
            throttle.OnThrottleChanged -= OnThrottleChanged;
        }
    }

    private static async Task WriteAsync(HttpResponse response, object payload, CancellationToken token)
    {
        var text = JsonSerializer.Serialize(payload);
        await response.WriteAsync($"data: {text}\n\n", token);
        await response.Body.FlushAsync(token);
    }
}