using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPlayerMana : MonoBehaviour
{
    private float currentPlayerMana;
    
    private void Start()
    {
        currentPlayerMana = 100;
        gameObject.transform.localScale=new Vector3(1,1,1);
    }
    void Update()
    {
        
        if (currentPlayerMana != Managers.player.Mana)
        {
            if (Mathf.Abs(Managers.player.Mana - currentPlayerMana) < 0.05)
            {
                currentPlayerMana = Managers.player.Mana;

            }
            else
            {
                gameObject.transform.localScale = new Vector3(Mathf.Lerp(currentPlayerMana, Managers.player.Mana, 0.01f) / 100, 1, 1);
                currentPlayerMana = Mathf.Lerp(currentPlayerMana, Managers.player.Mana, 0.05f);
            }
        }
    }
}
