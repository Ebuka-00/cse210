using System;
using System.Collections.Generic;

class Scripture
{
    private Reference _reference;
    private List<Word> _words;
    private Random _random = new Random();

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = new List<Word>();

        string[] splitWords = text.Split(" ");
        foreach (string word in splitWords)
        {
            _words.Add(new Word(word));
        }
    }

    public string GetDisplayText()
    {
        string text = _reference.GetDisplayText() + "\n";

        foreach (Word word in _words)
        {
            text += word.GetDisplayText() + " ";
        }

        return text.Trim();
    }

    // Stretch requirement: only chooses from words that are not already hidden.
    public void HideRandomWords(int numberToHide)
    {
        List<Word> hiddenCandidates = new List<Word>();
        foreach (Word word in _words)
        {
            if (!word.IsHidden())
            {
                hiddenCandidates.Add(word);
            }
        }

        for (int i = 0; i < numberToHide && hiddenCandidates.Count > 0; i++)
        {
            int index = _random.Next(hiddenCandidates.Count);
            hiddenCandidates[index].Hide();
            hiddenCandidates.RemoveAt(index);
        }
    }

    public bool IsCompletelyHidden()
    {
        foreach (Word word in _words)
        {
            if (!word.IsHidden())
            {
                return false;
            }
        }
        return true;
    }
}