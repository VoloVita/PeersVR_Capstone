using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System.Diagnostics;

public class QuizLoader : MonoBehaviour
{
    public GameObject quiz_content; // quiz content frame

    private QuizData quizData; // All quiz content Data returned from JSON

    public void loadQuiz(int quizNum)
    {
        // loads the data from json
        quizData = GetComponent<Jsonconfig>().LoadLessonData(2);

        // determine which quiz's content to load
        switch (quizNum)
        {
            case 1:
                quiz = quizData.topic_1_quiz;
                break;
            case 2:
                quiz = quizData.topic_2_quiz;
                break;
            case 3:
                quiz = quizData.topic_3_quiz;
                break;
            case 4:
                quiz = quizData.topic_4_quiz;
                break;
            case 5:
                quiz = quizData.topic_5_quiz;
                break;
            default:
                quiz = quizData.topic_1_quiz;
                break;
        }


    }



}
