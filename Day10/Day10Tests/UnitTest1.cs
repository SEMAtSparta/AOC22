using Day10;
namespace Day10Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
        
    }
    [Test]
    public void ValueOfRegister_After20Cycles()
    {
        string[] inputStrings =
        {
            "addx 15",
            "addx -11",
            "addx 6",
            "addx -3",
            "addx 5",
            "addx -1",
            "addx -8",
            "addx 13",
            "addx 4",
            "noop",
            "addx -1",
            "addx 5",
            "addx -1"
        };

        int[] checkCycles = { 20 };
        int[] result = Program.ValuesOfRegisterAfterSeriesOfCycles(checkCycles, inputStrings);

        Assert.That(result[0], Is.EqualTo(420));
    }
}