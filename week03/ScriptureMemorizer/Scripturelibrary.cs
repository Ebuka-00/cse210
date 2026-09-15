using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ScriptureMemorizer
{
    /// <summary>
    /// Holds a collection of scriptures and hands them out one at a
    /// time in random order, without repeating one until every
    /// scripture in the library has been offered once. This is one of
    /// the "exceeds requirements" features: rather than memorizing a
    /// single hard-coded scripture, the user works through a whole
    /// library, picked randomly, optionally loaded from a text file.
    /// </summary>
    public class ScriptureLibrary
    {
        private readonly List<Scripture> _allScriptures;
        private readonly List<Scripture> _remaining;
        private static readonly Random _random = new Random();

        public ScriptureLibrary()
        {
            _allScriptures = new List<Scripture>();
            _remaining = new List<Scripture>();
        }

        public void Add(Scripture scripture)
        {
            _allScriptures.Add(scripture);
            _remaining.Add(scripture);
        }

        public int Count => _allScriptures.Count;

        public bool HasScripturesRemaining => _remaining.Count > 0;

        /// <summary>
        /// Returns a random scripture that has not yet been offered in
        /// this run, and removes it from the "remaining" pool. Once
        /// every scripture has been served, the pool automatically
        /// refills so the library can be used indefinitely.
        /// </summary>
        public Scripture GetRandomScripture()
        {
            if (_remaining.Count == 0)
            {
                _remaining.AddRange(_allScriptures);
            }

            int index = _random.Next(_remaining.Count);
            Scripture chosen = _remaining[index];
            _remaining.RemoveAt(index);
            return chosen;
        }

        /// <summary>
        /// Loads additional scriptures from a simple pipe-delimited text
        /// file, one scripture per line, in the format:
        ///   Book|Chapter|Verse|Text                (single verse)
        ///   Book|Chapter|StartVerse|EndVerse|Text   (verse range)
        /// Blank lines and lines starting with '#' are ignored.
        /// Missing or malformed files are ignored quietly so the
        /// program still runs with its built-in scriptures.
        /// </summary>
        public void LoadFromFile(string path)
        {
            if (!File.Exists(path))
            {
                return;
            }

            foreach (string rawLine in File.ReadAllLines(path))
            {
                string line = rawLine.Trim();
                if (line.Length == 0 || line.StartsWith("#"))
                {
                    continue;
                }

                string[] parts = line.Split('|');
                try
                {
                    if (parts.Length == 4)
                    {
                        string book = parts[0].Trim();
                        int chapter = int.Parse(parts[1].Trim());
                        int verse = int.Parse(parts[2].Trim());
                        string text = parts[3].Trim();
                        Add(new Scripture(new Reference(book, chapter, verse), text));
                    }
                    else if (parts.Length == 5)
                    {
                        string book = parts[0].Trim();
                        int chapter = int.Parse(parts[1].Trim());
                        int startVerse = int.Parse(parts[2].Trim());
                        int endVerse = int.Parse(parts[3].Trim());
                        string text = parts[4].Trim();
                        Add(new Scripture(new Reference(book, chapter, startVerse, endVerse), text));
                    }
                    // Lines with any other number of fields are silently skipped.
                }
                catch (FormatException)
                {
                    // Skip malformed lines rather than crashing the program.
                }
            }
        }
    }
}