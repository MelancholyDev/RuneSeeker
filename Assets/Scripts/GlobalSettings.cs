using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalSettings : MonoBehaviour
{
    [SerializeField] Button settingsbutton;
    [SerializeField] GameObject settingsmenu;
    [SerializeField] Slider MusicSlider;
    [SerializeField] Slider SoundSlider;
    void Awake()
    {   
        settingsmenu.SetActive(false);
        DontDestroyOnLoad(gameObject);
    }
    public void OpenOrCloseMenu()
    {
        settingsmenu.SetActive(!settingsmenu.active);
    }
    public void changeMusicVolume()
    {

    }
    
    
}
