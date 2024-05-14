using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    public Slider slider;
    public float time = 2f;

    private float target;
    private float currentValue;

    public GameObject loading;

    public Text loadText;

    void Start()
    {
        UiManager.instance.isPauseWin = true; 
        target = slider.maxValue;
    }

    void Update()
    {
        currentValue = slider.value;
        LoadingTime();

        if (currentValue < target)
        {
            currentValue += Time.deltaTime / time;
            slider.value = currentValue;
        }

        if (slider.value >= target)
        {
            loading.SetActive(false);
            UiManager.instance.isPauseWin = false;
        }

        int percent = Mathf.RoundToInt((currentValue / target) * 100f);
        loadText.text = $"{percent}%";
    }

    void LoadingTime()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1  || SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (currentValue <= slider.maxValue * 0.3f)
                time = 15f;
            else if (currentValue <= slider.maxValue * 0.5f)
                time = 4f;
            else if (currentValue >= slider.maxValue * 0.9f)
                time = 15f;
        }
        if(SceneManager.GetActiveScene().buildIndex ==3 || SceneManager.GetActiveScene().buildIndex == 4)
        {
            if (currentValue <= slider.maxValue * 0.3f)
                time = 2f;
            else if (currentValue <= slider.maxValue * 0.5f)
                time = 10f;
            else if (currentValue >= slider.maxValue * 0.9f)
                time = 25f;
        }
        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            if (currentValue <= slider.maxValue * 0.3f)
                time = 5f;
            else if (currentValue <= slider.maxValue * 0.5f)
                time = 15f;
            else if (currentValue >= slider.maxValue * 0.9f)
                time = 30f;
        }
    }
}
