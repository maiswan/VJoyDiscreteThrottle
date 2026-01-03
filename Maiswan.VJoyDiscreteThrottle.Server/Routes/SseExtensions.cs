using Maiswan.VJoyDiscreteThrottle.Throttle;
using System.Text.Json;

namespace Maiswan.VJoyDiscreteThrottle.Server;

public class SseExtensions(ThrottleEventBroadcaster broadcaster)
{
    private readonly ThrottleEventBroadcaster broadcaster = broadcaster;

    public async Task NewStreamAsync(
        HttpContext context,
        HttpResponse response,
        Func<ThrottleChangedEventArgs, object> transformer)
    {
        response.Headers.Append("Content-Type", "text/event-stream");
        response.Headers.Append("Cache-Control", "no-cache");
        response.Headers.Append("Connection", "keep-alive");

        CancellationToken token = context.RequestAborted;

        var clientId = broadcaster.Subscribe(async args =>
        {
            var output = transformer(args);
            var payload = JsonSerializer.Serialize(output);
            await response.WriteAsync($"data: {payload}\n\n", token);
            await response.Body.FlushAsync(token);
        });

        try
        {
            await Task.Delay(Timeout.Infinite, token);
        }
        catch (TaskCanceledException)
        {
            // client disconnected
        }
        finally
        {
            broadcaster.Unsubscribe(clientId);
        }
    }
}