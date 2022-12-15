using System.IO;
namespace Day5;

public class Program
{
    static void Main(string[] args)
    {
        Part1();
    }

    public static void Part1()
    {
        string[] input = File.ReadAllLines("input.txt");
        List<Instruction> listOfInstructions = new();
        Stack<string>[] arrayOfPiles = ParseInputToStackAndInstructions(input, 9, 8, out listOfInstructions);

        foreach (Instruction i in listOfInstructions)
        {
            MoveCargo(i.numberOfCrates, i.initialLocation, i.targetLocation, ref arrayOfPiles);
        }

        foreach (Stack<string> pile in arrayOfPiles)
        {
            Console.WriteLine(pile.Peek());
        }
    }
    static Stack<string>[] ParseInputToStackAndInstructions(string[] inputStrings, int numberOfPiles, int maxSizeOfStack, out List<Instruction> instructions)
    {
        Stack<string>[] arrayOfPiles = ParseStringToArrayOfPiles(inputStrings, maxSizeOfStack);

        List<Instruction> listOfInstructions = new();
        //skip the two dead lines in the input
        for (int i = maxSizeOfStack + 2; i < inputStrings.Length; i++)
        {
            //sort out the instructions
            listOfInstructions.Add(ParseStringToInstruction(inputStrings[i]));
        }

        instructions = listOfInstructions;
        return arrayOfPiles;
    }

    static Instruction ParseStringToInstruction(string input)
    {
        //split input into two pieces, one containing number of crates, the other containing location and targetlocation
        string[] splitInput = input.Split("from");

        int numberOfCrates = int.Parse(splitInput[0].Trim().Split(" ")[1]);

        string[] locations = splitInput[1].Replace("from", "").Replace(" ", "").Split("to");
        //subtract 1 to make the locations match with array indexes
        int initialLocation = int.Parse(locations[0]) - 1;
        int targetLocation = int.Parse(locations[1]) - 1;

        return new Instruction(initialLocation, targetLocation, numberOfCrates);
    }

    public static Stack<string>[] ParseStringToArrayOfPiles(string[] inputStrings, int maxSizeOfStack)
    {
        Stack<string>[] arrayOfPiles = new Stack<string>[9];
        for (int i = 0; i < arrayOfPiles.Length; i++)
        {
            arrayOfPiles[i] = new Stack<string>();
        }

        for (int i = 0; i < maxSizeOfStack; i++)
        {
            //sort out the crates
            for (int j = 0; j < inputStrings[i].Length; j += 4)
            {
                if (!inputStrings[i].Substring(j, 3).StartsWith("["))
                {
                    continue;
                }
                int pileIndex = j / 4;
                arrayOfPiles[pileIndex].Push(inputStrings[i].Substring(j, 3));
            }
        }

        return arrayOfPiles;
    }

    public static void MoveCargo(int numberOfCrates, int initialLocation, int targetLocation, ref Stack<string>[] arrayOfPiles)
    {
        for(int i = 0; i < numberOfCrates; i++)
        {
            string crate = arrayOfPiles[initialLocation].Pop();
            arrayOfPiles[targetLocation].Push(crate);
        }
    }
}

public struct Instruction
{
    public int initialLocation;
    public int targetLocation;
    public int numberOfCrates;

    public Instruction(int initial, int target, int num)
    {
        initialLocation = initial;
        targetLocation = target;
        numberOfCrates = num;
    }
}
