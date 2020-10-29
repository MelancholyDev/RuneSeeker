using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    Sprite[] background_ = new Sprite[78];
    [SerializeField] GameObject backgroundPlane;
    int indexer = 0;
    int BackNumer = 0;
    [SerializeField] Button YesButton;
    [SerializeField] Button NoButton;
    [SerializeField] Image attentionBack;
    [SerializeField] Button trashcan;


    public void Awake()
    {
        for (int i = 0; i < 78; i++)
        {
            background_[i] = Resources.Load<Sprite>("MainMenu/" + i);
        }
        trashcan.onClick.AddListener(()=>attentionBack.gameObject.SetActive(!attentionBack.gameObject.active));
        YesButton.onClick.AddListener(()=> { PlayerPrefs.DeleteAll(); ; attentionBack.gameObject.SetActive(false); });
        NoButton.onClick.AddListener(()=>attentionBack.gameObject.SetActive(false));

    }
 

    public void Update()
    {
        if (BackNumer == 3)
        {
            if (background_[indexer] != null)
                backgroundPlane.GetComponent<Image>().sprite = background_[indexer];
            indexer++;
            if (indexer > background_.Length - 1)
                indexer = 0;
            BackNumer = 0;
        }
        else
        {
            BackNumer++;
        }

    }
  
}

    
