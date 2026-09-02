using System;
using System.Collections.Generic;
using System.IO;

class Journal
{
    private List<Entry> _entries = new List<Entry>();
    private List<string> _prompts = new List<string>()
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?",
        "What is something I learned today?",
        "What am I grateful for today?"
    };

    private Random _random = new Random();

    public void WriteNewEntry()
    {
        string prompt = _prompts[_random.Next(_prompts.Count)];
        Console.WriteLine(prompt);
        Console.Write("> ");
        string response = Console.ReadLine();

        string date = DateTime.Now.ToShortDateString();

        Entry newEntry = new Entry(date, prompt, response);
        _entries.Add(newEntry);

        Console.WriteLine("Entry saved.");
        Console.WriteLine();
    }

    public void DisplayJournal()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("The journal is empty.");
            Console.WriteLine();
            return;
        }

        foreach (Entry entry in _entries)
        {
            entry.Display();
        }
    }

    public void SaveToFile()
    {
        Console.Write("What is the filename to save to? ");
        string filename = Console.ReadLine();

        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            foreach (Entry entry in _entries)
            {
                outputFile.WriteLine(entry.ToFileString());
            }
        }

        Console.WriteLine($"Journal saved to {filename}");
        Console.WriteLine();
    }

    public void LoadFromFile()
    {
        Console.Write("What is the filename to load from? ");
        string filename = Console.ReadLine();

        if (!File.Exists(filename))
        {
            Console.WriteLine("That file does not exist.");
            Console.WriteLine();
            return;
        }

        _entries.Clear();

        string[] lines = File.ReadAllLines(filename);
        foreach (string line in lines)
        {
            string[] parts = line.Split("~|~");
            string date = parts[0];
            string prompt = parts[1];
            string response = parts[2];

            Entry entry = new Entry(date, prompt, response);
            _entries.Add(entry);
        }

        Console.WriteLine($"Journal loaded from {filename}");
        Console.WriteLine();
    }
}