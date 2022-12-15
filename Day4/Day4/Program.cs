using System.Diagnostics.CodeAnalysis;
using System.IO;
namespace Day4;

public class Program
{
    static void Main(string[] args)
    {
        string[] inputStrings = File.ReadAllLines("input.txt");
        int sum = 0;
        foreach (string line in inputStrings)
        {
            var pairArray = SplitLineIntoPair(line);
            if (CheckIfPairContainsPair(pairArray[0].Item1, pairArray[0].Item2, pairArray[1].Item1, pairArray[1].Item2))
            {
                sum += 1;
            }
        }

        Console.WriteLine(sum);
    }

    static (int, int)[] SplitLineIntoPair(string inputString)
    {
        string[] stringPair = inputString.Split(",");
        var outputArray = new (int, int)[2];
        int index = 0;
        foreach (string str in stringPair)
        {
            string[] intPair = str.Split("-");
            (int, int) outputPair = new(Int32.Parse(intPair[0]), Int32.Parse(intPair[1]));
            outputArray[index] = outputPair;
            index++;
        }

        return outputArray;
    }

    public static bool CheckIfPairContainsPair(int pair1Lower, int pair1Upper, int pair2Lower, int pair2Upper)
    {
        if (pair1Lower >= pair2Lower)
        {
            //pair 2 contains pair 1
            if (pair1Upper <= pair2Upper)
            {
                return true;
            }
            else return false;
        }
        //pair 1 contains pair 2
        else if (pair1Upper >= pair2Upper)
        {
            return true;
        }
        else return false;
    }
}