using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource audioSource;
    public AudioClip[] bgmClips;
    public AudioClip[] effectClips;

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
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 체인을 걸어서 이 함수는 매 씬마다 호출, 델리게이트체인
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(scene.buildIndex);
        bgmPlay(scene.buildIndex);
    }

    public void bgmPlay(int i)
    {
        audioSource.Stop();
        audioSource.clip = bgmClips[i];
        audioSource.loop = true;
        audioSource.Play();
    }

    
}
