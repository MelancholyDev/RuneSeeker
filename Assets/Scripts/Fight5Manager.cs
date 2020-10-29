using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight5Manager : MonoBehaviour,CurrentManager
{
    [SerializeField] GameObject mainCanvas;
    [SerializeField] GameObject VuforiaCamera;
    [SerializeField] Dialogue Fight1Dialogue;
    [SerializeField] Sprite[] DialoguBackSprites;
    public bool DialogueFinish { get; set; }


    [SerializeField] GameObject FightCam;

    void Awake()
    {
            DialoguBackSprites = Resources.LoadAll<Sprite>("Fight5");
    }
    void Start()
    {
        Managers.music.SoundsNewScene();
        if (Managers.dialogueManager.FinishedDialogues==4)
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
