using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPotion:item_use {

	public void effect()
    {
        PlayerStat.instance.current_hp += 50;
    }
}
