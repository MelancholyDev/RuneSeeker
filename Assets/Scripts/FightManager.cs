using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightManager : MonoBehaviour,UIManagerPreset
{
    #region
    public GameObject Logs;
    public ManagerStatus status { get ; set ; }
    [HideInInspector] public bool placed;
    GameObject plane;
    public FightTurn turn;
    [HideInInspector]public string speelcast;
    [HideInInspector]public Enemy currentmonster;
    [HideInInspector]public GameObject sceneFaderer;   
    public GameObject Paint;
    [HideInInspector] public AudioSource FightSounds;
    [HideInInspector]public GameObject[] Debuffs=new GameObject[4];
    public Sprite[] RunesImages;
    [HideInInspector]public Image[] Runes=new Image[2];
    public int GestureNumer=0;
    public bool finishedanimation;
    public int Logsnumer;
    #endregion

    public void StartUP()
    {
        status = ManagerStatus.Started;
    }
    public void PlaceDebuffsInRightQue()
    {
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 3; j++)
                if (Debuffs[j].transform.position.y > Debuffs[j + 1].transform.position.y)
                {
                    GameObject a = Debuffs[j];
                    Debuffs[j] = Debuffs[j + 1];
                    Debuffs[j + 1] = a;
                }
    }
    public void InitializeFightGameObjects()
    {
        plane = GameObject.FindGameObjectWithTag("PlaneFinder");
        FightSounds = GameObject.FindGameObjectWithTag("FightSounds").GetComponent<AudioSource>();
        Debuffs = GameObject.FindGameObjectsWithTag("Debuff");
        Runes[0] = GameObject.FindGameObjectWithTag("FirstRune").GetComponent<Image>();
        Runes[1] = GameObject.FindGameObjectWithTag("SecondRune").GetComponent<Image>();
    }
    public void StartFight()
    {
        Logsnumer = 0;
        Paint.SetActive(false);
        InitializeFightGameObjects();
        Debug.Log(Logs==null);
        Logs.GetComponent<ContentScript>().texT.GetComponent<Text>().text = "История битвы:\n";
        PlaceDebuffsInRightQue();
        StartCoroutine(Fight());
    }
    public Sprite RuneSprite(string spellname)
    {
        switch (spellname)
        {
            case "Lightning":return RunesImages[0];
            case "Poison": return RunesImages[1]; 
            case "Flash": return RunesImages[2]; 
            case "Whirl": return RunesImages[3]; 
            default:return null;
        }
    }
    public void Spawn()
    {
        Managers.music.SpawnSound();
        currentmonster.GetComponent<Animator>().enabled = true;
        currentmonster.RefreshDebuffs();
        Managers.music.StartMonsterSounds();
    }
    public void Preparation()
    {
        placed = false;
        plane.SetActive(false);
        turn = FightTurn.Me;
        currentmonster = GameObject.FindGameObjectWithTag("Monster").GetComponent<Enemy>();
        sceneFaderer = GameObject.FindGameObjectWithTag("SceneFader");
        sceneFaderer.SetActive(false);
        Spawn();
    }
    IEnumerator Fight()
    {
        //preparezone
        plane.GetComponent<Vuforia.ContentPositioningBehaviour>().OnContentPlaced.AddListener((GameObject) => placed = true);
        while (!placed)
            yield return null;
        Preparation();
        //preparezone

        //fightzone
        while (Managers.player.Health>0 & currentmonster.HP>0)
        {           
            if (turn == FightTurn.Me)
            {
                Logs.GetComponent<ContentScript>().ChangeText("-----Ваш ход номер "+(++Logsnumer)+ "-----");
                Managers.player.changeMana(25);
                Paint.SetActive(true);
                speelcast = "";
                while (speelcast == "")
                    yield return null;
                Runes[0].sprite = RuneSprite(speelcast);
                Runes[0].gameObject.GetComponent<Animation>().Play("RuneAnimationOpen");             
                speelcast += "_";
                while(speelcast[speelcast.Length-1]=='_')
                    yield return null;
                Runes[1].sprite = RuneSprite(speelcast.Split('_')[1]);
                currentmonster.CastSpell(Managers.spells.SpellBook[speelcast]);
                Debug.Log("Усилитель урона:" + Managers.spells.SpellBook[speelcast].disarmour);
                Runes[0].gameObject.GetComponent<Animation>().Play("RuneAnimationClose");
                Runes[1].gameObject.GetComponent<Animation>().Play("RuneAnimationClose");
                Paint.SetActive(false);
                turn = FightTurn.Enemy;

                int nuu=0;
                yield return null;
                while (currentmonster.delayedamage!=0 & Managers.player.Health > 0 & currentmonster.HP > 0)
                {
                    Debug.Log(currentmonster.delayedamage);
                    yield return null;
                }
            }
            else
            {
                Logs.GetComponent<ContentScript>().ChangeText("-----Ход противника номер " + (Logsnumer-1) + "-----");
                currentmonster.MonsterDealDamage();
                currentmonster.GetBleedEffect();
                currentmonster.DispellEndTurnEffects();
                turn = FightTurn.Me;

                Debug.Log("НАЧАЛО:"+Managers.player.Health+"FinishedAnimation:"+finishedanimation);
                finishedanimation = false;
                yield return null;
                while (!finishedanimation & Managers.player.Health > 0 & currentmonster.HP > 0)
                {
                    Debug.Log(finishedanimation);
                    finishedanimation = currentmonster.DelayedAttack == 0;
                    yield return null;
                }
                
            }
        }
        //fightzone
       

        //endzone
        sceneFaderer.SetActive(true);
        EndStatus end;
        if (Managers.player.Health > 0)
        {
            end = EndStatus.Win;
            Logs.GetComponent<ContentScript>().ChangeText("Победа!");
            Managers.level.RefreshLevelPassed();
            yield return new WaitForSeconds(2);
        }
        else
        {
            end = EndStatus.Lose;
            Logs.GetComponent<ContentScript>().ChangeText("Поражение!");
            
        }
        int stars = Managers.player.Health > 90 ? 3 : Managers.player.Health > 60 ? 2 : Managers.player.Health > 25 ? 1 : 0;
        Debug.Log("StartEnding");
        GameObject.FindGameObjectWithTag("End").GetComponent<BattleEnd>().StartEnding(end, Managers.player.Health > 90 ? 3 : Managers.player.Health > 60 ? 2 : Managers.player.Health > 25 ? 1 : 0);
        char curelevel = Managers.level.CurrentScene[Managers.level.CurrentScene.Length - 1];
        if (PlayerPrefs.GetInt("Star" + curelevel) < stars)
            PlayerPrefs.SetInt("Star" + curelevel, stars);
        //endzone
    }

}


public enum FightTurn
{
    Me,
    Enemy
}
public enum EndStatus
{
    Win,
    Lose
}
