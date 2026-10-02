using System;

class Program
{
    static void Main(string[] args)
    {
        // Creativity and exceeding requirements:
        // This program includes a session counter that tracks how many
        // mindfulness activities have been completed during the current run.
        // It also includes animated countdowns and a spinner to make
        // the experience more interactive.

        int completedActivities = 0;
        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("Mindfulness Program");
            Console.WriteLine();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Start breathing activity");
            Console.WriteLine("  2. Start reflecting activity");
            Console.WriteLine("  3. Start listing activity");
            Console.WriteLine("  4. Show completed activities");
            Console.WriteLine("  5. Quit");
            Console.WriteLine();
            Console.Write("Select a choice from the menu: ");

            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    BreathingActivity breathing = new BreathingActivity();
                    breathing.Run();
                    completedActivities++;
                    PauseBeforeMenu();
                    break;

                case "2":
                    ReflectionActivity reflection = new ReflectionActivity();
                    reflection.Run();
                    completedActivities++;
                    PauseBeforeMenu();
                    break;

                case "3":
                    ListingActivity listing = new ListingActivity();
                    listing.Run();
                    completedActivities++;
                    PauseBeforeMenu();
                    break;

                case "4":
                    Console.Clear();
                    Console.WriteLine($"You have completed {completedActivities} mindfulness activity/activities this session.");
                    PauseBeforeMenu();
                    break;

                case "5":
                    running = false;
                    Console.WriteLine();
                    Console.WriteLine("Thank you for using the Mindfulness Program.");
                    break;

                default:
                    Console.WriteLine();
                    Console.WriteLine("Invalid choice. Please select 1, 2, 3, 4, or 5.");
                    ThreadHelper.Pause(2);
                    break;
            }
        }
    }

    private static void PauseBeforeMenu()
    {
        Console.WriteLine();
        Console.WriteLine("Returning to the menu...");
        ThreadHelper.Pause(2);
    }
}