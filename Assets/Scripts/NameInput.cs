using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameInput : MonoBehaviour
{
    [SerializeField] InputField Name;
    [SerializeField] Canvas can;
    bool nameinputFinish;
    public void StartInput()
    {
        StartCoroutine(Input());
    }
    IEnumerator Input()
    {
        OpenOrCloseNameInput();
        can.gameObject.SetActive(false);
        while (!nameinputFinish)
            yield return null;

    }
    public void OpenOrCloseNameInput()
    {
        Name.gameObject.SetActive(!Name.gameObject.active);
    }
    public void Finish()
    {
        nameinputFinish = true;
    }

}
