using System;

public class BreathingActivity : Activity
{
    public BreathingActivity()
        : base(
            "Breathing Activity",
            "This activity will help you relax by walking you through breathing in and out slowly. " +
            "Clear your mind and focus on your breathing.")
    {
    }

    public void Run()
    {
        DisplayStartingMessage();

        int duration = GetDuration();
        DateTime endTime = DateTime.Now.AddSeconds(duration);

        Console.WriteLine("Focus on your breathing.");
        Console.WriteLine();

        bool breatheIn = true;

        while (DateTime.Now < endTime)
        {
            int remainingSeconds = (int)(endTime - DateTime.Now).TotalSeconds;

            if (remainingSeconds <= 0)
            {
                break;
            }

            if (breatheIn)
            {
                Console.WriteLine("Breathe in...");
                ThreadHelper.Countdown(Math.Min(4, remainingSeconds));
            }
            else
            {
                Console.WriteLine("Breathe out...");
                ThreadHelper.Countdown(Math.Min(4, remainingSeconds));
            }

            breatheIn = !breatheIn;
        }

        DisplayEndingMessage();
    }
}