using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : ItemSlot, IPointerClickHandler
{
    protected override void Start()
    {
       // ItemInform._CountChange += Check;
        base.Start();

    }
    public void Check()
    {
        Inventory.Instance.HaveItemCheck(this);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if(SelectItem.Instance.ItemInform == null)
            {
                if(ItemInform != null)
                {
                    SelectItem.Instance.ItemInform = ItemInform;
                    ItemInform = null;
                    return;
                }
            }
            if (SelectItem.Instance.ItemInform != null)
            {
                if (ItemInform != null)
                {
                    if(ItemInform.ID == SelectItem.Instance.ItemInform.ID)
                    {
                        if (SelectItem.Instance.ItemInform.Count + ItemInform.Count > ItemInform.MaxCount)
                        {
                            int count = ItemInform.MaxCount - ItemInform.Count;
                            int Count = Mathf.Clamp(count, 0, SelectItem.Instance.ItemInform.Count);
                            SelectItem.Instance.ItemInform.Count -= Count;
                            ItemInform.Count += Count;
                            return;
                        }
                        else
                        {
                            SelectItem.Instance.ItemInform.Count += ItemInform.Count;
                            ItemInform = null;
                        }
                    }
                    if (ItemInform.ID != SelectItem.Instance.ItemInform.ID)
                    {
                        Item Temp = ItemInform;
                        ItemInform = SelectItem.Instance.ItemInform;
                        SelectItem.Instance.ItemInform = Temp;
                        return;
                    }
                }
                if (ItemInform == null)
                {
                    Item Temp = SelectItem.Instance.ItemInform;
                    SelectItem.Instance.ItemInform = ItemInform;
                    ItemInform = Temp;
                    return;
                }
            }
        }
    }
}
