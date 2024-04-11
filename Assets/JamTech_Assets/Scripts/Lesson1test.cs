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

    /// <summary>
    /// Massive fuckoff function that idk what it does
    /// </summary>
    /// <param name="i">The # of the lesson to be loaded</param>
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

        //if lesson number 1 AND lessonData exists AND Lesson data file is found...
        if (i == 1 && lessonData != null && lessonData.lesson1 != null)
        {
            //update title from json
            header.text = lessonData.lesson1.title;
            //update description from json
            description.text = lessonData.lesson1.description;
            // Update more info text from json
            moreinfo.text = lessonData.lesson1.more_info;

            // Update non example thumbnail images 
            // For each file path listed in the Non-example thumbnail section of json...
            foreach (string badthumbnail in lessonData.lesson1.non_example_thumbnails)
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
            foreach (string badvideo in lessonData.lesson1.non_example_videos)
            {
                // find TMP object associated with thumbnail title
                TMPro.TextMeshProUGUI badVid = non_example_content.transform.Find($"video{badvideocount + 1}").Find("Button Front").Find("Text (TMP) ").GetComponent<TMPro.TextMeshProUGUI>();
                // update thumbnail title from JSON
                badVid.text = lessonData.lesson1.non_example_titles[badvideocount];
                // Get Button component of thumbnail
                Button butt = non_example_content.transform.Find($"video{badvideocount + 1}").GetComponent<Button>();

                // This line removes any existing listeners attached to the onClick event of the button.
                // It prevents multiple listeners from stacking up if this code is executed multiple times.
                butt.onClick.RemoveAllListeners();
                // This line adds a new listener to the onClick event of the button.
                // When this button is clicked, it will call the ChangeVideo method and pass badvideo as an argument.
                butt.onClick.AddListener(delegate { ChangeVideo(badvideo); });
                // Increment video count
                badvideocount++;
            }

            // Update example thumbnail images [SAME AS NON_THUMBNAILS]
            foreach (string goodthumbnail in lessonData.lesson1.example_thumbnails)
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
            foreach (string goodvideo in lessonData.lesson1.example_videos)
            {
                TMPro.TextMeshProUGUI goodVid = example_content.transform.Find($"video{goodvideocount + 1}").Find("Button Front").Find("Text (TMP) ").GetComponent<TMPro.TextMeshProUGUI>();
                goodVid.text = lessonData.lesson1.example_titles[goodvideocount];
                Button butt = example_content.transform.Find($"video{goodvideocount + 1}").GetComponent<Button>();
                butt.onClick.RemoveAllListeners();
                butt.onClick.AddListener(delegate { ChangeVideo(goodvideo); });
                goodvideocount++;
            }

            // update lesson exercises list
            // find exercise text and set to empty string
            TMPro.TextMeshProUGUI exerciseText = lessonPannelcontent.transform.Find("Exercise button").Find("Exercise text").GetComponent<TMPro.TextMeshProUGUI>();
            exerciseText.text = "";
            // for each exercise listed in JSON...
            foreach (string exercises in lessonData.lesson1.exercises)
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
