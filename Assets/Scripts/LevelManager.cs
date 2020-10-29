using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour, UIManagerPreset
{
    public ManagerStatus status { get; set; }
    [HideInInspector] public int levelpassed = 0;
    public string CurrentScene="MainMenu";
    public int[] MaxStars = new int[5];
    public void StartUP()
    {
        status = ManagerStatus.Started;
    }
    public void LoadLevel(string levelname)
    {
        CurrentScene = levelname;
        Messenger.Broadcast("NEWLEVEL");
        SimpleSceneFader.ChangeSceneWithFade(levelname);
    }
    public void LoadLevelStars()
    {
        for(int i = 0; i < 5; i++)
        {
            MaxStars[i] = PlayerPrefs.GetInt("Star"+(i+1));
        }
    }
    public void LoadGame()
    {
        Managers.level.LoadLevelStars();
        Managers.inventory_.LoadInventory();
        Managers.player.LoadNickName();
        Managers.dialogueManager.LoadFinishedDialogs();
        levelpassed = PlayerPrefs.GetInt("LevelPassed");
        LoadLevel("Map");
    }
    public void InitializeEmptyFields()
    {
        Managers.inventory_.HealPotions = 0;
        Managers.inventory_.Manapotions = 0;
        Managers.inventory_.Money = 0;
        Managers.level.levelpassed = 0;
        for (int i = 0; i < 5; i++)
            Managers.level.MaxStars[i] = 0;
    }
    public void CreateEmptySave()
    {
        InitializeEmptyFields();
        PlayerPrefs.SetInt("ManaPotions",0);
        PlayerPrefs.SetInt("HealPotions", 0);
        PlayerPrefs.SetInt("Money", 0);
        PlayerPrefs.SetInt("LevelPassed", 0);
        for(int i=0;i<5;i++)
            PlayerPrefs.SetInt("Star"+i,0);

    }
    public void StartNewGame()
    {
        LoadLevel("Introduction");
        CreateEmptySave();
    }
    public void RefreshLevelPassed()
    {
        int num=0;
        string numer =""+ CurrentScene[CurrentScene.Length - 1];
        int.TryParse(numer,out num);
        if (num > PlayerPrefs.GetInt("LevelPassed"))
            PlayerPrefs.SetInt("LevelPassed", num);
        levelpassed = PlayerPrefs.GetInt("LevelPassed");
    }
    
   
    public void FinishCreatingSave()
    {
        PlayerPrefs.SetInt("NotEmpty",1);     
    }
    public void SaveLevelData()
    {
        PlayerPrefs.SetInt("LevelPassed", levelpassed);
    }
    public void OnApplicationQuit()
    {
        if (CurrentScene != "MainMenu")
        {
            Managers.music.SaveSoundPrefs();
            Managers.inventory_.ReSaveInventory();
            SaveLevelData();
            Managers.player.SaveNameData();
            Managers.dialogueManager.SaveDialogueData();
            PlayerPrefs.Save();
        }
        else
        {
            Managers.music.SaveSoundPrefs();
        }
    }


}
