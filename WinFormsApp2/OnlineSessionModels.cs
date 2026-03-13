namespace Indigo
{
    internal sealed class OnlinePlayerInfo
    {
        public int PlayerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsHost { get; set; }
    }

    public sealed class TurnMessage
    {
        public int TurnNumber { get; set; }
        public int PlayerIndex { get; set; }
        public int TileIndex { get; set; }
        public int Rotation { get; set; }
        public int BoardIndex { get; set; }
        public string StateHashBefore { get; set; } = string.Empty;
        public string StateHashAfter { get; set; } = string.Empty;
    }

    internal sealed class OnlineSessionEnvelope
    {
        public string Type { get; set; } = string.Empty;

        public string? Message { get; set; }
        public int? PlayerId { get; set; }
        public int? PlayerCount { get; set; }
        public int? MaxPlayers { get; set; }

        public string? SessionName { get; set; }
        public string? PlayerName { get; set; }

        public List<OnlinePlayerInfo> Players { get; set; } = new List<OnlinePlayerInfo>();

        public TurnMessage? Turn { get; set; }
        public GameStateSnapshot? Snapshot { get; set; }
    }
}
