using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueActions:MonoBehaviour,UIManagerPreset
{
    [HideInInspector] public bool finishaction;
    public ManagerStatus status { get ; set ; }
    public void StartUP()
    {
        status = ManagerStatus.Started;
    }
    [SerializeField] Sprite[] itemGiveSprites;
    [HideInInspector] public DialogueReplique timelyReplique;
    public void Awake()
    {
        Messenger.AddListener("ENDFAIRYTURN", ReturnItemgiveScale);
    }
    public void StartAction(DialogueReplique rep)
    {
        switch(rep.CurEvent.EventNum)
        {
            case 1: StartCoroutine(Nameinput(rep)); break;
            case 2: StartCoroutine(GiveMoney(rep.CurEvent.NumOfThings)); break;
            case 3: StartCoroutine(GiveHealthPack(rep.CurEvent.NumOfThings)); break;
            case 4: StartCoroutine(GiveManaPack(rep.CurEvent.NumOfThings)); break;
            case 5: StartCoroutine(ActivateSpellBook()); break;
            case 6: StartCoroutine(LoadFinish());break;
            case 7: StartCoroutine(MusicChanger(rep));break;
            case 8: {Managers.dialogueManager.FinishedDialogues++; Managers.level.LoadLevel("End"); };break;
        }
    }
    IEnumerator MusicChanger(DialogueReplique rep)
    {
       
        switch (rep.CurEvent.NumOfThings)
        {
            case 1:Managers.music.DialogueClipChange(Resources.Load<AudioClip>("Audio/Dialogues/"+1));break;
        }
        yield return null;
     
        finishaction = true;
    }
    IEnumerator LoadFinish()
    {
        yield return null;
    }
    IEnumerator Nameinput(DialogueReplique rep)
    {
        Debug.Log("УУУУУУУУУУУУУУУУУУУУУУУУУ");
        yield return new WaitForSeconds(0.7f);
        Managers.dialogueManager.Inputfield.SetActive(true);
        Managers.dialogueManager.Fairy.SetActive(false);
        GameObject back = GameObject.FindGameObjectWithTag("TextBack");
        back.SetActive(false);
        bool finishinput=false;
        Managers.dialogueManager.Inputfield.GetComponentInChildren<Button>().onClick.AddListener(()=>finishinput=true);
        while (!finishinput)
            yield return null;       
        Managers.player.SetNickName(GameObject.FindGameObjectWithTag("Name").GetComponent<Text>().text);
        string RepliqueChoose="";
        Debug.Log("Никнейм:"+ Managers.player.NickName);
        if (Managers.player.NickName.Length < 1)
        {
            RepliqueChoose = "Не хочешь говорить,ну что же,твое дело...";
        }
        else
        {
            switch (Random.Range(1, 4))
            {
                case 1: RepliqueChoose = Managers.player.NickName + " какое красивое имя..."; break;
                case 2: RepliqueChoose = "Рада познакомиться," + Managers.player.NickName + "."; break;
                case 3: RepliqueChoose = "Рада знакомству," + Managers.player.NickName; break;
            }
        }
        timelyReplique = new DialogueReplique(-100, RepliqueChoose, rep.SerialNumber+1,Who.Opponent,new DialogueEvent(-1,0),1);
        Managers.dialogueManager.Inputfield.SetActive(false);       
        back.SetActive(true);
        finishaction = true;
    }
    IEnumerator GiveMoney(int number)
    {
        Managers.dialogueManager.DialogueStorage.addMoney(number);
        Managers.dialogueManager.ItemGive.SetActive(true);
        Managers.dialogueManager.ItemGive.GetComponent<Image>().sprite = itemGiveSprites[0];
        Managers.dialogueManager.ItemGive.GetComponentInChildren<Text>().text = "x" + number;
        Managers.dialogueManager.ItemGive.GetComponent<Animation>().Play();
        while (Managers.dialogueManager.ItemGive.GetComponent<Animation>().isPlaying)
            yield return null;
        finishaction = true;
    }
    IEnumerator GiveHealthPack(int number)
    {
        Managers.dialogueManager.DialogueStorage.addHeal(number);
        Managers.dialogueManager.ItemGive.SetActive(true);
        Managers.dialogueManager.ItemGive.GetComponent<Image>().sprite = itemGiveSprites[1];
        Managers.dialogueManager.ItemGive.GetComponentInChildren<Text>().text = "x" + number;
        Managers.dialogueManager.ItemGive.GetComponent<Animation>().Play();
        while (Managers.dialogueManager.ItemGive.GetComponent<Animation>().isPlaying)
            yield return null;
        finishaction = true;

    }   
    IEnumerator GiveManaPack(int number)
    {
        Managers.dialogueManager.DialogueStorage.addMana(number);
        Managers.dialogueManager.ItemGive.SetActive(true);
        Managers.dialogueManager.ItemGive.GetComponent<Image>().sprite = itemGiveSprites[2];
        Managers.dialogueManager.ItemGive.GetComponentInChildren<Text>().text = "x" + number;
        Managers.dialogueManager.ItemGive.GetComponent<Animation>().Play();
        while (Managers.dialogueManager.ItemGive.GetComponent<Animation>().isPlaying)
            yield return null;
        finishaction = true;
    }
    public void ReturnItemgiveScale()
    {
        finishaction = false;
        Managers.dialogueManager.ItemGive.transform.localScale = new Vector3(0, 0, 1);
    }
    IEnumerator ActivateSpellBook()
     {
        PlayerPrefs.SetInt("SpellBook",1);
        finishaction = true;
        yield return null;

     }
    public void SetDialogueFinish()
    {
        Managers.dialogueManager.FinishedDialogues++;
    }
}
