using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainStart : MonoBehaviour
{
    public GameObject mainButton;
    public GameObject modChoice;
    public GameObject optionWin;
    public GameObject infoWin;
    public GameObject backButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButton()
    {
        mainButton.SetActive(false);
        modChoice.SetActive(true);
        backButton.SetActive(true);
    }
    public void OptionWin()
    {
        optionWin.SetActive(true);
    }

    public void BackButton()
    {
        mainButton.SetActive(true);
        modChoice.SetActive(false);
        backButton.SetActive(false);
    }

    public void NormalStart()
    {
        SceneManager.LoadScene("Main");
    }

    public void InfoWin()
    {
        infoWin.SetActive(true);
    }
    public void CloseInfo()
    {
        infoWin.SetActive(false);
    }
    public void CloseOption()
    {
        optionWin?.SetActive(false);
    }
}
