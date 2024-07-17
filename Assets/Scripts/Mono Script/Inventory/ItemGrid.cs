using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrid : MonoBehaviour
{
    public const float TileSizeWidth = 22;
    public const float TileSizeHeight = 22;

    RectTransform rectTransform;

    InventoryItem[,] InventoryItemSlot;
    [SerializeField] int gridwideSize;
    [SerializeField] int gridheightSize;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Init(gridwideSize, gridheightSize);
    }

    private void Init(int width, int height)
    {
        InventoryItemSlot = new InventoryItem[width, height];
        Vector2 Size = new Vector2(width * TileSizeWidth, height * TileSizeHeight);
        rectTransform.sizeDelta = Size;
    }

    Vector2 positionOnGrid = new Vector2();
    Vector2Int tileGridPosition = new Vector2Int();
    public Vector2Int GetTileGridPosition(Vector2 MousePos)
    {
        positionOnGrid.x = MousePos.x - rectTransform.position.x;
        positionOnGrid.y = rectTransform.position.y - MousePos.y;

        tileGridPosition.x = (int)(positionOnGrid.x / TileSizeWidth);
        tileGridPosition.y = (int)(positionOnGrid.y / TileSizeHeight);

        return tileGridPosition;
    }

    internal InventoryItem GetItem(int x, int y)
    {
        return InventoryItemSlot[x, y];
    }

    public bool PlaceItem(InventoryItem inventoryItem, int PosX, int PosY, ref InventoryItem overlapItem)
    {
        if (BoundaryCheck(PosX, PosY, inventoryItem.WIDTH, inventoryItem.HEIGHT) == false) return false;

        if (OverLapCheck(PosX, PosY, inventoryItem.WIDTH, inventoryItem.HEIGHT, ref overlapItem) == false)
        {
            overlapItem = null;
            return false;
        }

        if (overlapItem != null)
        {
            CleanGridReference(overlapItem);
        }
        PlaceItem(inventoryItem, PosX, PosY);

        return true;
    }

    public void PlaceItem(InventoryItem inventoryItem, int PosX, int PosY)
    {
        RectTransform rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(this.rectTransform);
        rectTransform.SetSiblingIndex(0);
        for (int i = 0; i < inventoryItem.WIDTH; i++)
        {
            for (int j = 0; j < inventoryItem.HEIGHT; j++)
            {
                InventoryItemSlot[PosX + i, PosY + j] = inventoryItem;
            }
        }
        inventoryItem.OnGridPositionX = PosX;
        inventoryItem.OnGridPositionY = PosY;

        Vector2 position = CalculatePositionOnGrid(inventoryItem, PosX, PosY);

        rectTransform.localPosition = position;
    }

    internal Vector2Int? FindSpaceForObject(InventoryItem itemToInsert)
    {
        int height = gridheightSize - itemToInsert.HEIGHT +1;
        int width = gridwideSize - itemToInsert.WIDTH +1;

        for(int y = 0; y< height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                if(CheckAvailableSpace(x, y, itemToInsert.WIDTH, itemToInsert.HEIGHT)== true) return new Vector2Int(x,y);
            }
        }

        return null;
    }

    public Vector2 CalculatePositionOnGrid(InventoryItem inventoryItem, int PosX, int PosY)
    {
        Vector2 position = new Vector2();
        position.x = PosX * TileSizeWidth + inventoryItem.WIDTH * TileSizeWidth / 2;
        position.y = -(PosY * TileSizeHeight + inventoryItem.HEIGHT * TileSizeHeight / 2);
        return position;
    }

    public InventoryItem PickUpItem(int x, int y)
    {
        InventoryItem ToReturn = InventoryItemSlot[x, y];
        
        if (ToReturn == null) return null;

        CleanGridReference(ToReturn);
        return ToReturn;
    }
    private bool OverLapCheck(int posX, int posY, int width, int height, ref InventoryItem overlapItem)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (InventoryItemSlot[posX + x, posY + y] != null)
                {
                    if (overlapItem == null)
                    {
                        overlapItem = InventoryItemSlot[posX + x, posY + y];

                    }
                    else
                    {
                        if (overlapItem != InventoryItemSlot[posX + x, posY + y]) return false;

                    }


                }
            }
        }

        return true;
    }

    private bool CheckAvailableSpace(int posX, int posY, int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (InventoryItemSlot[posX + x, posY + y] != null)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private void CleanGridReference(InventoryItem item)
    {
        for (int ix = 0; ix < item.WIDTH; ix++)
        {
            for (int jy = 0; jy < item.HEIGHT; jy++)
            {
                InventoryItemSlot[item.OnGridPositionX + ix, item.itemSize.height + jy] = null;
            }
        }

        for (int ix = 0; ix < gridwideSize; ix++)
        {
            for (int jy = 0; jy < gridheightSize; jy++)
            {

                Debug.Log(InventoryItemSlot[ix,jy]);

            }
        }
    }

    bool CheckPosition(int posX, int posY)
    {
        if(posX < 0 || posY < 0)
        {
            return false;
        }
        if(posX >= gridwideSize || posY >= gridheightSize)
        {
            return false;
        }

        return true;
    }

    public bool BoundaryCheck(int posX, int posY, int width, int height)
    {
        if (CheckPosition(posX, posY) == false) return false;

        posX += width -1;
        posY += height -1;

        if (CheckPosition(posX, posY) == false) return false;

        return true;

    }

}
