using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MainSlotManager : MonoBehaviour, IListener
{
    public static MainSlotManager Instance;
    MainSlot selectSlot;
    public MainSlot SelectSlot
    {
        get { return selectSlot; }
        set {
            selectSlot = value;
        }
    }
    public ItemSlot ConnectSlot;
    public Image SelectSlotImage;
    int SlotNum;
    public ItemSlot[] MainItemSlots;
    public ItemSlot[] InventoryItemSlots;

    void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        
    }
    void SelectItemSet(int slotNum)
    {
       // MainItemSlots[slotNum].InputItem(ref InventoryItemSlots[slotNum].ItemInform, InventoryItemSlots[slotNum].ItemInform.Count);
    }
    private void OnEnable()
    {
        //EventManager.Instance.AddListener(EventType.MainSlotChange, )
    }
    private void OnDisable()
    {
        
    }

    public void OnEvent(EventType _EventType, Component Sender, object Param = null)
    {
        
    }
}
