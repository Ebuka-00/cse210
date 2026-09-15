using System.Text;

namespace ScriptureMemorizer
{
    /// <summary>
    /// Represents a single word within a scripture. A word knows its own
    /// text and whether it is currently hidden. It is responsible for
    /// deciding how it should be displayed (either as itself, or as a
    /// series of underscores matching its length).
    /// </summary>
    public class Word
    {
        private readonly string _text;
        private bool _isHidden;

        public Word(string text)
        {
            _text = text;
            _isHidden = false;
        }

        public bool IsHidden => _isHidden;

        public void Hide()
        {
            _isHidden = true;
        }

        public void Show()
        {
            _isHidden = false;
        }

        /// <summary>
        /// Returns the word as it should currently be displayed: the
        /// underlying text if it is visible, or a run of underscores
        /// (one per letter/number character, punctuation preserved)
        /// if it is hidden.
        /// </summary>
        public string GetDisplayText()
        {
            if (!_isHidden)
            {
                return _text;
            }

            var builder = new StringBuilder();
            foreach (char c in _text)
            {
                // Preserve punctuation attached to the word (e.g. "God," -> "____,")
                // so the hidden version still "looks" like a sentence.
                builder.Append(char.IsLetterOrDigit(c) ? '_' : c);
            }
            return builder.ToString();
        }
    }
}