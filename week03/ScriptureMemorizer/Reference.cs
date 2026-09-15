namespace ScriptureMemorizer
{
    /// <summary>
    /// Represents the reference for a scripture, e.g. "John 3:16" or
    /// "Proverbs 3:5-6". Supports both single-verse and verse-range
    /// references via overloaded constructors.
    /// </summary>
    public class Reference
    {
        private readonly string _book;
        private readonly int _chapter;
        private readonly int _startVerse;
        private readonly int _endVerse;

        /// <summary>
        /// Constructor for a single-verse reference, e.g. Reference("John", 3, 16).
        /// </summary>
        public Reference(string book, int chapter, int verse)
            : this(book, chapter, verse, verse)
        {
        }

        /// <summary>
        /// Constructor for a verse-range reference, e.g. Reference("Proverbs", 3, 5, 6).
        /// </summary>
        public Reference(string book, int chapter, int startVerse, int endVerse)
        {
            _book = book;
            _chapter = chapter;
            _startVerse = startVerse;
            _endVerse = endVerse;
        }

        /// <summary>
        /// Returns the reference formatted for display, e.g. "John 3:16"
        /// or "Proverbs 3:5-6".
        /// </summary>
        public string GetDisplayText()
        {
            if (_startVerse == _endVerse)
            {
                return $"{_book} {_chapter}:{_startVerse}";
            }
            return $"{_book} {_chapter}:{_startVerse}-{_endVerse}";
        }
    }
}