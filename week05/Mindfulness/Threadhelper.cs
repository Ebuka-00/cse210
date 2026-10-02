using System;
using System.Threading;

public static class ThreadHelper
{
    public static void Pause(int seconds)
    {
        Thread.Sleep(seconds * 1000);
    }

    public static void Spinner(int seconds)
    {
        string[] animation = { "|", "/", "-", "\\" };

        DateTime endTime = DateTime.Now.AddSeconds(seconds);

        int index = 0;

        while (DateTime.Now < endTime)
        {
            Console.Write(animation[index % animation.Length]);

            Thread.Sleep(250);

            Console.Write("\b \b");

            index++;
        }
    }

    public static void Countdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write(i);
            Thread.Sleep(1000);
            Console.Write("\b \b");
        }
    }
}