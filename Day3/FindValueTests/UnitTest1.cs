using Day3;
namespace FindValueTests;

public class Tests
{
    [TestCase('a', 1)]
    [TestCase('p', 16)]
    [TestCase('z', 26)]
    [TestCase('A', 27)]
    [TestCase('L', 38)]
    [TestCase('Z', 52)]
    
    public void GivenChar_FindValueOfItem_ReturnsCorrectValue(char c, int expectedValue)
    {
        Assert.That(Program.FindValueOfItem(c), Is.EqualTo(expectedValue));
    }

    [TestCase()]

    public void GivenGroupOfRucksacks_FindValueOfDuplicateInGroup_ReturnsCorrectValue(string[] stringArray, int expectedValue)
    {

    }
}