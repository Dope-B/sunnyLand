using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventory : MonoBehaviour
{


    public static inventory instance;
    private player_movement player;
    private AudioManager Audio;
    private DataBase database;
    private Equipiment eq;
    private inventorySlot[] slots;
    public GameObject floating_text;
    private List<item> inventoryItemList;
    private List<item> inventoryTabList;
    private UseRequest useRequest;
    public Equipiment equip;
    public Text descriptionText;
    public string[] tabDescription;
    public Transform tf;
    public GameObject go;
    public GameObject go_select;
    public GameObject[] selectedTabImage;
    private int selectedItem;
    private int selectedTab;
    public bool activated;
    private bool activatedTab;
    private bool activatedItem;
    private bool stopKeyInput;
    private bool preventExec;

    // Use this for initialization
    void Start()
    {
        instance = this;
        inventoryItemList = new List<item>();
        inventoryTabList = new List<item>();
        slots = tf.GetComponentsInChildren<inventorySlot>();
        player = FindObjectOfType<player_movement>();
        database = FindObjectOfType<DataBase>();
        useRequest = FindObjectOfType<UseRequest>();
        equip = FindObjectOfType<Equipiment>();
        Audio = FindObjectOfType<AudioManager>();
        eq = FindObjectOfType<Equipiment>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!stopKeyInput && !Menu.instance.activated)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                activated = !activated;
                if (activated)
                {
                    Time.timeScale = 0;
                    go.SetActive(true);
                    selectedTab = 0;
                    activatedTab = true;
                    activatedItem = false;
                    player.OutofControl = true;
                    Audio.play("ui_open");
                    ShowTab();
                }
                else
                {
                    if (!eq.activated) { Time.timeScale = 1; player.OutofControl = false; }
                    StopAllCoroutines();
                    go.SetActive(false);
                    activatedTab = false;
                    activatedItem = false;
                    Audio.play("back");
                }
            }
            if (activated&&Input.GetKeyDown(KeyCode.Escape))
            {
                if (!eq.activated) { Time.timeScale = 1; player.OutofControl = false; }
                activated = false;
                StopAllCoroutines();
                go.SetActive(false);
                activatedTab = false;
                activatedItem = false;
                Audio.play("back");
            }
            if (activated)
            {

                if (activatedTab)
                {
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        if (selectedTab < selectedTabImage.Length - 1)
                        {
                            selectedTab++;
                        }
                        else
                        {
                            selectedTab = 0;
                        }
                        SelectedTab();
                        Audio.play("click");
                    }
                    else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        if (selectedTab > 0)
                        {
                            selectedTab--;
                        }
                        else
                        {
                            selectedTab = selectedTabImage.Length - 1;
                        }
                        SelectedTab();
                        Audio.play("click");
                    }
                    else if (Input.GetKeyDown(KeyCode.Return))
                    {
                        Color color = selectedTabImage[selectedTab].GetComponent<Image>().color;
                        color.a = 1f;
                        selectedTabImage[selectedTab].GetComponent<Image>().color = color;
                        activatedItem = true;
                        activatedTab = false;
                        preventExec = false;
                        ShowItem();
                        Audio.play("click");
                    }
                    
                }
                else if (activatedItem)
                {
                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        if (selectedItem < inventoryTabList.Count - 6)
                        {
                            selectedItem += 6;
                        }
                        else
                        {
                            selectedItem %= 6;
                        }
                        SelectedItem();
                    }
                    else if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        if (selectedItem > 6)
                        {
                            selectedItem -= 6;
                        }
                        else
                        {
                            selectedItem = inventoryTabList.Count - 1 - selectedItem;
                        }
                        SelectedItem();
                    }
                    else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        if (selectedItem > 0)
                        {
                            selectedItem -= 1;
                        }
                        else
                        {
                            selectedItem = inventoryTabList.Count - 1;
                        }
                        SelectedItem();
                    }
                    else if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        if (selectedItem < inventoryTabList.Count - 1)
                        {
                            selectedItem += 1;
                        }
                        else
                        {
                            selectedItem = 0;
                        }
                        SelectedItem();
                    }
                    else if (Input.GetKeyDown(KeyCode.Z))
                    {
                        StopAllCoroutines();
                        preventExec = true;
                        activatedItem = false;
                        activatedTab = true;
                        ShowTab();
                    }
                    else if (Input.GetKeyDown(KeyCode.X) && !preventExec)
                    {
                        Audio.play("click");
                        if (selectedTab == 0)
                        {
                            if (inventoryTabList.Count != 0) { StartCoroutine(use("장착", "취소")); }
                        }
                        else if (selectedTab == 1)
                        {
                            if (inventoryTabList.Count != 0) { StartCoroutine(use("사용", "취소")); }
                        }
                        else
                        {

                        }
                    }
                    
                }

            }
        }
    }
    public void getItem(int item_ID, int item_Count = 1)
    {
        for (int i = 0; i < database.itemList.Count; i++)
        {
            if (item_ID == database.itemList[i].itemID)
            {
                var clone = Instantiate(floating_text, player_movement.player.transform.position + new Vector3(0, 0.1f, 0), Quaternion.Euler(Vector3.zero));
                clone.GetComponent<Floating_Text>().text.text = database.itemList[i].itemName + " " + item_Count + "개 획득";
                clone.transform.SetParent(this.transform);
                for (int j = 0; j < inventoryItemList.Count; j++)
                {
                    if (inventoryItemList[j].itemID == item_ID)
                    {
                        inventoryItemList[j].itemCount += item_Count;
                        return;
                    }
                }
                inventoryItemList.Add(database.itemList[i]);
                inventoryItemList[inventoryItemList.Count - 1].itemCount = item_Count;
                return;
            }
        }
        Debug.LogError("no such a item in DataBase");
    }
    public void ShowItem(int k = 0)
    {
        inventoryTabList.Clear();
        removeSlot();
        selectedItem = k;
        switch (selectedTab)
        {
            case 0:
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (item.ItemType.Equip == inventoryItemList[i].itemType)
                    {
                        inventoryTabList.Add(inventoryItemList[i]);
                    }
                }
                break;
            case 1:
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (item.ItemType.Use == inventoryItemList[i].itemType)
                    {
                        inventoryTabList.Add(inventoryItemList[i]);
                    }
                }
                break;
            case 2:
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (item.ItemType.Quest == inventoryItemList[i].itemType)
                    {
                        inventoryTabList.Add(inventoryItemList[i]);
                    }
                }
                break;
        }
        for (int i = 0; i < inventoryTabList.Count; i++)
        {
            slots[i].gameObject.SetActive(true);
            slots[i].addItem(inventoryTabList[i]);
        }
        SelectedItem();
    }
    public void SelectedItem()
    {
        if (inventoryTabList.Count > 0)
        {
            Color color = slots[0].I_icon.GetComponent<Image>().color;
            color.a = 0.3f;
            for (int i = 0; i < inventoryTabList.Count; i++)
            {
                slots[i].I_icon.GetComponent<Image>().color = color;
            }
            color.a = 1f;
            slots[selectedItem].I_icon.GetComponent<Image>().color = color;
            descriptionText.text = inventoryTabList[selectedItem].itemDescription;

        }
        else
        {
            descriptionText.text = "아이템 없음";
        }
    }
    public void ShowTab()
    {
        removeSlot();
        SelectedTab();
    }
    public void SelectedTab()
    {
        Color color = selectedTabImage[selectedTab].GetComponent<Image>().color;
        color.a = 0.3f;
        for (int i = 0; i < selectedTabImage.Length; i++)
        {
            selectedTabImage[i].GetComponent<Image>().color = color;
        }
        color.a = 1f;
        selectedTabImage[selectedTab].GetComponent<Image>().color = color;
        descriptionText.text = tabDescription[selectedTab];

    }
    public void removeSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].removeItem();
            slots[i].gameObject.SetActive(false);
        }
    }
    public void EquipToInventory(item item)
    {
        inventoryItemList.Add(item);
    }
    IEnumerator use(string up, string down)
    {
        stopKeyInput = true;
        go_select.SetActive(true);
        go_select.transform.position = slots[selectedItem].transform.position;
        useRequest.show(up, down);
        yield return new WaitUntil(() => !useRequest.activated);
        if (useRequest.getResult())
        {
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (inventoryItemList[i].itemID == inventoryTabList[selectedItem].itemID)
                    {
                    if (selectedTab == 1) {
                        database.use_item(inventoryItemList[i].itemID);
                        if (inventoryItemList[i].itemCount > 1)
                        {
                            inventoryItemList[selectedItem].itemCount--;
                            ShowItem(i);
                        }
                        else
                        {
                            inventoryItemList.RemoveAt(i);
                            if (i == 0) { ShowItem(); }
                            else { ShowItem(--i); }
                        }
                        break;
                    }
                    else if (selectedTab == 0)
                    {
                        Audio.play("equip");
                        equip.equipItem(inventoryItemList[i]);
                        inventoryItemList.RemoveAt(i);
                        ShowItem();
                        break;
                    }
                    }
            }
        }
        stopKeyInput = false;
        go_select.SetActive(false);
    }
}
