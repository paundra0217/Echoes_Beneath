using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItem : MonoBehaviour
{

    public ItemSize itemSize;
    public int MaxStack = 4;
    public int count = 1;
    
    public int HEIGHT
    {
        get
        {
            if (IsRotated) return itemSize.width;
            else return itemSize.height;
        }

    }

    public int WIDTH
    {
        get
        {
            if (IsRotated) return itemSize.height;
            else return itemSize.width;
        }

    }

    [SerializeField] private TMP_Text CountUI;
    [HideInInspector]public int OnGridPositionX;
    [HideInInspector]public int OnGridPositionY;
    [HideInInspector]public bool IsRotated = false;
    internal void Set(ItemSize itemSize)
    {
        this.itemSize = itemSize;

        GetComponent<Image>().sprite = itemSize.Icon;
        Vector2 size = new Vector2();
        size.x = itemSize.width * ItemGrid.TileSizeWidth;
        size.y = itemSize.height  * ItemGrid.TileSizeHeight;
        GetComponent<RectTransform>().sizeDelta = size;
    }

    internal void Rotate()
    {
        IsRotated = !IsRotated;

        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.rotation = Quaternion.Euler(0, 0, IsRotated == true ? 90f : 0f);


    }
 
    public void SetActiveUI()
    {
        CountUI.gameObject.SetActive(true);
        RefreshCount();
    }
    public void RefreshCount()
    {
        CountUI.text = count.ToString();
    }

}
