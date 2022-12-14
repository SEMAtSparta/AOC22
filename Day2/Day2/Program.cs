using System.IO;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Day2;

internal class Program
{
    static void Main(string[] args)
    {
        List<(string OpponentPlay, string MyPlay)> rpsList = ParseToPairs("input.txt");
        int sum = 0;
        foreach((string,string) match in rpsList)
        {
            sum += RPSMatch(match.Item1, match.Item2);
        }
        Console.WriteLine(sum);
        Console.WriteLine(-2%3);
    }

    public static List<(string,string)> ParseToPairs(string input)
    {
        string[] lines = File.ReadAllLines(input);

        var output = new List<(string, string)>();
        foreach(string line in lines)
        {
            string[] splitLine = line.Split(" ");
            output.Add((splitLine[0], splitLine[1]));
        }
        return output;
    }

    public static int RPSMatch(string player1Choice, string player2Choice)
    {
        string[] orderOfRPS = { "A", "B", "C" };
        int output = 0;
        switch (player2Choice)
        {
            case "X":
                output += 1;
                player2Choice = "A";
                break;
            case "Y":
                output += 2;
                player2Choice = "B";
                break;
            case "Z":
                output += 3;
                player2Choice = "C";
                break;
            default:
                break;
        }
        int p1ChoiceInt = 0;
        int p2ChoiceInt = 0;
        for(int i = 0; i < orderOfRPS.Count(); i++)
        {
            if(orderOfRPS[i] == player1Choice) p1ChoiceInt = i;
            if(orderOfRPS[i] == player2Choice) p2ChoiceInt = i;
        }
        int matchResult = p1ChoiceInt - p2ChoiceInt;
        if(matchResult < -1)
        {
            matchResult += 3;
        }

        switch (matchResult)
        {
            case -1:
                return output + 0;
            case 0:
                return output + 3;
            case 1:
                return output + 6;
            default:
                return 0;
        }

    }
}