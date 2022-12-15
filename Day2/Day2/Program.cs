using System.IO;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Day2;

public class Program
{
    static void Main(string[] args)
    {
        List<(string OpponentPlay, string MyPlay)> rpsList = ParseToPairs("input.txt");
        int sum = 0;
        //part1
        foreach ((string, string) match in rpsList)
        {
            sum += RPSMatch(match.Item1, match.Item2);
        }
        Console.WriteLine("Result for part 1: " + sum);

        sum = 0;
        //part 2
        foreach((string,string) match in rpsList)
        {
            sum += RPSChosenResult(match.Item1, match.Item2);
        }
        Console.WriteLine("Result for part 2: " + sum);

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

    public static int RPSChosenResult(string player1Choice, string resultOfMatch)
    {
        int score = 0;

        int outcomeOfMatch = 0;
        string[] orderOfRPS = { "A", "B", "C" };
        switch (resultOfMatch)
        {
            //lose
            case "X":
                score += 0;
                outcomeOfMatch = 2;
                break;
            //draw
            case "Y":
                score += 3;
                outcomeOfMatch = 0;
                break;
            //win
            case "Z":
                score += 6;
                outcomeOfMatch = 1;
                break;
            default:
                break;
        }
        int nextChoiceIndex = 0;
        for(int i = 0; i < orderOfRPS.Length; i++)
        {
            if (orderOfRPS[i] == player1Choice)
            {
                nextChoiceIndex = (i + outcomeOfMatch)%3;
            }
        }
        return nextChoiceIndex + 1 + score;
    }

    public static int RPSMatch(string player1Choice, string player2Choice)
    {
        
        int score = 0;
        switch (player2Choice)
        {
            case "X":
                score += 1;
                player2Choice = "A";
                break;
            case "Y":
                score += 2;
                player2Choice = "B";
                break;
            case "Z":
                score += 3;
                player2Choice = "C";
                break;
            default:
                break;
        }

        string[] orderOfRPS = { "A", "B", "C" };
        int p1ChoiceInt = 0;
        int p2ChoiceInt = 0;
        //turn choice letter into index
        for(int i = 0; i < orderOfRPS.Count(); i++)
        {
            if(orderOfRPS[i] == player1Choice) p1ChoiceInt = i;
            if(orderOfRPS[i] == player2Choice) p2ChoiceInt = i;
        }
        int matchResult = p1ChoiceInt - p2ChoiceInt;

        if(matchResult == -1|| matchResult == 2){
            return score + 6;
        }
        else if(p1ChoiceInt == p2ChoiceInt)
        {
            return score + 3;
        }
        else
        {
            return score;
        }
    }
}