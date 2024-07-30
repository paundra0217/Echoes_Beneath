using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHighlight : MonoBehaviour
{
    [SerializeField] RectTransform highlighter;
    //Buat Nyalain/Matiin Highlighter
    public void Show(bool b)
    {
        highlighter.gameObject.SetActive(b);
    }
    //Buat Ngeset Ukuran Highlighter sesuai sama TargetItem
    public void Setsize(InventoryItem targetItem)
    {
        Vector2 size = new Vector2();
        size.x = targetItem.WIDTH * ItemGrid.TileSizeWidth;
        size.y = targetItem.HEIGHT * ItemGrid.TileSizeHeight;
        highlighter.sizeDelta = size;

    }
    //buat ngesetPosisi
    public void SetPostion(ItemGrid targetGrid, InventoryItem targetItem)
    {
        Vector2 pos = targetGrid.CalculatePositionOnGrid(targetItem, targetItem.OnGridPositionX, targetItem.OnGridPositionY);

        highlighter.localPosition = pos;

    }
    //Biar bisa posisinya dibawah dari item
    public void SetParent(ItemGrid targetGrid)
    {
        if (targetGrid == null) return;
        highlighter.SetParent(targetGrid.GetComponent<RectTransform>());
    }
    //buat ngesetPosisi
    public void SetPosition(ItemGrid targetGrid, InventoryItem targetItem, int posX, int posY)
    {
        Vector2 pos = targetGrid.CalculatePositionOnGrid(targetItem, posX, posY);

        highlighter.localPosition = pos;

    }

}
