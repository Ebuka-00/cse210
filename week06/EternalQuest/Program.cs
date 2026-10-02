using System;

class Program
{
    static void Main(string[] args)
    {
        /*
         * ETERNAL QUEST - CREATIVITY / EXCEEDING REQUIREMENTS
         *
         * In addition to the required Simple, Eternal, and Checklist
         * goals, I added a leveling system and achievement milestones.
         *
         * The user gains a level for every 100 points earned and receives
         * achievement messages when reaching important point milestones.
         *
         * These features were added to make the program feel more like
         * a real gamified goal-tracking application.
         */

        GoalManager manager = new GoalManager();

        manager.Start();
    }
}