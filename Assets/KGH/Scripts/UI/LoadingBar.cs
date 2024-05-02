using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    public Slider slider;
    public float time = 5f;

    private float target;
    private float loadingtime;

    public GameObject loading;

    void Start()
    {
        target = slider.maxValue;
    }

    void Update()
    {
        loadingtime += Time.deltaTime;
        float currentvalue = Mathf.Lerp(0f, target, loadingtime / time);
        slider.value = currentvalue;
        if(slider.value >= target)
        {
            loading.SetActive(false);
        }
    }
}
