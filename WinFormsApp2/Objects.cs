using System.Numerics;

namespace Indigo
{
    internal class Tile : Object
    {
        public Image tilePic;
        public Image originalPic;
        public int numOfRotation = 0;
        public int index = -1;
        public int[] paths = new int[6];
        public int[] neighbors = [-1, -1, -1, -1, -1, -1];
        public List<Gem> gemsInside = new List<Gem>();

        public bool active = false;
        public Rectangle rect;
        public Tile(string imageLocation)
        {
            name = Path.GetFileNameWithoutExtension(imageLocation);
            name = name.Remove(name.Length - 5);

            width = 106;
            height = 122;

            tilePic = Image.FromFile(imageLocation);

            //if (imageLocation != "System.Drawing.Bitmap")
            //{
            //    tilePic = Image.FromFile(imageLocation);
            //}
            //else
            //    tilePic = Image.FromFile(Directory.GetFiles("Images", "BackOfTile.png").Last());        //some shit


            originalPic = tilePic;

            rect = new Rectangle(position.X, position.Y, width, height - height / 4);

            switch (name.ToLower())
            {
                case "sad":
                    paths = [5, 3, 4, 1, 2, 0];
                    break;
                case "goback":
                    paths = [5, 2, 1, 4, 3, 0];
                    break;
                case "overlap":
                    paths = [3, 4, 5, 0, 1, 2];
                    break;
                case "letterh":
                    paths = [2, 4, 0, 5, 1, 3];
                    break;
                case "oneway":
                    paths = [5, 4, 3, 2, 1, 0];
                    break;
                case "edge":
                    paths = [2, 1, 0, -1, -1, -1];
                    break;
                case "center":
                    paths = [0, 1, 2, 3, 4, 5];
                    break;
                default:
                    paths = [-1, -1, -1, -1, -1, -1];
                    break;
            }
        }
    }
    internal class Gem : Object
    {
        public Image gemPic;
        public int onTile = -1;
        public int onPath = -1;
        public bool active = false;
        public Gem(string imageLocation) 
        {
            name = Path.GetFileNameWithoutExtension(imageLocation);
            name = name.Remove(name.Length - 4);

            width = 25;
            height = 25;

            gemPic = Image.FromFile(imageLocation);
        }
    }
    internal class BoardImage : Object
    {
        public Image BoardPic;
        public BoardImage()
        {
            name = "Board_dot";
            width = 1000;
            height = 1000;
            position = new Point(0, 0);

            BoardPic = Image.FromFile("Images/board1.jpg");
            //BoardPic = Image.FromFile("Images/Board2.png");
        }
    }
    internal class Object
    {
        public string name = "Null";
        public int width;
        public int height;
        public Point position = new Point();
    }


    internal class DoubleBufferedPanel : Panel
    {
        public DoubleBufferedPanel()
        {
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;
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
