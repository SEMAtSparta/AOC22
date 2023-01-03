namespace Day11;

public class Program
{
    static void Main(string[] args)
    {
        List<Monkey> monkeyList = MonkeySetup();

        int rounds = 10_000;

        for(int i = 0; i < rounds; i++)
        {
            foreach (Monkey monkey in monkeyList)
            {
                monkey.InspectAll();
            }
        }

        long largest = 0, second = 0;

        foreach(Monkey monkey in monkeyList)
        {
            if(monkey.NumberOfInspectedItems > second)
            {
                if(monkey.NumberOfInspectedItems > largest)
                {
                    largest = monkey.NumberOfInspectedItems;
                }
                else
                {
                    second = monkey.NumberOfInspectedItems;
                }
            }
        }

        Console.WriteLine(largest * second);
    }

    public static List<Monkey> MonkeySetup()
    {
        MonkeyTest monkey0Test = (int item) => item % 3 == 0;
        MonkeyOperation monkey0Operation = (int item) => item * 17;

        MonkeyTest monkey1Test = (int item) => item % 13 == 0;
        MonkeyOperation monkey1Operation = (int item) => item + 2;

        MonkeyTest monkey2Test = (int item) => item % 2 == 0;
        MonkeyOperation monkey2Operation = (int item) => item + 1;

        MonkeyTest monkey3Test = (int item) => item % 11 == 0;
        MonkeyOperation monkey3Operation = (int item) => item + 7;

        MonkeyTest monkey4Test = (int item) => item % 19 == 0;
        MonkeyOperation monkey4Operation = (int item) => item * item;

        MonkeyTest monkey5Test = (int item) => item % 17 == 0;
        MonkeyOperation monkey5Operation = (int item) => item + 8;

        MonkeyTest monkey6Test = (int item) => item % 5 == 0;
        MonkeyOperation monkey6Operation = (int item) => item * 2;

        MonkeyTest monkey7Test = (int item) => item % 7 == 0;
        MonkeyOperation monkey7Operation = (int item) => item + 6;

        List<Monkey> monkeyList = new();

        Monkey monkey0 = new(0, monkey0Operation, monkey0Test);
        Monkey monkey1 = new(1, monkey1Operation, monkey1Test);
        Monkey monkey2 = new(2, monkey2Operation, monkey2Test);
        Monkey monkey3 = new(3, monkey3Operation, monkey3Test);
        Monkey monkey4 = new(4, monkey4Operation, monkey4Test);
        Monkey monkey5 = new(5, monkey5Operation, monkey5Test);
        Monkey monkey6 = new(6, monkey6Operation, monkey6Test);
        Monkey monkey7 = new(7, monkey7Operation, monkey7Test);

        foreach (int item in new int[] { 59, 65, 86, 56, 74, 57, 56 })
        {
            monkey0.AddItem(item);
        }
        foreach (int item in new int[] { 63, 83, 50, 63, 56 })
        {
            monkey1.AddItem(item);
        }
        foreach (int item in new int[] { 93, 79, 74, 55 })
        {
            monkey2.AddItem(item);
        }
        foreach (int item in new int[] { 86, 61, 67, 88, 94, 69, 56, 91 })
        {
            monkey3.AddItem(item);
        }
        foreach (int item in new int[] { 76, 50, 51 })
        {
            monkey4.AddItem(item);
        }
        foreach (int item in new int[] { 77, 76 })
        {
            monkey5.AddItem(item);
        }
        foreach (int item in new int[] { 74 })
        {
            monkey6.AddItem(item);
        }
        foreach (int item in new int[] { 86, 85, 52, 86, 91, 95 })
        {
            monkey7.AddItem(item);
        }

        monkey0.SetTargets(monkey3, monkey6);
        monkey1.SetTargets(monkey3, monkey0);
        monkey2.SetTargets(monkey0, monkey1);
        monkey3.SetTargets(monkey6, monkey7);
        monkey4.SetTargets(monkey2, monkey5);
        monkey5.SetTargets(monkey2, monkey1);
        monkey6.SetTargets(monkey4, monkey7);
        monkey7.SetTargets(monkey4, monkey5);

        monkeyList.Add(monkey0);
        monkeyList.Add(monkey1);
        monkeyList.Add(monkey2);
        monkeyList.Add(monkey3);
        monkeyList.Add(monkey4);
        monkeyList.Add(monkey5);
        monkeyList.Add(monkey6);
        monkeyList.Add(monkey7);
        
        return monkeyList;
    }
}

public delegate int MonkeyOperation(int item);
public delegate bool MonkeyTest(int item);

public class Monkey
{
    private MonkeyOperation _operation;
    private MonkeyTest _test;
    private Monkey[] _targets;
    private Queue<int> _heldItems;

    public int Identifier { get; }
    public long NumberOfInspectedItems { get; set; }

    public void InspectAll()
    {
        while(_heldItems.Any())
        {
            int item = _heldItems.Dequeue();
            Inspect(item);
        }
    }

    public void Inspect(int item)
    {
        int adjustedItemValue = Operate(item);
        //bool test = Test(adjustedItemValue, out adjustedItemValue);
        bool test = Test(adjustedItemValue);
        if (test) Throw(adjustedItemValue, _targets[0]);
        else Throw(adjustedItemValue, _targets[1]);
        NumberOfInspectedItems++;
    }

    public int Operate(int item)
    {
        long newItemValue = _operation(item);
        int output = (int) newItemValue % 9_699_690;
        return output;
    }

    //part 1 test
    //public bool Test(int item, out int newItemValue)
    //{
    //    newItemValue = item / 3;
    //    return _test(newItemValue);
    //}

    //part 2 test
    public bool Test(int item)
    {
        bool test = _test(item);

        return test;
    }
    public void AddItem(int item)
    {
        _heldItems.Enqueue(item);
    }
    public void Throw(int item, Monkey target)
    {
        target.AddItem(item);
    }
    public Monkey(int identifier, MonkeyOperation operation, MonkeyTest test)
    {
        _test = test;
        _operation = operation;
        _heldItems = new();
        Identifier = identifier;
        _targets = new Monkey[2];
    }
    
    public void SetTargets(Monkey target1, Monkey target2)
    {
        _targets[0] = target1;
        _targets[1] = target2;
    }
}