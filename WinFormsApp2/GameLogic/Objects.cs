using System.Numerics;

namespace Indigo
{
    internal class GameObject
    {
        public int id;
        public string name = "Null";
        public Image picture = null!;

        public Point position = new Point();
        public int Width = 100;
        public int Height = 100;

        public virtual void Draw(Graphics g, bool debugMode)
        {
            g.DrawImage(picture, position.X, position.Y, Width, Height);
        }
    }
    internal class BoardImage : GameObject
    {
        public static new int Width = 1000;
        public static new int Height = (int)(Width * 2475f / 2452f);

        public BoardImage()
        {
            id = 0;
            name = "Empty_Board";
            picture = Properties.Resources.Fin_Board;

            position = new Point(0, 0);
        }

        public override void Draw(Graphics g, bool debugMode)
        {
            g.DrawImage(picture, position.X, position.Y, Width, Height);
        }
    }
    internal class Tile : GameObject
    {
        public Image originalPic;
        public static new int Height = 125;
        public static new int Width = (int)(Height * 0.866);

        public int numOfRotation = 0;
        public int index = -1;
        public int[] paths = new int[6];
        public int[] neighbors = [-1, -1, -1, -1, -1, -1];
        public List<Gem> gemsInside = new List<Gem>();

        public bool active = false;
        public Rectangle rect;

        public Tile(int id, int tileNumber)
        {
            this.id = id;
            rect = new Rectangle(position.X, position.Y, Width, Height - Height / 4);
            picture = Properties.Resources.BackOfTile;

            switch (tileNumber)
            {
                case 0:
                    name = "Center";
                    originalPic = Properties.Resources.Center_tile;
                    paths = [0, 1, 2, 3, 4, 5];

                    break;
                case 1:
                    name = "Edge";
                    originalPic = Properties.Resources.Edge_tile;
                    paths = [2, 1, 0, -1, -1, -1];

                    break;
                case 2:
                    name = "GoBack";
                    originalPic = Properties.Resources.GoBack_tile;
                    paths = [5, 2, 1, 4, 3, 0];

                    break;
                case 3:
                    name = "LetterH";
                    originalPic = Properties.Resources.LetterH_tile;
                    paths = [2, 4, 0, 5, 1, 3];

                    break;
                case 4:
                    name = "OneWay";
                    originalPic = Properties.Resources.OneWay_tile;
                    paths = [5, 4, 3, 2, 1, 0];

                    break;
                case 5:
                    name = "Overlap";
                    originalPic = Properties.Resources.Overlap_tile;
                    paths = [3, 4, 5, 0, 1, 2];

                    break;
                case 6:
                    name = "Sad";
                    originalPic = Properties.Resources.Sad_tile;
                    paths = [5, 3, 4, 1, 2, 0];

                    break;
                default:
                    name = "Null";
                    originalPic = Properties.Resources.BackOfTile;
                    paths = [-1, -1, -1, -1, -1, -1];

                    break;
            }
        }

        public override void Draw(Graphics g, bool debugMode)
        {
            g.DrawImage(picture, position.X, position.Y, Width, Height);

            if (debugMode)
                g.DrawString(id.ToString(), SystemFonts.DefaultFont, Brushes.White, position.X + 25, position.Y + 25);
        }
    }
    internal class Gem : GameObject
    {
        public static new int Width = 25;
        public static new int Height = 25;

        public int onTile = -1;
        public int onPath = -1;
        public bool active = false;

        public Gem(int id, int gemNumber) 
        {
            this.id = id;

            switch (gemNumber)
            {
                case 0:
                    name = "Blue";
                    picture = Properties.Resources.Blue_gem;

                    break;
                case 1:
                    name = "Green";
                    picture = Properties.Resources.Green_gem;

                    break;
                case 2:
                default:
                    name = "Yellow";
                    picture = Properties.Resources.Yellow_gem;

                    break;
            }
        }

        public override void Draw(Graphics g, bool debugMode)
        {
            g.DrawImage(picture, position.X, position.Y, Width, Height);

            if (debugMode)
            {
                g.DrawString(id.ToString(), SystemFonts.DefaultFont, Brushes.Red, position.X + 25, position.Y);
                g.FillRectangle(Brushes.Gray, position.X - 2, position.Y - 2, 4, 4);
            }
        }
    }
    internal class PlayerToken : GameObject
    {
        public static new int Width = 50;
        public static new int Height = 50;

        public int playerNumber;

        public PlayerToken(int id, int playerNumber, string color)
        {
            this.id = id;
            this.playerNumber = playerNumber;
            name = color;

            switch (name)
            {
                case "Cyan":
                    picture = Properties.Resources.Cyan_point;
                    break;
                case "Purple":
                    picture = Properties.Resources.Purple_point;
                    break;
                case "Red":
                    picture = Properties.Resources.Red_point;
                    break;
                case "White":
                default:
                    picture = Properties.Resources.White_point;
                    break;
            }
        }
        public override void Draw(Graphics g, bool debugMode)
        {
            g.DrawImage(picture, position.X, position.Y, Width, Height);

            if (debugMode)
            {
                g.DrawString(id.ToString(), SystemFonts.DefaultFont, Brushes.White, position.X + 25, position.Y + 25);
                g.FillRectangle(Brushes.Gray, position.X - 2, position.Y - 2, 4, 4);
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
        
        public Tile? nextTile;
        public Vector2 startPoint;
        public Vector2 middlePoint;
        public Vector2 endPoint;
        public int willExitBy;
        public int diff;
        public float speed;
    }
}
