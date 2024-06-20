using RDCT.Item;
using UnityEngine;

public class KeyBaseItem : BaseItem
{
    [Header("Key Base Attribute")]

    [SerializeField] private string _keyName;
    [SerializeField] private int _keyCode;

    public int getKeyCode()
    {
        return _keyCode;
    }

    public string getKeyName()
    {
        return _keyName;
    }
}
