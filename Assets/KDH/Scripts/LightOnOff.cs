using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOnOff : MonoBehaviour
{
    public GameObject _1Flight_1;
    public GameObject _1Flight_2;
    public GameObject _2Flight;

    void LightSwitch()
    {
        if(GameManager.instance.puzzleLevel == 2)
        {
            _2Flight.SetActive(true);
        }
        if(GameManager.instance.clublast)
        {
            _1Flight_1.SetActive(false);
            _1Flight_2.SetActive(false);
        }
    }
}
