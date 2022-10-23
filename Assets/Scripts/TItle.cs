using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TItle : MonoBehaviour {

    public GameObject start;
    public GameObject exit;
    private AudioManager Audio;
    private BGM_Manager bgm;
    private int sel=1;
    private int result;
    private fadeManager FM;


	// Use this for initialization
	void Start () {
        select();
        FM = FindObjectOfType<fadeManager>();
        Audio = FindObjectOfType<AudioManager>();
        bgm = FindObjectOfType<BGM_Manager>();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (sel == 1) { sel = 2; select(); }
            else { sel = 1; select(); }
            Audio.play("star",0.2f);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            result = sel;
            switch (result)
            {
                case 1:
                    Audio.play("start",1f);
                    StartCoroutine(StartGame());
                    break;
                case 2:
                    Application.Quit();
                    break;
            }
        }
	}
    private void select()
    {
        Color color = start.GetComponent<Image>().color;
        if (sel == 1)
        {
            color.a = 1f;
            start.GetComponent<Image>().color = color;
            color.a = 0.5f;
            exit.GetComponent<Image>().color = color;
        }
        else
        {
            color.a = 0.5f;
            start.GetComponent<Image>().color = color;
            color.a = 1f;
            exit.GetComponent<Image>().color = color;
        }
    }
    IEnumerator StartGame()
    {
        bgm.fadeout();
        FM.FadeOut();
        yield return new WaitUntil(() => FM.fadeDone&& bgm.isDone);
        bgm.play(1);
        bgm.fadein();
        SceneManager.LoadScene("Map1");
        FM.FadeIn();
        yield return new WaitUntil(() => FM.fadeDone);
    }
}
