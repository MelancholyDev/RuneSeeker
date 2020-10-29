using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaPotionsNumbers : MonoBehaviour
{  
    void Update()
    {

        GetComponent<Text>().text = "x" + Managers.inventory_.Manapotions;
    }
}
