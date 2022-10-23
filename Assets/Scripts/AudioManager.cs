using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    [SerializeField]
    public Sound[] sounds;

    public static AudioManager instance;
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
    // Use this for initialization
    void Start () {
		for(int i = 0; i < sounds.Length; i++)
        {
            GameObject soundObject = new GameObject("soundFile" + i + "-" + sounds[i].name);
            sounds[i].Setsource(soundObject.AddComponent<AudioSource>());
            soundObject.transform.SetParent(this.transform);
        }
	}
    public void play(string name, float vol=1f)
    {
        for(int i = 0; i < sounds.Length; i++)
        {
            if (name == sounds[i].name)
            {
                sounds[i].setVolume(vol);
                sounds[i].play();
                return;
            }
        }
    }
    public void stop(string name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (name == sounds[i].name)
            {
                sounds[i].stop();
                return;
            }
        }
    }
    public void setLoop(string name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (name == sounds[i].name)
            {
                sounds[i].setLoop();
                return;
            }
        }
    }
    public void setLoopCancel(string name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (name == sounds[i].name)
            {
                sounds[i].setLoopCancel();
                return;
            }
        }
    }
    public void setVolume(float vol)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (name == sounds[i].name)
            {
                sounds[i].setVolume(vol);
                return;
            }
        }
    }
}
[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    private AudioSource source;
    public float volume;
    public bool isloop;
    public void Setsource(AudioSource source)
    {
        this.source = source;
        source.clip = this.clip;
        source.loop = this.isloop;
        source.volume = this.volume;
    }
    public void play()
    {
        source.Play();
    }
    public void stop()
    {
        source.Stop();
    }
    public void setLoop()
    {
        source.loop = true;
    }
    public void setLoopCancel()
    {
        source.loop = false;
    }
    public void setVolume(float vol)
    {
        source.volume = vol;
    }
}
