using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableStartObjects : MonoBehaviour
{
    public GameObject pannel1;
    public GameObject pannel2;
    public GameObject pannel3;
    public GameObject pannel4;
    public GameObject pannel5;

    // Start is called before the first frame update
    void Start()
    {
        pannel1.SetActive(false);
        pannel2.SetActive(false);

        pannel3.SetActive(false);

        pannel4.SetActive(false);

        pannel5.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
