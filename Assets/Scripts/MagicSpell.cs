using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSpell
{
    
    public MagicSpell(AudioClip soundeffect, int blinddebuff, bool stun, int bleedperturndamage, int disarmour, int damage,int manacost)
    {
        this.soundeffect = soundeffect;
        this.blinddebuff = blinddebuff;
        this.stun = stun;
        this.bleedperturndamage=bleedperturndamage;
        this.disarmour = disarmour;
        this.damage = damage;
        this.manacost = manacost;

    }
    [HideInInspector] public AudioClip soundeffect;
    [HideInInspector] public int blinddebuff;
    [HideInInspector] public bool stun;
    [HideInInspector] public int bleedperturndamage;
    [HideInInspector] public int disarmour;
    [HideInInspector] public int damage;
    [HideInInspector] public int manacost;
    [HideInInspector] public string name;


}
