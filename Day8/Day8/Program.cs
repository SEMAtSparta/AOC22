namespace Day8;

public class Program
{
    static void Main(string[] args)
    {
        string[] inputStrings = File.ReadAllLines("input.txt");
        List<int[]> input2DArray = StringArrayTo2DInts(inputStrings);

        int largest = 0;
        for (int i = 0; i < input2DArray.Count; i++)
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
    public static int VisibilityScoreFromTree(int targetX, int targetY, List<int[]> input2D)
    {
        int tree = input2D[targetY][targetX];
        int[] visibilityByDirection = VisibilityFromTreeInAllDirections(tree, targetX, targetY, input2D);

        return visibilityByDirection[0] * visibilityByDirection[1] * visibilityByDirection[2] * visibilityByDirection[3];
    }

    public static int[] VisibilityFromTreeInAllDirections(int tree, int targetX, int targetY, List<int[]> input2D)
    {
        int[] output = new int[4];

        //North
        int index = targetY - 1 < 0 ? 0 : targetY - 1;
        int upperBound = 0;
        int sum = 0;

        do
        {
            sum++;
            if (input2D[index][targetX] >= tree) break;
            index--;
        }
        while (index > upperBound);

        output[0] = sum;

        //South
        index = targetY + 1 == input2D.Count ? targetY : targetY + 1;
        upperBound = input2D.Count;
        sum = 0;

        do
        {
            sum++;
            if (input2D[index][targetX] >= tree) break;
            index++;
        }
        while (index < upperBound);

        output[1] = sum;

        //West
        index = targetX - 1 < 0 ? 0 : targetX - 1;
        upperBound = 0;
        sum = 0;

        do
        {
            sum++;
            if (input2D[targetY][index] >= tree) break;
            index--;
        }
        while (index > upperBound) ;

        output[2] = sum;

        //East
        index = targetX + 1 == input2D[0].Length ? targetX : targetX + 1;
        upperBound = input2D[0].Length;
        sum = 0;

        do
        {
            sum++;
            if (input2D[targetY][index] >= tree) break;
            index++;
        }
        while (index < upperBound);

        output[3] = sum;

        return output;
    }

    public static int FindVisibleTrees(List<int[]> input2D)
    {
        //outer edges - 4 so we don't double count corners
        int sum = (input2D.Count * 2) + (input2D[0].Length * 2) - 4;
        for(int col = 1; col < input2D.Count-1; col++)
        {
            for(int row = 1; row < input2D[0].Length-1; row++)
            {
                sum += CheckIfTreeIsVisible(input2D, row, col) ? 1 : 0;
            }
        }

        return sum;
    }

    public static bool CheckIfTreeIsVisible(List<int[]> input2D, int row, int col)
    {
        bool[] visibleFromDirections = new bool[4];
        int target = input2D[col][row];

        visibleFromDirections[0] = CheckIfTreeIsVisibleOneDirection(input2D, target, 0, col, true, row);
        visibleFromDirections[1] = CheckIfTreeIsVisibleOneDirection(input2D, target, col+1, input2D.Count, true, row);
        visibleFromDirections[2] = CheckIfTreeIsVisibleOneDirection(input2D, target, 0, row, false, col);
        visibleFromDirections[3] = CheckIfTreeIsVisibleOneDirection(input2D, target, row+1, input2D[col].Length, false, col);
        
        foreach(bool b in visibleFromDirections)
        {
            if( b ) return true;
        }
        return false;
    }

    public static bool CheckIfTreeIsVisibleOneDirection(List<int[]> input2D, int target, int start, int end, bool isColumn, int fixedDimension)
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

    public enum Cardinal
    {
        NORTH,
        SOUTH,
        EAST,
        WEST
    }
}