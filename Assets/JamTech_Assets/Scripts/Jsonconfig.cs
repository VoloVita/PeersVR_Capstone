using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Jsonconfig : MonoBehaviour
{
    private string lessonDataFilePath = "Assets/JamTech_Assets/JSON/lesson.json";
    private string quizDataFilePath = "Assets/JamTech_Assets/JSON/quiz.json";
    public TextAsset lessonJSONFile;
    public TextAsset quizJSONFile;


    /// <summary>
    ///  This function opens the content JSON file and makes a playerData object
    /// </summary>
    /// <returns></returns> Returns a playerData object
    public LessonData LoadLessonData()
    {
       // if (File.Exists(lessonDataFilePath))
     //   {
          //  string json = File.ReadAllText(lessonDataFilePath);
            string json = lessonJSONFile.text;
            return JsonUtility.FromJson<LessonData>(json);
       // }
 //       else
 //       {
  //          Debug.LogError("Lesson data file not found!");
  //          return null;
  //      }
    }

    /// <summary>
    ///  This function opens the content JSON file and makes a quizData object
    /// </summary>
    /// <returns></returns> Returns a quizData object
    public QuizData LoadQuizData()
    {
      //  if (File.Exists(quizDataFilePath))
      //  {
        //    string json = File.ReadAllText(quizDataFilePath);
            string json = quizJSONFile.text;
            return JsonUtility.FromJson<QuizData>(json);
       // }
 //       else
 //       {
 //           Debug.LogError("Quiz data file not found!");
 //           return null;
 //       }
    }
}
