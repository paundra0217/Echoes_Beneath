using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseBox : InteractObject
{
    public ItemGrid grids;
    public ItemSize fuse;
    public Animator pintu;
    private bool Nyala = false;
    public override void Interaction()
    {
        foreach (InventoryItem items in grids.InventoryItems)
        {
            if (items.itemSize == fuse)
            {
                pintu.SetTrigger("Kebuka");
                Nyala = true;
                grids.BersihinGridBuatUseItem(items);
            }
        }
    }

    public bool GetFuse()
    {
        return Nyala;
    }

}
