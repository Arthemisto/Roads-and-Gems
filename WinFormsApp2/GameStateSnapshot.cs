namespace Indigo
{
    internal sealed class GameStateLogEntry
    {
        public string Reason { get; set; } = string.Empty;
        public GameStateSnapshot State { get; set; } = new GameStateSnapshot();
    }

    internal sealed class GameStateSnapshot
    {
        public DateTime TimestampUtc { get; set; }
        public int PlayerCount { get; set; }
        public int CurrentPlayerIndex { get; set; }
        public bool DebugMode { get; set; }
        public bool HideMode { get; set; }
        public int RemainingDrawTiles { get; set; }
        public int MovingGemCount { get; set; }
        public List<PlayerStateSnapshot> Players { get; set; } = new List<PlayerStateSnapshot>();
        public List<GatewayStateSnapshot> Gateways { get; set; } = new List<GatewayStateSnapshot>();
        public List<TileStateSnapshot> PlacedTiles { get; set; } = new List<TileStateSnapshot>();
        public List<GemStateSnapshot> Gems { get; set; } = new List<GemStateSnapshot>();
    }

    internal sealed class PlayerStateSnapshot
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public float Score { get; set; }
    }

    internal sealed class GatewayStateSnapshot
    {
        public int GatewayIndex { get; set; }
        public int[] Owners { get; set; } = Array.Empty<int>();
    }

    internal sealed class TileStateSnapshot
    {
        public int TileId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int BoardIndex { get; set; }
        public int Rotation { get; set; }
        public int[] Paths { get; set; } = Array.Empty<int>();
        public int[] Neighbors { get; set; } = Array.Empty<int>();
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public bool IsActive { get; set; }
    }

    internal sealed class GemStateSnapshot
    {
        public int GemId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int OnTile { get; set; }
        public int OnPath { get; set; }
        public bool IsActive { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
    }
}
