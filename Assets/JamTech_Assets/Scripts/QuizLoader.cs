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
    private Quiz quiz; // this lesson's quiz content
    private TMPro.TextMeshProUGUI quiz_prompt;
    private TMPro.TextMeshProUGUI option1;
    private TMPro.TextMeshProUGUI option2;


    public void loadQuiz(int quizNum, int questionNum)
    {
        // validate input
        System.Diagnostics.Debug.Assert(questionNum == 1 || questionNum == 2, "QuestionNum must be a 1 or 2");

        // loads the data from json
        quizData = GetComponent<Jsonconfig>().LoadQuizData();
        // Check for null case of quizData
        System.Diagnostics.Debug.Assert(quizData != null, "Quiz data failed to load from JSON");

        // determine which quiz's content to load
        switch (quizNum)
        {
            case 1:
                quiz = quizData.quiz1;
                break;
            case 2:
                quiz = quizData.quiz2;
                break;
            case 3:
                quiz = quizData.quiz3;
                break;
            case 4:
                quiz = quizData.quiz4;
                break;
            case 5:
                quiz = quizData.quiz5;
                break;
            default:
                quiz = quizData.quiz1;
                break;
        }
        // Check for null case of lessonData
        System.Diagnostics.Debug.Assert(quiz != null, "Current quiz failed to load from JSON");


        if (questionNum == 1)
        {
            // Get TMP component associated with prompt text
            quiz_prompt = quiz_content.transform.Find("Prompt Text").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
            quiz_prompt.text = quiz.q1_prompt;
            // update quiz question 1
            option1 = quiz_content.transform.Find("Response Button 1").Find("Text (Legacy) ").GetComponent<TMPro.TextMeshProUGUI>();
            option1.text = quiz.q2_options[0];
            // update quiz question 2
            option2 = quiz_content.transform.Find("Response Button 2").Find("Text (Legacy) ").GetComponent<TMPro.TextMeshProUGUI>();
            option2.text = quiz.q2_options[1];
        }
        else
        {
            // Get TMP component associated with prompt text
            quiz_prompt = quiz_content.transform.Find("Prompt Text").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
            quiz_prompt.text = quiz.q1_prompt;
            // update quiz question 1
            option1 = quiz_content.transform.Find("Response Button 1").Find("Text (Legacy) ").GetComponent<TMPro.TextMeshProUGUI>();
            option1.text = quiz.q2_options[0];
            // update quiz question 2
            option2 = quiz_content.transform.Find("Response Button 2").Find("Text (Legacy) ").GetComponent<TMPro.TextMeshProUGUI>();
            option2.text = quiz.q2_options[1];
        }
    }
}
