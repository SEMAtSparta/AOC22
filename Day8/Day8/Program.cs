namespace Day8;

public class Program
{
    static void Main(string[] args)
    {
        string[] inputStrings = File.ReadAllLines("input.txt");
        List<int[]> input2DArray = StringArrayTo2DInts(inputStrings);
        Console.WriteLine(FindVisibleTrees(input2DArray)); 
    }

    public static List<int[]> StringArrayTo2DInts(string[] inputStrings)
    {
        List<int[]> output = new List<int[]>();
        for(int i = 0; i < inputStrings.Length; i++)
        {
            output.Add(StringToIntArray(inputStrings[i]));
        }
        return output;
    }

    public static int[] StringToIntArray(string input)
    {
        char[] inputToChar = input.ToCharArray();
        int[] output = new int[inputToChar.Length];
        for (int i = 0; i < output.Length; i++)
        {
            string s = inputToChar[i].ToString();
            output[i] = Int32.Parse(s);
        }

        return output;
    }

    public static int FindVisibleTrees(List<int[]> input2D)
    {
        int sum = (input2D.Count * 2) + ((input2D[0].Length - 2) * 2);
        for(int col = 1; col < input2D.Count-1; col++)
        {
            for(int row = 1; row < input2D[0].Length-1; row++)
            {
                sum += CheckVisibility(input2D, row, col) ? 1 : 0;
            }
        }

        return sum;
    }

    public static bool CheckVisibility(List<int[]> input2D, int row, int col)
    {
        bool[] visibleFromDirections = new bool[4];
        int target = input2D[col][row];

        visibleFromDirections[0] = CheckOneDirection(input2D, target, 0, col, true, row);
        visibleFromDirections[1] = CheckOneDirection(input2D, target, col+1, input2D.Count, true, row);
        visibleFromDirections[2] = CheckOneDirection(input2D, target, 0, row, false, col);
        visibleFromDirections[3] = CheckOneDirection(input2D, target, row+1, input2D[col].Length, true, col);
        
        foreach(bool b in visibleFromDirections)
        {
            if( b ) return true;
        }
        return false;
    }

    public static bool CheckOneDirection(List<int[]> input2D, int target, int start, int end, bool column, int fixedDimension)
    {
        for (int i = start; i < end; i++)
        {
            int tree = column ? input2D[i][fixedDimension] : input2D[fixedDimension][i];
            if (tree >= target)
            {
                return false;
            }
        }
        return true;
    }
}