using System;

public class Activity
{
    private string _name;
    private string _description;
    private int _duration;

    public Activity(string name, string description)
    {
        _name = name;
        _description = description;
        _duration = 0;
    }

    public string GetName()
    {
        return _name;
    }

    public string GetDescription()
    {
        return _description;
    }

    public int GetDuration()
    {
        return _duration;
    }

    protected void SetDuration(int duration)
    {
        _duration = duration;
    }

    public void DisplayStartingMessage()
    {
        Console.Clear();

        Console.WriteLine($"Starting {_name}");
        Console.WriteLine();
        Console.WriteLine(_description);
        Console.WriteLine();

        Console.Write("How long, in seconds, would you like for your session? ");

        int duration;

        while (!int.TryParse(Console.ReadLine(), out duration) || duration <= 0)
        {
            Console.Write("Please enter a positive number of seconds: ");
        }

        _duration = duration;

        Console.WriteLine();
        Console.WriteLine("Get ready...");
        ThreadHelper.Spinner(3);

        Console.WriteLine();
    }

    public void DisplayEndingMessage()
    {
        Console.WriteLine();
        Console.WriteLine("Well done!!");
        Console.WriteLine();

        Console.WriteLine(
            $"You have completed another {_duration} seconds of the {_name}."
        );

        ThreadHelper.Spinner(3);

        Console.WriteLine();
    }
}