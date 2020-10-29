using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthCurrent : MonoBehaviour
{
    public Enemy curEnemy;

   
    void Update()
    {
        GetComponent<Text>().text = curEnemy.HP +"/"+ curEnemy.MaxHP;
    }
}
