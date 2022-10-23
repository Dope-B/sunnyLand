using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseRequest : MonoBehaviour {


    public GameObject Y_panel;
    public GameObject N_panel;
    public Text yes;
    public Text no;
    public bool activated;
    private bool input;
    private bool result=true;
    private AudioManager Audio;

	// Use this for initialization
	void Start () {
        Audio = FindObjectOfType<AudioManager>();
	}

    public void selected()
    {
        if (result)
        {
            Y_panel.SetActive(true);
            N_panel.SetActive(false);
        }
        else
        {
            N_panel.SetActive(true);
            Y_panel.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (input)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                result = !result;
                Audio.play("click");
                selected();
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                result = !result;
                Audio.play("click");
                selected();
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                input = false;
                activated = false;
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                input = false;
                activated = false;
                Audio.play("click");
                result = false;
            }
            
        }
	}
    public void show(string yes, string no)
    {
        activated = true;
        result = true;
        this.yes.text = yes;
        this.no.text = no;
        Y_panel.SetActive(true);
        N_panel.SetActive(false);
        StartCoroutine(wait());
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(Time.deltaTime*5);
        input = true;
    }
    public bool getResult()
    {
        return result;
    }
}
