using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2_Ending : MonoBehaviour
{
    public ParticleSystem explosion1;
    public ParticleSystem explosion2;
    public ParticleSystem explosion3;

    public GameObject car;

    public GameObject clear;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        explosion1.Stop(); explosion2.Stop(); explosion3.Stop();
        StartCoroutine(Explosion());

    }
    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(7f);
        explosion1.Play();
        SoundManager.instance.EffectPlay(4, true, 0.3f);
        yield return new WaitForSeconds(0.2f);
        explosion1.Stop();
        explosion2.Play();
        SoundManager.instance.EffectPlay(4, true, 0.3f);
        yield return new WaitForSeconds(0.4f);
        explosion2.Stop();
        explosion3.Play();
       
        yield return new WaitForSeconds(5f);
        explosion3.Stop(); 
        SoundManager.instance.EffectPlay(4, true, 0.3f);
        clear.SetActive(true);
    }
}
