using System.IO;
namespace Day5;

public class Program
{
    static void Main(string[] args)
    {
        string[] input = File.ReadAllLines("input.txt");
        ParseInputToStackAndInstructions(input, 9, 10, out string[] instructions);
    }

    static Stack<string> ParseInputToStackAndInstructions(string[] inputStrings, int numberOfPiles, int maxSizeOfStack, out string[] instructions)
    {
        for (int i = 0; i < maxSizeOfStack; i++)
        {
            //sort out the crates
        }

        List<Instruction> listOfInstructions = new();
        //skip the two dead lines in the input
        for(int i = maxSizeOfStack + 2; i < inputStrings.Length; i++)
        {
            //sort out the instructions
            listOfInstructions.Add(ParseStringToInstruction(inputStrings[i]));
        }

        throw new NotImplementedException();
    }

    static Instruction ParseStringToInstruction(string input)
    {
        //split input into two pieces, one containing number of crates, the other containing location and targetlocation
        string[] splitInput = input.Split("from");

        int numberOfCrates = int.Parse(splitInput[0].Trim().Split(" ")[1]);

        string[] locations = splitInput[1].Replace("from", "").Replace(" ", "").Split("to");
        int initialLocation = int.Parse(locations[0]);
        int targetLocation = int.Parse(locations[1]);

        return new Instruction(initialLocation, targetLocation, numberOfCrates);
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
