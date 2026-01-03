using Maiswan.VJoyDiscreteThrottle.Throttle;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace Maiswan.VJoyDiscreteThrottle.Server;

public class ThrottleEventBroadcaster
{
    private readonly ConcurrentDictionary<Guid, Func<ThrottleChangedEventArgs, Task>> clients = new();

    public Guid Subscribe(Func<ThrottleChangedEventArgs, Task> callback)
    {
        Guid id = Guid.NewGuid();
        clients[id] = callback;
        return id;
    }

    public void Unsubscribe(Guid id)
    {
        clients.TryRemove(id, out _);
    }

    public async Task BroadcastAsync(ThrottleChangedEventArgs args)
    {
        foreach (Func<ThrottleChangedEventArgs, Task> client in clients.Values)
        {
            try
            {
                await client(args);
            }
            catch
            {
                // Client disconnected, do nothing
            }
        }
    }
}