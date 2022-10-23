using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    public GameObject go;
    public static Menu instance;
    private Equipiment equipment;
    private inventory inventory;
    public GameObject resume;
    private AudioManager Audio;
    public GameObject exit;
    private player_movement player;
    private int result;
    private int sel=1;
    public bool activated;
    private bool key_input = false;

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
    void Start()
    {
        equipment = FindObjectOfType<Equipiment>();
        inventory = FindObjectOfType<inventory>();
        player = FindObjectOfType<player_movement>();
        Audio = FindObjectOfType<AudioManager>();
    }

	// Update is called once per frame
	void Update () {
        if (!inventory.activated && !equipment.activated && Input.GetKeyDown(KeyCode.Escape))
        {
            activated = !activated;
            if (activated)
            {
                Audio.play("ui_open");
                go.SetActive(true);
                player.OutofControl = true;
                Time.timeScale = 0;
                select();
                key_input = true;
                input();
            }
            else
            {
                Audio.play("back");
                go.SetActive(false);
                sel = 1;
                player.OutofControl = false;
                key_input = false;
                Time.timeScale = 1;
            }
        }
        if (key_input) { input(); }
	}
    private void input()
    { 
        if (Input.GetKeyDown(KeyCode.UpArrow)|| Input.GetKeyDown(KeyCode.DownArrow))
        {
            Audio.play("click");
            if (sel == 1) { sel = 2; select(); }
            else { sel = 1; select(); }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            result = sel;
            switch (result)
            {
                case 1:
                    Audio.play("start");
                    go.SetActive(false);
                    activated = false;
                    player.OutofControl = false;
                    key_input = false;
                    sel = 1;
                    Time.timeScale = 1;
                    break;
                case 2:
                    Application.Quit();
                    break;
            }
        }
    }
    private void select()
    {
        Color color = resume.GetComponent<Image>().color;
        if (sel == 1)
        {
            color.a = 1f;
            resume.GetComponent<Image>().color = color;
            color.a = 0.5f;
            exit.GetComponent<Image>().color = color;
        }
        else
        {
            color.a = 0.5f;
            resume.GetComponent<Image>().color = color;
            color.a = 1f;
            exit.GetComponent<Image>().color = color;
        }
    }
}
