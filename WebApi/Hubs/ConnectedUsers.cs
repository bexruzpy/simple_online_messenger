public static class ConnectedUsers
{
    // UserId => List of ConnectionIds
    public static Dictionary<int, List<string>> UserConnections { get; } = new();
}
