using System;

[Serializable]
public class LessonData
{
    public Lesson lesson1;
    public Lesson lesson2;
    public Lesson lesson3;
    public Lesson lesson4;
    public Lesson lesson5;
}

[Serializable]
public class Lesson
{
    public string title;
    public string[] non_example_videos;
    public string[] non_example_thumbnails;
    public string description;
    public string more_info;
    public string[] non_example_titles;
    public string[] example_titles;
    public string[] example_videos;
    public string[] example_thumbnails;
    public string[] exercises;
    public string quiz_url;
}
