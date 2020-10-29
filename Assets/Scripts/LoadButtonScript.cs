using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadButtonScript : MonoBehaviour
{

    void Start()
    {
        if (PlayerPrefs.HasKey("NotEmpty"))
            gameObject.SetActive(true);
        else
        {
            gameObject.SetActive(false);
        }
    }



}
