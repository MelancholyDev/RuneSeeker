using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeMainMenu : MonoBehaviour
{

    
    void Update()
    {
        if (GetComponent<Image>().color.a > 0)
            GetComponent<Image>().color = new Color(0,0,0, GetComponent<Image>().color.a-0.005F);
        else
        {
            Destroy(gameObject);
        }
        
    }
}
