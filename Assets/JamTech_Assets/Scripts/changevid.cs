using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;


public class changevid : MonoBehaviour
{
    VideoPlayer vid;
    
    
    // Start is called before the first frame update
    void Start()
    {
       //vid = GameObject.FindGameObjectWithTag("videop").GetComponent<VideoPlayer>();
       vid = GetComponent<VideoPlayer>();
       vid.url = "Assets/VRTemplateAssets/Videos/onboarding_video_final.mp4";
    }

    // Update is called once per frame
    void Update()
    {
     
    }
//function for the dropdown that changes the video url
    public void HandleInputData(int val)
    {
        if (val == 0){
            vid.url = "Assets/VRTemplateAssets/Videos/onboarding_video_final.mp4";
        }
        if (val == 1){
            vid.url = "Assets/Scenes/test for vr vid.mp4";
        }
        if (val == 2){
            vid.url = "Assets/Scenes/mixkit-pet-owner-playing-with-a-cute-cat-1779-medium.mp4";
        }
    }
    
}
