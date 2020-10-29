using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour,UIManagerPreset
{
    [SerializeField] GameObject Settingsmenu;
    [SerializeField] Sprite[] background;
    [SerializeField] GameObject backgroundPlane;
    [SerializeField] Slider SliderMusic;
    [SerializeField] Slider SliderSound;
    int index = 0;
    public ManagerStatus status { get ; set ; }

    public void StartUP()
    {
        Debug.Log("UIManaager is starting...");
        Settingsmenu.SetActive(false);
        SliderMusic.value = 0.5f;
        SliderSound.value = 0.5f;
        status = ManagerStatus.Started;
    }

    public void SettingsMenu()
    {
        Settingsmenu.SetActive(!Settingsmenu.active);
    }
    
    public void Update()
    {
        backgroundPlane.GetComponent<Image>().sprite = background[index];
        index++;
        if (index > 17)
            index = 0;
    }
}
