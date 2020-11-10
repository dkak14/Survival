using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour, IListener
{
    [Range(0, 4)]
    public int SelectSlotNum;
    public Transform ItemPos;
    public GameObject ItemImage;

    void Awake()
    {
        ItemChange();
        EventManager.Instance.AddListener(EventType.MainSlotChange, this);
        EventManager.Instance.AddListener(EventType.GetItem, this);
    }
    public void ItemChange()
    {
        SpriteRenderer ItemRenderer = ItemImage.GetComponent<SpriteRenderer>();
        if (MainSlot.SelectSlot != null)
        {
            if (MainSlot.SelectSlot.ItemInform != null)
            {
                ItemRenderer.sprite = MainSlot.SelectSlot.ItemIcon.sprite;
                ItemRenderer.color = new Color(1, 1, 1, 1);
            }
            else
            {
                ItemRenderer.sprite = null;
                ItemRenderer.color = new Color(1, 1, 1, 0);
            }
        }

    }

    public void OnEvent(EventType _EventType, Component Sender, object Param = null)
    {
        if(_EventType == EventType.MainSlotChange)
        {
            Debug.Log("아이템 슬롯 바꿈");
            if(Sender == MainSlot.SelectSlot)
            ItemChange();
            return;
        }
        if(_EventType == EventType.GetItem)
        {
            if (Sender != MainSlot.SelectSlot.ConnectSlot)
                ItemChange();
            return;
        }
    }
}
