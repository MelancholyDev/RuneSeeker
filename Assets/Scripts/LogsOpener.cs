using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogsOpener : MonoBehaviour
{
    [SerializeField] GameObject ContenT;
    [SerializeField] GameObject Logs;
    void Awake()
    {
        Logs.SetActive(false);
        GetComponent<Button>().onClick.AddListener(()=>Logs.SetActive(!Logs.active));
        Managers.fightmanager.Logs = ContenT;
    }

}
