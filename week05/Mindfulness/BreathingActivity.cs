using System;
using System.Threading;

public class BreathingActivity : Activity
{
    private int _inSeconds = 4;
    private int _outSeconds = 6;

    public BreathingActivity() : base(
        "Breathing Activity",
        "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
    }

    protected override void RunActivity()
    {
        DateTime end = GetEndTime();

        while (SecondsLeft(end) > 0)
        {
            Breathe("Breathe in...", Math.Min(_inSeconds, SecondsLeft(end)), true);

            if (SecondsLeft(end) == 0) break;

            Breathe("Breathe out...", Math.Min(_outSeconds, SecondsLeft(end)), false);
        }
    }

    // Animated bar with a countdown. The bar moves quickly at first and slows
    // toward the end of the breath (ease-out). It fills on inhale, empties on exhale.
    private void Breathe(string message, int seconds, bool inhale)
    {
        const int width = 20;
        DateTime start = DateTime.Now;
        double elapsed;

        while ((elapsed = (DateTime.Now - start).TotalSeconds) < seconds)
        {
            double t = elapsed / seconds;
            double eased = 1 - Math.Pow(1 - t, 2);
            double fraction = inhale ? eased : 1 - eased;
            int filled = (int)Math.Round(fraction * width);
            int remaining = (int)Math.Ceiling(seconds - elapsed);

            Console.Write($"\r{message} [{new string('#', filled)}{new string(' ', width - filled)}] {remaining} ");
            Thread.Sleep(50);
        }

        Console.WriteLine();
    }
}