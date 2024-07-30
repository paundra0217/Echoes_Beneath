using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrid : MonoBehaviour
{
    //ukuran dari gambar (hitung manual)
    public const float TileSizeWidth = 22;
    public const float TileSizeHeight = 22;

    RectTransform rectTransform;

    //Array dari ItemGrid
    InventoryItem[,] InventoryItemSlot;

    //Ukuran dari Array
    [SerializeField] int gridwideSize;
    [SerializeField] int gridheightSize;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        //buat Ngeset Ukuran dari Gambar dan ukuran array
        Init(gridwideSize, gridheightSize);
    }

    Vector2 positionOnGrid = new Vector2();
    Vector2Int tileGridPosition = new Vector2Int();

    #region PickUp && Drop

    //Buat PickUp
    public InventoryItem PickUpItem(int x, int y)
    {
        //Cek Item di Array
        InventoryItem ToReturn = InventoryItemSlot[x, y];
        
        //Kalo Kosong langsung balikin
        if (ToReturn == null) return null;

        //kalo ada bakal dibersihin di arraynya ada dikurangin countnya
        ToReturn = BersihinGridBuatPickUp(ToReturn);
        //balikin Item yang mau di PickUp
        return ToReturn;
    }

    //Buat Ngecek bisa ditaro ato kaga
    public bool PlaceItem(InventoryItem inventoryItem, int PosX, int PosY, ref InventoryItem overlapItem)
    {
        //Buat Cek apakah itemnya keluar dari grid ato kaga
        if (BoundaryCheck(PosX, PosY, inventoryItem.WIDTH, inventoryItem.HEIGHT) == false) return false;
        //Buat Cek apakah itemnya ngenimpa item yang lain
        if (OverLapCheck(PosX, PosY, inventoryItem.WIDTH, inventoryItem.HEIGHT, ref overlapItem, 
            inventoryItem) == false)
        {
            overlapItem = null;
            return false;
        }
        //Kalo Ngenimpa item di gridnya di angkat
        if (overlapItem != null)
        {
            CleanGridReference(overlapItem);
        }
        //Item barunya di taro di tempat
        PlaceItem(inventoryItem, PosX, PosY);

        return true;
    }

    //Buat Naro Item 
    public void PlaceItem(InventoryItem inventoryItem, int PosX, int PosY)
    {
        RectTransform rectTransform = inventoryItem.GetComponent<RectTransform>();
        //Buat NgeSet parentnya jadi si ItemGrid
        rectTransform.SetParent(this.rectTransform);
        rectTransform.SetSiblingIndex(0);

        //Ngecek Item yang ditaro bisa di Stack dan sama ato kaga
        if(InventoryItemSlot[PosX, PosY] != null)
        {
            if(InventoryItemSlot[PosX, PosY].itemSize.CanStack)
            {
                if(InventoryItemSlot[PosX, PosY].itemSize.Name == inventoryItem.itemSize.Name)
                {
                    //Kalo Iya itemnya di destroy trus item yang udh ada Countnya++
                    InventoryItemSlot[PosX, PosY].count++;
                    InventoryItemSlot[PosX, PosY].RefreshCount();
                    Destroy(inventoryItem.gameObject);
                    return;
                }
            }
        }
        //Item yang kaga bisa distack atau item pertama Diisi kaya biasa pake 2 for loop buat 2d array
        else
        {
            for(int i = 0; i < inventoryItem.WIDTH; i++)
            {
                for (int j = 0; j < inventoryItem.HEIGHT; j++)
                {
                InventoryItemSlot[PosX + i, PosY + j] = inventoryItem;
                }
            }
            if (inventoryItem.itemSize.CanStack)
            {
                InventoryItemSlot[PosX, PosY].SetActiveUI();
            }
        }

        
        //ngesimpen posisi item di itemnya
        inventoryItem.OnGridPositionX = PosX;
        inventoryItem.OnGridPositionY = PosY;
        //Ngehitung localposition di Grid
        Vector2 position = CalculatePositionOnGrid(inventoryItem, PosX, PosY);
        //ubah posisi biar pas
        rectTransform.localPosition = position;
    }

    #endregion

    #region Function yang Ngebantu

    //Buat Otomatis Nyari posisi Item yang kosong dan pas
    internal Vector2Int? FindSpaceForObject(InventoryItem itemToInsert)
    {
        int height = gridheightSize - itemToInsert.HEIGHT +1;
        int width = gridwideSize - itemToInsert.WIDTH +1;

        for(int y = 0; y< height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                //Ngecek diposisi ini bisa masuk ato kaga
                if(CheckAvailableSpace(x, y, itemToInsert.WIDTH, itemToInsert.HEIGHT)== true) return new Vector2Int(x,y);
            }
        }

        return null;
    }
    //Buat Ngitung Posisi di Grid
    public Vector2 CalculatePositionOnGrid(InventoryItem inventoryItem, int PosX, int PosY)
    {
        Vector2 position = new Vector2();
        position.x = PosX * TileSizeWidth + inventoryItem.WIDTH * TileSizeWidth / 2;
        position.y = -(PosY * TileSizeHeight + inventoryItem.HEIGHT * TileSizeHeight / 2);
        return position;
    }
    
    //Ngecek ada yang ketimpa ato kaga
    private bool OverLapCheck(int posX, int posY, int width, int height, ref InventoryItem overlapItem, 
        InventoryItem inventoryItem)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (InventoryItemSlot[posX + x, posY + y] != null)
                {
                    if(InventoryItemSlot[posX + x, posY + y].itemSize.CanStack)
                    {
                        if(InventoryItemSlot[posX + x, posY + y].itemSize.Name == inventoryItem.itemSize.Name)
                        {
                        
                        overlapItem = null;
                        return true;
                        }
                        
                    }
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

    //Ngecek tempatnya cukup ato kaga
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

    //Buat Bersihin/Kosongin Grid ketika Terjadi Overlap
    private void CleanGridReference(InventoryItem item)
    {

        for (int ix = 0; ix < item.WIDTH; ix++)
        {
            for (int jy = 0; jy < item.HEIGHT; jy++)
            {

                InventoryItemSlot[item.OnGridPositionX + ix, item.OnGridPositionY + jy] = null;

            }
        }
    }
    //Buat Bersihin khusus PickUp
    private InventoryItem BersihinGridBuatPickUp(InventoryItem item)
    {
        if (item.count > 1)
        {
            item.count--;
            item.RefreshCount();
            InventoryItem oke = Instantiate(item);
            oke.count = 1;
            oke.RefreshCount();
            oke.gameObject.transform.SetParent(item.transform.parent);
            return oke;
        }

        for (int ix = 0; ix < item.WIDTH; ix++)
        {
            for (int jy = 0; jy < item.HEIGHT; jy++)
            {

                InventoryItemSlot[item.OnGridPositionX + ix, item.OnGridPositionY + jy] = null;

            }
        }

        return item;
    }
    //Ngecek Posisi apakah keluar dari Grid
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

    //Buat NgeSet ukuran dari gambar dan ukuran dari Array
    private void Init(int width, int height)
    {
        InventoryItemSlot = new InventoryItem[width, height];
        Vector2 Size = new Vector2(width * TileSizeWidth, height * TileSizeHeight);
        rectTransform.sizeDelta = Size;
    }
    //Ngereturn isi dari Inventory Menggunakan kordinat
    internal InventoryItem GetItem(int x, int y)
    {
        return InventoryItemSlot[x, y];
    }
    //Ngereturn Posisi Grid menggunakan Mouse
    public Vector2Int GetTileGridPosition(Vector2 MousePos)
    {
        positionOnGrid.x = MousePos.x - rectTransform.position.x;
        positionOnGrid.y = rectTransform.position.y - MousePos.y;

        tileGridPosition.x = (int)(positionOnGrid.x / TileSizeWidth);
        tileGridPosition.y = (int)(positionOnGrid.y / TileSizeHeight);

        return tileGridPosition;
    }
   //Untuk Ngecek Item Keluar dari Grid ato kaga
    public bool BoundaryCheck(int posX, int posY, int width, int height)
    {
        if (CheckPosition(posX, posY) == false) return false;

        posX += width -1;
        posY += height -1;

        if (CheckPosition(posX, posY) == false) return false;

        return true;

    }

    #endregion

}
