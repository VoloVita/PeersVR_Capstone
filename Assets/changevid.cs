using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;


public class changevid : MonoBehaviour
{
    VideoPlayer vid;
    
    // Start is called before the first frame update
    void Start()
    {
       vid = GameObject.FindGameObjectWithTag("videop").GetComponent<VideoPlayer>();
       vid.url = "Assets/VRTemplateAssets/Videos/onboarding_video_final.mp4";
       
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    
}
