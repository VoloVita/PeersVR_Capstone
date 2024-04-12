using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefreshScrollbar : MonoBehaviour
{
    // Start is called before the first frame update
    public void reset()
    {
       GetComponent<Scrollbar>().value = 1;
    }
}
