using Microsoft.VisualBasic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;

namespace Day3;

public class Program
{
    static void Main(string[] args)
    {
        Part1();
        Part2();
    }

    static void Part1()
    {
        string[] input = File.ReadAllLines("input.txt");

        List<(string, string)> listOfRucksacks = ParseToListOfRucksacksCompartments(input);
        int sum = 0;
        foreach ((string, string) rucksack in listOfRucksacks)
        {
            sum += FindValueOfDuplicates(rucksack);
        }

        Console.WriteLine($"The result for part 1 is: {sum}");
    }

    static void Part2()
    {
        string[] input = File.ReadAllLines("input.txt");

        int sum = 0;

        List<string[]> listOfGroupedRucksacks = ParseToGroupOfThreeRucksacks(input);
        foreach (string[] stringArray in listOfGroupedRucksacks)
        {
            sum += FindValueOfDuplicateInGroup(stringArray);
        }

        Console.WriteLine($"The result for part 2 is: {sum}");
    } 

    public static int FindValueOfDuplicates((string, string) rucksack)
    {
        string compartment1 = rucksack.Item1;
        string compartment2 = rucksack.Item2;
        foreach(char item in compartment1)
        {
            if (!compartment2.Contains(item))
            {
                continue;
            }
            return FindValueOfItem(item);
        }
        return -1;
    }
    public static int FindValueOfDuplicateInGroup(string[] stringArray)
    {
        foreach (char c in stringArray[0])
        {
            if (stringArray[1].Contains(c) && stringArray[2].Contains(c))
            {
                return FindValueOfItem(c);
            }
        }
        return -1;
    }

    public static int FindValueOfItem(char item)
    {
        int itemToInt = Convert.ToInt32(item);
        if(itemToInt < 91)
        {
            itemToInt -= 64;
            itemToInt += 26;
        }
        else
        {
            itemToInt -= 96;
        }

        return itemToInt;
    }  

    static List<string[]> ParseToGroupOfThreeRucksacks(string[] lines)
    {
        List<string[]> listOfGroupRucksacks = new List<string[]> { new string[3] };

        int listIndex = 0;
        int stringArrayIndex = 0;

        foreach(string line in lines)
        {
            if(stringArrayIndex == 3) { 
                stringArrayIndex %= 3;
                listIndex++;
                listOfGroupRucksacks.Add(new string[3]);
            }

            listOfGroupRucksacks[listIndex][stringArrayIndex] = line;
            stringArrayIndex++;
        }
        return listOfGroupRucksacks;
    }

    static List<(string, string)> ParseToListOfRucksacksCompartments(string[] lines)
    {
        List<(string, string)> listOfRucksacks = new();
        foreach (string line in lines)
        {
            int compartmentSize = line.Length / 2;
            (string, string) rucksack = (line.Substring(0, compartmentSize), line.Substring(compartmentSize, compartmentSize));
            listOfRucksacks.Add(rucksack);
        }

        return listOfRucksacks;
    }
}