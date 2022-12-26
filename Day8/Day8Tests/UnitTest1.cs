using Day8;

namespace Day8Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void GivenStringArray_Return2DIntArray()
    {
        string[] input = {
            "30373",
            "25512",
            "65332",
            "33549",
            "35390"
        };

        List<int[]> ints = Program.stringArrayTo2DInts(input);
        Assert.That(ints[0][2], Is.EqualTo(3));
        Assert.That(ints[3][1], Is.EqualTo(5));


        
    }

    [Test] 
    public void Test2() 
    {
        int result = FindVisibleTrees();
        Assert.Pass();
    }
}