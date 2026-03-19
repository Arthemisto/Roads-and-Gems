using System.Numerics;

namespace Indigo.Tests;

[TestClass]
public sealed class GameBoardLogicTests
{
    [TestMethod]
    public void CreateGatewayOwners_ForTwoPlayers_ReturnsAlternatingPairs()
    {
        List<int[]> owners = GameBoardLogic.CreateGatewayOwners(2);

        Assert.AreEqual(6, owners.Count);
        CollectionAssert.AreEqual(new[] { 0, 0 }, owners[0]);
        CollectionAssert.AreEqual(new[] { 1, 1 }, owners[1]);
        CollectionAssert.AreEqual(new[] { 0, 0 }, owners[2]);
        CollectionAssert.AreEqual(new[] { 1, 1 }, owners[3]);
        CollectionAssert.AreEqual(new[] { 0, 0 }, owners[4]);
        CollectionAssert.AreEqual(new[] { 1, 1 }, owners[5]);
    }

    [TestMethod]
    public void CreateGatewayOwners_ForUnsupportedPlayerCount_Throws()
    {
        Assert.ThrowsException<InvalidOperationException>(() => GameBoardLogic.CreateGatewayOwners(5));
    }

    [TestMethod]
    public void CreateHexGrid_ForFiveRings_ReturnsExpectedPointCount()
    {
        Vector2[] grid = GameBoardLogic.CreateHexGrid(new Vector2(100, 100), 5, 10);

        Assert.AreEqual(61, grid.Length);
    }

    [TestMethod]
    public void CreateHexGrid_ForTwoRings_PlacesSixPointsAroundCenter()
    {
        Vector2 center = new(50, 50);
        Vector2[] grid = GameBoardLogic.CreateHexGrid(center, 2, 10);

        Assert.AreEqual(7, grid.Length);
        Assert.AreEqual(center, grid[0]);
        Assert.IsTrue(grid.Skip(1).All(point => Math.Abs(Vector2.Distance(center, point) - 10f) < 0.001f));
    }
}
