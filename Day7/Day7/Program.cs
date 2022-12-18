using System.Security.AccessControl;

namespace Day7;

public class Program
{
    
    static void Main(string[] args)
    {
        ControlFlow("input.txt");
    }

    //stack of accessed directories
    //cd .. pops the stack
    //active directory is accessed by peek
    //cd XXX pushes xxx to top of stack

    public static void ControlFlow(string fileName)
    {
        string[] inputStrings = File.ReadAllLines(fileName);
        Stack<Folder> directoryHistory = new();
        directoryHistory.Push(new Folder(" "));
        Folder rootFolder = new("/");
        directoryHistory.Peek().AddChild(rootFolder);

        foreach(string line in inputStrings)
        {
            LineType lineType = GetLineType(line);

            switch (lineType)
            {
                case LineType.INSTRUCTION:
                    MoveInstruction moveType = StringToInstruction(line, out string targetDirectory);
                    if(moveType == MoveInstruction.MOVEOUT)
                    {
                        directoryHistory.Pop();
                    }
                    else if(moveType == MoveInstruction.MOVEIN)
                    {
                        Folder targetFolder = directoryHistory.Peek().GetChildWithName(targetDirectory);
                        directoryHistory.Push(targetFolder);
                    }
                    break;

                case LineType.DIRECTORY:
                    Folder newFolder = StringToNewDirectory(line);
                    directoryHistory.Peek().AddChild(newFolder);
                    break;

                case LineType.DATA:
                    int newData = StringToData(line);
                    Folder currentFolder = directoryHistory.Peek();
                    currentFolder.AddData(newData);
                    break;
            }
        }
        Console.WriteLine(rootFolder.GetValueOfChildren());
    }

    public static LineType GetLineType(string inputString)
    {
        if (inputString.StartsWith('$'))
        {
            return LineType.INSTRUCTION;
        }
        else if (inputString.StartsWith("dir"))
        {
            return LineType.DIRECTORY;
        }
        else return LineType.DATA;
    }

    public static MoveInstruction StringToInstruction(string inputString, out string targetDirectory)
    {
        string[] splitInstruction = inputString.Split(" ");
        targetDirectory = " ";


        if (splitInstruction[1] == "ls")
        {
            return MoveInstruction.EMPTY;
        }

        targetDirectory = splitInstruction[2];

        if (targetDirectory == "..")
        {
            return MoveInstruction.MOVEOUT;
        }
        else
        {
            return MoveInstruction.MOVEIN;
        }
    }

    public static Folder StringToNewDirectory(string inputString)
    {
        string newFolderName = inputString.Split(" ")[1];
        Folder newFolder = new(newFolderName);
        return newFolder;
    }

    public static int StringToData(string inputString)
    {
        int newData = Int32.Parse(inputString.Split(" ")[0]);
        return newData;
    }
}

public enum LineType
{
    INSTRUCTION, DIRECTORY, DATA
}

public enum MoveInstruction
{
    MOVEIN,MOVEOUT, EMPTY
}

public struct Folder
{
    public string Name { get; set; }
    public int Data = 0;
    List<Folder> Children;

    public Folder(string folderName)
    {
        Name = folderName;
        Children = new List<Folder>();
    }

    public void AddChild(Folder node)
    {
        Children.Add(node);
    }
    
    public void AddData(int data)
    {
        Data += data;
    }

    public Folder GetChildWithName(string name)
    {
        foreach(Folder child in Children)
        {
            if(child.Name == name)
            {
                return child;
            }
        }
        throw new ArgumentException($"No folder exists with {name}");
    }

    public int GetValueOfChildren()
    {
        int totalSize = Data;
        foreach(Folder child in Children)
        {
            totalSize += child.GetValueOfChildren();
        }
        return totalSize;
    }
}