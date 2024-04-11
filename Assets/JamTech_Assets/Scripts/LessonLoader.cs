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
    public GameObject thumbnail_prefab; // object reference to video thumbnail prefab
    public VideoPlayer video_output; // reference to video view player


    // Private Class Variables
    private LessonData lessonData; // All Lesson content Data returned from JSON
    private Lesson lesson; // Current Lesson's content 
    private TMPro.TextMeshProUGUI lesson_desc; // not referenced in scene, initialized in start()
    private TMPro.TextMeshProUGUI lesson_title; // not referenced in scene, initialized in start()

    void Start()
    {

    }

    public void loadLesson(int lessonNum)
    {

        //loads the data from json
        lessonData = GetComponent<Jsonconfig>().LoadLessonData(1);

        // Check for null case of lessonData
        System.Diagnostics.Debug.Assert(lessonData != null, "Lesson data failed to load from JSON");


        // load current lesson data based on argument passed to loadLesson()
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

        // Check for null case of current lesson
        System.Diagnostics.Debug.Assert(lesson != null, "Current lesson failed to load");


        // find TMP for lesson title, update content from JSON
        lesson_title = lesson_content.transform.Find("header").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        lesson_title.text = lesson.title;

        // find TMP for lesson description, update content from JSON
        lesson_desc = lesson_content.transform.Find("description").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        lesson_desc.text = lesson.description;

        // Update Non-example video carousel thumbnails and titles

        // Destroy all video thumbnail objects in content 
        foreach (Transform child in non_example_content.transform)
        {
            Destroy(child.gameObject);
        }

        // Get number of videos from lesson object
        int numVids = lesson.non_example_thumbnails.Length;

        for (int i = 0; i < numVids; i++)
        {
            // instantiate new thumbnail, parent, and rename
            GameObject newObject = Instantiate(thumbnail_prefab, non_example_content.transform);
            newObject.name = "video_" + i.ToString();

            // find image component, check for null
            Image imageComponent = newObject.transform.Find("Button Front").GetComponent<Image>();
            System.Diagnostics.Debug.Assert(imageComponent != null, "Image Component of Thumbnail does not exist");
            // create sprite from filepath, check for null
            Sprite thumbnailSprite = LoadSpriteFromFile(lesson.non_example_thumbnails[i]);
            System.Diagnostics.Debug.Assert(thumbnailSprite != null, "Failed to create sprite from filepath");

            // update thumbnail image and enable thumbnail button
            imageComponent.sprite = thumbnailSprite;


            // find thumbnail title component
            TMPro.TextMeshProUGUI vidTitle = newObject.transform.Find("Button Front").Find("Text (TMP) ").GetComponent<TMPro.TextMeshProUGUI>();
            vidTitle.text = lesson.non_example_titles[i];

            // update Listeners and Onclick event
            // Get Button component of thumbnail
            Button thumbnailButton = newObject.GetComponent<Button>();

            // This line removes any existing listeners attached to the onClick event of the button.
            // It prevents multiple listeners from stacking up if this code is executed multiple times.
            thumbnailButton.onClick.RemoveAllListeners();
            // This line adds a new listener to the onClick event of the button.
            // When this button is clicked, it will call the ChangeVideo method and pass badvideo as an argument.
            thumbnailButton.onClick.AddListener(delegate { ChangeVideo(lesson.non_example_videos[i]); });
        }


        // Update example video carousel thumbnails and titles


        // Update Rules Popup



        // Update Exercises



    }





    private void rebuildCarousel(LessonData lessonData)
    {

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
