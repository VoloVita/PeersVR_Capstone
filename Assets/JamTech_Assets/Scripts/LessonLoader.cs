using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;
using UnityEngine.Events;


public class LessonLoader : MonoBehaviour
{
    // Public Class Variables
    public GameObject lesson_content; // object reference to scroll view content of Lesson View
    public GameObject example_content; // object reference to example video scroll view content
    public GameObject non_example_content; // object reference to non example video scroll view content
    public VideoPlayer video_output; // reference to video view player

    // Private Class Variables
    private LessonData lessonData; // 
    private TMPro.TextMeshProUGUI lesson_desc; // not referenced in scene, initialized in start()
    private TMPro.TextMeshProUGUI lesson_title; // not referenced in scene, initialized in start()


    void Start()
    {

    }

    public void loadLesson(int lessonNum)
    {

        // load the data from JSON
        lessonData = Jsonconfig.LoadLessonData();

        if (lessonData != null)
        {
            // find TMP for lesson title, update content from JSON
            lesson_title = lesson_content.transform.Find("header").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
            lesson_title.text = lessonData[lessonNum].title;

            // find TMP for lesson description, update content from JSON
            lesson_desc = lesson_content.transform.Find("description").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
            lesson_desc.text = lessonData[lessonNum].description;

            // Update Non-example video carousel thumbnails and titles

            // Destroy all video thumbnail objects in content object
            foreach (Transform child in non_example_content)
            {
                Destroy(child.gameObject);
            }
            // Get number of videos from JSON
            numVids = lessonData[lessonNum].Length;

            for (int i = 0; i < numVids; i++)
            {
                // instantiate new thumnail prefab

                // initialize thumbnail image

                // initialize thumbnail title

            }



            // Update example video carousel thumbnails and titles


            // Update Rules Popup



            // Update Exercises
        }
        else
        {
            Debug.LogError("Lesson Content JSON not loaded successfully");
        }


    }





    private void rebuildCarousel(LessonData lessonData)
    {


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
