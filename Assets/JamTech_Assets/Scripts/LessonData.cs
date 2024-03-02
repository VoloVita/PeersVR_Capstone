using System;

[Serializable]
public class LessonData {
    public Lesson lesson1;
    public Lesson lesson2;
}

[Serializable]
public class Lesson {
    public string title;
    public string[] non_example_videos;
    public string description;
    public string more_info;
    public string example_videos;
    public string[] exercises;
    public string quiz_url;
}
