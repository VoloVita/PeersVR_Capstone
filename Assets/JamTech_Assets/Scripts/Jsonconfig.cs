using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Better.StreamingAssets;

public class Jsonconfig : MonoBehaviour
{
    private string lessonDataFilePath = "JSON/lesson.json";
    private string quizDataFilePath = "JSON/quiz.json";

    void Awake()
    {
        BetterStreamingAssets.Initialize();
    }

    /// <summary>
    ///  This function opens the content JSON file and makes a playerData object
    /// </summary>
    /// <returns>Returns a playerData object</returns>
    public LessonData LoadLessonData()
    {
        if (BetterStreamingAssets.FileExists(lessonDataFilePath))
        {
            using (var stream = BetterStreamingAssets.OpenRead(lessonDataFilePath))
            using (var reader = new StreamReader(stream))
            {
                string json = reader.ReadToEnd();
                return JsonUtility.FromJson<LessonData>(json);
            }
        }
        else
        {
            Debug.LogError("Lesson data file not found: " + lessonDataFilePath);
            return null;
        }
    }

    /// <summary>
    ///  This function opens the content JSON file and makes a quizData object
    /// </summary>
    /// <returns>Returns a quizData object</returns>
    public QuizData LoadQuizData()
    {
        if (BetterStreamingAssets.FileExists(quizDataFilePath))
        {
            using (var stream = BetterStreamingAssets.OpenRead(quizDataFilePath))
            using (var reader = new StreamReader(stream))
            {
                string json = reader.ReadToEnd();
                return JsonUtility.FromJson<QuizData>(json);
            }
        }
        else
        {
            Debug.LogError("Quiz data file not found: " + quizDataFilePath);
            return null;
        }
    }
}
