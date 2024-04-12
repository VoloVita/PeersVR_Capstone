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


    // Private Class Variables
    private LessonData lessonData; // All Lesson content Data returned from JSON
    private Lesson lesson; // Current Lesson's content 
    private TMPro.TextMeshProUGUI lesson_desc; // not referenced in scene
    private TMPro.TextMeshProUGUI lesson_title; // not referenced in scene
    private TMPro.TextMeshProUGUI moreinfo_text; // not referenced in scene
    private QuizLoader quizLoader;

    void start()
    {
        loadLesson(1);
    }



    public void loadLesson(int lessonNum)
    {

        // load the data from json, check for null
        lessonData = GetComponent<Jsonconfig>().LoadLessonData();
        System.Diagnostics.Debug.Assert(lessonData != null, "Lesson data failed to load from JSON");

        // load specific lesson data based on argument passed to loadLesson(), check for null
        switch (lessonNum)
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

        // Disable all non_example video objects
        foreach (Transform child in non_example_content.transform)
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
            // Increment video count
            j++;
        }


        // Update Rules Popup


        // Update Exercises

        // Update Quiz button
        Button quiz_button = take_quiz.GetComponent<Button>();
        quiz_button.onClick.RemoveAllListeners();
        quiz_button.onClick.AddListener(delegate { quizLoader.loadQuiz(lessonNum, 1); });
    }



    public void ChangeVideo(string url)
    {
        video_output.url = url;
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
