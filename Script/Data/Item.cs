using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public delegate void CountChange();
    public event CountChange _CountChange;
    public string ItemName;
    public int ID;
    public string Des;
    public float Power;
    public float Speed;
    public int MaxCount;
    [SerializeField]
    int count;
    public int Count
    {
        get { if (ItemName == null)
                return 0;
        else
                return count; }
        set
        {
            count = Mathf.Clamp(value, 0, MaxCount);
            if(_CountChange != null)
            _CountChange();

        }
    }
    public Sprite Icon;
    public ItemType Type;
    public Item()
    {
        count = 1;
    }
    public enum ItemType
    {
        Weapon, Armor, Use, Food, Guitar
    }

}
