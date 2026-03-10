namespace Indigo
{
    internal sealed class OnlinePlayerInfo
    {
        public int PlayerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsHost { get; set; }
    }

    internal sealed class OnlineSessionEnvelope
    {
        public string Type { get; set; } = string.Empty;
        public string? Message { get; set; }
        public int? PlayerId { get; set; }
        public int? MaxPlayers { get; set; }
        public string? SessionName { get; set; }
        public string? PlayerName { get; set; }
        public List<OnlinePlayerInfo> Players { get; set; } = new List<OnlinePlayerInfo>();
    }
}
