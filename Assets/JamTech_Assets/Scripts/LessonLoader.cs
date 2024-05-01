using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;
using UnityEngine.Events;
using System.Diagnostics;


public class LessonLoader : MonoBehaviour
{
    // Public Class Variables
    public GameObject lesson_content; // object reference to scroll view content of Lesson View
    public GameObject example_content; // object reference to example video scroll view content
    public GameObject non_example_content; // object reference to non example video scroll view content
    public GameObject moreinfo_content; // object reference to scroll view content of moreinfo popup
    public GameObject take_quiz; // button to start quiz
    public VideoPlayer video_output; // reference to video view player
    public GameObject lPannel;
    public GameObject VPannel;
    public GameObject QPannel;
    public GameObject CPannel;
    public GameObject readMoreContent;
    public GameObject quiz_content; // quiz content frame
    public GameObject correctButton;
    public GameObject incorrectButton;
    public GameObject correctButton2;
    public GameObject incorrectButton2;

    // Private Class Variables
    private LessonData lessonData; // All Lesson content Data returned from JSON
    private Lesson lesson; // Current Lesson's content 
    private TMPro.TextMeshProUGUI lesson_desc; // not referenced in scene
    private TMPro.TextMeshProUGUI lesson_title; // not referenced in scene
    private TMPro.TextMeshProUGUI moreinfo_text; // not referenced in scene
    private QuizData quizData; // All quiz content Data returned from JSON
    private Quiz quiz; // this lesson's quiz content
    private TMPro.TextMeshProUGUI quiz_prompt;
    private TMPro.TextMeshProUGUI option1;
    private TMPro.TextMeshProUGUI option2;
    private int lessonNumber;
    

    public void changeLesson(int number){
        // load the data from json, check for null
        lessonData = GetComponent<Jsonconfig>().LoadLessonData();
        System.Diagnostics.Debug.Assert(lessonData != null, "Lesson data failed to load from JSON");


        // load specific lesson data based on argument passed to loadLesson(), check for null
        switch (number)
        {
            case 1:
                lesson = lessonData.lesson1;
                break;
            case 2:
                lesson = lessonData.lesson2;
                break;
            case 3:
                lesson = lessonData.lesson3;
                break;
            case 4:
                lesson = lessonData.lesson4;
                break;
            case 5:
                lesson = lessonData.lesson5;
                break;
            default:
                lesson = lessonData.lesson1;
                break;
        }
        CPannel.transform.Find("Lesson Info").Find("Current Topic").gameObject.GetComponent<Text>().text = $"Topic {number}" ;
        CPannel.transform.Find("Lesson Info").Find("Topic Name").gameObject.GetComponent<Text>().text = lesson.title;
        lessonNumber = number;
    }



