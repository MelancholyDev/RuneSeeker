using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class Fight1Manager : MonoBehaviour,CurrentManager
{
    /// <summary>
    /// //
    /// </summary>
    [SerializeField] GameObject mainCanvas;
    [SerializeField]GameObject VuforiaCamera;
    [SerializeField] Dialogue Fight1Dialogue;
    [SerializeField] Sprite[] DialoguBackSprites;
    public bool DialogueFinish { get ; set; }
    

    [SerializeField] GameObject FightCam;

    void Start()
    {
       
        Managers.music.SoundsNewScene();
        if (Managers.dialogueManager.FinishedDialogues == 2)
        {
            mainCanvas.SetActive(false);
            VuforiaCamera.SetActive(false);
            Managers.dialogueManager.DialogueCanvas.SetActive(true);
            Managers.dialogueManager.BackImages = DialoguBackSprites;  
            Managers.dialogueManager.StartDialogue(Fight1Dialogue);
        }
        else
        {
            DialogueFinish = true;
        }

    }
    void Update()
    {
        if (PlayerPrefs.HasKey("SpellBook"))
            GameObject.FindGameObjectWithTag("GameController").GetComponent<Managers>().SpellBookUI.SetActive(true);
        if (DialogueFinish)
        {
            mainCanvas.SetActive(true);
            Managers.dialogueManager.DialogueCanvas.SetActive(false);
            DialogueFinish = false;
            VuforiaCamera.SetActive(true);
            Managers.fightmanager.StartFight();
        }
    }

}
