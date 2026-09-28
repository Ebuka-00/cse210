using System;
using System.Collections.Generic;

// Hands out random items but never repeats one until every item has been used.
public class PromptPicker
{
    private List<string> _all;
    private List<string> _remaining = new List<string>();
    private Random _random = new Random();

    public PromptPicker(List<string> items)
    {
        _all = items;
    }

    public string Next()
    {
        if (_remaining.Count == 0)
        {
            _remaining = new List<string>(_all);
        }

        int index = _random.Next(_remaining.Count);
        string item = _remaining[index];
        _remaining.RemoveAt(index);
        return item;
    }
}