using Day6;
namespace ParseStringTests;

public class Tests
{
    [TestCase("bvwbjplbgvbhsrlpgdmjqwftvncz", 5)]
    [TestCase("nppdvjthqldpwncqszvftbrmjlhg", 6)]
    [TestCase("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 10)]
    [TestCase("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 11)]

    public void GivenString_FindStartOfPacketMarker_ReturnsPosition(string input, int expectedResult)
    {
        Assert.That(Program.FindStartOfPacketMarker(input), Is.EqualTo(expectedResult));
    }
}