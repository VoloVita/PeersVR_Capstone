using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Jsonconfig : MonoBehaviour
{
    private string lessonDataFilePath = "Assets/JamTech_Assets/JSON/lesson.json";
    private LessonData lessonData;

    void Start()
    {
        // Load lesson data
        lessonData = LoadLessonData();
        if (lessonData != null)
        {
            // Log Lesson 1 data
            Debug.Log("Lesson 1 Title: " + lessonData.lesson1.title);
            Debug.Log("Lesson 1 Description: " + lessonData.lesson1.description);
            Debug.Log("Lesson 1 Example Videos: ");
            foreach (string videoUrl in lessonData.lesson1.non_example_videos)
            {
                Debug.Log("- " + videoUrl);
            }
            Debug.Log("Lesson 1 Exercises: ");
            foreach (string exercise in lessonData.lesson1.exercises)
            {
                Debug.Log("- " + exercise);
            }

            // Log Lesson 2 data
            Debug.Log("Lesson 2 Title: " + lessonData.lesson2.title);
            Debug.Log("Lesson 2 Description: " + lessonData.lesson2.description);
            Debug.Log("Lesson 2 Example Videos: " + lessonData.lesson2.example_videos);
            foreach (string videoUrl in lessonData.lesson2.non_example_videos)
            {
                Debug.Log("- " + videoUrl);
            }
            Debug.Log("Lesson 2 Exercises: ");
            foreach (string exercise in lessonData.lesson2.exercises)
            {
                Debug.Log("- " + exercise);
            }
        }
    }

    public LessonData LoadLessonData()
    {
        if (File.Exists(lessonDataFilePath))
        {
            string json = File.ReadAllText(lessonDataFilePath);
            return JsonUtility.FromJson<LessonData>(json);
        }
        else
        {
            Debug.LogError("Lesson data file not found!");
            return null;
        }
    }



}
