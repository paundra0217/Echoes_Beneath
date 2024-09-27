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
        AudioController.Instance.PlaySFX("CardTap");
        foreach (InventoryItem items in grids.InventoryItems)
        {
            if (items.itemSize == keycards && canOpen)
            {
                changedMaterial.SetMaterials(green);
                DoorAnimator.SetTrigger("buka");
                canOpen = false;
                AudioController.Instance.PlaySFX("MetalDoor");
            }
        }
    }
}
