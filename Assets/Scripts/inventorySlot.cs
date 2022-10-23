using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventorySlot : MonoBehaviour {

    public Image I_icon;
    public Text I_name;
    public Text I_count;

    public void addItem(item item)
    {
        I_name.text = item.itemName;
        I_icon.sprite = item.itemIcon;
        if (item.ItemType.Use == item.itemType)
        {
            if (item.itemCount > 0)
            {
                I_count.text = "x" + item.itemCount.ToString();
            }
            
        }
        else
        {
            I_count.text = "";
        }

    }
    public void removeItem()
    {
        I_count.text = "";
        I_name.text = "";
        I_icon.sprite = null;
    }
}
