using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class DataParser
{
    public static Item[] ItemDataParser(string CSVFileName)
    {
        List<Item> ItemList = new List<Item>();
        TextAsset csvData = Resources.Load<TextAsset>("CSV/" + CSVFileName);
        
        string[] data = csvData.text.Split(new char[] { '\n' });
        for (int i = 1; i < data.Length; i++)
        {
            Item itemData = new Item();
            // 0 : ID, 1 : Name, 2 : Description, 3 : MaxCount, 4 : Power, 5 : Speed. 6 : Type
            string[] row = data[i].Split(',');
            int id = int.Parse(row[0]);
            string name = row[1];
            string des = row[2];
            int maxCount = Int32.Parse(row[3]);
            float power = float.Parse(row[4]);
            float speed = float.Parse(row[5]);
            Item.ItemType type = (Item.ItemType)Enum.Parse(typeof(Item.ItemType), row[6]);
            itemData.ID = id;
            itemData.ItemName = name;
            itemData.Des = des;
            itemData.MaxCount = maxCount;
            itemData.Power = power;
            itemData.Speed = speed;
            itemData.Type = type;
            itemData.Icon = Resources.Load<Sprite>("Image/" + id);
            ItemList.Add(itemData);
        }
        Item[] itemdatas = ItemList.ToArray();
        return itemdatas;
    }
}
