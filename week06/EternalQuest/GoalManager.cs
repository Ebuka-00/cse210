using System;
using System.Collections.Generic;
using System.IO;

public class GoalManager
{
    private List<Goal> _goals;
    private int _score;

    public GoalManager()
    {
        _goals = new List<Goal>();
        _score = 0;
    }

    public void Start()
    {
        bool running = true;

        while (running)
        {
            DisplayStatus();

            Console.WriteLine();
            Console.WriteLine("Menu:");
            Console.WriteLine("  1. Create New Goal");
            Console.WriteLine("  2. List Goals");
            Console.WriteLine("  3. Record Event");
            Console.WriteLine("  4. Save Goals");
            Console.WriteLine("  5. Load Goals");
            Console.WriteLine("  6. Quit");
            Console.Write("Select a choice: ");

            string choice = Console.ReadLine();

            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    CreateGoal();
                    break;

                case "2":
                    ListGoals();
                    break;

                case "3":
                    RecordEvent();
                    break;

                case "4":
                    SaveGoals();
                    break;

                case "5":
                    LoadGoals();
                    break;

                case "6":
                    running = false;
                    Console.WriteLine("Thanks for playing Eternal Quest!");
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please select 1-6.");
                    break;
            }

            if (running)
            {
                Console.WriteLine();
                Console.WriteLine("Press ENTER to continue...");
                Console.ReadLine();
            }
        }
    }

    private void DisplayStatus()
    {
        int level = GetLevel();

        Console.WriteLine("==========================================");
        Console.WriteLine("             ETERNAL QUEST");
        Console.WriteLine("==========================================");
        Console.WriteLine($"Score: {_score}");
        Console.WriteLine($"Level: {level}");
        Console.WriteLine($"Progress to next level: {_score % 100}/100 XP");
        Console.WriteLine("==========================================");
    }

    private int GetLevel()
    {
        return (_score / 100) + 1;
    }

    private void CreateGoal()
    {
        Console.WriteLine("The types of goals are:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.Write("Which type of goal would you like to create? ");

        string type = Console.ReadLine();

        Console.Write("What is the name of your goal? ");
        string name = Console.ReadLine();

        Console.Write("What is a short description of it? ");
        string description = Console.ReadLine();

        int points = ReadInt("What is the amount of points associated with this goal? ");

        switch (type)
        {
            case "1":
                _goals.Add(
                    new SimpleGoal(
                        name,
                        description,
                        points));

                Console.WriteLine("Simple goal created!");
                break;

            case "2":
                _goals.Add(
                    new EternalGoal(
                        name,
                        description,
                        points));

                Console.WriteLine("Eternal goal created!");
                break;

            case "3":
                int target = ReadInt(
                    "How many times does this goal need to be completed? ");

                int bonus = ReadInt(
                    "What is the bonus for completing the goal? ");

                _goals.Add(
                    new ChecklistGoal(
                        name,
                        description,
                        points,
                        target,
                        bonus));

                Console.WriteLine("Checklist goal created!");
                break;

            default:
                Console.WriteLine("Invalid goal type.");
                break;
        }
    }

    private void ListGoals()
    {
        Console.WriteLine("Your Goals:");

        if (_goals.Count == 0)
        {
            Console.WriteLine("You currently have no goals.");
            return;
        }

        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine(
                $"{i + 1}. {_goals[i].GetDetailsString()}");
        }
    }

    private void RecordEvent()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("You have no goals to record.");
            return;
        }

        ListGoals();

        int choice = ReadInt(
            "Which goal did you accomplish? ");

        if (choice < 1 || choice > _goals.Count)
        {
            Console.WriteLine("Invalid goal number.");
            return;
        }

        Goal selectedGoal = _goals[choice - 1];

        if (selectedGoal.IsComplete())
        {
            Console.WriteLine("This goal has already been completed.");
            return;
        }

        int pointsEarned = selectedGoal.RecordEvent();

        _score += pointsEarned;

        Console.WriteLine();
        Console.WriteLine(
            $"Congratulations! You earned {pointsEarned} points.");

        Console.WriteLine($"Your new score is {_score}.");

        CheckAchievements();
    }

    private void CheckAchievements()
    {
        if (_score >= 100 && _score - 100 < 50)
        {
            Console.WriteLine("🏆 Achievement unlocked: First 100 Points!");
        }

        if (_score >= 500 && _score - 500 < 50)
        {
            Console.WriteLine("🏆 Achievement unlocked: Eternal Warrior!");
        }

        if (_score >= 1000 && _score - 1000 < 50)
        {
            Console.WriteLine("🏆 Achievement unlocked: Quest Master!");
        }
    }

    private void SaveGoals()
    {
        Console.Write("Enter the filename to save to: ");
        string filename = Console.ReadLine();

        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            outputFile.WriteLine(_score);

            foreach (Goal goal in _goals)
            {
                outputFile.WriteLine(
                    goal.GetStringRepresentation());
            }
        }

        Console.WriteLine("Goals saved successfully!");
    }

    private void LoadGoals()
    {
        Console.Write("Enter the filename to load: ");
        string filename = Console.ReadLine();

        if (!File.Exists(filename))
        {
            Console.WriteLine("That file does not exist.");
            return;
        }

        string[] lines = File.ReadAllLines(filename);

        if (lines.Length == 0)
        {
            Console.WriteLine("The file is empty.");
            return;
        }

        _goals.Clear();

        _score = int.Parse(lines[0]);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split('|');

            string goalType = parts[0];

            switch (goalType)
            {
                case "SimpleGoal":
                    {
                        string name = parts[1];
                        string description = parts[2];
                        int points = int.Parse(parts[3]);
                        bool complete = bool.Parse(parts[4]);

                        _goals.Add(
                            new SimpleGoal(
                                name,
                                description,
                                points,
                                complete));

                        break;
                    }

                case "EternalGoal":
                    {
                        string name = parts[1];
                        string description = parts[2];
                        int points = int.Parse(parts[3]);

                        _goals.Add(
                            new EternalGoal(
                                name,
                                description,
                                points));

                        break;
                    }

                case "ChecklistGoal":
                    {
                        string name = parts[1];
                        string description = parts[2];
                        int points = int.Parse(parts[3]);
                        int target = int.Parse(parts[4]);
                        int bonus = int.Parse(parts[5]);
                        int completed = int.Parse(parts[6]);

                        _goals.Add(
                            new ChecklistGoal(
                                name,
                                description,
                                points,
                                target,
                                bonus,
                                completed));

                        break;
                    }
            }
        }

        Console.WriteLine("Goals loaded successfully!");
    }

    private int ReadInt(string message)
    {
        while (true)
        {
            Console.Write(message);

            string input = Console.ReadLine();

            if (int.TryParse(input, out int number))
            {
                return number;
            }

            Console.WriteLine("Please enter a valid number.");
        }
    }
}