namespace Day11;

public class Program
{
    static void Main(string[] args)
    {
        List<Monkey> monkeyListPart1 = MonkeySetup(true);
        List<Monkey> monkeyListPart2 = MonkeySetup(false);

        //Console.WriteLine(RunSimulation(monkeyListPart1, 20));  
        Console.WriteLine(RunSimulation(monkeyListPart2, 10_000));  
    }

    public static long RunSimulation(List<Monkey> monkeys, int rounds)
    {

        for (int i = 0; i < rounds; i++)
        {
            foreach (Monkey monkey in monkeys)
            {
                monkey.InspectAll();
            }
        }

        long[] mostInspections = new long[2];
        foreach (Monkey monkey in monkeys)
        {
            if (monkey.NumberOfInspectedItems > mostInspections[1])
            {
                if (monkey.NumberOfInspectedItems > mostInspections[0])
                {
                    mostInspections[0] = monkey.NumberOfInspectedItems;
                }
                else
                {
                    mostInspections[1] = monkey.NumberOfInspectedItems;
                }
            }
        }

        return mostInspections[0] * mostInspections[1];
    }

    public static List<Monkey> MonkeySetup(bool part1)
    {
        int[] testPrimes = { 3, 13, 2, 11, 19, 17, 5, 7 };

        List<MonkeyOperation> monkeyOperations = SetMonkeyOperations();
        List<MonkeyTest> monkeyTests = SetMonkeyTests(testPrimes);
        List<Monkey> monkeys = CreateMonkeys(monkeyOperations, monkeyTests, part1);

        SetMonkeyLCM(testPrimes, monkeys);
        SetMonkeyTargets(monkeys);
        SetMonkeyItems(monkeys);

        return monkeys;
    }

    public static List<Monkey> CreateMonkeys(List<MonkeyOperation> monkeyOperations, List<MonkeyTest> monkeyTests, bool part1)
    {
        List<Monkey> monkeys = new();

        for (int i = 0; i < monkeyOperations.Count; i++)
        {
            monkeys.Add(new Monkey(i, monkeyOperations[i], monkeyTests[i], part1));
        }

        return monkeys;
    }

    public static void SetMonkeyTargets(List<Monkey> monkeys)
    {
        monkeys[0].SetTargets(monkeys[3], monkeys[6]);
        monkeys[1].SetTargets(monkeys[3], monkeys[0]);
        monkeys[2].SetTargets(monkeys[0], monkeys[1]);
        monkeys[3].SetTargets(monkeys[6], monkeys[7]);
        monkeys[4].SetTargets(monkeys[2], monkeys[5]);
        monkeys[5].SetTargets(monkeys[2], monkeys[1]);
        monkeys[6].SetTargets(monkeys[4], monkeys[7]);
        monkeys[7].SetTargets(monkeys[4], monkeys[5]);
    }

    public static List<MonkeyTest> SetMonkeyTests(int[] testPrimes)
    {
        List<MonkeyTest> monkeyTests = new();
        

        foreach (int prime in testPrimes)
        {
            monkeyTests.Add((long item) => item % prime == 0);
        }

        return monkeyTests;
    }
    public static List<MonkeyOperation> SetMonkeyOperations()
    {
        List<MonkeyOperation> monkeyOperations = new();

        monkeyOperations.Add((long item) => item * 17);
        monkeyOperations.Add((long item) => item + 2);
        monkeyOperations.Add((long item) => item + 1);
        monkeyOperations.Add((long item) => item + 7);
        monkeyOperations.Add((long item) => item * item);
        monkeyOperations.Add((long item) => item + 8);
        monkeyOperations.Add((long item) => item * 2);
        monkeyOperations.Add((long item) => item + 6);

        return monkeyOperations;
    }

    public static void SetMonkeyItems(List<Monkey> monkeys)
    {
        List<long[]> itemArrays = new()
        {
            new long[] { 59, 65, 86, 56, 74, 57, 56 },
            new long[] { 63, 83, 50, 63, 56 },
            new long[] { 93, 79, 74, 55 },
            new long[] { 86, 61, 67, 88, 94, 69, 56, 91 },
            new long[] { 76, 50, 51 },
            new long[] { 77, 76 },
            new long[] { 74 },
            new long[] { 86, 85, 52, 86, 91, 95 }
        };

        for(int i = 0; i < monkeys.Count; i++)
        {
            monkeys[i].AddItem(itemArrays[i]);
        }
    }

    public static void SetMonkeyLCM(int[] primes, List<Monkey> monkeys)
    {
        int lcm = 1;
        foreach (int num in primes)
        {
            lcm *= num;
        }
        foreach (Monkey monkey in monkeys)
        {
            monkey.LCM = lcm;
        }
    }
}

public delegate long MonkeyOperation(long item);
public delegate bool MonkeyTest(long item);

public class Monkey
{
    private MonkeyOperation _operation;
    private MonkeyTest _test;
    private (Monkey firstTarget, Monkey secondTarget) _targets;
    private List<long> _heldItems;
    private bool _part1;

    public int Identifier { get; }
    public int LCM { get; set; }
    public long NumberOfInspectedItems { get; set; }

    public void InspectAll()
    {
        long[] itemsTemp = _heldItems.ToArray();
        _heldItems.Clear();
        foreach(int item in itemsTemp)
        {
            Inspect(item);
        }
    }
    public void Inspect(long item)
    {
        long adjustedItemValue = Operate(item);
        Test(adjustedItemValue);
        NumberOfInspectedItems++;
    }
    public long Operate(long item)
    {
        long newItemValue = _operation(item);

        if (_part1) return newItemValue / 3;
        //9699690 is the lcm of every monkey's test value
        else return newItemValue % LCM;
    }
    public void Test(long item)
    {
        if (_test(item)) Throw(item, _targets.firstTarget);
        else Throw(item, _targets.secondTarget);
    }
    public void AddItem(long item)
    {
        _heldItems.Add(item);
    }
    public void AddItem(long[] itemArray)
    {
        foreach(long item in itemArray)
        {
            AddItem(item);
        }
    }
    public void Throw(long item, Monkey target)
    {
        target.AddItem(item);
    }
    public Monkey(int identifier, MonkeyOperation operation, MonkeyTest test, bool part1)
    {
        _part1 = part1;
        _test = test;
        _operation = operation;
        _heldItems = new();
        Identifier = identifier;
    }
    
    public void SetTargets(Monkey target1, Monkey target2)
    {
        _targets.firstTarget = target1;
        _targets.secondTarget = target2;
    }

    public override string ToString()
    {
        return $"Monkey {Identifier}";
    }
}