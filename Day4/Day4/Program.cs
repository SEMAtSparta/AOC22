using System.Diagnostics.CodeAnalysis;
using System.IO;
namespace Day4;

public class Program
{
    static void Main(string[] args)
    {
        Part1();
        Part2();
    }

    static void Part1()
    {
        string[] inputStrings = File.ReadAllLines("input.txt");
        int sum = 0;
        foreach (string line in inputStrings)
        {
            var pairArray = SplitLineIntoPair(line);
            if (CheckIfPairContainsPair(pairArray[0].Item1, pairArray[0].Item2, pairArray[1].Item1, pairArray[1].Item2))
            {
                sum++;
            }
        }

        Console.WriteLine($"The answer to part 1 is: {sum}");
    }

    static void Part2()
    {
        string[] inputStrings = File.ReadAllLines("input.txt");
        int sum = 0;
        foreach(string line in inputStrings)
        {
            var pairArray = SplitLineIntoPair(line);
            if(CheckIfPairsOverlap(pairArray[0].Item1, pairArray[0].Item2, pairArray[1].Item1, pairArray[1].Item2))
            {
                sum++;
            }
        }
        Console.WriteLine($"The answer to part 2 is: {sum}");
    }

    public static (int, int)[] SplitLineIntoPair(string inputString)
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
        bool pair1ContainsPair2 = (pair1Lower <= pair2Lower) && (pair1Upper >= pair2Upper);
        bool pair2ContainsPair1 = (pair1Lower >= pair2Lower) && (pair1Upper <= pair2Upper);
        return ( pair1ContainsPair2 || pair2ContainsPair1 );

    }

    public static bool CheckIfPairsOverlap(int pair1Lower, int pair1Upper, int pair2Lower, int pair2Upper)
    {
        bool pair1StrictlyGreaterThanPair2 = pair1Lower > pair2Upper;
        bool pair2StrictlyGreaterThanPair1 = pair2Lower > pair1Upper;

        return !(pair1StrictlyGreaterThanPair2 || pair2StrictlyGreaterThanPair1);
    }
}