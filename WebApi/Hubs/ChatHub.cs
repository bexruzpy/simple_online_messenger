using Microsoft.AspNetCore.SignalR;

namespace WebApi.Hubs;

public class ChatHub : Hub
{
    public override Task OnConnectedAsync()
    {
        // Foydalanuvchi ID sini aniqlash (bu sizda token orqali bo'lishi mumkin)
        var userId = GetUserId();

        lock (ConnectedUsers.UserConnections)
        {
            if (ConnectedUsers.UserConnections.ContainsKey(userId))
            {
                ConnectedUsers.UserConnections[userId].Add(Context.ConnectionId);
            }
            else
            {
                ConnectedUsers.UserConnections[userId] = new List<string> { Context.ConnectionId };
            }
        }

        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        lock (ConnectedUsers.UserConnections)
        {
            foreach (var kvp in ConnectedUsers.UserConnections)
            {
                if (kvp.Value.Contains(Context.ConnectionId))
                {
                    kvp.Value.Remove(Context.ConnectionId);
                    if (kvp.Value.Count == 0)
                        ConnectedUsers.UserConnections.Remove(kvp.Key);
                    break;
                }
            }
        }

        return base.OnDisconnectedAsync(exception);
    }

    private int GetUserId()
    {
        // Token yoki Context.User.Claims dan olish
        var idClaim = Context.User?.FindFirst("id");
        return idClaim != null ? int.Parse(idClaim.Value) : 0;
    }
    
    public async Task SendPrivateMessage(int userId, string message)
    {
        if (ConnectedUsers.UserConnections.TryGetValue(userId, out var connections))
        {
            foreach (var connectionId in connections)
            {
                await Clients.Client(connectionId).SendAsync("ReceiveMessage", message);
            }
        }
    }

}
