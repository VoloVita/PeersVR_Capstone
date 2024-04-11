using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Jsonconfig : MonoBehaviour
{
    private string lessonDataFilePath;
    private LessonData lessonData;

    void Start()
    {

    }
    /// <summary>
    ///  This function opens the content JSON file and makes a playerData object
    /// </summary>
    /// <param name="whichPath"></param> 1 for lesson data, 2 for quiz data
    /// <returns></returns> Returns a playerData object
    public LessonData LoadLessonData(int whichPath)
    {
        switch (whichPath)
        {
            case 1:
                lessonDataFilePath = "Assets/JamTech_Assets/JSON/lesson.json";
                break;
            case 2:
                lessonDataFilePath = "Assets/JamTech_Assets/JSON/quiz.json";
        }

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
