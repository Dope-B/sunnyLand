using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equipiment : MonoBehaviour
{
    private player_movement player;
    private inventory inventory;
    private const int WEAPON = 0, HELMET = 1, ARMOR = 2, SHOOSE = 3, NECK = 4, RING = 5;
    public GameObject go;
    public GameObject go_useRequest;
    private AudioManager Audio;
    public Text[] text;
    public Image[] icon;
    public item[] equip;
    private UseRequest useRequest;
    private int selectedNum;
    public bool activated=false;
    public bool input=true;


    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<player_movement>();
        inventory = FindObjectOfType<inventory>();
        useRequest = FindObjectOfType<UseRequest>();
        Audio = FindObjectOfType<AudioManager>();
    }
    public void clear()
    {
        Color color = icon[0].color;
        color.a = 0f;
        for (int i = 0; i < icon.Length; i++)
        {
            icon[i].sprite = null;
        }
    }
    public void show()
    {
        for (int i = 0; i < icon.Length; i++)
        {
            if (equip[i].itemID != 0)
            {
                icon[i].sprite = equip[i].itemIcon;
            }
        }
    }
    public void select()
    {
        Color color = icon[0].color;
        for (int i = 0; i < icon.Length; i++)
        {
            if (i == selectedNum && equip[i].itemID != 0) { color.a = 1f; icon[i].color = color; }
            else if (equip[i].itemID != 0) { color.a = 0.4f; icon[i].color = color; }
            else { color.a = 0f; icon[i].color = color; }
        }
    }
    public void equipItem(item item)
    {
        string temp = item.itemID.ToString();
        temp= temp.Substring(0, 3);
        switch (temp)
        {
            case "200"://weapon
                equipItemCheck(WEAPON, item);
                break;
            case "201"://helmet
                equipItemCheck(HELMET, item);
                break;
            case "202"://armor
                equipItemCheck(ARMOR, item);
                break;
            case "203"://shoose
                equipItemCheck(SHOOSE, item);
                break;
            case "204"://neck
                equipItemCheck(NECK, item);
                break;
            case "205"://ring
                equipItemCheck(RING, item);
                break;
        }
    }
    public void equipItemCheck(int count, item item)
    {
        if (equip[count].itemID == 0)
        {
            equip[count] = item;
        }
        else
        {
            inventory.EquipToInventory(equip[count]);
            equip[count] = item;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (input&&!Menu.instance.activated)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                activated = !activated;
                if (activated)
                {
                    Time.timeScale = 0;
                    Audio.play("ui_open");
                    player.OutofControl=true;
                    go.SetActive(true);
                    selectedNum = 0;
                    clear();
                    show();
                    select();
                }
                else
                {
                    Audio.play("back");
                    if (!inventory.activated) { Time.timeScale = 1; player.OutofControl = false; }
                    go.SetActive(false);
                    clear();
                }
            }
            else if (activated && Input.GetKeyDown(KeyCode.Escape))
            {
                Audio.play("back");
                if (!inventory.activated) { Time.timeScale = 1; player.OutofControl = false; }
                activated = false;
                go.SetActive(false);
                clear();
            }
            if (activated)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    Audio.play("click");
                    for (int i = 0; i < icon.Length; i++)
                    {
                        if (equip[i].itemID != 0) { break; }
                        else { if (i >= icon.Length-1) { return; } }
                    }
                    do
                    {
                        if (selectedNum < icon.Length - 1) { selectedNum++; }
                        else { selectedNum = 0; }
                    } while (equip[selectedNum].itemID == 0);
                    select();
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    Audio.play("click");
                    for (int i = 0; i < icon.Length; i++)
                    {
                        if (equip[i].itemID != 0) { break; }
                        else { if (i >= icon.Length-1) {  return; } }
                    }
                    do
                    {
                        if (selectedNum <= 0) { selectedNum = icon.Length - 1; }
                        else { selectedNum--; }
                    } while (equip[selectedNum].itemID == 0);
                    select();
                }
                else if (Input.GetKeyDown(KeyCode.Z))
                {
                    Audio.play("click");
                    if (equip[selectedNum].itemID != 0)
                    {
                        input = false;
                        StartCoroutine(use());
                    }  
                }
            }
        }
    }
    IEnumerator use()
    {
        go_useRequest.SetActive(true);
        go_useRequest.transform.position = icon[selectedNum].transform.position;
        useRequest.show("장착 해제", "취소");
        yield return new WaitUntil(() => !useRequest.activated);
        if (useRequest.getResult())
        {
            Audio.play("equip");
            inventory.EquipToInventory(equip[selectedNum]);
            equip[selectedNum] = new item(0,"","",item.ItemType.Equip,null,null);
            clear();
            select();
            show();
        }
        input = true;
        go_useRequest.SetActive(false);
    }
}
