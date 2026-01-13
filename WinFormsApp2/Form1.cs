using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Net;
using System.Numerics;
using System.Security.Cryptography;

namespace Indigo
{
    public partial class Form1 : Form
    {
        List<Tile> tiles = new List<Tile>();
        Tile SelectedTile;
        int indexValue;
        int xPos = 100;
        List<string> imageLocation = new List<string>();
        int tileNumber = -1;
        int totalTiles = 0;
        int totalGems = 12;
        int lineAnimation = 0;

        int offsetX = -4;
        int offsetY = 5;            //Persition by eye
        int boardOffset = 5;

        float distanceFromCtoC = 10000;
        int rings = 5;

        public Size formOriginalSize;
        BoardImage boardImage;

        Vector2[] points;
        Tile[] placedTiles;
        List<Gem> gems = new List<Gem>();
        List<Gem> movingGems = new List<Gem>();
        Image rotated;

        Movement m1 = new Movement();

        bool debugMode = false;
        public Form1()
        {
            InitializeComponent();
            //SetUpApp();

            boardImage = new BoardImage();
            placedTiles = new Tile[3 * rings * rings - 3 * rings + 1];

            formOriginalSize = ClientSize;
            distanceFromCtoC = 0.1f * boardImage.width + boardOffset;

            boardImage.position.X = (Board.Width - boardImage.width) / 2;

            var centerX = boardImage.position.X + boardImage.width / 2 + offsetX;
            var centerY = boardImage.position.Y + boardImage.height / 2 + offsetY;
            Vector2 center = new Vector2(centerX, centerY);

            points = CreateHexGrid(center, 5, distanceFromCtoC);

            SetUpApp();

            //GemTimer.Start();
        }
        private void SetUpApp()
        {
            imageLocation = Directory.GetFiles("Images", "*_tile.png").ToList();

            /*
            List<int> picNumbers = new List<int>();

            for (int i = 0; i < imageLocation.Count; i++)
                picNumbers.Add(i);

            for (int i = 0; i < 5; i++)
                picNumbers.Insert(1, picNumbers[1]);

            int lastIndex = picNumbers.Count - 1;

            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 3; j++)
                    picNumbers.Insert(lastIndex - i, picNumbers[lastIndex - i]);

            totalTiles = picNumbers.Count;
            */

            List<string> temp = new List<string>(imageLocation);

            temp.RemoveAt(0);
            temp.RemoveAt(0);

            for (int i = 0; i < 5; i++)
                imageLocation.Insert(1, imageLocation[1]);

            for (int i = 0; i < 3; i++)
                imageLocation.AddRange(temp);

            totalTiles = imageLocation.Count;
            for (int i = 0; i < totalTiles; i++)
                MakeTiles();

            imageLocation = Directory.GetFiles("Images", "*_gem.png").ToList();
            for (int i = 0; i < 4; i++)
            {
                imageLocation.Insert(1, imageLocation[1]);
                int lasGem = imageLocation.Count - 1;
                imageLocation.Insert(lasGem, imageLocation[lasGem]);
            }
            imageLocation.Insert(7, imageLocation[7]);

            for (int i = 0; i < totalGems; i++)
                MakeGems(i);

            label1.Text = "Card " + (tileNumber + 1) + " of " + totalTiles + " Offset: " + (0.1f * boardImage.width + 5);
        }
        private void MakeTiles()
        {
            tileNumber++;
            Tile newTile = new Tile(imageLocation[tileNumber]);
            int placedIndex = -1;

            int x_1;
            int y_1;

            if (newTile.name == "Center")
            {
                x_1 = (int)(points[0].X - newTile.width / 2f); // + boardOffset);
                y_1 = (int)(points[0].Y - newTile.height / 2f); // + boardOffset);
                newTile.index = 0;
                placedIndex = 0;
            }
            else if (newTile.name == "Edge")
            {
                int[] temp = new int[6];
                int p = 37 + (tileNumber + 2) % 6;
                newTile.index = p;
                placedIndex = p;

                x_1 = (int)(points[p].X - newTile.width / 2f); // + boardOffset);
                y_1 = (int)(points[p].Y - newTile.height / 2f); // + boardOffset);

                if (tileNumber > 1)
                {
                    newTile.tilePic = ImageUtils.RotateHex(newTile.tilePic, 60f * (tileNumber - 1), newTile.width, newTile.height);

                    for (int i = 0; i < 6; i++)
                    {
                        temp[i] = newTile.paths[(i + 5 + (2 - tileNumber)) % 6];
                        if (temp[i] != -1)
                            temp[i] = (temp[i] + tileNumber - 1) % 6;
                    }

                    newTile.paths = temp;
                }
            }
            else
            {
                x_1 = xPos;
                y_1 = 1000;
                xPos += 200;

                if (xPos >= 5 * 200)
                    xPos -= 5 * 200 + 10;       //magic number
            }

            newTile.position.X = x_1;
            newTile.position.Y = y_1;
            newTile.rect.X = newTile.position.X;
            newTile.rect.Y = newTile.position.Y + newTile.height / 8;

            tiles.Add(newTile);
            if (placedIndex != -1)
                placedTiles[placedIndex] = newTile;
        }
        private void MakeGems(int gemNumber)
        {
            Gem newGem = new Gem(imageLocation[gemNumber]);
            int x_1 = 0;
            int y_1 = 0;
            if (newGem.name == "Blue")
            {
                newGem.onTile = 0;
                x_1 = (int)(points[0].X - newGem.width / 2f);
                y_1 = (int)(points[0].Y - newGem.height / 2f);
            }
            else if (newGem.name == "Green")
            {
                newGem.onTile = 0;
                x_1 = (int)(points[0].X - newGem.width / 2f + 30 * (float)Math.Cos(gemNumber * 72 * Math.PI / 180f));
                y_1 = (int)(points[0].Y - newGem.height / 2f + 30 * (float)Math.Sin(gemNumber * 72 * Math.PI / 180f));
            }
            else if (newGem.name == "Yellow")
            {
                newGem.onTile = 37 + gemNumber % 6;
                newGem.onPath = (4 + gemNumber % 6) % 6;

                Vector2 v_1 = Vector2.Lerp(points[newGem.onTile], points[newGem.onTile - 18], 1 / 4f);
                x_1 = (int)(v_1.X - newGem.width / 2f);
                y_1 = (int)(v_1.Y - newGem.height / 2f);
            }
            newGem.position.X = x_1;
            newGem.position.Y = y_1;

            gems.Add(newGem);
            placedTiles[newGem.onTile].gemsInside.Add(newGem);
        }

