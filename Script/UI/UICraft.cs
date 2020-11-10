using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICraft : MonoBehaviour, IListener
{
    public GameObject CraftContents;
    public Scrollbar Scroll;
    public ItemSlot ItemImage;
    public Text ItemNameText;
    
    public List<ItemSlot> MeterialImage;
    public List<Text> MeterialText;
    public Text ItemDesText;
    CraftTable CraftTableData;
    public CraftSlot CraftSlotPrefab;
    public List<CraftSlot> CraftSlotList;
    CraftTable.CraftData CraftData;
    CraftSlot SelectCraftSlot;
    void Start()
    {
        EventManager.Instance.AddListener(EventType.SelectCraftSlot, this);
        EventManager.Instance.AddListener(EventType.GetItem, this);
    }
    /// <summary>
    /// 조합 리스트 설정
    /// </summary>
    /// <param name="craftTable"></param>
    public void CraftItemList(CraftTable craftTable)
    {
        InitList();
        for(int i = 0; i < MeterialImage.Count; i++)
        {
            MeterialImage[i].SlotClear();
            MeterialText[i].text = "";
        }
        foreach (CraftTable.CraftData itemdata in craftTable.m_Craft)
        {
            ItemImage.SlotClear();
            ItemDesText.text = "";
            ItemNameText.text = "";
            CraftSlot slot = Instantiate(CraftSlotPrefab, transform.position, CraftSlotPrefab.transform.rotation);
            slot.transform.parent = CraftContents.transform;
            slot.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            slot.SetSlot(itemdata);
            CraftSlotList.Add(slot);
        }
    }
    public void InitList()
    {
        if(CraftSlotList.Count > 0)
        {
            for(int i = 0;i < CraftSlotList.Count; i ++)
            {
                Destroy(CraftSlotList[i].gameObject);
            }
        }
        CraftSlotList = new List<CraftSlot>();
    }
    public void SelectSlot(CraftTable.CraftData craftData)
    {
        CraftData = new CraftTable.CraftData();
        CraftData = craftData;
        Item iteminform = ItemDataManager.Instance.GetItem(craftData.CraftItemID);
        iteminform.Count = 1;
        ItemImage.InputItem(iteminform);
        ItemNameText.text = iteminform.ItemName;
        ItemDesText.text = iteminform.Des;
        for (int i = 0; i < craftData.Materials.Length; i++)
        {
            MaterialSet(craftData.Materials[i], MeterialImage[i], MeterialText[i]);
        }
    }
    /// <summary>
    /// 아이템 재료의 이미지와 갯수를 설정
    /// </summary>
    /// <param name="craftData"></param>
    /// <param name="Image"></param>
    /// <param name="text"></param>
    void MaterialSet(CraftTable.Material MeterialData, ItemSlot imageSlot,Text text)
    {
        Item iteminform = ItemDataManager.Instance.GetItem(MeterialData.MaterialItemID);
        iteminform.Count = 0;
        imageSlot.InputItem(iteminform);
        text.text =  Inventory.Instance.GetItemNum(iteminform.ID).ToString() + " / "+ MeterialData.NeedNum.ToString();
    }

    public void OnEvent(EventType _EventType, Component Sender, object Param = null)
    {
        if (_EventType == EventType.SelectCraftSlot)
        {
            for (int i = 0; i < CraftSlotList.Count; i++)
            {
                if (Sender == CraftSlotList[i])
                {
                    SelectSlot(CraftSlotList[i].CraftItemData);
                }
            }
        }
        if(_EventType == EventType.GetItem)
        {
            if (CraftContents.activeSelf == true)
            {
                int id;
                if (Sender.GetComponent<ItemSlot>().ItemInform != null)
                {
                    id = Sender.GetComponent<ItemSlot>().ItemInform.ID;
                }
                else
                    return;
                if (CraftData.Materials != null)
                {
                    for (int i = 0; i < CraftData.Materials.Length; i++)
                    {
                        if (CraftData.Materials[i].MaterialItemID == id)
                        {
                            SelectSlot(CraftData);
                            return;
                        }
                    }
                }
            }
        }
    }
    public void CraftItem()
    {
        if (CraftData.Materials == null)
            return;

        for (int i = 0; i < CraftData.Materials.Length; i++)
        {
            if(Inventory.Instance.GetItemNum(CraftData.Materials[i].MaterialItemID) < CraftData.Materials[i].NeedNum)
            {
                Debug.Log(Inventory.Instance.GetItemNum(CraftData.Materials[i].MaterialItemID) + " " + CraftData.Materials[i].NeedNum);
                return;
            }
        }

        Item item = ItemDataManager.Instance.GetItem(CraftData.CraftItemID);
        if (!Inventory.Instance.SearchSlot(item))
            return;

        for (int i = 0; i < CraftData.Materials.Length; i++)
        {
            Inventory.Instance.ItemDelete(CraftData.Materials[i].MaterialItemID, CraftData.Materials[i].NeedNum);
        }

        if (CraftData.Materials != null)
        {
            for (int i = 0; i < CraftData.Materials.Length; i++)
            {
                SelectSlot(CraftData);
            }
        }
    }
    void ClickCraft()
    {

    }
}
