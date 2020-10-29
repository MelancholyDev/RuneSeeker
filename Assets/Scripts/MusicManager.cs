using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour,UIManagerPreset
{
    
    [SerializeField] AudioSource MusicSound;
    AudioSource[] Sounds=new AudioSource[3];
    [SerializeField] public Slider MusicSlider;
    [SerializeField] public Slider SoundSlider;
    public ManagerStatus status { get; set; }
    public int lastsound=-1;
    bool endload = false;
    public bool changing=false;
    public void SpawnSound()
    {
        SoundsNewScene();
        Sounds[1].clip= Resources.Load("Audio/Spawn") as AudioClip;
        Sounds[1].Play();
    }
    public void EndLoad()
    {
        endload = true;
    }
    public void LoadSoundPrefs()
    {
        
        if (PlayerPrefs.HasKey("Music"))
            MusicSlider.value = PlayerPrefs.GetFloat("Music");
        if(PlayerPrefs.HasKey("Sound"))
            SoundSlider.value = PlayerPrefs.GetFloat("Sound");
    }
    public void SaveSoundPrefs()
    {
        PlayerPrefs.SetFloat("Music", MusicSlider.value);
        PlayerPrefs.SetFloat("Sound", SoundSlider.value);
    }
    public void StartUP()
    {
        LoadSoundPrefs();
        Messenger.AddListener("NEWLEVEL", EndLoad);
        Messenger.AddListener("NEWLEVEL", MusicClipChangeScene);
        MusicSound.clip = Resources.Load("Audio/" + Managers.level.CurrentScene) as AudioClip;
        MusicSound.Play();
        status = ManagerStatus.Started;
    }
    public void MusicClipChangeScene()
    {
        
        AudioClip clip = Resources.Load("Audio/"+Managers.level.CurrentScene) as AudioClip;
        StartCoroutine(MusicValueChanger(clip));       
    }
    IEnumerator Delay()
    {
        endload = false;
        yield return new WaitForSeconds(0.1f);
        MusicClipChangeScene();
    }
    public void DialogueClipChange(AudioClip clip)
    {
        StartCoroutine(MusicValueChanger(clip));       
    }
    IEnumerator MusicValueChanger(AudioClip clip)
    {
        changing = true;
        while (MusicSound.volume != 0)
        {
            MusicSound.volume = Mathf.Lerp(MusicSound.volume, 0, 0.05f);
            if (MusicSound.volume < 0.04)
                MusicSound.volume = 0;
            yield return null;
        }
        MusicSound.clip =clip;
        MusicSound.Play();
        while (MusicSound.volume != MusicSlider.value)
        {
            if (endload)
            {
                StartCoroutine(Delay());
                break;

            }
            MusicSound.volume = Mathf.Lerp(MusicSound.volume, MusicSlider.value, 0.05f);
            if (MusicSlider.value - MusicSound.volume < 0.04)
                MusicSound.volume = MusicSlider.value;
            yield return null;
        }
        changing = false;
    }
    public void SoundsNewScene()
    {
        Sounds[0] = GameObject.FindGameObjectWithTag("FightSounds").GetComponent<AudioSource>();
        Sounds[1] = GameObject.FindGameObjectWithTag("MonsterSounds").GetComponent<AudioSource>();
        ChangeSoundVolume();
    }
    public void ChangeMusicVolume()
    {
        MusicSound.volume = MusicSlider.value;
    }
    public void ChangeSoundVolume()
    {
        if (Sounds[0]!=null)
        {
            Sounds[0].volume = SoundSlider.value;
            Sounds[1].volume = SoundSlider.value;
        }
    }
    public void SpellSound(AudioClip clip)
    {
        Sounds[0].clip = clip;
        Sounds[0].Play();
    }
    public void StartMonsterSounds()
    {
        StartCoroutine(MonsterSoundsProcess()); 
    }
    IEnumerator MonsterSoundsProcess()
    {
        int soundnumer = 0;
        if(Managers.fightmanager.currentmonster.GetComponent<Enemy>()!=null)
        while (Managers.fightmanager.currentmonster != null && Managers.fightmanager.currentmonster.GetComponent<Enemy>().HP != 0  )
        {
            if (soundnumer != 10)
                soundnumer++;
            else
            {
                if(!Managers.fightmanager.currentmonster.GetComponent<Enemy>().animator.GetBool("Stun"))
                MonsterSound();
                soundnumer = 0;
            }
            yield return new WaitForSeconds(1);
        }

    }
    public void Update()
    {
        if (!changing) 
            MusicSound.volume = MusicSlider.value;
    }
    public void MonsterSound()
    {
        int curSounds=lastsound;

        while (lastsound==curSounds)
        {
            curSounds = Random.Range(1, 5);
        }
        if (!Managers.fightmanager.currentmonster.GetComponent<Enemy>().animator.GetBool("Stun"))
        {
            Sounds[1].clip = Resources.Load("Audio/Monster" + curSounds) as AudioClip;
            Sounds[1].Play();
        }
    }
    public void DamageSound()
    {
        Sounds[1].Stop();
        Sounds[1].clip= Resources.Load("Audio/Damage" ) as AudioClip;
        Sounds[1].Play();
    }
}
