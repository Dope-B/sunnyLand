using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class item {

    public int itemID;
    public string itemName;
    public string itemDescription;
    public int itemCount;
    public Sprite itemIcon;
    public ItemType itemType;
    public item_use effect;
    public enum ItemType
    {
        Equip,Use,Quest
    }

    public item(int itemID,string itemName,string itemDescription, ItemType itemType, Sprite itemIcon,item_use effect, int itemCount=1)
    {
        this.itemID = itemID;
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.itemType = itemType;
        this.itemCount = itemCount;
        this.itemIcon = itemIcon;
        this.effect = effect;
    }

}