    public void loadLesson()
    {

        // load the data from json, check for null
        lessonData = GetComponent<Jsonconfig>().LoadLessonData();
        System.Diagnostics.Debug.Assert(lessonData != null, "Lesson data failed to load from JSON");

        // load specific lesson data based on argument passed to loadLesson(), check for null
        switch (lessonNumber)
        {
            case 1:
                lesson = lessonData.lesson1;
                break;
            case 2:
                lesson = lessonData.lesson2;
                break;
            case 3:
                lesson = lessonData.lesson3;
                break;
            case 4:
                lesson = lessonData.lesson4;
                break;
            case 5:
                lesson = lessonData.lesson5;
                break;
            default:
                lesson = lessonData.lesson1;
                break;
        }
        System.Diagnostics.Debug.Assert(lesson != null, "Current lesson failed to load");

        // find TMP for lesson title, update content from JSON
        lesson_title = lesson_content.transform.Find("header").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        lesson_title.text = lesson.title;

        // find TMP for lesson description, update content from JSON
        lesson_desc = lesson_content.transform.Find("description").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        lesson_desc.text = lesson.description;

        // find TMP for More Info text, update content from JSON
        moreinfo_text = moreinfo_content.transform.Find("Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>();
        moreinfo_text.text = lesson.description;

        // Disable all video objects
        foreach (Transform child in non_example_content.transform)
        {
            child.gameObject.SetActive(false);
        }
        foreach (Transform child in example_content.transform)
        {
            child.gameObject.SetActive(false);
        }

        // Update all non_example thumbnails
        int j = 0;
        foreach (string thumbnail in lesson.non_example_thumbnails)
        {
            // find video1, video2, ..., video6 in non-example content
            Transform video = non_example_content.transform.Find($"video{j + 1}");
            // find the image used for the thumbnail button 
            Image imageComponent = video.Find("Button Front").GetComponent<Image>();
            // check for null case
            System.Diagnostics.Debug.Assert(imageComponent != null, "Image Component of Thumbnail does not exist");
            // create sprite from filepath
            Sprite thumbnailSprite = LoadSpriteFromFile(thumbnail);
            // check for null case
            System.Diagnostics.Debug.Assert(thumbnailSprite != null, "Failed to create sprite from filepath");
            // update thumbnail and set active
            imageComponent.sprite = thumbnailSprite;
            video.gameObject.SetActive(true);
            // increment to find next video
            j++;
        }

        j = 0; // reset counter for next loop

        // updating non-example video titles and changing onclick methods
        // For each file path listed in the Non-example thumbnail section of json...
        foreach (string video in lesson.non_example_videos)
        {
            // find TMP object associated with thumbnail title
            TMPro.TextMeshProUGUI vid_title = non_example_content.transform.Find($"video{j + 1}").Find("Button Front").Find("Text (TMP) ").GetComponent<TMPro.TextMeshProUGUI>();
            System.Diagnostics.Debug.Assert(vid_title != null, "Failed find vid title");

            // update thumbnail title from JSON
            vid_title.text = lesson.non_example_titles[j];
            // Get Button component of thumbnail
            Button thumbnail_button = non_example_content.transform.Find($"video{j + 1}").GetComponent<Button>();
            // This line removes any existing listeners attached to the onClick event of the button.
            // It prevents multiple listeners from stacking up if this code is executed multiple times.
            thumbnail_button.onClick.RemoveAllListeners();
            // This line adds a new listener to the onClick event of the button.
            // When this button is clicked, it will call the ChangeVideo method and pass badvideo as an argument.
            thumbnail_button.onClick.AddListener(delegate { ChangeVideo(video); });
            thumbnail_button.onClick.AddListener(delegate { VideoClicked(); });
            // Increment video count
            j++;
        }

        // Update all example thumbnails
        j = 0; // reset counter
        foreach (string thumbnail in lesson.example_thumbnails)
        {
            // find video1, video2, ..., video6 in example content
            Transform video = example_content.transform.Find($"video{j + 1}");
            // find the image used for the thumbnail button 
            Image imageComponent = video.Find("Button Front").GetComponent<Image>();
            // check for null case
            System.Diagnostics.Debug.Assert(imageComponent != null, "Image Component of Thumbnail does not exist");
            // create sprite from filepath
            Sprite thumbnailSprite = LoadSpriteFromFile(thumbnail);
            // check for null case
            System.Diagnostics.Debug.Assert(thumbnailSprite != null, "Failed to create sprite from filepath");
            // update thumbnail and set active
            imageComponent.sprite = thumbnailSprite;
            video.gameObject.SetActive(true);
            // increment to find next video
            j++;
        }

        j = 0; // reset counter

        // updating non-example video titles and changing onclick methods
        // For each file path listed in the Non-example thumbnail section of json...
        foreach (string video in lesson.example_videos)
        {
            // find TMP object associated with thumbnail title
            TMPro.TextMeshProUGUI vid_title = example_content.transform.Find($"video{j + 1}").Find("Button Front").Find("Text (TMP) ").GetComponent<TMPro.TextMeshProUGUI>();
            // update thumbnail title from JSON
            vid_title.text = lesson.example_titles[j];
            // Get Button component of thumbnail
            Button thumbnail_button = example_content.transform.Find($"video{j + 1}").GetComponent<Button>();
            // This line removes any existing listeners attached to the onClick event of the button.
            // It prevents multiple listeners from stacking up if this code is executed multiple times.
            thumbnail_button.onClick.RemoveAllListeners();
            // This line adds a new listener to the onClick event of the button.
            // When this button is clicked, it will call the ChangeVideo method and pass badvideo as an argument.
            thumbnail_button.onClick.AddListener(delegate { ChangeVideo(video); });
            thumbnail_button.onClick.AddListener(delegate { VideoClicked(); });
            // Increment video count
            j++;
        }


        // Update Rules Popup
        foreach (Transform child in readMoreContent.transform)
        {
            child.gameObject.SetActive(false);
        }
        readMoreContent.transform.Find($"Lesson{lessonNumber}").gameObject.SetActive(true);

        // Update Exercises

        // Update Quiz button
        Button quiz_button = take_quiz.GetComponent<Button>();
        quiz_button.onClick.RemoveAllListeners();
        quiz_button.onClick.AddListener(delegate { LoadQuiz(lessonNumber, 1); });

    }

    public void LoadQuiz(int quizNum, int questionNum)
    {
        // validate input
        // System.Diagnostics.Debug.Assert(questionNum == 1 || questionNum == 2, "QuestionNum must be a 1 or 2");
        // int questionNum = 1;
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


        if (questionNum == 1) // if first quiz question...
        {

            // Change the text sections of the Quiz View
            quiz_prompt = quiz_content.transform.Find("Prompt Text").gameObject.GetComponent<TMPro.TextMeshProUGUI>(); // locate text component
            quiz_prompt.text = quiz.q1_prompt; //update the text for the quiz prompt
            option1 = quiz_content.transform.Find("Response Button 1").Find("Text (Legacy)").GetComponent<TMPro.TextMeshProUGUI>(); // locate text component
            option1.text = quiz.q1_options[0]; //update the text for the first quiz answer
            option2 = quiz_content.transform.Find("Response Button 2").Find("Text (Legacy)").GetComponent<TMPro.TextMeshProUGUI>(); // locate text component
            option2.text = quiz.q1_options[1]; // update the text for the second quiz answer
            // Update quiz button functionality
            Button quiz_button = quiz_content.transform.Find("Response Button 1").GetComponent<Button>();
            quiz_button.onClick.RemoveAllListeners();
            quiz_button.onClick.AddListener(delegate { LoadQuiz(quizNum, 2); });
            
            if(quiz.topic_answers[0]==1)
            quiz_button.onClick.AddListener(() => correctButton.SetActive(true));
            else
            quiz_button.onClick.AddListener(() => incorrectButton.SetActive(true)); // on click, open quiz question number 2
            
            Button quiz_button1 = quiz_content.transform.Find("Response Button 2").GetComponent<Button>();
            quiz_button1.onClick.RemoveAllListeners();
            quiz_button1.onClick.AddListener(delegate { LoadQuiz(quizNum, 2); });
            
            if(quiz.topic_answers[0]==2)
            quiz_button1.onClick.AddListener(() => correctButton.SetActive(true));
            else
            quiz_button1.onClick.AddListener(() => incorrectButton.SetActive(true)); // on click, open quiz question number 2
        }
        else // else second quiz question...
        {
            // Change the text sections of the Quiz View
            quiz_prompt = quiz_content.transform.Find("Prompt Text").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
            quiz_prompt.text = quiz.q2_prompt;  //update the text for the quiz prompt
            option1 = quiz_content.transform.Find("Response Button 1").Find("Text (Legacy)").GetComponent<TMPro.TextMeshProUGUI>();
            option1.text = quiz.q2_options[0];  //update the text for the quiz prompt
            option2 = quiz_content.transform.Find("Response Button 2").Find("Text (Legacy)").GetComponent<TMPro.TextMeshProUGUI>();
            option2.text = quiz.q2_options[1]; // update the text for the second quiz answer
            // Update quiz button functionality
            Button quiz_button = quiz_content.transform.Find("Response Button 1").GetComponent<Button>();
            quiz_button.onClick.RemoveAllListeners();
             // on click, switch to curriculum view
            
            if(quiz.topic_answers[1]==1)
            quiz_button.onClick.AddListener(() => correctButton2.SetActive(true));
            else
            quiz_button.onClick.AddListener(() => incorrectButton2.SetActive(true));
            
            
            Button quiz_button1 = quiz_content.transform.Find("Response Button 2").GetComponent<Button>();
            quiz_button1.onClick.RemoveAllListeners();
             // on click, switch to curriculum view

            if(quiz.topic_answers[1]==2)
            quiz_button1.onClick.AddListener(() => correctButton2.SetActive(true));
            else
            quiz_button1.onClick.AddListener(() => incorrectButton2.SetActive(true));
            
            
        }

    }

    public void ChangeVideo(string url)
    {
        video_output.url = url;
    }

    public void VideoClicked()
    {
        lPannel.SetActive(false); // switch from lesson view to video view
        VPannel.SetActive(true);



    }
    public void QuizClicked()
    {
        QPannel.SetActive(false); // switch from quiz view to curriculum view
        CPannel.SetActive(true);
    }

    /// <summary>
    /// This function takes in a file path to an image and create a sprite object 
    /// </summary>
    /// <param name="path">File path to image </param> 
    /// <returns>Returns a newly created sprite object</returns>
    public Sprite LoadSpriteFromFile(string path)
    {
        // Read the bytes from the file
        byte[] fileData = System.IO.File.ReadAllBytes(path);

        // Create a texture and load the bytes into it
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData);

        // Create a sprite from the texture
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        return sprite;
    }
}
