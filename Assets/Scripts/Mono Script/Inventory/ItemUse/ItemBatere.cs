using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBatere : InventoryItem
{

    public override void UseItem()
    {
        FindAnyObjectByType<Flashlight>().GetComponent<Flashlight>().Recharge();
    }

}
