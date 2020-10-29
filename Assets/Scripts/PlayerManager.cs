using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour,UIManagerPreset
{
    [HideInInspector] public string NickName;
    [HideInInspector] public float Health;
    [HideInInspector] public float Mana;
    public ManagerStatus status { get ; set; }

    public void StartUP()
    {
        status = ManagerStatus.Started;
        Messenger.AddListener("NEWLEVEL",ResetAll);
    }
    public void ResetAll()
    {
        if(!Application.loadedLevelName.Contains("Fight"))
        Health = 100;
        Mana = 100;
    }
    public void LoadNickName()
    {
        NickName = PlayerPrefs.GetString("NickName");
    }
    public void SetNickName(string value)
    {
        PlayerPrefs.SetString("NickName", value);
        NickName = value;
    }   
    public void changeHealth(float change)
    {

        if (Managers.fightmanager.Logs != null)
            if (change > 0)
                Managers.fightmanager.Logs.GetComponent<ContentScript>().ChangeText("Восстановлено здоровье:" + change);
        else
                Managers.fightmanager.Logs.GetComponent<ContentScript>().ChangeText("Получено урона:" + change);
        Health += change;
        if (Health > 100)
            Health = 100;
        if (Health < 0)
            Health = 0;
    }
    public void changeMana(float change)
    {
        if (Managers.fightmanager.Logs != null)
            if (change > 0)
                Managers.fightmanager.Logs.GetComponent<ContentScript>().ChangeText("Восстановлено маны:" + change);
            else
                Managers.fightmanager.Logs.GetComponent<ContentScript>().ChangeText("Затрачено маны:" + change);
        Mana += change;
        if (Mana > 100)
            Mana = 100;
        if (Mana < 0)
            Mana = 0;
    }
   public void SaveNameData()
   {
        PlayerPrefs.SetString("NickName", NickName);
   }


}
