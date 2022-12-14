using Day2;
namespace RPSTests;

public class Tests
{

    [TestCase("A","X",4)]
    [TestCase("A","Y",8)]
    [TestCase("A","Z",3)]
    [TestCase("B","X", 1)]
    [TestCase("B","Y", 5)]
    [TestCase("B","Z", 9)]
    [TestCase("C", "X", 7)]
    [TestCase("C", "Y", 2)]
    [TestCase("C", "Z", 6)]

    public void GivenInputs_RPSMatch_ReturnsExpectedValues(string p1Choice, string p2Choice, int expectedValue)
    {
        Assert.That(Program.RPSMatch(p1Choice, p2Choice), Is.EqualTo(expectedValue));
    }
}