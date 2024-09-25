using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenDoor : InteractObject
{
    public GameObject door;
    
    private Animator DoorAnimator;

    private bool canOpen;

    [SerializeField] private MeshRenderer changedMaterial;

    public List<Material> green;

    private void Start()
    {
        changedMaterial = GetComponent<MeshRenderer>();
        DoorAnimator = door.GetComponent<Animator>();
    }

    public override void Interaction()
    {
        changedMaterial.SetMaterials(green);
        DoorAnimator.SetTrigger("buka");
    }
}
