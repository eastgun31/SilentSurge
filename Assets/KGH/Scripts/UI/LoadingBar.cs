using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        target = slider.maxValue;
    }

    void Update()
    {
        currentValue = slider.value;
        if (currentValue <= slider.maxValue * 0.3f)
            time = 10f;
        else if (currentValue <= slider.maxValue * 0.5f)
            time = 5f;
        else if (currentValue >= slider.maxValue * 0.9f)
            time = 15f;

        if (currentValue < target)
        {
            currentValue += Time.deltaTime / time;
            slider.value = currentValue;
        }

        if (slider.value >= target)
            loading.SetActive(false);

        int percent = Mathf.RoundToInt((currentValue / target) * 100f);
        loadText.text = $"{percent}%";
    }
}
