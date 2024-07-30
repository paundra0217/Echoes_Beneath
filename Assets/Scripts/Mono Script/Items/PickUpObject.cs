using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpObject : InteractObject
{
    [SerializeField] InventoryItem ItemObject;
    [SerializeField] ItemSize itemData;
    ItemGrid itemGrid;
    Canvas canvas;
    private void Start()
    {
        ItemObject.GetComponent<InventoryItem>().Set(itemData);
    }

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
        itemGrid = canvas.GetComponentInChildren<ItemGrid>();

    }
    public override void Interaction()
    {
        itemGrid.gameObject.SetActive(true);

        InventoryItem oke = Instantiate(ItemObject);     
        oke.gameObject.transform.SetParent(itemGrid.gameObject.transform.parent);

        Vector2Int? posOnGrid = itemGrid.FindSpaceForObject(ItemObject);

        if (posOnGrid == null)
        {
            Destroy(oke.gameObject);
            return;
        }


        itemGrid.PlaceItem(oke, posOnGrid.Value.x, posOnGrid.Value.y);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        itemGrid.gameObject.SetActive(false);
        //Destroy(gameObject);

    }

}
