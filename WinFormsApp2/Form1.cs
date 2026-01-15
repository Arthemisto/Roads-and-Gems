using System.Numerics;

namespace Indigo
{
    public partial class Form1 : Form
    {
        List<Tile> tiles = new List<Tile>();
        List<Gem> gems = new List<Gem>();                           // lists of main objects
        List<PlayerToken> playerTokens = new List<PlayerToken>();

        Vector2[] points;
        List<int> picNumbers = new List<int>();
        Tile[] placedTiles;
        List<Gem> movingGems = new List<Gem>();                     // additional lists of objects
        List<float> playersPoints = new List<float>();

        BoardImage boardImage;
        Tile SelectedTile;                                          // important objects
        Movement m1 = new Movement();

        int rings = 5;
        int totalTiles = 0;
        int totalGems = 12;                                         // game parameters
        float distanceFromCtoC = 10000;
        int numOfPlayers = 0;
        
        int xPos = 50;
        int tileNumber = -1;                                        // tile creation and movement visuals
        int lineAnimation = 0;

        int widthOffset = 20;
        int heightOffset = 5;                                       // persition by eye

        bool debugMode = false;
        bool hideMode = false;                                      // modes
        public Form1()
        {
            InitializeComponent();

            placedTiles = new Tile[3 * rings * rings - 3 * rings + 1];

            sizeAdjustments();

            boardImage = new BoardImage();
            boardImage.position.X = 40 + Tile.width;
            boardImage.position.Y = 25;

            distanceFromCtoC = Tile.width;

            var centerX = boardImage.position.X + BoardImage.width / 2;
            var centerY = boardImage.position.Y + BoardImage.height / 2;
            Vector2 center = new Vector2(centerX, centerY);

            points = CreateHexGrid(center, rings, distanceFromCtoC);

            SetUpApp();
        }
        private void sizeAdjustments()
        {
            BoardImage.width += widthOffset;
            BoardImage.height += heightOffset;

            float percent = 0.9f;        // Hardcoded

            BoardImage.width = (int)(BoardImage.width * percent);
            BoardImage.height = (int)(BoardImage.height * percent);

            Tile.width = (int)(Tile.width * percent);
            Tile.height = (int)(Tile.height * percent);

            Gem.width = (int)(Gem.width * percent);
            Gem.height = (int)(Gem.height * percent);

            PlayerToken.width = (int)(PlayerToken.width * percent);
            PlayerToken.height = (int)(PlayerToken.height * percent);
        }
        private void SetUpApp()
        {
            for (int i = 0; i < 7; i++)
                picNumbers.Add(i);

            List<int> temp = new List<int>(picNumbers);

            temp.Remove(0);
            temp.Remove(1);     // center and edge

            for (int i = 0; i < 5; i++)
            {
                picNumbers.Insert(1, 1);        // add edge
                picNumbers.AddRange(temp);      // add 5 normal tiles i times
            }

            temp.Remove(2);
            temp.Remove(5);     // goBack and overlap

            
            for (int i = 0; i < 14 - 6; i++)                     
                picNumbers.AddRange(temp);
            
            totalTiles = picNumbers.Count;
            for (int i = 0; i < totalTiles; i++)
                MakeTiles();

            picNumbers.Clear();

            for (int i = 0; i < 3; i++)
                picNumbers.Add(i);

            for (int i = 0; i < 4; i++)
            {
                picNumbers.Insert(1, picNumbers[1]);

                int lasGem = picNumbers.Count - 1;
                picNumbers.Insert(lasGem, picNumbers[lasGem]);
            }
            picNumbers.Insert(7, picNumbers[7]);

            for (int i = 0; i < totalGems; i++)
                MakeGems(i);

            debugLabel1.Text = "Card " + (tileNumber + 1) + " of " + totalTiles + " Offset: " + (0.1f * BoardImage.width + 5);
        }

