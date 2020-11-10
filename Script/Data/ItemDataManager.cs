using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ItemDataManager : MonoBehaviour
{
    public static ItemDataManager Instance;
    public List<Item> items;
    Dictionary<int, Item> ItemDataDic;
    void Start()
    {
        Instance = this;
        Item[] ItemDatas = DataParser.ItemDataParser("ItemData");
        ItemDataDic = new Dictionary<int, Item>();
        for (int i = 0; i< ItemDatas.Length; i++)
        {
            ItemDataDic.Add(ItemDatas[i].ID, ItemDatas[i]);
            items.Add(ItemDatas[i]);
        }
    }
    // 얕은 복사 방지를 위해서 설정 일일히 다 짜줘야함
    /// <summary>아이디에 맞는 아이템을 돌려준다..</summary>
    public Item GetItem(int ID)
    {
        if(ItemDataDic.ContainsKey(ID))
        {
            Item DicItem = ItemDataDic[ID];
            Item item = new Item();
            item.ItemName = DicItem.ItemName;
            item.ID = DicItem.ID;
            item.Des = DicItem.Des;
            item.Icon = DicItem.Icon;
            item.Power = DicItem.Power;
            item.Speed = DicItem.Speed;
            item.MaxCount = DicItem.MaxCount;
            return item;
        }
        else
        {
            Debug.LogError("없는 아이디 입니다");
            return null;
        }
    }
    /// <summary>아이디가 있는 아이템이 있는지 검사한다.</summary>
    public bool GetBool(int ID)
    {
        if (ItemDataDic.ContainsKey(ID))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
