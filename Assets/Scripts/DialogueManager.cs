using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
public class DialogueManager : MonoBehaviour, UIManagerPreset
{
    public ManagerStatus status { get; set; }
    [HideInInspector] public int FinishedDialogues;
    [HideInInspector] public Image dialogueBackground;
    [HideInInspector] public GameObject Me;
    [HideInInspector] public GameObject Fairy;   
    [HideInInspector] public GameObject TextField;
    [HideInInspector] public GameObject ItemGive;
    [HideInInspector] public GameObject Inputfield;
    [HideInInspector] public int Chosen;
    [HideInInspector] public Sprite[] BackImages;
    [HideInInspector] public GameObject sceneManager;
    [HideInInspector] public GameObject Arrow;
    [HideInInspector] public GameObject DialogueCanvas;
    public int delay=1;
    
    int DialogueBackgroundArrayNumer = 0;
    int framechecker;
    bool chosenbool;
    public TimeStorage DialogueStorage=new TimeStorage();
    DialogueReplique curReplique;
    public Dialogue curdial;
    public string NameOfDialogue;

    public void StartUP()
    {
        DialogueCanvas = GetComponent<Managers>().DialogueCanvas;
        status = ManagerStatus.Started;
    }
    public void LoadFinishedDialogs()
    {
        FinishedDialogues = PlayerPrefs.GetInt("FinishedDialogues");
    }
    public void StartDialogue(Dialogue dial)
    {
        delay = dial.delay;
        curdial = dial;
        TextAsset asset = Resources.Load<TextAsset>("Dialogues/" + dial.DialogueName + "/0");
        if(asset==null)
            asset= Resources.Load<TextAsset>("Dialogues/" + dial.DialogueName + "/1");
        curReplique = JsonUtility.FromJson<DialogueReplique>(asset.text);
        DialogueStorage.CleanStorage();
        framechecker = 0;
        initializeFields(); 
        StartCoroutine(DialogueProgress(dial));       

    }
    IEnumerator DialogueProgress(Dialogue dial)
    {

        while(true)
        {
            if (curReplique.currentTalker == Who.Opponent)
            {
                
                Arrow.SetActive(false);
                ActivateFairyPhase();
                Fairy.GetComponent<Image>().sprite = Resources.Load<Sprite>("DialogueImages/Fairy/"+curReplique.ImageName);
                string curentsentence;
                curentsentence = curReplique.Sentence;
                TextField.GetComponent<Text>().text = "";
                bool Skipdialogue = false;
                TextField.GetComponent<Button>().onClick.AddListener(() => Skipdialogue = true);
                for (int j = 0; j < curentsentence.Length; j++)
                {
                    TextField.GetComponent<Text>().text += curentsentence.ToCharArray()[j];
                    if (Skipdialogue)
                    {
                        TextField.GetComponent<Text>().text = curentsentence;
                        break;
                    }
                        yield return new WaitForSeconds(0.05f);
                    
                }
                Skipdialogue = false;
                TextField.GetComponent<Button>().onClick.RemoveAllListeners();
                if (curReplique.CurEvent.EventNum == 1 & curReplique.NextReplique != curReplique.SerialNumber)
                    Managers.actions.StartAction(curReplique);
                else if (curReplique.CurEvent.EventNum == 1 & curReplique.NextReplique == curReplique.SerialNumber)
                    curReplique.NextReplique = curReplique.NextReplique + 1;
                else Managers.actions.StartAction(curReplique);
                if (curReplique.CurEvent.EventNum>0)
                {              
                    while (!Managers.actions.finishaction)
                        yield return 0.3f;
                }
                if (curReplique.CurEvent.EventNum != 1)
                {
                    bool pressed = false;
                    TextField.GetComponent<Button>().onClick.RemoveAllListeners();
                    TextField.GetComponent<Button>().onClick.AddListener(()=>pressed=true);
                    while (!pressed)
                    {
                        if (Arrow.active == false)
                        {
                            Arrow.SetActive(true);
                            Arrow.GetComponent<Animation>().Play();
                        }
                        yield return null;
                    }
                    TextField.GetComponent<Button>().onClick.RemoveAllListeners();
                }
                if (curReplique.NextReplique < 0)
                    break;
                if (curReplique.CurEvent.EventNum != 1)
                {
                    TextAsset asset = Resources.Load<TextAsset>("Dialogues/" + dial.DialogueName + "/" + curReplique.NextReplique);
                    curReplique = JsonUtility.FromJson<DialogueReplique>(asset.text);
                }
                else
                {
                    curReplique = Managers.actions.timelyReplique;
                }
                Messenger.Broadcast("ENDFAIRYTURN");
            }
            else
            {
                ActivateMePhase();
                Me.GetComponent<Image>().sprite = Resources.Load<Sprite>("DialogueImages/Me/"+curReplique.ImageName);
                GameObject.FindGameObjectWithTag("Button1").GetComponentInChildren<Text>().text = curReplique.Sentence.Split('%')[0];
                GameObject.FindGameObjectWithTag("Button2").GetComponentInChildren<Text>().text = curReplique.Sentence.Split('%')[1];
                chosenbool = false;
                while (!chosenbool)
                    yield return null;
                TextAsset asset = Resources.Load<TextAsset>("Dialogues/" + dial.DialogueName + "/" + curReplique.NextReplique);
                curReplique = JsonUtility.FromJson<DialogueReplique>(asset.text);
            }
        }
        DialogueStorage.ReleaseStorage();
        DialogueStorage.CleanStorage();
        sceneManager.GetComponent<CurrentManager>().DialogueFinish = true;
        Managers.actions.SetDialogueFinish();
    } 
    public void initializeFields()
    {
        Me = GetComponent<Managers>().MeSample;
        Fairy = GetComponent<Managers>().FairySample;
        TextField = GetComponent<Managers>().TextFieldSample;
        GetComponent<Managers>().Button1Sample.GetComponent<Button>().onClick.AddListener(()=> Choose(1));
        GetComponent<Managers>().Button2Sample.GetComponent<Button>().onClick.AddListener(() => Choose(2));
        ItemGive = GetComponent<Managers>().itemGiveSample;
        Inputfield = GetComponent<Managers>().InputFieldSample;
        dialogueBackground = GetComponent<Managers>().dialogueBackgroundSample.GetComponent<Image>();
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager");
        DialogueBackgroundArrayNumer = 0;
        Arrow = GetComponent<Managers>().ArrowSample;
    }  
    public void Update()
    {
        if(BackImages!=null)
        if (dialogueBackground!=null)
        {
            if (framechecker++ == delay)
            {
                if (DialogueBackgroundArrayNumer <= BackImages.Length)
                {
                    dialogueBackground.sprite = BackImages[DialogueBackgroundArrayNumer];
                    DialogueBackgroundArrayNumer++;
                    if (DialogueBackgroundArrayNumer == BackImages.Length)
                        DialogueBackgroundArrayNumer = 0;
                }
                framechecker = 0;
            }          
        }
        

    }
    public void ActivateFairyPhase()
    {
        Inputfield.SetActive(false);
        ItemGive.SetActive(false);
        TextField.SetActive(true);
        Fairy.SetActive(true);
        Me.SetActive(false);
    }
    public void ActivateMePhase()
    {
        Inputfield.SetActive(false);
        Fairy.SetActive(false);
        Me.SetActive(true);
        TextField.SetActive(false);
        ItemGive.SetActive(false);
    }
    public void Choose(int i)
    {
        Managers.dialogueManager.curReplique.NextReplique = Managers.dialogueManager.curReplique.SerialNumber + i;
        chosenbool = true;
        
    }
    public void SaveDialogueData()
    {
        PlayerPrefs.SetInt("FinishedDialogues",FinishedDialogues);
    }
}
public class TimeStorage
{
    public int Money=0;
    public int Mana=0;
    public int Heal=0;
    public void CleanStorage()
    {
        Money = 0;
        Mana = 0;
        Heal = 0;
    }
    public void addMoney(int num)
    {
        Money += num;

    }
    public void addMana(int num)
    {
        Mana += num;

    }
    public void addHeal(int num)
    {
        Heal += num;

    }
    public void ReleaseStorage()
    {
        Managers.inventory_.AddHealpotion(Heal);
        Managers.inventory_.AddManapotion(Mana);
        Managers.inventory_.AddMoney(Money);
    }
}
