using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour,UIManagerPreset
{
    public ManagerStatus status { get; set; }
    [HideInInspector]public int Manapotions;
    [HideInInspector] public int HealPotions;
    [HideInInspector] public int Money;
    public void StartUP()
    {
    }
    public void LoadInventory()
    {
        Manapotions = PlayerPrefs.GetInt("ManaPotions");
        HealPotions = PlayerPrefs.GetInt("HealPotions");
    }
    public void UseManaPotion()
    {
        if(Manapotions>0)
        {
            Manapotions--;
            Managers.player.changeMana(50);
            PlayerPrefs.SetInt("ManaPotions", Manapotions);
        }
    }
    public void UsehealthPotion()
    {
        if (HealPotions > 0)
        {
            HealPotions--;
            PlayerPrefs.SetInt("HealPotions", HealPotions);
            
            Managers.player.changeHealth(50);
        }
    }
    public void AddManapotion(int num)
    {
        Manapotions += num;
        PlayerPrefs.SetInt("ManaPotions",Manapotions);
    }
    public void AddHealpotion(int num)
    {
        HealPotions += num;
        PlayerPrefs.SetInt("HealPotions", HealPotions);
    }
    public void AddMoney(int number)
    {
        Money += number;
    }
    public void ReSaveInventory()
    {
        PlayerPrefs.SetInt("ManaPotions",Manapotions);
        PlayerPrefs.SetInt("HealPotions",HealPotions);

    }
}
