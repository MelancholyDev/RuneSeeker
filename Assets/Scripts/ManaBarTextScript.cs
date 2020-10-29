using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBarTextScript : MonoBehaviour
{
    
    void Update()
    {
        GetComponent<Text>().text = Managers.player.Mana + "/" + 100;
    }
}
