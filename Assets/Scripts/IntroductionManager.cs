using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroductionManager : MonoBehaviour, CurrentManager
{
    #region
    [SerializeField] Sprite[] introductionBack;
    [SerializeField] Dialogue introductionDialogue;
   GameObject DialogueCanvass;
    public bool DialogueFinish { get ; set ; }
    #endregion
    void Start()
    {
        Managers.dialogueManager.DialogueCanvas.SetActive(true);
        Managers.dialogueManager.BackImages = introductionBack;
        Managers.dialogueManager.StartDialogue(introductionDialogue);
    }
    public void Update()
    {
        if (DialogueFinish)
        {
            Managers.level.LoadLevel("Map");
            DialogueFinish = !DialogueFinish;
        }
    }

}
