using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellBookScript : MonoBehaviour
{
   [SerializeField] Slider mS;
   [SerializeField] Slider sS;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(StartOpen);
        
    }

    public void StartOpen()
    {
        mS.interactable = false;
        sS.interactable = false;
        GetComponent<Button>().interactable = false;
        Animation[] animationsBook = GetComponentsInChildren<Animation>();
        if (animationsBook[0].GetClip("OpenBookFirstOs") != null)
        {
            animationsBook[0].Play("OpenBookFirstOs");
            animationsBook[1].Play("OpenBookSecondOs");
        }
        else
        {
            animationsBook[1].Play("OpenBookFirstOs");
            animationsBook[0].Play("OpenBookSecondOs");
        }

    }
}
