using System.Numerics;

namespace Indigo
{
    internal class Object
    {
        public string name = "Null";
        public Point position = new Point();
    }
    internal class BoardImage : Object
    {
        public Image boardPic;
        public static int width = 1000;
        public static int height = (int)(width * 2475f / 2452f);
        public BoardImage()
        {
            name = "Empty_Board";
            position = new Point(0, 0);

            boardPic = Resource1.Fin_Board;
        }
    }
    internal class Tile : Object
    {
        public Image tilePic;
        public Image originalPic;
        public static int height = 125;
        public static int width = (int)(height * 0.866);

        public int numOfRotation = 0;
        public int index = -1;
        public int[] paths = new int[6];
        public int[] neighbors = [-1, -1, -1, -1, -1, -1];
        public List<Gem> gemsInside = new List<Gem>();

        public bool active = false;
        public Rectangle rect;

        public Tile(int tileNumber)
        {
            rect = new Rectangle(position.X, position.Y, width, height - height / 4);

            switch (tileNumber)
            {
                case 0:
                    name = "Center";
                    tilePic = Resource1.Center_tile;
                    paths = [0, 1, 2, 3, 4, 5];

                    break;
                case 1:
                    name = "Edge";
                    tilePic = Resource1.Edge_tile;
                    paths = [2, 1, 0, -1, -1, -1];

                    break;
                case 2:
                    name = "GoBack";
                    tilePic = Resource1.GoBack_tile;
                    paths = [5, 2, 1, 4, 3, 0];

                    break;
                case 3:
                    name = "LetterH";
                    tilePic = Resource1.LetterH_tile;
                    paths = [2, 4, 0, 5, 1, 3];

                    break;
                case 4:
                    name = "OneWay";
                    tilePic = Resource1.OneWay_tile;
                    paths = [5, 4, 3, 2, 1, 0];

                    break;
                case 5:
                    name = "Overlap";
                    tilePic = Resource1.Overlap_tile;
                    paths = [3, 4, 5, 0, 1, 2];

                    break;
                case 6:
                    name = "Sad";
                    tilePic = Resource1.Sad_tile;
                    paths = [5, 3, 4, 1, 2, 0];

                    break;
                default:
                    name = "Null";
                    tilePic = Resource1.BackOfTile;
                    paths = [-1, -1, -1, -1, -1, -1];

                    break;
            }
            originalPic = tilePic;
        }
    }
    internal class Gem : Object
    {
        public Image gemPic;
        public static int width = 25;
        public static int height = 25;
        public int onTile = -1;
        public int onPath = -1;
        public bool active = false;
        public Gem(int gemNumber) 
        {
            switch (gemNumber)
            {
                case 0:
                    name = "Blue";
                    gemPic = Resource1.Blue_gem;

                    break;
                case 1:
                    name = "Green";
                    gemPic = Resource1.Green_gem;

                    break;
                case 2:
                default:
                    name = "Yellow";
                    gemPic = Resource1.Yellow_gem;

                    break;
            }
        }
    }
    internal class PlayerToken : Object
    {
        public Image tokenPic;
        public static int width = 50;
        public static int height = 50;

        public int playerNumber;
        public PlayerToken(int playerNumber, string color)
        {
            this.playerNumber = playerNumber;
            name = color;

            switch (name)
            {
                case "Cyan":
                    tokenPic = Resource1.Cyan_player;
                    break;
                case "Purple":
                    tokenPic = Resource1.Purple_player;
                    break;
                case "Red":
                    tokenPic = Resource1.Red_player;
                    break;
                case "White":
                default:
                    tokenPic = Resource1.White_player;
                    break;
            }
        }
    }

    internal class DoubleBufferedPanel : Panel
    {
        public DoubleBufferedPanel()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
        }
    }
    internal struct Movement
    {
        public float t;
        public Movement()
        {
            t = 0f;
            speed = 1f;
        }
        
        public Tile nextTile;
        public Vector2 startPoint;
        public Vector2 middlePoint;
        public Vector2 endPoint;
        public int willExitBy;
        public int diff;
        public float speed;
    }
}
