using System;

// Extended requirements:
// - Added extra journal prompts beyond the required five.
// - Journal prompts are chosen at random each time, using Journal's private
//   Random instance, so entries stay varied over time.
// - Entry file format uses a custom "~|~" separator, and Entry knows how to
//   turn itself into a single savable line (ToFileString), keeping the file
//   format logic close to the class that owns the data (abstraction).

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        bool running = true;

        while (running)
        {
            Console.WriteLine("Please select one of the following choices:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Quit");
            Console.Write("What would you like to do? ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            if (choice == "1")
            {
                journal.WriteNewEntry();
            }
            else if (choice == "2")
            {
                journal.DisplayJournal();
            }
            else if (choice == "3")
            {
                journal.SaveToFile();
            }
            else if (choice == "4")
            {
                journal.LoadFromFile();
            }
            else if (choice == "5")
            {
                running = false;
            }
            else
            {
                Console.WriteLine("That is not a valid choice. Please try again.");
                Console.WriteLine();
            }
        }
    }
}