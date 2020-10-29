using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour,CurrentManager
{
    [SerializeField] GameObject[] FootPrints;
    string[] buttonOnclick = { "1","2","3","4","5"};
    [SerializeField] Dialogue First;
    [SerializeField] Dialogue Second;
    [SerializeField] Dialogue Third;
    [SerializeField] Sprite[] dialogueBackImages;
    [SerializeField] Button[] FightButtons;
    [SerializeField] Image[,] skulls = new Image[5, 3];
    
    public bool DialogueFinish { get ; set; }
    public void EnableSkuls()
    {

        for (int i = 0; i < 5; i++)
            for (int j = 0; j < 3; j++)
            {
                skulls[i, j].gameObject.SetActive(Managers.level.levelpassed>=i);
                skulls[i, j].color = new Color(skulls[i, j].color.r, skulls[i, j].color.g, skulls[i, j].color.b, j < Managers.level.MaxStars[i] ? 1 : 0.3f);
            }
    }
    void Start()
    {
        Managers.level.LoadLevelStars();
        for (int i = 0; i < 5; i++)
            for (int j = 0; j < 3; j++)
                skulls[i, j] = FightButtons[i].gameObject.GetComponent<UiAnimation>().skuls[j];
        EnableSkuls();
        AddButtonsEvents();
        Managers.dialogueManager.BackImages = dialogueBackImages;      
        for (int i = 0; i <= Managers.level.levelpassed; i++)
        {
            if (i > 4)
                break;
            FightButtons[i].gameObject.SetActive(true);
        }
        for(int i=1;i< Managers.level.levelpassed; i++)
        {
            FootPrints[i-1].SetActive(true);
        }
        if (Managers.dialogueManager.FinishedDialogues==1)
        {
            Managers.level.FinishCreatingSave();
            Managers.dialogueManager.DialogueCanvas.SetActive(true);
            Managers.dialogueManager.StartDialogue(First);
        }
        if(Managers.dialogueManager.FinishedDialogues == 3 & Managers.level.levelpassed==1)
        {
            Managers.dialogueManager.DialogueCanvas.SetActive(true);
            Managers.dialogueManager.StartDialogue(Second);
        }
        if (Managers.dialogueManager.FinishedDialogues == 5 & Managers.level.levelpassed == 5)
        {
            Managers.dialogueManager.DialogueCanvas.SetActive(true);
            Managers.dialogueManager.StartDialogue(Third);
        }
    }
    public void Update()
    {
        if (PlayerPrefs.HasKey("SpellBook"))
            GameObject.FindGameObjectWithTag("GameController").GetComponent<Managers>().SpellBookUI.SetActive(true);
        if (DialogueFinish)
        {
            Managers.dialogueManager.DialogueCanvas.SetActive(false);
            DialogueFinish = !DialogueFinish;
        }
    }
   
    public void AddButtonsEvents()
    {
        FightButtons[0].onClick.AddListener(() => Managers.level.LoadLevel("Fight1"));
        FightButtons[1].onClick.AddListener(() => Managers.level.LoadLevel("Fight2"));
        FightButtons[2].onClick.AddListener(() => Managers.level.LoadLevel("Fight3"));
        FightButtons[3].onClick.AddListener(() => Managers.level.LoadLevel("Fight4"));
        FightButtons[4].onClick.AddListener(() => Managers.level.LoadLevel("Fight5"));
    }
}
