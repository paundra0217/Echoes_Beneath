using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenDoor : InteractObject
{
    public GameObject door;
    public ItemGrid grids;

    private Animator DoorAnimator;

    private bool canOpen;

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
            if (items.itemSize == keycards)
            {
                changedMaterial.SetMaterials(green);
                DoorAnimator.SetTrigger("buka");
            }
        }
    }
}
