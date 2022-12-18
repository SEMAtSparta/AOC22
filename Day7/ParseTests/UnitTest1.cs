using Day7;
namespace ParseTestsApp;

public class Tests
{
    Folder exampleFolder;
    [SetUp]

    public void SetUp()
    {
        exampleFolder = new Folder("ExampleFolder");
    }
        //arrange
        //act
        //assert

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
        var output = Program.StringToDirectory(inputString).Name;
        //assert
        Assert.That(output, Is.EqualTo(expectedValue));
    }
}