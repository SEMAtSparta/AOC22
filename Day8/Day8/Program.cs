namespace Day8;

public class Program
{
    static void Main(string[] args)
    {
        string[] inputStrings = File.ReadAllLines("input.txt");
        int[][] input2DArray = StringArrayTo2DInts(inputStrings);

        int largest = 0;
        for (int i = 0; i < input2DArray.Length; i++)
        {
            for(int j = 0; j < input2DArray[i].Length; j++)
            {
                int x = VisibilityScoreFromTree(j, i, input2DArray);

                largest = x > largest ? x : largest;
            }
        }
        Console.WriteLine(FindVisibleTrees(input2DArray));
        Console.WriteLine(largest);
    }

    public static int[][] StringArrayTo2DInts(string[] inputStrings)
    {
        List<int[]> output = new List<int[]>();
        for(int i = 0; i < inputStrings.Length; i++)
        {
            output.Add(StringToIntArray(inputStrings[i]));
        }
        return output.ToArray();
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
    public static int VisibilityScoreFromTree(int xPos, int yPos, int[][] input2D)
    {
        int tree = input2D[yPos][xPos];
        return VisibilityFromTreeInAllDirections(tree, xPos, yPos, input2D);
    }

    public static int VisibilityFromTreeInAllDirections(int tree, int xPos, int yPos, int[][] input2D)
    {
        //North/South
        List<int> treesToCheckList = new();

        for(int i = 0; i < input2D.Length; i++)
        {
            treesToCheckList.Add(input2D[i][xPos]);
        }
        int colOutput = CheckVisibilityFromTree(tree, treesToCheckList.ToArray(), yPos);

        treesToCheckList.Clear();

        for(int i = 0; i < input2D[xPos].Length; i++)
        {
            treesToCheckList.Add(input2D[yPos][i]);
        }
        int rowOutput = CheckVisibilityFromTree(tree, treesToCheckList.ToArray(), xPos);

        return rowOutput * colOutput;
    }

    public static int CheckVisibilityFromTree(int tree, int[] trees, int treeLocation)
    {
        int sumBefore = 0;
        int sumAfter = 0;

        if(treeLocation == 0)
        {
            sumBefore = 1;
        }
        else
        {
            int[] treesBefore = new int[treeLocation];
            Array.Copy(trees, treesBefore, treeLocation);
            Array.Reverse(treesBefore);

            foreach (int t in treesBefore)
            {
                sumBefore++;
                if (t >= tree) break;
            }
        }

        if (treeLocation == trees.Length - 1)
        {
            sumAfter = 1;
        }
        else
        {
            int[] treesAfter = new int[trees.Length - treeLocation - 1];
            Array.Copy(trees, treeLocation+1, treesAfter, 0, trees.Length - treeLocation - 1);

            foreach (int t in treesAfter)
            {
                sumAfter++;
                if (t >= tree) break;
            }
        }
        return sumBefore * sumAfter;
    }

    public static int FindVisibleTrees(int[][] input2D)
    {
        //outer edges - 4 so we don't double count corners
        int sum = (input2D.Length * 2) + (input2D[0].Length * 2) - 4;
        for(int col = 1; col < input2D.Length-1; col++)
        {
            for(int row = 1; row < input2D[0].Length-1; row++)
            {
                sum += CheckIfTreeIsVisible(input2D, row, col) ? 1 : 0;
            }
        }

        return sum;
    }

    public static bool CheckIfTreeIsVisible(int[][] input2D, int row, int col)
    {
        bool[] visibleFromDirections = new bool[4];
        int target = input2D[col][row];

        visibleFromDirections[0] = CheckIfTreeIsVisibleOneDirection(input2D, target, 0, col, true, row);
        visibleFromDirections[1] = CheckIfTreeIsVisibleOneDirection(input2D, target, col+1, input2D.Length, true, row);
        visibleFromDirections[2] = CheckIfTreeIsVisibleOneDirection(input2D, target, 0, row, false, col);
        visibleFromDirections[3] = CheckIfTreeIsVisibleOneDirection(input2D, target, row+1, input2D[col].Length, false, col);
        
        foreach(bool b in visibleFromDirections)
        {
            if( b ) return true;
        }
        return false;
    }

    public static bool CheckIfTreeIsVisibleOneDirection(int[][] input2D, int target, int start, int end, bool isColumn, int fixedDimension)
    {
        for (int i = start; i < end; i++)
        {
            int tree = isColumn ? input2D[i][fixedDimension] : input2D[fixedDimension][i];
            if (tree >= target)
            {
                return false;
            }
        }
        return true;
    }
}