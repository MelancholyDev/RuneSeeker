using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomHealth : MonoBehaviour
{
    public float nowhealth;
    Enemy curMonster;
    public void Start()
    {
        curMonster = GameObject.FindGameObjectWithTag("Monster").GetComponent<Enemy>();
        nowhealth = curMonster.MaxHP;
    }
    void Update()
    {
        if (nowhealth != curMonster.HP)
        {
            nowhealth = Mathf.Lerp(nowhealth, curMonster.HP, (float)0.1);
            gameObject.transform.localScale = new Vector3((float)nowhealth/curMonster.MaxHP,gameObject.transform.localScale.y,1);
        }
    }
}
