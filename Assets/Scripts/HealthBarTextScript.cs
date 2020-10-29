using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarTextScript : MonoBehaviour
{
    
    void Update()
    {
        GetComponent<Text>().text = Managers.player.Health + "/" + 100;
    }
}
