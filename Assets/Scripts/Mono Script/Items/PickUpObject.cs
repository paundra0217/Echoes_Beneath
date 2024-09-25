using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpObject : InteractObject
{
    [SerializeField] InventoryItem ItemObject;
    [SerializeField] ItemSize itemData;
    ItemGrid itemGrid;
    Image img;
    GameObject canvass;
    private void Start()
    {
        ItemObject.GetComponent<InventoryItem>().Set(itemData);
        canvass = GameObject.FindGameObjectWithTag("InventoryGrid");
        Debug.Log(canvass);
        if (canvass != null)
        {
            itemGrid = canvass.GetComponentInChildren<ItemGrid>();
            img = itemGrid.GetComponent<Image>();
            //canvass.SetActive(false);
        }
    }
    public override void Interaction()
    {
        //itemGrid.gameObject.SetActive(true);

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

        Debug.Log("IMAGE " + img.IsActive());

        if (img.IsActive() == true)
        {
            ItemObject.isVisible(true);
        }
        else ItemObject.isVisible(false);

        itemGrid.InventoryItems.Add(oke);

        //itemGrid.gameObject.SetActive(false);
        Destroy(gameObject);

    }

}
