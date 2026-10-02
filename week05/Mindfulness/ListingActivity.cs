using System;
using System.Collections.Generic;

public class ListingActivity : Activity
{
    private readonly List<string> _prompts;
    private readonly Random _random;

    public ListingActivity()
        : base(
            "Listing Activity",
            "This activity will help you reflect on the good things in your life by having " +
            "you list as many things as you can in a certain area.")
    {
        _random = new Random();

        _prompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };
    }

    public void Run()
    {
        DisplayStartingMessage();

        string prompt = _prompts[_random.Next(_prompts.Count)];

        Console.WriteLine("List as many responses as you can to the following prompt:");
        Console.WriteLine();
        Console.WriteLine($"--- {prompt} ---");
        Console.WriteLine();

        Console.WriteLine("You will have a few seconds to prepare.");
        ThreadHelper.Countdown(5);

        Console.WriteLine();
        Console.WriteLine("Start listing your responses below.");
        Console.WriteLine();

        List<string> responses = new List<string>();

        int duration = GetDuration();

        DateTime endTime = DateTime.Now.AddSeconds(duration);

        while (DateTime.Now < endTime)
        {
            Console.Write("> ");

            string response = Console.ReadLine() ?? "";

            if (DateTime.Now >= endTime)
            {
                break;
            }

            if (!string.IsNullOrWhiteSpace(response))
            {
                responses.Add(response);
            }
        }

        Console.WriteLine();
        Console.WriteLine($"You listed {responses.Count} item(s)!");

        if (responses.Count > 0)
        {
            Console.WriteLine();
            Console.WriteLine("Your responses:");

            for (int i = 0; i < responses.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {responses[i]}");
            }
        }

        DisplayEndingMessage();
    }
}