using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [HideInInspector]
    private ItemGrid selectedItemGrid;
    public ItemGrid SelectedItemGrid { 
        get => selectedItemGrid;
        set
        {
            selectedItemGrid = value;
            inventoryHighlight.SetParent(value);
        }
    }
    InventoryItem selectedItem;
    InventoryItem OverlapItem;
    RectTransform rectTransform;
    [SerializeField] List<ItemSize> items;
    [SerializeField] private GameObject gameObject1;
    [SerializeField] Transform canvasTransform;

    InventoryHighlight inventoryHighlight;

    private void Awake()
    {
        inventoryHighlight = GetComponent<InventoryHighlight>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            RandomItemSpawn();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            InsertRandomItem();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RotateItem();
        }

        ItemIconDrag();

        if (SelectedItemGrid == null)
        {
            inventoryHighlight.Show(false);
            return;
        }


        HandleHighlight();

        if (Input.GetMouseButtonDown(0))
        {
        LeftMouseButtonclick();
        }


    }

    private void RotateItem()
    {
        if (selectedItem == null) return;

        selectedItem.Rotate();

    }

    private void InsertRandomItem()
    {
        if (selectedItemGrid == null) return;
        RandomItemSpawn();
        InventoryItem itemToInsert = selectedItem;
        selectedItem = null;
        InsertItem(itemToInsert);
    }

    private void InsertItem(InventoryItem itemToInsert)
    {
        Vector2Int? posOnGrid = selectedItemGrid.FindSpaceForObject(itemToInsert);

        if (posOnGrid == null)
        {
            Destroy(itemToInsert.gameObject);
            return;
        }
        

        selectedItemGrid.PlaceItem(itemToInsert, posOnGrid.Value.x, posOnGrid.Value.y);

    }

    Vector2Int Oldposition;
    InventoryItem ItemtoHighlight;


    private void HandleHighlight()
    {
        Vector2Int positionOnGrid = GetTileGridPosition();
        if (Oldposition == positionOnGrid) return;
        Oldposition = positionOnGrid;

        if(selectedItem == null)
        {
            ItemtoHighlight = SelectedItemGrid.GetItem(positionOnGrid.x, positionOnGrid.y);
            if(ItemtoHighlight != null)
            {
                inventoryHighlight.Show(true);
                inventoryHighlight.Setsize(ItemtoHighlight);
                inventoryHighlight.SetPostion(SelectedItemGrid, ItemtoHighlight);
            }
            else
            {
                inventoryHighlight.Show(false);
            }

        }
        else
        {
            inventoryHighlight.Show(SelectedItemGrid.BoundaryCheck(
                positionOnGrid.x,
                positionOnGrid.y,
                selectedItem.WIDTH, 
                selectedItem.HEIGHT));
            inventoryHighlight.Setsize(selectedItem);
            inventoryHighlight.SetPosition(SelectedItemGrid, selectedItem, positionOnGrid.x, positionOnGrid.y);
        }
    }

    public void RandomItemSpawn()
    {
        InventoryItem inventoryItem = Instantiate(gameObject1).GetComponent<InventoryItem>();
        selectedItem = inventoryItem;

        rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(canvasTransform);

        int selectedItemID = Random.Range(0, items.Count);
        inventoryItem.set(items[selectedItemID]);

    }

    private void LeftMouseButtonclick()
    {
        Vector2Int tileGridPosition = GetTileGridPosition();
        if (selectedItem == null)
        {
            PickUpItem(tileGridPosition);
        }
        else
        {
            PlaceItem(tileGridPosition);

        }

    }

    private Vector2Int GetTileGridPosition()
    {
        Vector2 position = Input.mousePosition;

        if (selectedItem != null)
        {
            position.x -= (selectedItem.WIDTH - 1) * ItemGrid.TileSizeWidth / 2;
            position.y += (selectedItem.HEIGHT - 1) * ItemGrid.TileSizeHeight / 2;
        }

        return SelectedItemGrid.GetTileGridPosition(position);
    }

    private void PickUpItem(Vector2Int tileGridPosition)
    {
        selectedItem = SelectedItemGrid.PickUpItem(tileGridPosition.x, tileGridPosition.y);
        if (selectedItem != null)
        {
            rectTransform = selectedItem.GetComponent<RectTransform>();
        }
    }

    private void PlaceItem(Vector2Int tileGridPosition)
    {
        bool complete = SelectedItemGrid.PlaceItem(selectedItem, tileGridPosition.x, tileGridPosition.y, ref OverlapItem);
        if (complete == true)
        {
            selectedItem = null;
            if (OverlapItem != null)
            {
                selectedItem = OverlapItem;
                OverlapItem = null;
                rectTransform = selectedItem.GetComponent<RectTransform>();
            }
        }
    }

    private void ItemIconDrag()
    {
        if (selectedItem != null)
        {
            rectTransform.position = Input.mousePosition;
        }
    }
}
