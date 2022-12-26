using System.Security.AccessControl;

namespace Day7;

public class Program
{
    
    static void Main(string[] args)
    {
        Folder rootFolder = PopulateFolders("input.txt");

        List<Folder> allFolders = rootFolder.GetSubfolders();

        Part2(allFolders, rootFolder.GetValueOfChildren());
    }


    static void Part1(List<Folder> allFolders)
    {
        int sum = 0;
        foreach (Folder folder in allFolders)
        {
            int sizeOfFolder = folder.GetValueOfChildren();
            if (sizeOfFolder < 100_000)
            {
                sum += sizeOfFolder;
            }
        }
        Console.WriteLine(sum);
    }

    static void Part2(List<Folder> allFolders, int sizeOfFileSystem)
    {
        int maxCapacity = 70_000_000;
        int requiredSpace = 30_000_000;
        int availableSpace = maxCapacity - sizeOfFileSystem;
        int threshold = requiredSpace - availableSpace;
        List<int> sizesOfFolders = new();
        foreach (Folder folder in allFolders) 
        {
            int sizeOfFolder = folder.GetValueOfChildren();
            if(sizeOfFolder >= threshold)
            {
                sizesOfFolders.Add(sizeOfFolder);
            }
        }
        sizesOfFolders.Sort();
        Console.WriteLine($"Solution for part 2: {sizesOfFolders[0]}");
    }

    public static Folder PopulateFolders(string fileName)
    {
        Stack<Folder> directoryHistory = new();
        List<string> inputStrings = File.ReadAllLines(fileName).ToList();
        inputStrings.RemoveAt(0);
        Folder rootFolder = new("/");
        directoryHistory.Push(rootFolder);

        foreach (string line in inputStrings)
        {
            Folder currentFolder = directoryHistory.Pop();

            LineType lineType = GetLineType(line);

            if(lineType == LineType.INSTRUCTION)
            {
                MoveInstruction moveInstruction = StringToInstruction(line, out string targetDirectory);
                if(moveInstruction == MoveInstruction.MOVEIN)
                {
                    Folder targetChild = currentFolder.GetChildWithName(targetDirectory);
                    directoryHistory.Push(currentFolder);
                    currentFolder = targetChild;
                }
                else if(moveInstruction == MoveInstruction.MOVEOUT)
                {
                    currentFolder = directoryHistory.Pop();
                }
            }
            else if(lineType == LineType.DIRECTORY)
            {
                Folder newFolder = StringToNewDirectory(line);
                currentFolder.AddChild(newFolder);
            }
            else if(lineType == LineType.DATA)
            {
                int data = StringToData(line);
                currentFolder.AddData(data);
            }

            directoryHistory.Push(currentFolder);

        }

        while(directoryHistory.Count > 1)
        {
            directoryHistory.Pop();
        }
        return directoryHistory.Pop();
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

public class Folder
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
        throw new ArgumentException($"No folder exists with name: {name}");
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

    public List<Folder> GetSubfolders()
    {
        List<Folder> output = new();
        foreach(Folder child in Children)
        {
            output.Add(child);
            output.AddRange(child.GetSubfolders());
        }
        return output;
    }

    public override string ToString()
    {
        return $"{Name}: {Data}";
    }
}