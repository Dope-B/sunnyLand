using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeManager : MonoBehaviour {

    private static fadeManager instance;
    public SpriteRenderer black;
    private Color color;
    private WaitForSecondsRealtime wait;
    public bool fadeDone=false;
  void Start()
    {
        wait = new WaitForSecondsRealtime(0.002f);
    }
    #region SingleTon
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion SingleTon
    public void FadeOut(float speed=0.04f)
    {
        fadeDone = false;
        StopAllCoroutines();
        StartCoroutine(FadeOutCoroutine(speed));
        
    }
    IEnumerator FadeOutCoroutine(float speed)
    {
        color = black.color;
        while (color.a < 1f)
        {
            color.a += speed;
            black.color = color;
            yield return wait; 
        }
        fadeDone = true;
    }
    public void FadeIn(float speed=0.04f)
    {
        fadeDone = false;
        StopAllCoroutines();
        StartCoroutine(FadeInCoroutine(speed));
    }
    IEnumerator FadeInCoroutine(float speed)
    {
        color = black.color;
        while (color.a > 0f)
        {
            color.a -= speed;
            black.color = color;
            yield return wait;
        }
        fadeDone = true;
    }
}
