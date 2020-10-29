using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaPotions : MonoBehaviour
{
    
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Managers.inventory_.UseManaPotion);
    }

    public void Update()
    {
    
        GetComponent<Button>().interactable = Managers.fightmanager.turn == FightTurn.Me;
    }
}
