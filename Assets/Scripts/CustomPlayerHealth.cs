using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPlayerHealth : MonoBehaviour
{
    private float currentPlayerHealth;
    [SerializeField] CustomPlayerMana man;
    private void Start()
    {
        gameObject.transform.localScale = new Vector3(1,1,1);
        currentPlayerHealth = 100;
    }
    void Update()
    {

        if (currentPlayerHealth != Managers.player.Health)
        {
            if (Mathf.Abs(Managers.player.Health - currentPlayerHealth) < 0.05)
            {
                currentPlayerHealth = Managers.player.Health;
                
            }
            else
            {
                gameObject.transform.localScale = new Vector3(Mathf.Lerp(currentPlayerHealth, Managers.player.Health, 0.01f) / 100, 1, 1);
                currentPlayerHealth = Mathf.Lerp(currentPlayerHealth, Managers.player.Health, 0.05f);
            }
        }
        
    }
}
