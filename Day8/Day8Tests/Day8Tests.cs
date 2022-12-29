using Day8;

namespace Day8Tests;

public class Tests
{
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

        List<int[]> ints = Program.StringArrayTo2DInts(input);
        Assert.That(ints[0][2], Is.EqualTo(3));
        Assert.That(ints[2][1], Is.EqualTo(5));
        Assert.That(ints[4][3], Is.EqualTo(9));   
    }

    [Test] 
    public void FindVisibleTreesTest() 
    {
        string[] input = {
            "30373",
            "25512",
            "65332",
            "33549",
            "35390"
        };
        List<int[]> ints = Program.StringArrayTo2DInts(input);
        int result = Program.FindVisibleTrees(ints);
        Assert.That(result, Is.EqualTo(21));
    }

    [TestCase(1,1,true)]
    [TestCase(3,3,false)]
    public void CheckVisibilityTest(int targetX, int targetY, bool expectedResult)
    {
        string[] input = {
            "30373",
            "25512",
            "65332",
            "33549",
            "35390"
        };
        List<int[]> ints = Program.StringArrayTo2DInts(input);
        bool result = Program.CheckIfTreeIsVisible(ints, targetX, targetY);
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [TestCase(2,1,4)]
    [TestCase(2,3,8)]
    public void VisibilityFromTreeTest(int targetX, int targetY, int expectedResults)
    {
        string[] input = {
            "30373",
            "25512",
            "65332",
            "33549",
            "35390"
        };

        List<int[]> ints = Program.StringArrayTo2DInts(input);
        int output = Program.VisibilityScoreFromTree(targetX, targetY, ints);
        Assert.That(output, Is.EqualTo(expectedResults));
    }
}