using System.IO;
using System.Runtime.ExceptionServices;

namespace Day1;

internal class Program
{
    static void Main(string[] args)
    {
        List<int> inputInts = ReadFileToInts("input.txt");
        
        int[] threeLargest = FindThreeLargest(inputInts);
        int sum = 0;
        foreach (int i in threeLargest)
        {
            sum += i;
        }
        Console.WriteLine(sum);       
    }

    public static List<int> ReadFileToInts(string fileName)
    {
        string[] inputStrings = File.ReadAllLines(fileName);
        var output = new List<int>();
        int sum = 0;
        foreach (string str in inputStrings)
        {
            int result = 0;
            if(Int32.TryParse(str, out result))
            {
                sum += result;
            }
            else
            {
                output.Add(sum);
                sum = 0;
            }
        }
        return output;
    }

    public static int FindLargest(List<int> inputInts)
    {

        int largest = 0;
        foreach (int total in inputInts)
        {
            if (total > largest) largest = total;
        }

        return largest;
    }

    public static int[] FindThreeLargest(List<int> inputInts)
    {
        int[] output = new int[3];
        //List.Sort()
        foreach(int num in inputInts)
        {
            if(num > output[2])
            {
                if(num > output[1])
                {
                    if(num > output[0])
                    {
                        output[2] = output[1];
                        output[1] = output[0];
                        output[0] = num;
                    }
                    else
                    {
                        output[2] = output[1];
                        output[1] = num;
                    }
                }
                else
                {
                    output[2] = num;
                }
            }
        }
        
        return output;
    }
}