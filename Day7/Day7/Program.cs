namespace Day7;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    //stack of accessed directories
    //cd .. pops the stack
    //active directory is accessed by peek
    //cd XXX pushes xxx to top of stack

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

    public static void StringToInstruction(string inputString, Folder currentDirectory)
    {
        string[] splitInstruction = inputString.Split(" ");
        if (splitInstruction[1] == "ls")
        {
            return;
        }

        string targetDirectory = splitInstruction[2];

        if (targetDirectory == "..")
        {
            MoveOutOfFolder();
        }
        else
        {
            MoveIntoFolder(currentDirectory.GetChildWithName(targetDirectory));
        }
    }

    public static Folder StringToDirectory(string inputString)
    {
        string newFolderName = inputString.Split(" ")[1];
        Folder newFolder = new Folder(newFolderName);
        return newFolder;
    }

    public static int StringToData(string inputString)
    {
        int newData = Int32.Parse(inputString.Split(" ")[0]);
        return newData;
    }

    public static void MoveIntoFolder(Folder folder)
    {
        throw new NotImplementedException();
    }

    public static void MoveOutOfFolder()
    {
        throw new NotImplementedException();
    }

}

public enum LineType
{
    INSTRUCTION, DIRECTORY, DATA
}

public struct Folder
{
    public string Name { get; set; }
    int Data = 0;
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