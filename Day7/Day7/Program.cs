namespace Day7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }

        public string StringToInstruction(string inputString)
        {
            throw new NotImplementedException();
        }

        public string StringToDirectory(string inputString)
        {
            throw new NotImplementedException();
        }

        public int StringToData(string inputString)
        {
            throw new NotImplementedException();
        }

    }

    struct Folder
    {
        string Name { get; }
        int Data = 0;
        List<Folder> Children { get; }

        public Folder(string folderName)
        {
            Name = folderName;
            Children = new List<Folder>();
        }

        public void AddChild(Folder node)
        {
            Children.Add(node);
        }

        public int GetValueOfChildren()
        {
            int totalSize = 0;
            foreach(Folder child in Children)
            {
                totalSize += child.Data;
                totalSize += child.GetValueOfChildren();
            }
            return totalSize;
        }
    }
}