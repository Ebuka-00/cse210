using System;
using System.Collections.Generic;

public class ReflectionActivity : Activity
{
    private readonly List<string> _prompts;
    private readonly List<string> _questions;
    private readonly Random _random;

    public ReflectionActivity()
        : base(
            "Reflection Activity",
            "This activity will help you reflect on times in your life when you have shown " +
            "strength and resilience. This will help you recognize the power you have and " +
            "how you can use it in other aspects of your life.")
    {
        _random = new Random();

        _prompts = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        _questions = new List<string>
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };
    }

    public void Run()
    {
        DisplayStartingMessage();

        Console.WriteLine("Consider the following prompt:");
        Console.WriteLine();

        string prompt = _prompts[_random.Next(_prompts.Count)];

        Console.WriteLine($"--- {prompt} ---");
        Console.WriteLine();

        Console.WriteLine("When you have something in mind, begin reflecting on the questions.");
        ThreadHelper.Spinner(5);

        Console.WriteLine();

        int duration = GetDuration();
        DateTime endTime = DateTime.Now.AddSeconds(duration);

        while (DateTime.Now < endTime)
        {
            string question = _questions[_random.Next(_questions.Count)];

            Console.WriteLine();
            Console.WriteLine(question);
            Console.WriteLine();

            int remainingSeconds = (int)(endTime - DateTime.Now).TotalSeconds;

            if (remainingSeconds <= 0)
            {
                break;
            }

            int pauseTime = Math.Min(5, remainingSeconds);

            Console.Write("Thinking ");
            ThreadHelper.Spinner(pauseTime);
        }

        DisplayEndingMessage();
    }
}