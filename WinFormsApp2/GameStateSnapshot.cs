namespace Indigo
{
    public sealed class GameStateSnapshot
    {
        public int CurrentPlayerIndex { get; set; }
        public int GemsLeft { get; set; }
        public List<float> PlayerScores { get; set; } = new List<float>();
        public List<TileSnapshot> Tiles { get; set; } = new List<TileSnapshot>();
        public List<GemSnapshot> Gems { get; set; } = new List<GemSnapshot>();
    }

    public sealed class TileSnapshot
    {
        public int TileIndex { get; set; }
        public int BoardIndex { get; set; }
        public int Rotation { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public bool Active { get; set; }
        public List<int> Paths { get; set; } = new List<int>();
        public List<int> Neighbors { get; set; } = new List<int>();
    }

    public sealed class GemSnapshot
    {
        public int GemIndex { get; set; }
        public int OnTile { get; set; }
        public int OnPath { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public bool Active { get; set; }
    }
}
