using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    public Slider bar;

    private float vol = 1f;

    // Start is called before the first frame update
    void Start()
    {
        vol = PlayerPrefs.GetFloat("vol", 1f);
        bar.value = SoundManager.instance.audioPlayer.volume;
    }

    // Update is called once per frame
    void Update()
    {
        SliderBar();
    }

    public void SliderBar()
    {
        SoundManager.instance.audioPlayer.volume = bar.value;

        vol = bar.value;
        PlayerPrefs.GetFloat("vol", vol);
    }
}
