using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBook : MonoBehaviour, UIManagerPreset
{
    public ManagerStatus status { get ; set ; }
    public Dictionary<string, MagicSpell> SpellBook=new Dictionary<string, MagicSpell>();

    public void Awake()
    {
        int i = 0;
                                                //звук      слеп стан  блид диз урон мана
        Lightning_Lightning = new MagicSpell(audioclips[i],  0, true, 0, 0,70,70);
        i++;
        Lightning_Flash = new MagicSpell(audioclips[i],  50, true, 0, 0, 55, 55);
        i++;
        Lightning_Poison = new MagicSpell(audioclips[i], 0, true, 20, 0, 60, 70);
        i++;
        Lightning_Whirl = new MagicSpell(audioclips[i],  0, true, 0, 25, 40, 55);
        i++;
        Whirl_Whirl = new MagicSpell(audioclips[i], 0, false, 0, 75, 20, 40);
        i++;
        Whirl_Flash = new MagicSpell(audioclips[i], 25, false, 0, 25, 35, 40);
        i++;
        Whirl_Poison = new MagicSpell(audioclips[i],  0, false, 20, 30, 30, 45);
        i++;
        Flash_Flash = new MagicSpell(audioclips[i],  100, false, 0, 0, 60, 60);
        i++;
        Flash_Poison = new MagicSpell(audioclips[i], 50, false, 20, 0, 30, 40);
        i++;
        Poison_Poison = new MagicSpell(audioclips[i], 0, false, 60, 0, 0, 50);
        SpellBook.Add("Lightning_Lightning", Lightning_Lightning);
        SpellBook.Add("Lightning_Flash", Lightning_Flash);
        SpellBook.Add("Flash_Lightning", Lightning_Flash);
        SpellBook.Add("Lightning_Poison", Lightning_Poison);
        SpellBook.Add("Poison_Lightning", Lightning_Poison);
        SpellBook.Add("Lightning_Whirl", Lightning_Whirl);
        SpellBook.Add("Whirl_Lightning", Lightning_Whirl);
        SpellBook.Add("Whirl_Whirl", Whirl_Whirl);
        SpellBook.Add("Whirl_Flash", Whirl_Flash);
        SpellBook.Add("Flash_Whirl", Whirl_Flash);
        SpellBook.Add("Whirl_Poison", Whirl_Poison);
        SpellBook.Add("Poison_Whirl", Whirl_Poison);
        SpellBook.Add("Flash_Flash", Flash_Flash);
        SpellBook.Add("Flash_Poison", Flash_Poison);
        SpellBook.Add("Poison_Flash", Flash_Poison);
        SpellBook.Add("Poison_Poison", Poison_Poison);
        
    }

    public void StartUP()
    {
        status = ManagerStatus.Started;
    }

    [SerializeField] AudioClip[] audioclips;
    [SerializeField] ParticleSystem[] particlesystems;
    MagicSpell Lightning_Lightning;
    MagicSpell Lightning_Flash;
    MagicSpell Lightning_Poison;
    MagicSpell Lightning_Whirl;

    MagicSpell Whirl_Whirl;
    MagicSpell Whirl_Poison;
    MagicSpell Whirl_Lightning;
    MagicSpell Whirl_Flash;

    MagicSpell Flash_Flash;
    MagicSpell Flash_Lightning;
    MagicSpell Flash_Poison;
    MagicSpell Flash_Whirl;

    MagicSpell Poison_Poison;
    MagicSpell Poison_Lightning;
    MagicSpell Poison_Flash;
    MagicSpell Poison_Whirl;
}
