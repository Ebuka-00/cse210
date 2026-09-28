using System;

/*
 * EXCEEDING REQUIREMENTS
 * 1. No repeats: PromptPicker shuffles through every prompt and question
 *    before any of them can appear again in a session.
 * 2. Activity log: ActivityLog counts how many times each activity was done
 *    and the total seconds spent, viewable from the menu.
 * 3. Save/load: the log is saved to mindfulness_log.txt on exit and loaded
 *    at startup, so progress carries across sessions.
 * 4. Breathing animation: a bar fills on "Breathe in" and empties on
 *    "Breathe out", moving quickly at first and slowing near the end
 *    of each breath, with a live countdown.
 */
class Program
{
    static void Main(string[] args)
    {
        ActivityLog log = new ActivityLog("mindfulness_log.txt");
        log.Load();

        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Start breathing activity");
            Console.WriteLine("  2. Start reflection activity");
            Console.WriteLine("  3. Start listing activity");
            Console.WriteLine("  4. View activity log");
            Console.WriteLine("  5. Quit");
            Console.Write("Select a choice from the menu: ");

            Activity activity = null;
            switch (Console.ReadLine())
            {
                case "1": activity = new BreathingActivity(); break;
                case "2": activity = new ReflectionActivity(); break;
                case "3": activity = new ListingActivity(); break;
                case "4":
                    log.Display();
                    Console.WriteLine();
                    Console.WriteLine("Press Enter to return to the menu.");
                    Console.ReadLine();
                    break;
                case "5":
                    running = false;
                    break;
            }

            if (activity != null)
            {
                activity.Run();
                log.Record(activity.Name, activity.Duration);
                log.Save();
            }
        }

        log.Save();
        Console.WriteLine("Goodbye!");
    }
}