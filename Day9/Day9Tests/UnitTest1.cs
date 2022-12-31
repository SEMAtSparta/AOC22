using Day9;
namespace Day9Tests;

public class Tests
{
    Knot knot;
    List<Vector2> instructions;
    [SetUp]
    public void Setup()
    {
        knot = new();
        instructions = new()
        {
            new Vector2('R',4),
            new Vector2('U',4),
            new Vector2('L',3),
            new Vector2('D',1),
            new Vector2('R',4),
            new Vector2('D',1),
            new Vector2('L',5),
            new Vector2('R',2)
        };
    }

    [Test]
    public void GivenInstructions_FindsVisitedPositions()
    {

        List<Vector2> visitedPositions = Program.Simulate(knot, instructions);
        int result = visitedPositions.Count;

        Assert.That(result, Is.EqualTo(13));
    }
}