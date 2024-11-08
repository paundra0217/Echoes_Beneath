using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Size", menuName = "ItemSize")]
public class ItemSize : ScriptableObject
{
    public string Name;
    public int width;
    public int height;
    public bool CanStack;

    public Sprite Icon;


}
