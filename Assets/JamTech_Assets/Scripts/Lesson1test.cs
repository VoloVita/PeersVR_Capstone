using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson1test : MonoBehaviour
{
    public GameObject testobject;
    public GameObject lessonPannelcontent;
    public TMPro.TextMeshProUGUI header;
    public TMPro.TextMeshProUGUI description;
    

    private LessonData lessonData;
    // Start is called before the first frame update
    void Start()
    {
        header = lessonPannelcontent.transform.Find("header").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        description = lessonPannelcontent.transform.Find("description").gameObject.GetComponent<TMPro.TextMeshProUGUI>();

    }

    public void lesson1load()
    {
        lessonData = testobject.GetComponent<Jsonconfig>().LoadLessonData();
        header.GetComponent<TMPro.TextMeshProUGUI>().text = lessonData.lesson2.title;
        description.GetComponent<TMPro.TextMeshProUGUI>().text = lessonData.lesson2.description;

    }


}
