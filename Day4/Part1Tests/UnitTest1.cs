using Day4;
namespace Part1Tests
{
    public class Tests
    {
        [TestCase(2,8,3,7,true)]
        [TestCase(2,5,1,8,true)]
        [TestCase(2,5,2,5,true)]
        public void GivenTwoPairs_CheckIfPairContainsPair_ReturnsBool(int pair1L,int pair1U,int pair2L,int pair2U, bool expectedResult)
        {
            Assert.That(Program.CheckIfPairContainsPair(pair1L, pair1U, pair2L, pair2U), Is.EqualTo(expectedResult));
        }

        [TestCase("71-71,42-72", 71,71,42,72)]
        [TestCase("27-28, 27-99", 27,28,27,99)]
        public void GivenString_SplitLineIntoPair_ReturnsArrayOfIntPairs(string input, int p1L, int p1U, int p2L, int p2U)
        {
            (int, int)[] expectedResult = new (int, int)[2];
            expectedResult[0] = (p1L, p1U);
            expectedResult[1] = (p2L, p2U);

            Assert.That(Program.SplitLineIntoPair(input), Is.EqualTo(expectedResult));
        }
    }
}