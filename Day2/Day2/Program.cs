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
        //this variable maps match outcomes: 0 = draw, 1 = win, 2 = loss.
        //for example where choiceIndex = 1, i.e: "B", the winning result is choiceIndex + 1, 2, i.e. "C"
        //to prevent index being out of bounds we apply %3
        int indexModifier = 0;

        int matchOutcomeModifier = 0;
        switch (resultOfMatch)
        {
            //lose
            case "X":
                matchOutcomeModifier += 0;
                indexModifier = 2;
                break;
            //draw
            case "Y":
                matchOutcomeModifier += 3;
                indexModifier = 0;
                break;
            //win
            case "Z":
                matchOutcomeModifier += 6;
                indexModifier = 1;
                break;
            default:
                break;
        }

        string[] orderOfRPS = { "A", "B", "C" };

        int nextChoiceIndex = 0;
        for(int i = 0; i < orderOfRPS.Length; i++)
        {
            if (orderOfRPS[i] == player1Choice)
            {
                nextChoiceIndex = (i + indexModifier)%3;
            }
        }
        return nextChoiceIndex + 1 + matchOutcomeModifier;
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