using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase : MonoBehaviour
{

    public string[] var_name;
    public float[] var;
    public string[] switch_name;
    public bool[] switches;
    public List<item> itemList = new List<item>();
    public static DataBase instance;
    private AudioManager Audio;
    private PlayerStat playerStat;
    public GameObject floating_text;
    public GameObject canvas;

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
    void Start()
    {
        playerStat = FindObjectOfType<PlayerStat>();
        Audio = FindObjectOfType<AudioManager>();
    }

    public void floatingText(int number, string color)
    {
        Vector3 vector = playerStat.transform.position;
        vector.y += 0.1f;
        GameObject clone = Instantiate(floating_text, vector, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<Floating_Text>().text.text = "+" + number;
        switch (color)
        {
            case "red":
                clone.GetComponent<Floating_Text>().text.color = Color.red;
                break;
            case "blue":
                clone.GetComponent<Floating_Text>().text.color = Color.blue;
                break;
            case "green":
                clone.GetComponent<Floating_Text>().text.color = Color.green;
                break;
        }

        clone.GetComponent<Floating_Text>().text.fontSize = 18;
        clone.transform.SetParent(canvas.transform);
    }

    public void use_item(int itemID)
    {
        switch (itemID)
        {
            case 10000:
                if (playerStat.hp >= playerStat.current_hp + 50)
                {
                    playerStat.current_hp += 50;
                }
                else { playerStat.current_hp = playerStat.hp; }
                floatingText(50, "red");
                Audio.play("potion");
                break;
            case 10001:
                if (playerStat.mp >= playerStat.current_mp + 10)
                {
                    playerStat.current_mp += 10;
                }
                else { playerStat.current_mp = playerStat.mp; }
                floatingText(50, "blue");
                Audio.play("potion");
                break;
        }

    }

}
