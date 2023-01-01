using Day9;
namespace Day9Tests;

public class Tests
{
    Knot knot;
    Knot largeKnot;
    List<Vector2> instructions;
    List<Vector2> largeInstructions;
    [SetUp]
    public void Setup()
    {
        knot = new(1);
        largeKnot = new(9);
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
        largeInstructions = new()
        {
            new Vector2('R',5),
            new Vector2('U',8),
            new Vector2('L',8),
            new Vector2('D',3),
            new Vector2('R',17),
            new Vector2('D',10),
            new Vector2('L',25),
            new Vector2('U',20)
        };
    }

    [Test]
    public void GivenInstructions_FindsVisitedPositions()
    {

        HashSet<Vector2> visitedPositions = Program.Simulate(knot, instructions);
        int result = visitedPositions.Count;

        Assert.That(result, Is.EqualTo(13));
    }

    [Test]
    public void GivenInstructions_LongerRope_FindsVisitedPositions()
    {

        HashSet<Vector2> visitedPositions = Program.Simulate(largeKnot, instructions);
        int result = visitedPositions.Count;

        Assert.That(result, Is.EqualTo(1));
    }

    [Test]
    public void GivenLargerInstructions_LongerRope_FindsVisitedPositions()
    {

        HashSet<Vector2> visitedPositions = Program.Simulate(largeKnot, largeInstructions);
        int result = visitedPositions.Count;

        Assert.That(result, Is.EqualTo(36));
    }
}