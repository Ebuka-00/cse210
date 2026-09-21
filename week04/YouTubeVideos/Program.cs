using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Create videos
        Video video1 = new Video(
            "Learning C# for Beginners",
            "Code Academy",
            600
        );

        video1.AddComment(new Comment("John", "This video helped me understand C#."));
        video1.AddComment(new Comment("Sarah", "Very clear explanation!"));
        video1.AddComment(new Comment("Mike", "I learned a lot from this."));
        video1.AddComment(new Comment("David", "Thanks for sharing!"));

        Video video2 = new Video(
            "How to Build a Website",
            "Web Dev Tutorials",
            900
        );

        video2.AddComment(new Comment("James", "Great tutorial."));
        video2.AddComment(new Comment("Emily", "This was exactly what I needed."));
        video2.AddComment(new Comment("Daniel", "Can you make a part two?"));

        Video video3 = new Video(
            "Introduction to Databases",
            "Tech Learning",
            750
        );

        video3.AddComment(new Comment("Chris", "The database explanation was great."));
        video3.AddComment(new Comment("Anna", "Very useful information."));
        video3.AddComment(new Comment("Peter", "I finally understand databases."));
        video3.AddComment(new Comment("Grace", "Nice work!"));

        Video video4 = new Video(
            "Git and GitHub Basics",
            "Developer World",
            500
        );

        video4.AddComment(new Comment("Mark", "GitHub makes collaboration easier."));
        video4.AddComment(new Comment("Linda", "Thanks for the explanation."));
        video4.AddComment(new Comment("Paul", "This helped me with my school project."));

        // Put all videos into a list
        List<Video> videos = new List<Video>
        {
            video1,
            video2,
            video3,
            video4
        };

        // Display each video and its comments
        foreach (Video video in videos)
        {
            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.Length} seconds");
            Console.WriteLine($"Number of comments: {video.GetNumberOfComments()}");

            Console.WriteLine("Comments:");

            foreach (Comment comment in video.GetComments())
            {
                Console.WriteLine($"- {comment.Name}: {comment.Text}");
            }

            Console.WriteLine();
        }
    }
}