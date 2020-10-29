using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EndManager : MonoBehaviour
{
    int frames=0;
    [SerializeField] Image back;
    [SerializeField] Sprite[] sprites;
    public void Start()
    {
        GameObject.FindGameObjectWithTag("Dialoguee").SetActive(false);
    }
    void Update()
    {
        int numOfFrames = 3;

                if (frames++ == numOfFrames)
                {
                   
                        back.sprite = sprites[frames];
                       frames++;
                        if (frames == sprites.Length)
                        frames = 0;
                    
                    frames = 0;
                }
            
    }
}
