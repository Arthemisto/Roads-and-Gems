using System.Numerics;
using System.Xml.Linq;

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
        public Tile(int tileNumber)
        {
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

            width = 106;
            height = 122;

            rect = new Rectangle(position.X, position.Y, width, height - height / 4);

            /*
            name = Path.GetFileNameWithoutExtension(imageLocation);
            name = name.Remove(name.Length - 5);

            width = 106;
            height = 122;

            //tilePic = Resource1.BackOfTile;
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
            */
        }
    }
    internal class Gem : Object
    {
        public Image gemPic;
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
                default:
                    name = "Yellow";
                    gemPic = Resource1.Yellow_gem;

                    break;
            }

            width = 25;
            height = 25;

            /*
            name = Path.GetFileNameWithoutExtension(imageLocation);
            name = name.Remove(name.Length - 4);

            width = 25;
            height = 25;

            gemPic = Image.FromFile(imageLocation);
            */
        }
    }
    internal class BoardImage : Object
    {
        public Image BoardPic;
        public BoardImage()
        {
            name = "Empty_Board";
            width = 1000;
            height = 1000;
            position = new Point(0, 0);

            BoardPic = Resource1.board1;
            //BoardPic = Image.FromFile("Images/board1.jpg");
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
