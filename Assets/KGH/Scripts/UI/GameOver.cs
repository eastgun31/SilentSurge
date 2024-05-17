using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Image Image;

    public GameObject[] button;
    public GameObject text;
    public GameObject gameover;

    ChangeCursor changeCursor;

    // Update is called once per frame
    void Update()
    {
       StartCoroutine(PlayGameOver());
    }
    IEnumerator PlayGameOver()
    {
        UiManager.instance.isGameOver = true;

        float targetAlpha = 1.0f;
        float duration = 2.0f;
        float elapsedTime = 0.0f;


        Color color = Image.color;
        color.a = 0.0f;
        Image.color = color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            color.a = Mathf.Lerp(0, targetAlpha, t);
            Image.color = color;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        SetButton();
        text.SetActive(true);
    }

    public void SetButton()
    {
        button[0].SetActive(true);
        button[1].SetActive(true);
        button[2].SetActive(true);
    }
    void  ClosedButton()
    {
        button[0].SetActive(false);
        button[1].SetActive(false);
        button[2].SetActive(false);
        text.SetActive(false);
    }

    public void CheckPoint()
    {
        UiManager.instance.isGameOver = false;
        ClosedButton();
        DataManager.instance.LoadData();
    }

    public void Restart()
    {
        UiManager.instance.isGameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void GoToMain()
    {
        UiManager.instance.isGameOver = false;
        ClosedButton();
        SceneManager.LoadScene("GameStart");
    }
}
