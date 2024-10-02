using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using RDCT.Audio;

public class OpenDoor : InteractObject
{
    public GameObject door;
    public ItemGrid grids;

    private Animator DoorAnimator;

    private bool canOpen = true;

    [SerializeField] private MeshRenderer changedMaterial;

    public List<Material> green;

    public ItemSize keycards;

    private void Start()
    {
        changedMaterial = GetComponent<MeshRenderer>();
        DoorAnimator = door.GetComponent<Animator>();
    }

    public override void Interaction()
    {
        foreach (InventoryItem items in grids.InventoryItems)
        {
            if (items.itemSize == keycards && canOpen)
            {
                AudioController.Instance.PlaySFX("CardTap");
                changedMaterial.SetMaterials(green);
                DoorAnimator.SetTrigger("buka");
                canOpen = false;
                var dialMan = FindAnyObjectByType(typeof(dialogBase));
                var DialComp = dialMan.GetComponent<dialogBase>();
                DialComp.panggilDialog("2.1");
                AudioController.Instance.PlaySFX("MetalDoor");
            }
            else
            {
                dialogBase.Instance.panggilDialog("Ga bisa Tap Card");
            }
        }

        if (grids.InventoryItems.Count < 1)
        {
            dialogBase.Instance.panggilDialog("ga ada apa apa");
        }
    }
}
