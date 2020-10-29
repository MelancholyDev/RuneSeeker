using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleEnd : MonoBehaviour
{
    public GameObject[] Skulls;
    public Button ReturnButton;
    public GameObject LoseText;
    public GameObject Win;
    public GameObject Lose;
    public void StartEnding(EndStatus end, int num)
    {
        StartCoroutine(StartFinal(end, num));
    }
    IEnumerator StartFinal(EndStatus end,int num)
    {
        Managers.fightmanager.sceneFaderer.SetActive(true);
        GetComponent<Animation>().Play();
        while (GetComponent<Animation>().isPlaying)
            yield return null;
        ReturnButton.onClick.AddListener(()=>Managers.level.LoadLevel("Map"));
        if (end == EndStatus.Win)
        {
            Lose.SetActive(false);
            Win.SetActive(true);
            for (int i = 0; i < num; i++)
            {
                Skulls[i].GetComponent<Animation>().Play();
            }
        }
        else
        {
            Lose.SetActive(true);
            Win.SetActive(false);
            LoseText.GetComponent<Animation>().Play();
        }
    }
}
