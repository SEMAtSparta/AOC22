using Day7;
namespace ParseTestsApp;

public class Tests
{
    List<Folder> exampleFolders;
    int[] sizeOfFolders;

    [SetUp]

    public void SetUp()
    {
        sizeOfFolders = new int[5] { 25, 21, 22, 0, 1 };
        exampleFolders = new List<Folder>();
        int index = 0;
        foreach(int i in sizeOfFolders)
        {
            Folder f = new Folder(i.ToString());
            f.AddData(i);
            exampleFolders.Add(f);
            if(index > 0)
            {
                exampleFolders[index - 1].AddChild(exampleFolders[index]);
            }
            index++;
        }
    }
    //arrange
    //act
    //assert

    [Test]
    public void GivenInt_AddData_IncreasesFolderData()
    {
        int expected = 50;
        Folder f = new();
        f.AddData(expected);

        Assert.That(expected, Is.EqualTo(f.Data));

    }


    [Test]

    public void GivenFolderWithChildren_GetValueOfChildren_ReturnsSumOfFolderSizes()
    {
        int sum = exampleFolders[0].GetValueOfChildren();
        int expectedResult = sizeOfFolders.Sum();

        Assert.That(sum, Is.EqualTo(expectedResult));
    }

    [Test]
    public void GivenStringBeginningDollarSign_GetLineType_ReturnsInstruction()
    {
        //arrange
        var expectedType = LineType.INSTRUCTION;
        var inputString = "$ cd /";
        //act
        var output = Program.GetLineType(inputString);
        //assert
        Assert.That(output, Is.EqualTo(expectedType));
    }

    [Test]
    public void GivenStringBeginningDIR_GetLineType_ReturnsDirectory()
    {
        //arrange
        var expectedType = LineType.DIRECTORY;
        var inputString = "dir cmvqf";
        //act
        var output = Program.GetLineType(inputString);
        //assert
        Assert.That(output, Is.EqualTo(expectedType));
    }

    [Test]
    public void GivenStringBeginningWithInt_GetLineType_ReturnsData()
    {
        //arrange
        var expectedType = LineType.DATA;
        var inputString = "124423 rjqns.prb";
        //act
        var output = Program.GetLineType(inputString);
        //assert
        Assert.That(output, Is.EqualTo(expectedType));
    }

    [Test]
    public void GivenStringBeginningWithInt_StringToData_ReturnsIntData()
    {
        //arrange
        var expectedValue = 124423;
        var inputString = "124423 rjqns.prb";
        //act
        var output = Program.StringToData(inputString);
        //assert
        Assert.That(output, Is.EqualTo(expectedValue));
    }
    [Test]
    public void GivenStringBeginningDir_StringToDirectory_ReturnsNewDirectory()
    {
        //arrange;
        var inputString = "dir ExampleFolder";
        var expectedValue = "ExampleFolder";
        //act
        var output = Program.StringToNewDirectory(inputString).Name;
        //assert
        Assert.That(output, Is.EqualTo(expectedValue));
    }
}