        public Vector2[] CreateHexGrid(Vector2 center, int diam, float originalR)
        {
            var points = new List<Vector2> { center };
            var r = originalR;

            for (int ring = 1; ring < diam; ring++)
            {
                for (int a = 0; a < 6; a++)
                {
                    var x_1 = center.X + r * (float)Math.Cos(a * 60 * Math.PI / 180f);
                    var y_1 = center.Y + r * (float)Math.Sin(a * 60 * Math.PI / 180f);
                    points.Add(new Vector2(x_1, y_1));
                }
                r += originalR;

                if (diam < 3 || ring == 1)
                    continue;

                int first = points.Count() - 6;

                for (int i = 0; i < 5; i++)
                    for (int j = 1; j < ring; j++)
                    {
                        Vector2 middlepoint = (Vector2.Lerp(points[first + i], points[first + i + 1], (float)j / ring));
                        points.Add(middlepoint);
                    }

                for (int j = 1; j < ring; j++)
                    points.Add(Vector2.Lerp(points[first + 5], points[first], (float)j / ring));
            }
            return points.ToArray();
        }
        public int GetClosestIndex(Vector2 v1)
        {
            Vector2 closestPoint = points.OrderByDescending(v2 => Vector2.Distance(v1, v2)).Last();

            if (Vector2.Distance(v1, closestPoint) > distanceFromCtoC / 2)  //magic number
                return -1;

            int index = Array.IndexOf(points, closestPoint);

            return index;
        }
        private void Snap(Tile tile)
        {
            Vector2 pos = new Vector2(tile.position.X + tile.width / 2, tile.position.Y + tile.height / 2);
            int index = GetClosestIndex(pos);

            if (index < 0 || placedTiles[index] != null)
                return;

            tile.index = index;
            Vector2 new_pos = points[index];

            int newX = (int)(new_pos.X - tile.width / 2f);      // + boardOffset);  // 5 in dot
            int newY = (int)(new_pos.Y - tile.height / 2f);     // + boardOffset);

            tile.position = new Point(newX, newY);
            placedTiles[index] = tile;              //DEBUG HERE TO CHECH IF VALUES ARE CORRECT

            List<int> neighborIndexies = FindNeighbors(tile, new_pos);
            if (!neighborIndexies.Any())
                return;

            EventsAfterPlacement(tile, neighborIndexies);
        }
        private List<int> FindNeighbors(Tile tile, Vector2 pos)
        {
            List<Vector2> neighborsV = points.OrderBy(v => Vector2.Distance(pos, v)).Take(7).ToList();
            List<int> realNeighbors = new List<int>();

            if (neighborsV[0] == pos)
                neighborsV.RemoveAt(0);
            else
            {
                label1.Text = "Error in FindNeighbors";
                return realNeighbors;
            }

            foreach (Vector2 v in neighborsV)
            {
                int vIndex = Array.IndexOf(points, v);

                if (placedTiles[vIndex] != null && Vector2.Distance(pos, v) < distanceFromCtoC * 1.5)
                    realNeighbors.Add(vIndex);
            }

            return realNeighbors;
        }
        private void EventsAfterPlacement(Tile placedTile, List<int> neighborIndexies)
        {
            foreach (var index in neighborIndexies)
            {
                Tile neighbor = placedTiles[index];
                int direction = FindDirection(placedTile, neighbor);

                if (direction == -1)
                {
                    label1.Text = "Error in FindDirection";
                    return;
                }

                placedTile.neighbors[direction] = index;

                direction = (direction + 3) % 6;
                neighbor.neighbors[direction] = placedTile.index;

                if (neighbor.gemsInside == null)
                    continue;

                if (neighbor.index == 0)
                {
                    int num = neighbor.gemsInside.Count - 1;
                    gems[num].onPath = direction;

                    var midPoint = (points[0] + points[placedTile.index]) / 2;

                    gems[num].position.X = (int)(midPoint.X - gems[num].width / 2);
                    gems[num].position.Y = (int)(midPoint.Y - gems[num].height / 2);
                }

                List<Gem> temp = new List<Gem>(neighbor.gemsInside);

                foreach (Gem gem in neighbor.gemsInside)
                    if (gem.onPath == direction)
                    {
                        if (neighbor.index > 36 && neighbor.index <= 42)
                        {
                            var midPoint = (points[neighbor.index - 18] + points[neighbor.index]) / 2;

                            gem.position.X = (int)(midPoint.X - gem.width / 2);
                            gem.position.Y = (int)(midPoint.Y - gem.height / 2);
                        }

                        gem.active = true;
                        movingGems.Add(gem);
                        GemTimer.Start();

                        temp.Remove(gem);

                    }

                neighbor.gemsInside = new List<Gem>(temp);
            }



            /*
             add index one to another and reverse
            if is index 0 -> get gem (count, on 6 - blue)
            maybe let gem go until no path (HZ)
             */
        }
        Vector2 Bezier(Vector2 p0, Vector2 p1, Vector2 p2, float t)
        {
            float u = 1 - t;
            return u * u * p0 +
                   2 * u * t * p1 +
                   t * t * p2;
        }
        private int FindDirection(Tile tile, Tile neighbor)
        {
            var deltaX = tile.position.X - neighbor.position.X;
            var deltaY = tile.position.Y - neighbor.position.Y;

            if (deltaX < 0)
            {
                if (deltaY > 0)
                    return 0;
                else if (deltaY < 0)
                    return 2;
                return 1;
            }
            if (deltaX > 0)
            {
                if (deltaY < 0)
                    return 3;
                else if (deltaY > 0)
                    return 5;
                return 4;
            }
            return -1;
        }
        private void RotateTile(Tile tile, bool clockwise)
        {
            int num = 1;
            if (!clockwise)
                num = -1;

            tile.numOfRotation = (6 + tile.numOfRotation + num) % 6;

            if (tile.numOfRotation == 0)
                tile.tilePic = new Bitmap(tile.originalPic);
            else if (tile.numOfRotation == 3)
            {
                tile.tilePic = new Bitmap(tile.originalPic);
                tile.tilePic.RotateFlip(RotateFlipType.Rotate180FlipNone);
            }
            else
            {
                var rotation = 60f;
                if (!clockwise)
                    rotation = -60f;

                tile.tilePic = ImageUtils.RotateHex(tile.tilePic, rotation, tile.width, tile.height);
            }

            int[] temp = new int[6];
            if (clockwise)
                for (int i = 0; i < 6; i++)
                {
                    temp[i] = tile.paths[(i + 5) % 6];
                    if (temp[i] != -1)
                        temp[i] = (temp[i] + 1) % 6;
                }
            else
                for (int i = 0; i < 6; i++)
                {
                    temp[i] = tile.paths[(i + 1) % 6];
                    if (temp[i] != -1)
                        temp[i] = (temp[i] + 5) % 6;
                }

            tile.paths = temp;
        }
        private void BoardMouseDown(object sender, MouseEventArgs e)
        {
            Point mousePosition = new Point(e.X, e.Y);
            foreach (Tile newTile in tiles)
            {
                if (SelectedTile == null)
                {
                    if (newTile.rect.Contains(mousePosition) && newTile.index == -1)
                    {
                        SelectedTile = newTile;
                        newTile.active = true;

                        //indexValue = tiles.IndexOf(newTile);
                        //label1.Text = "Card " + (indexValue + 1) + " of " + totalTiles;
                    }
                }
            }
        }
        private void BoardMouseMove(object sender, MouseEventArgs e)
        {
            if (SelectedTile != null)
            {
                SelectedTile.position.X = e.X - (SelectedTile.width / 2);
                SelectedTile.position.Y = e.Y - (SelectedTile.height / 2);
            }
        }
        private void BoardMouseUp(object sender, MouseEventArgs e)
        {
            Tile temp = null;
            foreach (Tile tempTile in tiles)
            {
                if (tempTile.active)
                {
                    Snap(tempTile);
                    temp = tempTile;
                }
                tempTile.active = false;
            }
            SelectedTile = null;
            lineAnimation = 0;

            if (temp == null)
                return;

            label1.Text = "Placed " + placedTiles.Count(x => x != null);

            //return;

            label1.Text += "\nIndex: " + temp.index + "\nNeighbors: \n[0, 1, 2, 3, 4, 5] \n[" + temp.paths[0];
            for (int i = 0; i < 5; i++)
                label1.Text += ", " + temp.paths[i + 1];
            label1.Text += "] \n[" + temp.neighbors[0];

            for (int i = 0; i < 5; i++)
                label1.Text += ", " + temp.neighbors[i + 1];
            label1.Text += ']';

            temp = placedTiles[0];
            label1.Text += "\n\nIndex: " + temp.index + "\nNeighbors: \n[  0,  1,  2,  3,  4,  5]    \n[" + temp.neighbors[0];
            for (int i = 0; i < 5; i++)
                label1.Text += ", " + temp.neighbors[i + 1];
            label1.Text += ']';
        }
        private void FormTimerEvent(object sender, EventArgs e)
        {
            foreach (Tile tile in tiles)
            {
                tile.rect.X = tile.position.X;
                tile.rect.Y = tile.position.Y + tile.height / 8;
            }
            if (SelectedTile != null)
            {
                if (lineAnimation < 5)
                {
                    lineAnimation++;
                }
            }
            Board.Invalidate();
        }
        private void GemTimerEvent(object sender, EventArgs e)
        {
            if (!movingGems.Any())
            {
                GemTimer.Stop();
                return;
            }
            Gem gem = movingGems[0];

            if (m1.t == 0f)
            {
                Tile currentTile = placedTiles[gem.onTile];
                int neighborIndex = currentTile.neighbors[gem.onPath];
                Tile nextTile = placedTiles[neighborIndex];                //mult in fiture - error out of range

                int enteringBy = (gem.onPath + 3) % 6;               //NOT HZ WHY BUT FOR SOME RESON WHEN X2-X3 - ERROR OUT OF INDEX
                m1.willExitBy = nextTile.paths[enteringBy];

                Vector2 currentPoint = points[currentTile.index];
                Vector2 middlePoint = points[nextTile.index];

                Vector2 startPoint = (currentPoint + middlePoint) / 2;

                var x_1 = middlePoint.X + distanceFromCtoC / 2 * (float)Math.Cos((m1.willExitBy - 1) * 60 * Math.PI / 180f);
                var y_1 = middlePoint.Y + distanceFromCtoC / 2 * (float)Math.Sin((m1.willExitBy - 1) * 60 * Math.PI / 180f);

                Vector2 endPoint = new Vector2(x_1, y_1);

                int diff = Math.Abs(enteringBy - m1.willExitBy);
                if (diff > 3)
                    diff = 6 - diff;

                if (diff == 1)
                {
                    Vector2 mid = (startPoint + endPoint) / 2;
                    Vector2 direction = Vector2.Normalize(mid - middlePoint);

                    middlePoint += direction * 20f;
                    m1.speed = 1.5f;                     //To Do speed
                }

                m1.startPoint = startPoint;
                m1.middlePoint = middlePoint;
                m1.endPoint = endPoint;
                m1.nextTile = nextTile;
                m1.diff = diff;
            }

            if (m1.t > 1f)                          // where reached the end of read
            {
                gem.onPath = m1.willExitBy;
                gem.onTile = m1.nextTile.index;

                int anotherTile = m1.nextTile.neighbors[gem.onPath];

                Vector2 anotherPoint = m1.endPoint;

                if (anotherTile == -1)              // no further road
                {
                    gem.active = false;
                    movingGems.Remove(gem);
                    m1.nextTile.gemsInside.Add(gem);
                }
                else
                    anotherPoint = (points[gem.onTile] + points[anotherTile]) / 2f;


                gem.position.X = (int)(anotherPoint.X - gem.width / 2);
                gem.position.Y = (int)(anotherPoint.Y - gem.height / 2);

                m1.t = 0f;
                m1.speed = 1f;

                List<Gem> activeGems = gems.Where(g => g.active == true).ToList();
                activeGems.Remove(gem);

                if (activeGems.Any())
                    foreach (var anotherGem in activeGems)
                        if (gem.position == anotherGem.position)
                        {
                            gems.Remove(anotherGem);
                            gems.Remove(gem);
                            movingGems.Remove(anotherGem);
                            movingGems.Remove(gem);
                            return;
                        }

                return;
            }

            m1.t += 0.04f * m1.speed;

            var currentPosition = Bezier(m1.startPoint, m1.middlePoint, m1.endPoint, m1.t);

            gem.position.X = (int)currentPosition.X - gem.width / 2;
            if (m1.diff != 3 || m1.willExitBy % 3 != 1)
                gem.position.Y = (int)currentPosition.Y - gem.height / 2;

            Invalidate();
        }
        private void Board_Paint(object sender, PaintEventArgs e)
        {
            var graphics = e.Graphics;
            graphics.DrawImage(
                boardImage.BoardPic,
                boardImage.position.X,
                boardImage.position.Y,
                boardImage.width,
                boardImage.height
            );

            foreach (Tile card in tiles)
            {
                graphics.DrawImage(card.tilePic, card.position.X, card.position.Y, card.width, card.height);
                Pen outline;
                if (card.active)
                {
                    outline = new Pen(Color.Maroon, lineAnimation);
                }
                else
                {
                    outline = new Pen(Color.Transparent, 1);
                }
                graphics.DrawRectangle(outline, card.rect);
            }

            if (debugMode)
            {
                Brush[] brushes = [Brushes.Green, Brushes.Red, Brushes.Blue, Brushes.Yellow, Brushes.Magenta, Brushes.DarkBlue];
                graphics.FillRectangle(Brushes.Black, points[0].X, points[0].Y, 5, 5);


                for (int i = 1; i < points.Count(); i++)
                {
                    Vector2 p = points[i];

                    graphics.FillRectangle(
                        brushes[i % 6],
                        p.X,
                        p.Y,
                        5,
                        5
                    );

                    graphics.DrawString(
                        i.ToString(),
                        Font,
                        Brushes.Black,
                        p.X + 12,
                        p.Y
                    );

                }

                graphics.FillRectangle(
                    Brushes.Cyan,
                    m1.startPoint.X - 5,
                    m1.startPoint.Y - 5,
                    10,
                    10
                );
                graphics.FillRectangle(
                        Brushes.DarkGray,
                        m1.middlePoint.X - 5,
                        m1.middlePoint.Y - 5,
                        10,
                        10
                    );
                graphics.FillRectangle(
                        Brushes.Orange,
                        m1.endPoint.X - 5,
                        m1.endPoint.Y - 5,
                        10,
                        10
                    );

                //Get the middle of the panel
                var x_0 = boardImage.position.X + boardImage.width / 2 + offsetX;
                var y_0 = boardImage.position.Y + boardImage.height / 2 + offsetY;
                //var x_0 = boardImage.position.X + boardImage.width / 2 - 6;
                //var y_0 = boardImage.height / 2 + 3;

                var shape = new PointF[6];

                var r = 0.1f * Board.Width / 2 + 5;
                //r *= (float)Math.Cos(30 * Math.PI / 180f);

                //Create 6 points
                for (int a = 0; a < 6; a++)
                {
                    shape[a] = new PointF(
                        x_0 + r * (float)Math.Sin(a * 60 * Math.PI / 180f),
                        y_0 + r * (float)Math.Cos(a * 60 * Math.PI / 180f)
                        );
                }


                graphics.DrawPolygon(Pens.Red, shape);
            }

            for (int i = 0; i < gems.Count(); i++)
            {
                graphics.DrawImage(gems[i].gemPic, gems[i].position.X, gems[i].position.Y, gems[i].width, gems[i].height);
                if (debugMode)
                    graphics.DrawString(i.ToString(), Font, Brushes.Red, gems[i].position.X + 25, gems[i].position.Y);
            }

            if (SelectedTile != null)
            {
                graphics.DrawImage(SelectedTile.tilePic, SelectedTile.position.X, SelectedTile.position.Y, SelectedTile.width, SelectedTile.height);
            }

            return;

            //foreach (Gem gem in gems)
            //{
            //    graphics.DrawImage(gem.gemPic, gem.position.X, gem.position.Y, gem.width, gem.height);
            //}


            //Vector2 startPoint = Vector2.Lerp(points[1], points[13], 0.5f);
            //Vector2 endPoint = Vector2.Lerp(points[13], points[8], 0.5f);
            //Vector2 center = points[2];
            //float radius = distanceFromCtoC;
            //RectangleF rect = new RectangleF(
            //    center.X - radius,
            //    center.Y - radius,
            //    radius * 2,
            //    radius * 2);

            //Vector2 dir = startPoint - center;
            //float startAngle = MathF.Atan2(dir.Y, dir.X) * 180f / MathF.PI;
            //float sweepAngle = 60f;

            //using (Pen pen = new Pen(Color.Red, 2))
            //{
            //    graphics.DrawArc(pen, rect, startAngle, sweepAngle);
            //}


            //Vector2 startPoint = Vector2.Lerp(points[1], points[13], 0.5f);
            //float startAngle = MathF.Atan2(
            //    startPoint.Y - points[2].Y,
            //    startPoint.X - points[2].X
            //);
            //e.Graphics.DrawArc(
            //    Pens.Blue,
            //    startPoint.Y,
            //    startPoint.Y,
            //    distanceFromCtoC * 2,
            //    distanceFromCtoC * 2,
            //    startAngle * 180f / MathF.PI,
            //    6f * 60f
            //);
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (SelectedTile == null)
                return;

            label2.Text = "Rotation: \nIndex: " + SelectedTile.index + "\nRotation num: " + SelectedTile.numOfRotation + "\nPaths: \n[" + SelectedTile.paths[0];
            for (int i = 0; i < 5; i++)
                label2.Text += ", " + SelectedTile.paths[i + 1];
            label2.Text += "]";

            switch (e.KeyCode)
            {
                case Keys.A:
                case Keys.Left:
                    RotateTile(SelectedTile, false);
                    break;
                case Keys.D:
                case Keys.Right:
                    RotateTile(SelectedTile, true);
                    break;
                default:
                    break;
            }

            label2.Text += "\n\nRotation num: " + SelectedTile.numOfRotation + "\nPaths: \n[" + SelectedTile.paths[0];
            for (int i = 0; i < 5; i++)
                label2.Text += ", " + SelectedTile.paths[i + 1];
            label2.Text += "]";

        }
        private void debugButton_Click(object sender, EventArgs e)
        {
            debugMode = !debugMode;
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            var graphics = e.Graphics;

            //Get the middle of the panel
            var x_0 = panel1.Width / 2;
            var y_0 = panel1.Height / 2;

            var shape = new PointF[6];

            var r = 70; //70 px radius 

            //Create 6 points
            for (int a = 0; a < 6; a++)
            {
                shape[a] = new PointF(
                    x_0 + r * (float)Math.Sin(a * 60 * Math.PI / 180f),
                    y_0 + r * (float)Math.Cos(a * 60 * Math.PI / 180f)
                    );
            }


            graphics.DrawPolygon(Pens.Red, shape);
        }       //useless

        private void TwoPlayersButton_Click(object sender, EventArgs e)
        {

        }
    }
}

