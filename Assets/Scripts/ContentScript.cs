using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentScript : MonoBehaviour
{
    [SerializeField] public GameObject texT;
    
    void Update()
    {
        if (Managers.fightmanager.Logs == null)
            Managers.fightmanager.Logs = gameObject;
        if (texT.GetComponent<RectTransform>().sizeDelta.y > GetComponent<RectTransform>().sizeDelta.y)
        {
            Debug.Log("Замена");
            GetComponent<RectTransform>().sizeDelta = texT.GetComponent<RectTransform>().sizeDelta;
        }
    }
    public void ChangeText(string text)
    {
        texT.GetComponent<Text>().text = texT.GetComponent<Text>().text + text + "\n";
        texT.GetComponent<RectTransform>().sizeDelta=new Vector2(texT.GetComponent<RectTransform>().sizeDelta.x, texT.GetComponent<RectTransform>().sizeDelta.y+37.1f);
    }
}
