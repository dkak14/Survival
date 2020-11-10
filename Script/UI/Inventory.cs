using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour, IListener
{
    public static Inventory Instance;
    public Dictionary<int, List<InventorySlot>> HaveItemDic;
    [SerializeField] GameObject Panel;
    public InventorySlot[] Slots;
    public 
     void Awake()
    {
        Instance = this;
        Slots = Panel.GetComponentsInChildren<InventorySlot>();
        EventManager.Instance.AddListener(EventType.GetItem, this);
        HaveItemDic = new Dictionary<int, List<InventorySlot>>();
    }
    public void ItemDelete(int ID, int Count)
    {
        if(GetItemNum(ID) >= Count)
        {
            List<InventorySlot> Slots = HaveItemDic[ID];
            for(int i =0; i < Slots.Count; i++)
            {
                Debug.Log("Count : " + Count);
                if(Slots[i].ItemInform.Count >= Count)
                {
                    Slots[i].ItemInform.Count -= Count;
                    return;
                }
                if(Slots[i].ItemInform.Count < Count)
                {
                    Count -= Slots[i].ItemInform.Count;
                    Slots[i].ItemInform.Count = 0;
                }
                if (Count == 0)
                    return;
            }
        }
    }
    public int GetItemNum(int ID)
    {
        int itemCount = 0;
        if (!HaveItemDic.ContainsKey(ID))
            return 0;
        List<InventorySlot> Slots = HaveItemDic[ID];
        Stack<int> DifferentSlot = new Stack<int>();
        for (int i = 0; i < Slots.Count; i++)
        {
            if(Slots[i].ItemInform != null)
                itemCount += Slots[i].ItemInform.Count;
            else
                DifferentSlot.Push(i);
        }
        if(DifferentSlot.Count > 0)
        HaveItemDic[ID].RemoveAt(DifferentSlot.Pop());
        return itemCount;
    }
    public void HaveItemCheck(InventorySlot slot)
    {
        if (slot == null)
            return;
        if(slot.ItemInform != null)
        {
            if (!HaveItemDic.ContainsKey(slot.ItemInform.ID))
            {
                List<InventorySlot> SlotList = new List<InventorySlot>();
                HaveItemDic.Add(slot.ItemInform.ID, SlotList);
            }
            if (HaveItemDic[slot.ItemInform.ID].Contains(slot))
            {
                if(slot.ItemInform.Count == 0)
                {
                    HaveItemDic[slot.ItemInform.ID].Remove(slot);
                }
            }
            else
            {
                HaveItemDic[slot.ItemInform.ID].Add(slot);
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ItemAdd(1001, 3);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ItemAdd(1000, 3);
        }
    }
    void ItemAdd(int ID, int Count)
    {
        Item item = ItemDataManager.Instance.GetItem(ID);
        item.Count = Count;
        if (ItemDataManager.Instance.GetBool(ID))
        {
            SearchSlot(item);
        }
        else
        {
            Debug.Log("아이템이 없다");
            return;
        }
    }

    public bool SearchSlot(Item _Item)
    {
        // 같은 아이템이 있을 경우 
        List<InventorySlot> NullSlots = new List<InventorySlot>();
        for (int i = 0; i < Slots.Length; i++)
        {
            // 빈슬롯 저장
            if (Slots[i].ItemInform == null)
            {
                if (NullSlots.Count == 0)
                    NullSlots.Add(Slots[i]);
                continue;
            }
            // Debug.Log("슬롯 번호 : "+ i +"슬롯 아이템 갯수 : " + Slots[i].ItemInform.Count);
            // 아이디가 같고 

            if (Slots[i].ItemInform.ID == _Item.ID)
            {
                
                if (_Item.Count < Slots[i].ItemInform.MaxCount && Slots[i].ItemInform.Count < Slots[i].ItemInform.MaxCount)
                {
                    // 들어갔을 때 갯수가 넘치지 않을 경우
                    if (Slots[i].ItemInform.Count + _Item.Count <= Slots[i].ItemInform.MaxCount)
                    {
                        Slots[i].ItemInform.Count +=  _Item.Count;
                        return true;
                    }
                    // 들어갔을 때 갯수가 넘칠 경우 최대 갯수만큼 넣고 다음 슬롯 검색
                    if (Slots[i].ItemInform.Count + _Item.Count > Slots[i].ItemInform.MaxCount)
                    {
                        int count = _Item.MaxCount - Slots[i].ItemInform.Count;
                        Slots[i].ItemInform.Count += count;
                        _Item.Count -= count;
                        continue;
                    }
                }
            }

        }
        // 중복 아이템이 없을경우 빈칸에 아이템 삽입
        if (NullSlots.Count != 0)
        {
            if (NullSlots[0].ItemInform == null)
                NullSlots[0].ItemInform = _Item;
            EventManager.Instance.PostNotification(EventType.GetItem, NullSlots[0], NullSlots[0]);
            return true;
        }
            return false;       
    }

    public void OnEvent(EventType _EventType, Component Sender, object Param = null)
    {
        if(_EventType == EventType.GetItem)
        {
            HaveItemCheck(Sender.GetComponent<InventorySlot>());
        }
    }
}
