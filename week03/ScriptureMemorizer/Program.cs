using System;
using System.Collections.Generic;

// Extended requirements:
// - Stretch challenge completed: HideRandomWords only selects from words that
//   are not already hidden (see Scripture.HideRandomWords).
// - Built a small library of scriptures instead of a single hardcoded one;
//   a random scripture from the library is chosen each time the program runs.
// - Each round hides 3 words at a time instead of 1, so the pace of the
//   memorization exercise feels closer to the demo video.

class Program
{
    static void Main(string[] args)
    {
        List<Scripture> library = new List<Scripture>()
        {
            new Scripture(
                new Reference("John", 3, 16),
                "For God so loved the world, that he gave his only begotten Son, " +
                "that whosoever believeth in him should not perish, but have everlasting life."
            ),
            new Scripture(
                new Reference("Proverbs", 3, 5, 6),
                "Trust in the Lord with all thine heart, and lean not unto thine own understanding. " +
                "In all thy ways acknowledge him, and he shall direct thy paths."
            ),
            new Scripture(
                new Reference("Philippians", 4, 13),
                "I can do all things through Christ which strengtheneth me."
            )
        };

        Random random = new Random();
        Scripture scripture = library[random.Next(library.Count)];

        while (true)
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine();

            if (scripture.IsCompletelyHidden())
            {
                break;
            }

            Console.Write("Press enter to continue or type 'quit' to end: ");
            string userInput = Console.ReadLine();

            if (userInput == "quit")
            {
                break;
            }

            scripture.HideRandomWords(3);
        }
    }
}