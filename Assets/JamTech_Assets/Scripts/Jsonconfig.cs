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
