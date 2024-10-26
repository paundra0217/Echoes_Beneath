using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RDCT.Audio;

public class FuseBox : InteractObject
{
    [SerializeField] private GameObject[] Fuses; 
    [SerializeField] private int Jumlahfuse;
    public ItemGrid grids;
    public ItemSize fuse;
    public Animator pintu;
    private bool Nyala = false;
    public override void Interaction()
    {
        if(Nyala == true)
        {
            return;
        }

        foreach (InventoryItem items in grids.InventoryItems)
        {
            if (items.itemSize == fuse)
            {
                FindAnyObjectByType<ObjectiveManager>().GetComponent<ObjectiveManager>().ObjectiveClear("Fuse");
                Fuses[Jumlahfuse].SetActive(true);
                Jumlahfuse++;
                //pintu.SetTrigger("Kebuka");
                //Nyala = true;
                grids.BersihinGridBuatUseItem(items);
            }

            if (Jumlahfuse == 3)
            {
                //pintu.SetTrigger("Kebuka");
                AudioController.Instance.PlaySFX("Fusebox");
                AudioController.Instance.PlaySFX("Screeching");
                Nyala = true;
            }
        }



    }

    public bool GetFuse()
    {
        return Nyala;
    }

}
