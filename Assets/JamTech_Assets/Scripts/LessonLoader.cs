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
    public TMPro.TextMeshProUGUI lesson_title; // not referenced in scene, initialized in start()
    public TMPro.TextMeshProUGUI lesson_desc; // not referenced in scene, initialized in start()
    public GameObject lesson_content; // object reference to scroll view content of Lesson View
    public GameObject example_content; // object reference to example video scroll view content
    public GameObject non_example_content; // object reference to non example video scroll view content
    public VideoPlayer video_output; // reference to video view player

    // Private Class Variables
    private LessonData lessonData; // 


    void Start()
    {

    }

    public void loadLesson(int lessonNum)
    {

        // load the data from JSON
        lessonData = Jsonconfig.LoadLessonData();

        if (lessonData != null)
        {
            TMPro.TextMeshProUGUI lesson_title = lesson_content.transform.Find("header").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
            lesson_title.text = lessonData[lessonNum].title;

            // Update Title and Description from first JSON section
            TMPro.TextMeshProUGUI lesson_title = lesson_content.transform.Find("description").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
            lesson_desc.text = lessonData[lessonNum].description;

            // Update Non-example video carousel thumbnails and titles
            // get ref to nonex content


            Transform nonex_carousel = non_example_content.transform.Find($"video{w}");
            nonex_carousel.gameObject.SetActive(false);
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


}
