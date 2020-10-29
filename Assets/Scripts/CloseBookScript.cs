using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CloseBookScript : MonoBehaviour
{
    [SerializeField] GameObject firstOs;
    [SerializeField] GameObject secondOs;
    [SerializeField] Slider Ms;
    [SerializeField] Slider Ss;
    [SerializeField] Button spellbook;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(StartClose);
    }

    public void StartClose()
    {
        Ms.interactable = true;
        Ss.interactable = true;
        spellbook.interactable = true;
        firstOs.GetComponent<Animation>().Play("CloseBookFirstOs");
        secondOs.GetComponent<Animation>().Play("CloseBookSecondOs");
    }
}
