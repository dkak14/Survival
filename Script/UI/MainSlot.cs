using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainSlot : ItemSlot, IListener, IPointerClickHandler
{
    [Range(0, 4)]
    [SerializeField]int ConnectSlotNum;
    public static MainSlot SelectSlot;
    public static List<MainSlot> MainSlots = new List<MainSlot>();
    public ItemSlot ConnectSlot;
    public Image SelectSlotImage;
    void Awake()
    {
        MainSlots.Add(this);
        if (SelectSlot == null)
        {
            SelectSlot = this;
        }
     
        EventManager.Instance.AddListener(EventType.MainSlotChange, this);
        EventManager.Instance.AddListener(EventType.GetItem, this);
    }
    protected override void Start()
    {
        base.Start();
        if(SelectSlot == this)
        {
            EventManager.Instance.PostNotification(EventType.MainSlotChange, this, SelectSlot);
        }
        ConnectSlot = Inventory.Instance.Slots[ConnectSlotNum];
    }
    public void OnEvent(EventType _EventType, Component Sender, object Param = null)
    {
        if(_EventType == EventType.MainSlotChange)
        {
            if(SelectSlot != this)
            {
                SelectSlotImage.color = new Color(1, 1, 1, 0);
            }
            else
            {
                SelectSlotImage.color = new Color(1, 1, 1, 1);
            }
        }
        // 연결된 인벤토리 슬롯에서 아이템을 얻을 때
        if(_EventType == EventType.GetItem)
        {
            if (Sender == Inventory.Instance.Slots[ConnectSlotNum])
            {
                ItemInform = Sender.GetComponent<ItemSlot>().ItemInform;
            }
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            SelectSlot = this;
            EventManager.Instance.PostNotification(EventType.MainSlotChange, this, SelectSlot);
            Debug.Log("메인슬롯 바꿈 알림");
        }
    }
}
