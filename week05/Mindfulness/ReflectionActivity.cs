using System;
using System.Collections.Generic;
using System.IO;

// Tracks how many times each activity was done and the total seconds spent.
// Saved to and loaded from a text file, one line per activity: name|count|seconds
public class ActivityLog
{
    private Dictionary<string, (int Count, int Seconds)> _entries = new Dictionary<string, (int, int)>();
    private string _filePath;

    public ActivityLog(string filePath)
    {
        _filePath = filePath;
    }

    public void Record(string activityName, int seconds)
    {
        _entries.TryGetValue(activityName, out var current);
        _entries[activityName] = (current.Count + 1, current.Seconds + seconds);
    }

    public void Display()
    {
        Console.Clear();
        Console.WriteLine("Your mindfulness log");
        Console.WriteLine("--------------------");

        if (_entries.Count == 0)
        {
            Console.WriteLine("No activities recorded yet.");
            return;
        }

        foreach (var pair in _entries)
        {
            Console.WriteLine($"{pair.Key}: {pair.Value.Count} time(s), {pair.Value.Seconds} seconds total");
        }
    }

    public void Save()
    {
        List<string> lines = new List<string>();
        foreach (var pair in _entries)
        {
            lines.Add($"{pair.Key}|{pair.Value.Count}|{pair.Value.Seconds}");
        }
        File.WriteAllLines(_filePath, lines);
    }

    public void Load()
    {
        if (!File.Exists(_filePath)) return;

        foreach (string line in File.ReadAllLines(_filePath))
        {
            string[] parts = line.Split('|');
            if (parts.Length == 3 && int.TryParse(parts[1], out int count) && int.TryParse(parts[2], out int seconds))
            {
                _entries[parts[0]] = (count, seconds);
            }
        }
    }
}