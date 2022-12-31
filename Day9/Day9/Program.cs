using System.Numerics;
using System.IO;

namespace Day9;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(Part1());   
    }

    public static int Part1()
    {
        Knot knot = new();
        List<Vector2> instructions = new();

        string[] inputStrings = File.ReadAllLines("input.txt");

        foreach(string str in inputStrings)
        {
            char direction = str.ToCharArray()[0];
            int magnitude = str.ToCharArray()[2];

            instructions.Add(new Vector2(direction, magnitude));
        }

        return Simulate(knot, instructions).Count;
    }

    public static List<Vector2> Simulate(Knot knot, List<Vector2> instructions)
    {
        foreach(Vector2 instruction in instructions)
        {
            knot.MoveHead(instruction);
        }

        return knot.VisitedPositions;
    }
}

public class Knot
{
    public Vector2 HeadPosition { get; set; }
    public Vector2 TailPosition { get; set; }

    public List<Vector2> VisitedPositions { get; }

    public Knot()
    {
        HeadPosition = new Vector2(0, 0);
        TailPosition = new Vector2(0, 0);
        VisitedPositions = new();
    }

    public void MoveHead(Vector2 instruction)
    {
        HeadPosition += instruction;
        while((HeadPosition - TailPosition).Size() > 1)
        {
            UpdateTail();
        }
    }

    private void UpdateTail()
    {
        Vector2 distance = HeadPosition - TailPosition;
        Vector2 increment = new(Math.Sign(distance.X), Math.Sign(distance.Y));

        TailPosition += increment;

        VisitedPositions.Add(TailPosition);
    }
}

public struct Vector2
{
    public int X { get; set; }
    public int Y { get; set; }

    public Vector2(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Vector2(char direction, int magnitude)
    {
        if (direction == 'U') { X = 0; Y = magnitude; }
        else if (direction == 'D') { X = 0; Y = -magnitude; }
        else if (direction == 'R') { X = magnitude; Y = 0; }
        else { X = -magnitude; Y = 0; }
    }

    public static Vector2 operator +(Vector2 a, Vector2 b)
    {
        return new Vector2(a.X + b.X, a.Y + b.Y);
    }
    public static Vector2 operator -(Vector2 a, Vector2 b)
    {
        return new Vector2(a.X - b.X, a.Y - b.Y);
    }

    public int Size()
    {
        //return whichever dimension is bigger as a positive int
        return Math.Abs(this.X) > Math.Abs(this.Y) ? Math.Abs(this.X) : Math.Abs(this.Y);
    }
}
