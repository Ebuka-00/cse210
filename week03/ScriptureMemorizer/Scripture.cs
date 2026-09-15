using System;
using System.Collections.Generic;
using System.Linq;

namespace ScriptureMemorizer
{
    /// <summary>
    /// Represents a full scripture: a Reference plus the list of Words
    /// that make up its text. Encapsulates all the logic for hiding
    /// words and reporting whether the scripture is fully hidden.
    /// </summary>
    public class Scripture
    {
        private readonly Reference _reference;
        private readonly List<Word> _words;
        private static readonly Random _random = new Random();

        public Scripture(Reference reference, string text)
        {
            _reference = reference;
            // Split on whitespace so punctuation stays attached to its word.
            _words = text
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(w => new Word(w))
                .ToList();
        }

        /// <summary>
        /// Builds the full display text: reference on its own line,
        /// followed by the scripture text with hidden words shown as
        /// underscores.
        /// </summary>
        public string GetDisplayText()
        {
            string words = string.Join(" ", _words.Select(w => w.GetDisplayText()));
            return $"{_reference.GetDisplayText()}{Environment.NewLine}{Environment.NewLine}{words}";
        }

        /// <summary>
        /// Hides up to <paramref name="numberOfWords"/> randomly chosen
        /// words that are not already hidden (the stretch-challenge
        /// behavior: never "wastes" a turn re-hiding a word that is
        /// already hidden).
        /// </summary>
        public void HideRandomWords(int numberOfWords)
        {
            List<Word> hideable = _words.Where(w => !w.IsHidden).ToList();

            int amountToHide = Math.Min(numberOfWords, hideable.Count);
            for (int i = 0; i < amountToHide; i++)
            {
                int index = _random.Next(hideable.Count);
                hideable[index].Hide();
                hideable.RemoveAt(index);
            }
        }

        /// <summary>
        /// True once every word in the scripture has been hidden.
        /// </summary>
        public bool IsCompletelyHidden()
        {
            return _words.All(w => w.IsHidden);
        }

        /// <summary>
        /// Percentage (0-100) of words currently hidden. Used to show
        /// the user their memorization progress.
        /// </summary>
        public int PercentHidden()
        {
            if (_words.Count == 0)
            {
                return 100;
            }
            int hiddenCount = _words.Count(w => w.IsHidden);
            return (int)Math.Round(100.0 * hiddenCount / _words.Count);
        }
    }
}