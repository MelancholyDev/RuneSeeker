using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPotions : MonoBehaviour
{
   
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Managers.inventory_.UsehealthPotion);
    }
    public void Update()
    {
        GetComponent<Button>().interactable = Managers.fightmanager.turn == FightTurn.Me;
    }


}
