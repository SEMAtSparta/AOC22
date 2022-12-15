using System.Drawing;
using System.IO;
namespace Day6;

public class Program
{
    static void Main(string[] args)
    {
        string[] inputString = File.ReadAllLines("input.txt");
        Console.WriteLine(FindStartOfPacketMarker(inputString[0], 4));
        Console.WriteLine(FindStartOfPacketMarker(inputString[0], 14));


    }

    public static int FindStartOfPacketMarker(string dataStreamBuffer, int sizeOfMarker)
    {
        Queue<char> packet = new();
        int currentPosition = 0;
        foreach(char c in dataStreamBuffer)
        {
            packet.Enqueue(c);
            currentPosition++;
            if (packet.Count == sizeOfMarker)
            {
                if (AreStackContentsDifferent(packet))
                {
                    return (currentPosition);
                }
                packet.Dequeue();
            }
        }
        return -1;
    }

    public static bool AreStackContentsDifferent(Queue<char> packet)
    {
        Queue<char> packetCopy = new Queue<char>(packet);
        for(int i = 0; i < packetCopy.Count+1; i++)
        {
            char c = packetCopy.Dequeue();
            foreach(char item in packetCopy)
            {
                if(c == item)
                {
                    return false;
                }
            }
        }
        return true;
    }
}