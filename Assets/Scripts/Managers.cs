using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    #region
    public GameObject DialogueCanvas;
    public GameObject MeSample;
    public GameObject FairySample;
    public GameObject TextFieldSample;
    public GameObject Button1Sample;
    public GameObject Button2Sample;
    public GameObject itemGiveSample;
    public GameObject InputFieldSample;
    public GameObject dialogueBackgroundSample;
    public GameObject ArrowSample;
    #endregion
    public GameObject SpellBookUI;
    public AppController contr;
    public GameObject PhoneBack;
    static GameObject manager;
    public List<UIManagerPreset> _sequenceList;
    public static MagicBook spells;
    public static LevelManager level;
    public static MusicManager music;
    public static SettingsMenuManager _SettingsMenu;
    public static DialogueManager dialogueManager;
    public static PlayerManager player;
    public static InventoryManager inventory_;
    public static DialogueActions actions;
    public static FightManager fightmanager;
    public void PhoneOpen()
    {
        contr.ResetPhone();
        PhoneBack.SetActive(!PhoneBack.active);
    }
    void Awake()
    {
        SpellBookUI.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(PhoneOpen);
        DontDestroyOnLoad(gameObject);
        if (manager == null)
            manager =gameObject;
        else
            if (gameObject != manager)
            Destroy(gameObject);
        spells = GetComponent<MagicBook>();
        fightmanager= GetComponent<FightManager>();
        actions = GetComponent<DialogueActions>();
        level = GetComponent<LevelManager>();
        music = GetComponent<MusicManager>();
        dialogueManager = GetComponent<DialogueManager>();
        _SettingsMenu = GetComponent<SettingsMenuManager>();
        inventory_ = GetComponent<InventoryManager>();
        player = GetComponent<PlayerManager>();
        _sequenceList = new List<UIManagerPreset>();
        _sequenceList.Add(fightmanager);
        _sequenceList.Add(inventory_);
        _sequenceList.Add(spells);
        _sequenceList.Add(player);
        _sequenceList.Add(level);
        _sequenceList.Add(music);
        _sequenceList.Add(_SettingsMenu);
        _sequenceList.Add(dialogueManager);
        _sequenceList.Add(actions);
        StartCoroutine(StartAllManagers());
    }
    IEnumerator StartAllManagers()
    {
        foreach(UIManagerPreset man in _sequenceList)
        {
            man.StartUP();
        }
        int allman = _sequenceList.Count;
        int now = 0;
        while (now < allman)
        {
            int progress = now;
            now = 0;
            foreach (UIManagerPreset man in _sequenceList)
            {
                if (man.status == ManagerStatus.Started)
                    now++;
            }
            if (now > progress)
            yield return null;
        }

    }

    
}
