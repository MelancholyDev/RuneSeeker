using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
   public void GameStarter()
   {
        if (PlayerPrefs.HasKey("NotEmpty"))
            Managers.level.LoadGame();
        else
            Managers.level.StartNewGame();


   }
    
}
