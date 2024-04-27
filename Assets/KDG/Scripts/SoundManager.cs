using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource audioPlayer;
    public AudioSource effectPlayer;
    public AudioSource enemyPlayer;

    public AudioClip[] bgmClips;
    public AudioClip[] effectClips;
    public AudioClip[] uiClips;
    public AudioClip[] enemyClips;

    [SerializeField]
    private bool playingSource = false;
    public bool playingAction = false;

    private void Awake()
    {
        if(instance != null)
            Destroy(gameObject);
        else
            instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        //volume = audioPlayer.volume;   
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // ü���� �ɾ �� �Լ��� �� ������ ȣ��, ��������Ʈü��
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(scene.buildIndex);
        bgmPlay(scene.buildIndex);
    }

    public void bgmPlay(int i)
    {
        audioPlayer.Stop();
        audioPlayer.volume = 0.3f;
        audioPlayer.clip = bgmClips[i];
        audioPlayer.loop = true;
        audioPlayer.Play();
    }

    public void EffectPlay(int i, bool type)
    {
        if(type)
            audioPlayer.PlayOneShot(effectClips[i]);
        else if(!type && !playingSource)
        {
            effectPlayer.clip = effectClips[i];
            effectPlayer.loop = true;
            effectPlayer.Play();
            playingSource = true;
        }
    }
    public void EffectOff()
    {
        effectPlayer.Stop();
        effectPlayer.clip = null;
        playingSource = false;
    }
    public void EnemyEffect(int i)
    {
        enemyPlayer.PlayOneShot(enemyClips[i]);
    }
    public void UiSoundPlay(int i)
    {
        audioPlayer.PlayOneShot(uiClips[i]);
    }
    
}
