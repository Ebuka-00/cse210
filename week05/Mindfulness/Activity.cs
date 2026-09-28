using System;
using System.Threading;

// Base class: everything the three activities share lives here.
public abstract class Activity
{
    private string _name;
    private string _description;
    private int _duration;

    protected Activity(string name, string description)
    {
        _name = name;
        _description = description;
    }

    public string Name => _name;
    public int Duration => _duration;

    // Template method: every activity follows start -> body -> end.
    public void Run()
    {
        DisplayStartingMessage();
        RunActivity();
        DisplayEndingMessage();
    }

    // Each derived class supplies its own activity body.
    protected abstract void RunActivity();

    private void DisplayStartingMessage()
    {
        Console.Clear();
        Console.WriteLine($"Welcome to the {_name}.");
        Console.WriteLine();
        Console.WriteLine(_description);
        Console.WriteLine();
        _duration = AskForDuration();

        Console.Clear();
        Console.WriteLine("Get ready...");
        ShowSpinner(4);
        Console.WriteLine();
    }

    private void DisplayEndingMessage()
    {
        Console.WriteLine();
        Console.WriteLine("Well done!!");
        ShowSpinner(3);
        Console.WriteLine();
        Console.WriteLine($"You have completed another {_duration} seconds of the {_name}.");
        ShowSpinner(4);
    }

    private int AskForDuration()
    {
        while (true)
        {
            Console.Write("How long, in seconds, would you like for your session? ");
            if (int.TryParse(Console.ReadLine(), out int seconds) && seconds > 0)
            {
                return seconds;
            }
            Console.WriteLine("Please enter a whole number greater than 0.");
        }
    }

    protected DateTime GetEndTime()
    {
        return DateTime.Now.AddSeconds(_duration);
    }

    // Whole seconds left until endTime (0 when time is up).
    protected int SecondsLeft(DateTime endTime)
    {
        double left = (endTime - DateTime.Now).TotalSeconds;
        return left <= 0 ? 0 : (int)Math.Ceiling(left);
    }

    protected void ShowSpinner(int seconds)
    {
        string[] frames = { "|", "/", "-", "\\" };
        DateTime end = DateTime.Now.AddSeconds(seconds);
        int i = 0;

        while (DateTime.Now < end)
        {
            Console.Write(frames[i % frames.Length]);
            Thread.Sleep(250);
            Console.Write("\b \b");
            i++;
        }
    }

    protected void ShowCountDown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write(i);
            Thread.Sleep(1000);
            Console.Write("\b \b");
        }
    }
}