        private void MakeTiles()
        {
            tileNumber++;
            Tile newTile = new Tile(picNumbers[tileNumber]);
            int placedIndex = -1;

            int x_1;
            int y_1;

            if (newTile.name == "Center")
            {
                x_1 = (int)(points[0].X - Tile.width / 2f);
                y_1 = (int)(points[0].Y - Tile.height / 2f);
                newTile.index = 0;
                placedIndex = 0;
            }
            else if (newTile.name == "Edge")
            {
                int[] temp = new int[6];
                int p = 37 + (tileNumber + 2) % 6;
                newTile.index = p;
                placedIndex = p;

                x_1 = (int)(points[p].X - Tile.width / 2f);
                y_1 = (int)(points[p].Y - Tile.height / 2f);

                if (tileNumber > 1)
                {
                    newTile.tilePic = ImageUtils.RotateHex(newTile.tilePic, 60f * (tileNumber - 1), Tile.width, Tile.height);

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
                //x_1 = (picNumbers[tileNumber] - 2) * 200 + xPos;
                //y_1 = BoardImage.height + boardImage.position.Y + 15;

                x_1 = 20;
                y_1 = (picNumbers[tileNumber] - 2) * 200 + xPos;

                if (tileNumber < 37 && (tileNumber - 7) % 6 == 0)
                    xPos -= 5;

                if (tileNumber >= 37 && (tileNumber - 7) % 4 == 0)
                    xPos -= 5;
            }

            newTile.position.X = x_1;
            newTile.position.Y = y_1;
            newTile.rect.X = newTile.position.X;
            newTile.rect.Y = newTile.position.Y + Tile.height / 8;

            tiles.Add(newTile);
            if (placedIndex != -1)
                placedTiles[placedIndex] = newTile;
        }
        private void MakeGems(int gemNumber)
        {
            Gem newGem = new Gem(picNumbers[gemNumber]);
            int x_1 = 0;
            int y_1 = 0;
            if (newGem.name == "Blue")
            {
                newGem.onTile = 0;
                x_1 = (int)(points[0].X - Gem.width / 2f);
                y_1 = (int)(points[0].Y - Gem.height / 2f);
            }
            else if (newGem.name == "Green")
            {
                newGem.onTile = 0;
                x_1 = (int)(points[0].X - Gem.width / 2f + 30 * (float)Math.Cos(gemNumber * 72 * Math.PI / 180f));
                y_1 = (int)(points[0].Y - Gem.height / 2f + 30 * (float)Math.Sin(gemNumber * 72 * Math.PI / 180f));
            }
            else if (newGem.name == "Yellow")
            {
                newGem.onTile = 37 + gemNumber % 6;
                newGem.onPath = (4 + gemNumber % 6) % 6;

                Vector2 v_1 = Vector2.Lerp(points[newGem.onTile], points[newGem.onTile - 18], 1 / 4f);
                x_1 = (int)(v_1.X - Gem.width / 2f);
                y_1 = (int)(v_1.Y - Gem.height / 2f);
            }
            newGem.position.X = x_1;
            newGem.position.Y = y_1;

            gems.Add(newGem);
            placedTiles[newGem.onTile].gemsInside.Add(newGem);
        }
        private void MakeTokens(List<string> colors)
        {
            //will break if numOfPlayers = 3 and 4

            int numOfPlayers = colors.Count;

            if (numOfPlayers == 2)
            {
                for (int i = 0; i < 6; i++)
                {
                    int playerNumber = i % numOfPlayers;
                    PlayerToken token = new PlayerToken(playerNumber, colors[playerNumber]);
                    playerTokens.Add(token);

                    token = new PlayerToken(playerNumber, colors[playerNumber]);
                    playerTokens.Add(token);
                }
            }

            var r = Tile.height / 2 * 3 - 5;
            var numOfRotations = 7;

            for (int i = 0; i < playerTokens.Count; i++)
            {
                playerTokens[i].position.X = (int)(points[25 + i].X + r * (float)Math.Sin(numOfRotations * 60 * Math.PI / 180f) - PlayerToken.width / 2);
                playerTokens[i].position.Y = (int)(points[25 + i].Y + r * (float)Math.Cos(numOfRotations * 60 * Math.PI / 180f) - PlayerToken.width / 2);

                playerTokens[i + 1].position.X = (int)(points[26 + i].X + r * (float)Math.Sin(numOfRotations * 60 * Math.PI / 180f) - PlayerToken.width / 2);
                playerTokens[i + 1].position.Y = (int)(points[26 + i].Y + r * (float)Math.Cos(numOfRotations * 60 * Math.PI / 180f)  - PlayerToken.width / 2);

                numOfRotations--;
                i++;
            }
        }
        public Vector2[] CreateHexGrid(Vector2 center, int totalNumOfRings, float originalR)
        {
            var points = new List<Vector2> { center };
            var r = originalR;

            for (int ring = 1; ring < totalNumOfRings; ring++)
            {
                for (int a = 0; a < 6; a++)
                {
                    var x_1 = center.X + r * (float)Math.Cos(a * 60 * Math.PI / 180f);
                    var y_1 = center.Y + r * (float)Math.Sin(a * 60 * Math.PI / 180f);
                    points.Add(new Vector2(x_1, y_1));
                }
                r += originalR;

                if (totalNumOfRings < 3 || ring == 1)
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

            if (Vector2.Distance(v1, closestPoint) > distanceFromCtoC / 2)
                return -1;

            int index = Array.IndexOf(points, closestPoint);

            return index;
        }
        private bool BorderApproved(Tile tile, int index)
        {
            if (index % 3 != 0)
                index += 3 - index % 3;

            int pathToBorder = ((index - 45) / 3 + 1) % 6;
            bool shortPathPlacedToBorder = tile.paths[pathToBorder] == (pathToBorder + 1) % 6;

            if (shortPathPlacedToBorder)
                return false;
            else
                return true;
        }
        private List<int> FindNeighbors(Vector2 pos)
        {
            List<Vector2> neighborsV = points.OrderBy(v => Vector2.Distance(pos, v)).Take(7).ToList();
            List<int> realNeighbors = new List<int>();

            if (neighborsV[0] == pos)
                neighborsV.RemoveAt(0);
            else
            {
                debugLabel1.Text = "Error in FindNeighbors";
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
        private void Snap(Tile tile)
        {
            Vector2 pos = new Vector2(tile.position.X + Tile.width / 2, tile.position.Y + Tile.height / 2);
            int index = GetClosestIndex(pos);

            if (index < 0 || placedTiles[index] != null)
                return;

            if (index >= 43)
                if (!BorderApproved(tile, index))
                    return;

            tile.index = index;
            Vector2 new_pos = points[index];

            int newX = (int)(new_pos.X - Tile.width / 2f);
            int newY = (int)(new_pos.Y - Tile.height / 2f);

            tile.position = new Point(newX, newY);
            placedTiles[index] = tile;

            List<int> neighborIndexies = FindNeighbors(new_pos);
            if (!neighborIndexies.Any())
                return;

            EventsAfterPlacement(tile, neighborIndexies);
        }
        private void EventsAfterPlacement(Tile placedTile, List<int> neighborIndexies)
        {
            foreach (var index in neighborIndexies)
            {
                Tile neighbor = placedTiles[index];
                int direction = FindDirection(placedTile, neighbor);

                if (direction == -1)
                {
                    debugLabel1.Text = "Error in FindDirection";
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

                    gems[num].position.X = (int)(midPoint.X - Gem.width / 2);
                    gems[num].position.Y = (int)(midPoint.Y - Gem.height / 2);
                }

                List<Gem> temp = new List<Gem>(neighbor.gemsInside);

                foreach (Gem gem in neighbor.gemsInside)
                    if (gem.onPath == direction)
                    {
                        if (neighbor.index > 36 && neighbor.index <= 42)
                        {
                            var midPoint = (points[neighbor.index - 18] + points[neighbor.index]) / 2;

                            gem.position.X = (int)(midPoint.X - Gem.width / 2);
                            gem.position.Y = (int)(midPoint.Y - Gem.height / 2);
                        }

                        gem.active = true;
                        movingGems.Add(gem);
                        GemTimer.Start();

                        temp.Remove(gem);

                    }

                neighbor.gemsInside = new List<Gem>(temp);
            }
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

                tile.tilePic = ImageUtils.RotateHex(tile.tilePic, rotation, Tile.width, Tile.height);
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
        private void ScoreUpdate(Gem gem)
        {
            int tileIndex = gem.onTile;

            if (tileIndex % 3 != 0)
                tileIndex += 3 - tileIndex % 3;

            int border = (tileIndex - 45) / 3 % 6;
            float score;

            if (gem.name == "Blue")
                score = 3;
            else if (gem.name == "Green")
                score = 2;
            else
                score = 1;

            var player1 = playerTokens[border * 2].playerNumber;
            var player2 = playerTokens[border * 2 + 1].playerNumber;

            if (player1 == player2)
                score /= 2;

            var pl = player1;
            for (int i = 0; i < 2; i++)
            {
                playersPoints[pl] += score;
                pl = player2;
            }

            playerScore0.Text = " " + (int)playersPoints[0];
            playerScore1.Text = " " + (int)playersPoints[1];
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (SelectedTile == null)
                return;

            debugLabel2.Text = "Rotation: \nRotation num: " + SelectedTile.numOfRotation + "\nPaths: \n[" + SelectedTile.paths[0];
            for (int i = 0; i < 5; i++)
                debugLabel2.Text += ", " + SelectedTile.paths[i + 1];
            debugLabel2.Text += "]";

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

            debugLabel2.Text += "\n\nRotation num: " + SelectedTile.numOfRotation + "\nPaths: \n[" + SelectedTile.paths[0];
            for (int i = 0; i < 5; i++)
                debugLabel2.Text += ", " + SelectedTile.paths[i + 1];
            debugLabel2.Text += "]";

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
                SelectedTile.position.X = e.X - (Tile.width / 2);
                SelectedTile.position.Y = e.Y - (Tile.height / 2);
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

            debugLabel1.Text = "Placed " + placedTiles.Count(x => x != null);
            debugLabel1.Text += "\nResolution (w by h): " + this.Width + " by " + Height;
            //return;

            debugLabel1.Text += "\nIndex: " + temp.index + "\nNeighbors: \n[0, 1, 2, 3, 4, 5] \n[" + temp.paths[0];
            for (int i = 0; i < 5; i++)
                debugLabel1.Text += ", " + temp.paths[i + 1];
            debugLabel1.Text += "] \n[" + temp.neighbors[0];

            for (int i = 0; i < 5; i++)
                debugLabel1.Text += ", " + temp.neighbors[i + 1];
            debugLabel1.Text += ']';

            temp = placedTiles[0];
            debugLabel1.Text += "\n\nIndex: " + temp.index + "\nNeighbors: \n[  0,  1,  2,  3,  4,  5]    \n[" + temp.neighbors[0];
            for (int i = 0; i < 5; i++)
                debugLabel1.Text += ", " + temp.neighbors[i + 1];
            debugLabel1.Text += ']';
        }

        private void FormTimerEvent(object sender, EventArgs e)
        {
            if (SelectedTile != null)
            {
                SelectedTile.rect.X = SelectedTile.position.X;
                SelectedTile.rect.Y = SelectedTile.position.Y + Tile.height / 8;

                if (lineAnimation < 5)
                    lineAnimation++;
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
                Tile nextTile = placedTiles[neighborIndex];

                int enteringBy = (gem.onPath + 3) % 6;
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
                    m1.speed = 1.5f;
                }

                m1.startPoint = startPoint;
                m1.middlePoint = middlePoint;
                m1.endPoint = endPoint;
                m1.nextTile = nextTile;
                m1.diff = diff;
            }

            if (m1.t > 1f)                          // if reached the end of road
            {
                gem.onPath = m1.willExitBy;
                gem.onTile = m1.nextTile.index;

                int anotherTile = m1.nextTile.neighbors[gem.onPath];

                Vector2 anotherPoint = m1.endPoint;

                if (anotherTile == -1)              // if no further road
                {
                    gem.active = false;
                    movingGems.Remove(gem);
                    m1.nextTile.gemsInside.Add(gem);
                    if (gem.onTile >= 43 && playerTokens.Any())
                        ScoreUpdate(gem);
                }
                else
                    anotherPoint = (points[gem.onTile] + points[anotherTile]) / 2f;


                gem.position.X = (int)(anotherPoint.X - Gem.width / 2);
                gem.position.Y = (int)(anotherPoint.Y - Gem.height / 2);

                m1.t = 0f;
                m1.speed = 1f;

                List<Gem> activeGems = gems.Where(g => g.active == true).ToList();
                activeGems.Remove(gem);

                if (activeGems.Any())               // gem collision
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

            gem.position.X = (int)currentPosition.X - Gem.width / 2;
            if (m1.diff != 3 || m1.willExitBy % 3 != 1)
                gem.position.Y = (int)currentPosition.Y - Gem.height / 2;

            Invalidate();
        }

        private void Board_Paint(object sender, PaintEventArgs e)
        {
            var graphics = e.Graphics;
            graphics.DrawImage(
                boardImage.boardPic,
                boardImage.position.X,
                boardImage.position.Y,
                BoardImage.width,
                BoardImage.height
            );

            if (!hideMode)
            {
                foreach (Tile tile in tiles)
                {
                    graphics.DrawImage(tile.tilePic, tile.position.X, tile.position.Y, Tile.width, Tile.height);
                    Pen outline;
                    if (tile.active)
                    {
                        outline = new Pen(Color.Maroon, lineAnimation);
                    }
                    else
                    {
                        outline = new Pen(Color.Transparent, 1);
                    }
                    graphics.DrawRectangle(outline, tile.rect);
                }

                for (int i = 0; i < playerTokens.Count(); i++)
                {
                    graphics.DrawImage(playerTokens[i].tokenPic, playerTokens[i].position.X, playerTokens[i].position.Y, PlayerToken.width, PlayerToken.height);
                    if (debugMode)
                    {
                        graphics.DrawString(i.ToString(), Font, Brushes.Red, playerTokens[i].position.X + 25, playerTokens[i].position.Y);
                        graphics.FillRectangle(Brushes.Gray, playerTokens[i].position.X - 3, playerTokens[i].position.Y - 3, 6, 6);
                    }

                }

                for (int i = 0; i < gems.Count(); i++)
                {
                    graphics.DrawImage(gems[i].gemPic, gems[i].position.X, gems[i].position.Y, Gem.width, Gem.height);
                    if (debugMode)
                    {
                        graphics.DrawString(i.ToString(), Font, Brushes.Red, gems[i].position.X + 25, gems[i].position.Y);
                        graphics.FillRectangle(Brushes.Gray, gems[i].position.X, gems[i].position.Y, 5, 5);
                    }
                }

                if (SelectedTile != null)
                {
                    graphics.DrawImage(SelectedTile.tilePic, SelectedTile.position.X, SelectedTile.position.Y, Tile.width, Tile.height);
                }
            }

            if (debugMode)
            {
                Brush[] brushes = [Brushes.Green, Brushes.Red, Brushes.Blue, Brushes.Yellow, Brushes.Magenta, Brushes.DarkBlue];

                var shape = new PointF[6];
                var r = Tile.height / 2;
                int pointOffset = 2;

                graphics.FillRectangle(Brushes.Black, points[0].X, points[0].Y, 5, 5);
                for (int a = 0; a < 6; a++)
                {
                    shape[a] = new PointF(
                        points[0].X + r * (float)Math.Sin(a * 60 * Math.PI / 180f),
                        points[0].Y + r * (float)Math.Cos(a * 60 * Math.PI / 180f)
                        );
                }
                graphics.DrawPolygon(Pens.Red, shape);

                for (int i = 1; i < points.Count(); i++)
                {
                    Vector2 p = points[i];

                    graphics.FillRectangle(
                        brushes[i % 6],
                        p.X - pointOffset,
                        p.Y - pointOffset,
                        pointOffset * 2,
                        pointOffset * 2
                    );

                    graphics.DrawString(
                        i.ToString(),
                        Font,
                        Brushes.Black,
                        p.X + 12,
                        p.Y
                    );


                    for (int a = 0; a < 6; a++)
                    {
                        shape[a] = new PointF(
                            p.X + r * (float)Math.Sin(a * 60 * Math.PI / 180f),
                            p.Y + r * (float)Math.Cos(a * 60 * Math.PI / 180f)
                            );
                    }

                    graphics.DrawPolygon(Pens.Red, shape);
                }
                

                pointOffset = 5;
                graphics.FillRectangle(
                    Brushes.Cyan,
                    m1.startPoint.X - pointOffset,
                    m1.startPoint.Y - pointOffset,
                    pointOffset * 2,
                    pointOffset * 2
                );
                graphics.FillRectangle(
                        Brushes.DarkGray,
                        m1.middlePoint.X - pointOffset,
                        m1.middlePoint.Y - pointOffset,
                        pointOffset * 2,
                        pointOffset * 2
                    );
                graphics.FillRectangle(
                        Brushes.Orange,
                        m1.endPoint.X - pointOffset,
                        m1.endPoint.Y - pointOffset,
                        pointOffset * 2,
                        pointOffset * 2
                    );

                graphics.DrawPolygon(Pens.Red, shape);
            }

            return;
        }
        private void Panel1_Paint(object sender, PaintEventArgs e)
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
        }       //unused

        private void DebugButton_Click(object sender, EventArgs e)
        {
            debugMode = !debugMode;

            debugLabel1.Visible = debugMode;
            debugLabel2.Visible = debugMode;

            label2.Visible = !debugMode;
            player0.Visible = !debugMode;
            palyer1.Visible = !debugMode;
            playerScore0.Visible = !debugMode;
            playerScore1.Visible = !debugMode;
        }
        private void HideTilesButton_Click(object sender, EventArgs e)
        {
            hideMode = !hideMode;
        }
        private void TwoPlayersButton_Click(object sender, EventArgs e)
        {
            if (numOfPlayers != 0)
                return;
            
            numOfPlayers = 2;
            using (var form = new PlayerForm(numOfPlayers))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    MakeTokens(form.playerColors);
                    
                    player0.Image = playerTokens[0].tokenPic;
                    playersPoints.Add(0);
                    playerScore0.Text = " 0";

                    palyer1.Image = playerTokens[0 + numOfPlayers].tokenPic;
                    playersPoints.Add(0);
                    playerScore1.Text = " 0";

                    foreach (var gem in gems.Where(g => g.onTile >= 43))
                        ScoreUpdate(gem);
                }

            }
        }
        
    }
}

