using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public bool stunImune;
    public bool HardSkin;
    public bool HawkEye;

    [HideInInspector] public AudioSource monstersource;
    [SerializeField]public float MaxHP;
    [HideInInspector] public float HP;
    [HideInInspector]public Animator animator;
    [SerializeField] public float damage;
    [HideInInspector] public float Monsterchancetodealdamage;
    [HideInInspector] public float Monsterdisarmour;
    [HideInInspector] public bool Monsterstun;
    [HideInInspector] public float Monsterbleeddebuf;
    [HideInInspector] public float delayedamage;
    [HideInInspector]public float DelayedAttack;

     GameObject missText;
    public void Start()
    {
        missText = GameObject.FindGameObjectWithTag("MissText");
        HP = MaxHP;
        monstersource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        RefreshDebuffs();
    }
 
    public void RefreshDebuffs()
    {
        Monsterstun = false;
        Monsterdisarmour = 0;
        Monsterbleeddebuf = 0;
        Monsterchancetodealdamage = 100;
    }
   
    public void GetMonsterDamage(float dmg)
    {
        if (dmg == 0)
            return;
        Managers.music.DamageSound();
        Debug.Log("базовый:"+dmg+  "Усиленный"+ dmg * (1f + Monsterdisarmour / 100f)+"  дизармор:"+ Monsterdisarmour);
        delayedamage= dmg * (1f +  Monsterdisarmour / 100f) * (HardSkin ? 0.5f : 1);

        if (HP - delayedamage > 0)
        {
            animator.SetTrigger("Damage");
        }
        else
        {
            Managers.fightmanager.Logs.GetComponent<ContentScript>().ChangeText("Нанесено урона:" + delayedamage);
            Death();
        }

    }

    public void MonsterDealDamage()
    {
        DelayedAttack = damage;
        if (!Monsterstun)
            animator.SetTrigger("Attack");
        else
            DelayedAttack = 0;
        
    }
    public void AttackDamage()
    {
        Debug.Log("ATTACKDAMAGE");
        if (HawkEye || Random.Range(0, 100) < Monsterchancetodealdamage)
        {
            int RandomDamage = Random.Range((int)damage - 10, (int)damage +10);
            Managers.player.changeHealth(-DelayedAttack);
            DelayedAttack = 0;
        }
        else
        {
            DelayedAttack = 0;
            Managers.fightmanager.Logs.GetComponent<ContentScript>().ChangeText("Противник промахнулся!");
            missText.GetComponent<Animation>().Play();
        }
    }

    public void Death()
    {
        HP = 0;
        animator.SetBool("Death", true);
        Managers.fightmanager.turn = FightTurn.Enemy;
        GetComponentInChildren<Canvas>().gameObject.SetActive(false);
    }

    public void CastSpell(MagicSpell spell)
    {              
        if(Managers.player.Mana<spell.manacost)
        {
            GameObject.FindGameObjectWithTag("NotEnoughMana").GetComponent<Animation>().Play();
            return;
        }
        GetMonsterDamage(spell.damage);
        Managers.music.SpellSound(spell.soundeffect);    
        Monsterstun = spell.stun & !stunImune;
        if (Monsterstun)
            Managers.fightmanager.Logs.GetComponent<ContentScript>().ChangeText("Наложен эффект оглушения!");
        Monsterbleeddebuf = spell.bleedperturndamage;
        if (Monsterbleeddebuf>0)
            Managers.fightmanager.Logs.GetComponent<ContentScript>().ChangeText("Наложен эффект кровотечения:" + Monsterbleeddebuf);
        Monsterdisarmour = 0;
        Monsterdisarmour = spell.disarmour;  
        if(spell.disarmour>0)
            Managers.fightmanager.Logs.GetComponent<ContentScript>().ChangeText("Наложен эффект усугубления ран:" + spell.disarmour);
        Monsterchancetodealdamage -= spell.blinddebuff;
        if(Monsterchancetodealdamage!=100)
            Managers.fightmanager.Logs.GetComponent<ContentScript>().ChangeText("Наложен эффект ослепления:" + (100 - Monsterchancetodealdamage));
        Managers.player.changeMana(-spell.manacost);
        if (HP < 0)
            Death();
        RefreshMonsterStatus();
    }

    public void RefreshMonsterStatus()
    {
        for (int i = 0; i < 4; i++)
            Managers.fightmanager.Debuffs[i].GetComponent<Text>().text = "";
        if (Monsterstun == true)
            Managers.fightmanager.Debuffs[0].GetComponent<Text>().text = "Обездвижен!";
        if(Monsterbleeddebuf!=0)
            for(int i=0;i<4;i++)
            {
                if(Managers.fightmanager.Debuffs[i].GetComponent<Text>().text=="")
                {
                    Managers.fightmanager.Debuffs[i].GetComponent<Text>().text = "Кровотечение:" + Monsterbleeddebuf + " урона";
                    break;
                }
            }
        if (Monsterchancetodealdamage!= 100)
            for (int i = 0; i < 4; i++)
            {
                if (Managers.fightmanager.Debuffs[i].GetComponent<Text>().text == "")
                {
                    Managers.fightmanager.Debuffs[i].GetComponent<Text>().text = "Слепота:" + Monsterchancetodealdamage + " шанс попадания";
                    break;
                }
            }
        if ( Monsterdisarmour!= 0)
            for (int i = 0; i < 4; i++)
            {
                if (Managers.fightmanager.Debuffs[i].GetComponent<Text>().text == "")
                {
                    Managers.fightmanager.Debuffs[i].GetComponent<Text>().text = "Глубокая рана:" + Monsterdisarmour + "% доп урона";
                    break;
                }
            }

    }

    public void GetBleedEffect()
    {
        GetMonsterDamage(Monsterbleeddebuf);
    }


    public void DispellEndTurnEffects()
    {
        Monsterbleeddebuf = 0;
        Monsterchancetodealdamage = 100;       
        RefreshMonsterStatus();
    }

    public void LateUpdate()
    {
        animator.SetBool("Stun",Monsterstun);
    }
    public void DisableStunAnimation()
    {
        Monsterstun = false;
        RefreshMonsterStatus();
    }
    public void makedelayeddamage()
    {
        Managers.fightmanager.Logs.GetComponent<ContentScript>().ChangeText("Нанесено урона: " + delayedamage);
        HP -= delayedamage;
        delayedamage = 0;
        if (HP < 0)
            HP = 0;
    }

}
