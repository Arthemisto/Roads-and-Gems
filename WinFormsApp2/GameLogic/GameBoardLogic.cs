using System.Numerics;

namespace Indigo
{
    public static class GameBoardLogic
    {
        public static List<int[]> CreateGatewayOwners(int playerCount)
        {
            return playerCount switch
            {
                2 =>
                [
                    [0, 0],
                    [1, 1],
                    [0, 0],
                    [1, 1],
                    [0, 0],
                    [1, 1]
                ],
                3 =>
                [
                    [0, 0],
                    [0, 1],
                    [2, 2],
                    [2, 0],
                    [1, 1],
                    [1, 2]
                ],
                4 =>
                [
                    [0, 1],
                    [1, 2],
                    [0, 3],
                    [3, 1],
                    [2, 0],
                    [2, 3]
                ],
                _ => throw new InvalidOperationException($"Unsupported player count: {playerCount}")
            };
        }

        public static Vector2[] CreateHexGrid(Vector2 center, int totalNumOfRings, float originalR)
        {
            var points = new List<Vector2> { center };
            var r = originalR;

            for (int ring = 1; ring < totalNumOfRings; ring++)
            {
                for (int a = 0; a < 6; a++)
                {
                    var x = center.X + r * (float)Math.Cos(a * 60 * Math.PI / 180f);
                    var y = center.Y + r * (float)Math.Sin(a * 60 * Math.PI / 180f);
                    points.Add(new Vector2(x, y));
                }

                r += originalR;

                if (totalNumOfRings < 3 || ring == 1)
                    continue;

                int first = points.Count - 6;

                for (int i = 0; i < 5; i++)
                    for (int j = 1; j < ring; j++)
                        points.Add(Vector2.Lerp(points[first + i], points[first + i + 1], (float)j / ring));

                for (int j = 1; j < ring; j++)
                    points.Add(Vector2.Lerp(points[first + 5], points[first], (float)j / ring));
            }

            return points.ToArray();
        }
    }
}
