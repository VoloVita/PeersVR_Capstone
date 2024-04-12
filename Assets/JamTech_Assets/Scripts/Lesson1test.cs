using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;
using UnityEngine.Events;

public class Lesson1test : MonoBehaviour
{
    // testobject -> reference to empty game object in the 
    //      scene that this script is attached to. Unneeded?
    public GameObject testobject;
    // lessonPannelcontent -> content object for Lesson view's 
    //      main scroll view
    public GameObject lessonPannelcontent;
    // header -> no gameobject referenced in scene, initialized in start()
    public TMPro.TextMeshProUGUI header;
    // description -> no gameobject referenced in scene, initialized in start()
    public TMPro.TextMeshProUGUI description;
    // non_example_content-> video carousel of bad role-play videos
    public GameObject non_example_content;
    // example_content-> video carousel of good role-play videos 
    public GameObject example_content;
    private LessonData lessonData;
    // videop -> reference to video view player
    public VideoPlayer videop;
    // moreinfo -> some TMP object in more-info window
    public TMPro.TextMeshProUGUI moreinfo;
    public GameObject lPannel;
    public GameObject VPannel;
    public Lesson lesson; // Current Lesson's content 


    // Start is called before the first frame update
    void Start()
    {
        // Find and store lesson view header and description components using tags
        header = lessonPannelcontent.transform.Find("header").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        description = lessonPannelcontent.transform.Find("description").gameObject.GetComponent<TMPro.TextMeshProUGUI>();

        // Get moreinfo and videop components from object references in unity editor
        moreinfo = moreinfo.GetComponent<TMPro.TextMeshProUGUI>();
        videop = videop.GetComponent<VideoPlayer>();
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

    public void ChangeVideo(string url)
    {
        videop.url = url;
    }

    public void VideoClicked()
    {
        lPannel.SetActive(false);
        VPannel.SetActive(true);

    }

    public void lessonload(int i)
    {
        // Reset all Video thumbnails in carousel
        for (int w = 1; w < 7; w++)
        {
            // find child object by the name video1, video2, ...video6
            // set all thumbnail buttons in carousel to inactive
            Transform videoreset = non_example_content.transform.Find($"video{w}");
            Transform videoreset2 = example_content.transform.Find($"video{w}");
            videoreset.gameObject.SetActive(false);
            videoreset2.gameObject.SetActive(false);

        }
        //ints for iterating over loops
        int badthumb = 0;
        int goodthumb = 0;
        int goodvideocount = 0;
        int badvideocount = 0;
        
        //loads the data from json
        lessonData = testobject.GetComponent<Jsonconfig>().LoadLessonData();
        switch (i)
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
        //lessonData exists AND Lesson data file is found...
        if (lessonData != null && lesson != null)
        {
            
            //update title from json
            header.text = lesson.title;
            //update description from json
            description.text = lesson.description;
            // Update more info text from json
            moreinfo.text = lesson.more_info;

            // Update non example thumbnail images 
            // For each file path listed in the Non-example thumbnail section of json...
            foreach (string badthumbnail in lesson.non_example_thumbnails)
            {
                // find video1, video2, ..., video6 in non-example content
                Transform video = non_example_content.transform.Find($"video{badthumb + 1}");
                // find the image used for the thumbnail button 
                Image imageComponent = video.Find("Button Front").GetComponent<Image>();
                // if there is an image already...
                if (imageComponent != null)
                {
                    // create sprite from filepath
                    Sprite thumbnailSprite = LoadSpriteFromFile(badthumbnail);
                    // if sprite created successfully...
                    if (thumbnailSprite != null)
                    {
                        // update thumbnail image and enable thumbnail button
                        imageComponent.sprite = thumbnailSprite;
                        video.gameObject.SetActive(true);
                    }
                }
                // if thumbnail is null, increment to next one
                badthumb++;
            }


            // updating non-example video titles and changing onclick methods
            // For each file path listed in the Non-example thumbnail section of json...
            foreach (string badvideo in lesson.non_example_videos)
            {
                // find TMP object associated with thumbnail title
                TMPro.TextMeshProUGUI badVid = non_example_content.transform.Find($"video{badvideocount + 1}").Find("Button Front").Find("Text (TMP) ").GetComponent<TMPro.TextMeshProUGUI>();
                // update thumbnail title from JSON
                badVid.text = lesson.non_example_titles[badvideocount];
                // Get Button component of thumbnail
                Button butt = non_example_content.transform.Find($"video{badvideocount + 1}").GetComponent<Button>();

                // This line removes any existing listeners attached to the onClick event of the button.
                // It prevents multiple listeners from stacking up if this code is executed multiple times.
                butt.onClick.RemoveAllListeners();
                // This line adds a new listener to the onClick event of the button.
                // When this button is clicked, it will call the ChangeVideo method and pass badvideo as an argument.
                butt.onClick.AddListener(delegate { ChangeVideo(badvideo); });

                butt.onClick.AddListener(delegate { VideoClicked(); });

                badvideocount++;
            }

            // Update example thumbnail images [SAME AS NON_THUMBNAILS]
            foreach (string goodthumbnail in lesson.example_thumbnails)
            {
                Transform video = example_content.transform.Find($"video{goodthumb + 1}");
                Image imageComponent = video.Find("Button Front").GetComponent<Image>();
                if (imageComponent != null)
                {
                    Sprite thumbnailSprite = LoadSpriteFromFile(goodthumbnail);
                    if (thumbnailSprite != null)
                    {
                        imageComponent.sprite = thumbnailSprite;
                        video.gameObject.SetActive(true);
                    }
                }
                goodthumb++;
            }


            // updating example video titles and changing onclick methods
            // SAME AS NON-EXAMPLE TITLES AND METHODS
            foreach (string goodvideo in lesson.example_videos)
            {
                TMPro.TextMeshProUGUI goodVid = example_content.transform.Find($"video{goodvideocount + 1}").Find("Button Front").Find("Text (TMP) ").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
                goodVid.text = lesson.example_titles[goodvideocount];
                Button butt = example_content.transform.Find($"video{goodvideocount + 1}").GetComponent<Button>();
                butt.onClick.RemoveAllListeners();
                butt.onClick.AddListener(delegate { ChangeVideo(goodvideo); });
                butt.onClick.AddListener(delegate { VideoClicked(); });
                goodvideocount++;
            }

            // update lesson exercises list
            // find exercise text and set to empty string
            TMPro.TextMeshProUGUI exerciseText = lessonPannelcontent.transform.Find("Exercise button").Find("Exercise text").GetComponent<TMPro.TextMeshProUGUI>();
            exerciseText.text = "";
            // for each exercise listed in JSON...
            foreach (string exercises in lesson.exercises)
            {
                // Find exercise text, update text 
                TMPro.TextMeshProUGUI exercisesText = lessonPannelcontent.transform.Find("Exercise button").Find("Exercise text").GetComponent<TMPro.TextMeshProUGUI>();
                exercisesText.text += exercises;
                // This line adds two line breaks for formatting
                exercisesText.text += "<br><br>";
            }
        }
    }
}
