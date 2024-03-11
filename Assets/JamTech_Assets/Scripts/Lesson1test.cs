using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;
using UnityEngine.Events;

public class Lesson1test : MonoBehaviour
{
    public GameObject testobject;
    public GameObject lessonPannelcontent;
    public TMPro.TextMeshProUGUI header;
    public TMPro.TextMeshProUGUI description;
    public GameObject non_example_content;
    public GameObject example_content;
    private LessonData lessonData;
    public VideoPlayer videop;
    public TMPro.TextMeshProUGUI moreinfo;
    

    // Start is called before the first frame update
    void Start()
    {
        header = lessonPannelcontent.transform.Find("header").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        description = lessonPannelcontent.transform.Find("description").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        moreinfo = moreinfo.GetComponent<TMPro.TextMeshProUGUI>();
        videop = videop.GetComponent<VideoPlayer>();
    }
    Sprite LoadSpriteFromFile(string path)
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
    public void lessonload(int i)
    {
        for( int w = 1; w<7;w++)
        {
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
//if lesson int passed is 1
        if (i == 1 && lessonData != null && lessonData.lesson1 != null)
        {
            //title
            header.text = lessonData.lesson1.title;
            //description
            description.text = lessonData.lesson1.description;
            //non example thumbnail images
            moreinfo.text = lessonData.lesson1.more_info;
            foreach (string badthumbnail in lessonData.lesson1.non_example_thumbnails)
            {
                Transform video = non_example_content.transform.Find($"video{badthumb+1}");
                Image imageComponent = video.Find("Button Front").GetComponent<Image>();
                
                if (imageComponent != null)
                {
                    Sprite thumbnailSprite = LoadSpriteFromFile(badthumbnail);
                    
                    if (thumbnailSprite != null)
                    {
                        
                        imageComponent.sprite = thumbnailSprite;                       
                        video.gameObject.SetActive(true); 
                    }
            
                }
                badthumb++; 
            }
            // video titles and changing onclick methods
            foreach (string badvideo in lessonData.lesson1.non_example_videos)
            {
                TMPro.TextMeshProUGUI badVid = non_example_content.transform.Find($"video{badvideocount+1}").Find("Button Front").Find("Text (TMP) ").GetComponent<TMPro.TextMeshProUGUI>();
                badVid.text = lessonData.lesson1.non_example_titles[badvideocount];
                Button butt = non_example_content.transform.Find($"video{badvideocount+1}").GetComponent<Button>();
                butt.onClick.RemoveAllListeners();
                butt.onClick.AddListener(delegate {ChangeVideo(badvideo); });
                badvideocount++;
            }
            
           // example thumbnail images
            foreach (string goodthumbnail in lessonData.lesson1.example_thumbnails)
            {
                Transform video = example_content.transform.Find($"video{goodthumb+1}");
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
            // titles for videos and changing onclick methods
            foreach (string goodvideo in lessonData.lesson1.example_videos)
            {
                TMPro.TextMeshProUGUI goodVid = example_content.transform.Find($"video{goodvideocount+1}").Find("Button Front").Find("Text (TMP) ").GetComponent<TMPro.TextMeshProUGUI>();
                goodVid.text = lessonData.lesson1.example_titles[goodvideocount];
                Button butt = example_content.transform.Find($"video{goodvideocount+1}").GetComponent<Button>();
                butt.onClick.RemoveAllListeners();
                butt.onClick.AddListener(delegate {ChangeVideo(goodvideo); });
                goodvideocount++;
            }
            TMPro.TextMeshProUGUI exerciseText = lessonPannelcontent.transform.Find("Exercise button").Find("Exercise text").GetComponent<TMPro.TextMeshProUGUI>();
            exerciseText.text = "";
            foreach (string exercises in lessonData.lesson1.exercises)
            {
                TMPro.TextMeshProUGUI exercisesText = lessonPannelcontent.transform.Find("Exercise button").Find("Exercise text").GetComponent<TMPro.TextMeshProUGUI>();
                exercisesText.text += exercises;
                exercisesText.text += "<br><br>";
            }
        }
    }
}
