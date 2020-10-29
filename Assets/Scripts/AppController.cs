using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppController : MonoBehaviour
{
    [SerializeField] Image first;
    [SerializeField] Image second;
    [SerializeField] Text spelltext;
    [SerializeField] Button Forward;
    [SerializeField] Button Backward;
    [SerializeField] Button appButton;
    [SerializeField] Button back_button;
    [SerializeField] Image Phone;
     string[] spells = {"Lightning_Lightning", "Lightning_Flash", "Lightning_Poison", "Lightning_Whirl", "Whirl_Whirl", "Whirl_Flash", "Whirl_Poison", "Flash_Flash", "Flash_Poison", "Poison_Poison" };
    int CurrentSpellIndex=0;
    public void ResetPhone()
    {
        GetComponent<RectTransform>().localScale = new Vector3(0,0,1);
        Phone.gameObject.SetActive(false);
    }
    private void Start()
    {
        GetComponent<Animation>()["AppAnimation"].speed = 3;
        GetComponent<Animation>()["AppBack"].speed = 3;
        Forward.onClick.AddListener(MoveNext);
        Backward.onClick.AddListener(MoveBack);
        ChangeSpell();
        appButton.onClick.AddListener(()=> { if (GetComponent<RectTransform>().localScale.x <= 0) GetComponent<Animation>().Play("AppAnimation");Debug.Log(GetComponent<RectTransform>().localScale.x); });
        back_button.onClick.AddListener(Back_Button);
    }
    public void MoveNext()
    {
        CurrentSpellIndex++;
        ChangeSpell();
    }
    public void MoveBack()
    {
        CurrentSpellIndex--;
        ChangeSpell();
    }
    public void Back_Button()
    {
        Debug.Log("Сделал");
        if (GetComponent<RectTransform>().localScale.x < 1)
            Phone.gameObject.SetActive(false);
        else
            GetComponent<Animation>().Play("AppBack");


    }
    public void ChangeSpell()
    {
        MagicSpell curspell = Managers.spells.SpellBook[spells[CurrentSpellIndex]];
        first.sprite = Resources.Load<Sprite>("Spells/"+spells[CurrentSpellIndex].Split('_')[0]);
        second.sprite = Resources.Load<Sprite>("Spells/" + spells[CurrentSpellIndex].Split('_')[1]);
        spelltext.text = "Урон:" + curspell.damage + "\nСтоимость:" + curspell.manacost + "\n";
        if (curspell.disarmour > 0)
            spelltext.text += "Усугубление ран:" + curspell.disarmour + "%\n";
        if(curspell.bleedperturndamage>0)
            spelltext.text += "Яд:" + curspell.bleedperturndamage + "\n";
        if(curspell.stun)
            spelltext.text += "Оглушение" +  "\n";
        if(curspell.blinddebuff>0)
            spelltext.text += "Ослепление:" +curspell.blinddebuff+ "\n";

    }

    void Update()
    {
        switch (CurrentSpellIndex)
        {
            case 0: { Forward.gameObject.SetActive(true);Backward.gameObject.SetActive(false); }; break;
            case 9: { Backward.gameObject.SetActive(true); Forward.gameObject.SetActive(false); }; break;
            default: { Backward.gameObject.SetActive(true); Forward.gameObject.SetActive(true); }; break;
        }
    }

}
