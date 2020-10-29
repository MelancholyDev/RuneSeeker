using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight3Manager : MonoBehaviour,CurrentManager
{
    [SerializeField] GameObject mainCanvas;
    [SerializeField] GameObject VuforiaCamera;
    [SerializeField] Sprite[] DialoguBackSprites;
    public bool DialogueFinish { get; set; }



    void Start()
    {
        mainCanvas.SetActive(true);
        Managers.dialogueManager.DialogueCanvas.SetActive(false);
        DialogueFinish = false;
        VuforiaCamera.SetActive(true);
        Managers.fightmanager.StartFight();

    }
   
}

