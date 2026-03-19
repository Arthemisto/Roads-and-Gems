namespace Indigo.Tests;

[TestClass]
public sealed class OnlineLobbyLogicTests
{
    [TestMethod]
    public void MapPlayerColors_UsesLastLetterSuffixes()
    {
        List<string> colors = OnlineLobbyLogic.MapPlayerColors(["Alice C", "Bob P", "Cara R", "Dana W"]);

        CollectionAssert.AreEqual(new[] { "Cyan", "Purple", "Red", "White" }, colors);
    }

    [TestMethod]
    public void MapPlayerColors_DefaultsToWhiteForUnknownOrEmptyNames()
    {
        List<string> colors = OnlineLobbyLogic.MapPlayerColors(["", "NoSuffix", null]);

        CollectionAssert.AreEqual(new[] { "White", "White", "White" }, colors);
    }

    [TestMethod]
    public void TryParseEndpoint_ParsesHttpStyleEndpoint()
    {
        bool result = OnlineLobbyLogic.TryParseEndpoint("http://127.0.0.1:4040", out string ip, out int port);

        Assert.IsTrue(result);
        Assert.AreEqual("127.0.0.1", ip);
        Assert.AreEqual(4040, port);
    }

    [TestMethod]
    public void TryParseEndpoint_RejectsMissingOrInvalidPorts()
    {
        Assert.IsFalse(OnlineLobbyLogic.TryParseEndpoint("127.0.0.1", out _, out _));
        Assert.IsFalse(OnlineLobbyLogic.TryParseEndpoint("127.0.0.1:80", out _, out _));
        Assert.IsFalse(OnlineLobbyLogic.TryParseEndpoint("127.0.0.1:not-a-port", out _, out _));
    }
}
