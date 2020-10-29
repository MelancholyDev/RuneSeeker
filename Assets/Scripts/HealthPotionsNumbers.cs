using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPotionsNumbers : MonoBehaviour
{

    void Update()
    {
        GetComponent<Text>().text = "x" + Managers.inventory_.HealPotions;
    }
}
