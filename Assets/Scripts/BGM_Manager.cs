using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_Manager : MonoBehaviour {

    public static BGM_Manager instance;
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

    public AudioClip[] clips;
    private AudioSource source;
    private WaitForSecondsRealtime wait;
    public bool isDone;

    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
        wait = new WaitForSecondsRealtime(0.01f);
        setVol(0.3f);
        play(0);
    }
    public void play(int track)
    {
        source.loop = true;
        source.clip = clips[track];
        source.Play();
    }
    public void stop()
    {
        source.Stop();
    }
    public void pause()
    {
        source.Pause();
    }
    public void unpause()
    {
        source.UnPause();
    }
    public void fadeout(float speed=0.005f)
    {
        StopAllCoroutines();
        StartCoroutine(Cfadeout(speed));
    }
    public void fadein(float speed=0.005f)
    {
        StopAllCoroutines();
        StartCoroutine(Cfadein(speed));
    }
    public void setVol(float vol)
    {
        source.volume = vol;
    }
    IEnumerator Cfadeout(float speed)
    {
        for (float i = 0.3f; i >=0f; i -= speed)
        {
            source.volume = i;
            yield return wait;
        }
        isDone = true;
    }
    IEnumerator Cfadein(float speed)
    {
        for (float i = 0f; i <= 0.3f; i += speed)
        {
            source.volume = i;
            yield return wait;
        }
        isDone = false;
    }
}
