using System;
using System.Collections.Generic;

public class ListingActivity : Activity
{
    private PromptPicker _prompts = new PromptPicker(new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    });

    public ListingActivity() : base(
        "Listing Activity",
        "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
    }

    protected override void RunActivity()
    {
        Console.WriteLine("List as many responses as you can to the following prompt:");
        Console.WriteLine();
        Console.WriteLine($"--- {_prompts.Next()} ---");
        Console.WriteLine();
        Console.Write("You may begin in: ");
        ShowCountDown(5);
        Console.WriteLine();
        Console.WriteLine();

        List<string> items = CollectItems(GetEndTime());

        Console.WriteLine();
        Console.WriteLine($"You listed {items.Count} items!");
    }

    // Polls the keyboard so the clock is still checked while the user is idle.
    // Note: once a user starts typing a line, they finish it with Enter.
    private List<string> CollectItems(DateTime end)
    {
        List<string> items = new List<string>();

        Console.Write("> ");
        while (DateTime.Now < end)
        {
            if (Console.KeyAvailable)
            {
                string entry = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(entry))
                {
                    items.Add(entry);
                }
                if (DateTime.Now < end)
                {
                    Console.Write("> ");
                }
            }
            else
            {
                System.Threading.Thread.Sleep(50);
            }
        }

        return items;
    }
}