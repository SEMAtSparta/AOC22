using System.Net.Mail;

namespace Day10;

public class Program
{
    static void Main(string[] args)
    {
        string[] inputStrings = File.ReadAllLines("input.txt");
        int[] importantCycles =
        {
            20,
            60,
            100,
            140,
            180,
            220
        };

        //Part 1
        //int[] checkedCycles = ValuesOfRegisterAfterSeriesOfCycles(importantCycles, inputStrings);

        //int sum = 0;
        //foreach(int i in checkedCycles)
        //{
        //    sum += i;
        //}
        //Console.WriteLine(sum);

        Render(inputStrings);
    }
    public static int[] ValuesOfRegisterAfterSeriesOfCycles(int[] checkCycles, string[] inputStrings)
    {
        List<Instruction> listOfInstructions = ParseStringsToInstructions(inputStrings);
        int checkIndex = 0;
        int nextCheck = checkCycles[0];
        int registerValue = 1;
        int cycles = 0;

        int[] output = new int[checkCycles.Length];

        foreach(Instruction instruction in listOfInstructions)
        {
            int cyclesToAdd = instruction.Type == InstructionType.NOOP ? 1 : 2;
            int queuedValue = instruction.Value;

            for(int i = 0; i < cyclesToAdd; i++)
            {
                cycles++;
                if (cycles == nextCheck)
                {
                    output[checkIndex] = registerValue * nextCheck;
                    checkIndex++;
                    if(checkIndex == checkCycles.Length)
                    {
                        return output;
                    }
                    nextCheck = checkCycles[checkIndex];
                }
            }
            registerValue += queuedValue;
        }
        return output;
    }

    public static void Render(string[] inputStrings)
    {
        List<Instruction> listOfInstructions = ParseStringsToInstructions(inputStrings);
        int registerValue = 1;
        int currentPixel = 0;

        foreach (Instruction instruction in listOfInstructions)
        {
            int cyclesToAdd = instruction.Type == InstructionType.NOOP ? 1 : 2;
            int queuedValue = instruction.Value;

            for (int i = 0; i < cyclesToAdd; i++)
            {
                if (currentPixel % 40 == 0)
                {
                    Console.Write("\n");
                    currentPixel = 0;
                }

                if (Math.Abs(currentPixel - registerValue) <= 1)
                {
                    Console.Write("#");
                }
                else Console.Write('.');
                currentPixel++;
            }
            registerValue += queuedValue;
        }
    }

    public static List<Instruction> ParseStringsToInstructions(string[] inputStrings)
    {
        List<Instruction> listOfInstructions = new();
        foreach (string str in inputStrings)
        {
            string[] instructionString = str.Split(" ");
            InstructionType type = instructionString[0] == "noop" ? InstructionType.NOOP : InstructionType.ADD;
            int value = type != InstructionType.NOOP ? Int32.Parse(instructionString[1]) : 0;

            listOfInstructions.Add(new(type, value));
        }

        return listOfInstructions;
    }
}

public enum InstructionType
{
    NOOP, ADD
}

public class Instruction
{
    public InstructionType Type;
    public int Value;

    public Instruction(InstructionType type, int value)
    {
        Type = type;
        Value = value;
    }

    public override string ToString()
    {
        return $"{Type} {Value}";
    }
}