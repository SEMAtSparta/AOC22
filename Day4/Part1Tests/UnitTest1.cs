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
    }
}