using Day6;
namespace ParseStringTests;

public class Tests
{
    [TestCase("bvwbjplbgvbhsrlpgdmjqwftvncz",4, 5)]
    [TestCase("nppdvjthqldpwncqszvftbrmjlhg",4, 6)]
    [TestCase("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg",4, 10)]
    [TestCase("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw",4, 11)]
    [TestCase("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 14, 19)]
    [TestCase("bvwbjplbgvbhsrlpgdmjqwftvncz", 14, 23)]
    [TestCase("nppdvjthqldpwncqszvftbrmjlhg", 14, 23)]
    [TestCase("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 14, 29)]
    [TestCase("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 14, 26)]

    

    public void GivenString_FindStartOfPacketMarker_ReturnsPosition(string input, int sizeOfMarker, int expectedResult)
    {
        Assert.That(Program.FindStartOfPacketMarker(input, sizeOfMarker), Is.EqualTo(expectedResult));
    }
}