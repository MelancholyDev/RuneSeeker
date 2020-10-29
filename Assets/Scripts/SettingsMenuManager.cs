using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuManager : MonoBehaviour,UIManagerPreset
{
    public ManagerStatus status { get; set ; }
    [SerializeField] Slider Ms;
    [SerializeField] Slider Ss;
    [SerializeField] GameObject SettingsImage;

    public void StartUP()
    {
        status = ManagerStatus.Started;
    }
    public void OpenOrCloseSettingsMenu()
    {
        SettingsImage.SetActive(!SettingsImage.active);
        GameObject.FindGameObjectWithTag("GameController").GetComponent<Managers>().contr.ResetPhone();
    }
}
