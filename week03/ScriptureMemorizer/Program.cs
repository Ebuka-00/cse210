using System;

namespace ScriptureMemorizer
{
    public static class Program
    {
        public static void Main()
        {
            ScriptureLibrary library = BuildLibrary();

            int completedCount = 0;
            bool keepGoing = true;

            while (keepGoing)
            {
                Scripture scripture = library.GetRandomScripture();
                RunMemorizationSession(scripture);
                completedCount++;

                Console.WriteLine();
                Console.WriteLine($"Nice work! You've completed {completedCount} scripture(s) this session.");
                Console.WriteLine("Press enter to practice another scripture, or type quit to exit:");
                string? response = Console.ReadLine();

                if (IsQuit(response))
                {
                    keepGoing = false;
                }
            }

            Console.WriteLine();
            Console.WriteLine("Goodbye, and happy memorizing!");
        }

        /// <summary>
        /// Runs the show/hide loop for a single scripture: display, prompt,
        /// hide a few more not-yet-hidden words, repeat until every word
        /// is hidden or the user quits.
        /// </summary>
        private static void RunMemorizationSession(Scripture scripture)
        {
            const int wordsToHidePerRound = 3;

            while (true)
            {
                Console.Clear();
                Console.WriteLine(scripture.GetDisplayText());
                Console.WriteLine();
                Console.WriteLine($"({scripture.PercentHidden()}% hidden)");

                if (scripture.IsCompletelyHidden())
                {
                    break;
                }

                Console.WriteLine();
                Console.WriteLine("Press enter to continue, or type quit to exit:");
                string? response = Console.ReadLine();

                if (IsQuit(response))
                {
                    Environment.Exit(0);
                }

                scripture.HideRandomWords(wordsToHidePerRound);
            }
        }

        private static bool IsQuit(string? input)
        {
            return string.Equals(input?.Trim(), "quit", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Builds the built-in scripture library and then layers on any
        /// user-supplied scriptures from scriptures.txt, if present.
        /// </summary>
        private static ScriptureLibrary BuildLibrary()
        {
            var library = new ScriptureLibrary();

            library.Add(new Scripture(
                new Reference("John", 3, 16),
                "For God so loved the world, that he gave his only begotten Son, " +
                "that whosoever believeth in him should not perish, but have everlasting life."));

            library.Add(new Scripture(
                new Reference("Proverbs", 3, 5, 6),
                "Trust in the Lord with all thine heart, and lean not unto thine own understanding. " +
                "In all thy ways acknowledge him, and he shall direct thy paths."));

            library.Add(new Scripture(
                new Reference("Joshua", 1, 9),
                "Have not I commanded thee? Be strong and of a good courage; be not afraid, " +
                "neither be thou dismayed: for the Lord thy God is with thee whithersoever thou goest."));

            library.Add(new Scripture(
                new Reference("Philippians", 4, 13),
                "I can do all things through Christ which strengtheneth me."));

            library.Add(new Scripture(
                new Reference("Psalm", 23, 1, 4),
                "The Lord is my shepherd; I shall not want. He maketh me to lie down in green pastures: " +
                "he leadeth me beside the still waters. He restoreth my soul: he leadeth me in the paths " +
                "of righteousness for his name's sake. Yea, though I walk through the valley of the shadow " +
                "of death, I will fear no evil: for thou art with me; thy rod and thy staff they comfort me."));

            library.LoadFromFile("scriptures.txt");

            return library;
        }
    }
}