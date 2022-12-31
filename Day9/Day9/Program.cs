using System.Numerics;
using System.IO;
using System.Diagnostics.CodeAnalysis;

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
            int magnitude = Int32.Parse(str.Split(" ")[1]);

            instructions.Add(new Vector2(direction, magnitude));
        }

        return Simulate(knot, instructions).Count;
    }

    public static HashSet<Vector2> Simulate(Knot knot, List<Vector2> instructions)
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

    public HashSet<Vector2> VisitedPositions { get; }

    public Knot()
    {
        HeadPosition = new Vector2(0, 0);
        TailPosition = new Vector2(0, 0);
        VisitedPositions = new() { TailPosition };
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

public class Vector2
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
    public int Size()
    {
        //return whichever dimension is bigger as a positive int
        return Math.Abs(this.X) > Math.Abs(this.Y) ? Math.Abs(this.X) : Math.Abs(this.Y);
    }
    public static Vector2 operator +(Vector2 a, Vector2 b)
    {
        return new Vector2(a.X + b.X, a.Y + b.Y);
    }
    public static Vector2 operator -(Vector2 a, Vector2 b)
    {
        return new Vector2(a.X - b.X, a.Y - b.Y);
    }

    public override bool Equals(object? obj)
    {
        Vector2? v = obj as Vector2;
        return v is not null && this.X == v.X && this.Y == v.Y;
    }
    public override int GetHashCode()
    {
        return this.X.GetHashCode() ^ this.Y.GetHashCode();
    }
    public override string ToString()
    {
        return $"[{this.X},{this.Y}]";
    }
}
