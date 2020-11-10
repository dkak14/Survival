using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ItemSlot : MonoBehaviour
{
    [SerializeField]
    Item _itemInform = null;
    public Item ItemInform { get {return _itemInform; } 
        set {  _itemInform = value;
            if (_itemInform != null)
            {
                _itemInform._CountChange += SlotCountSet;
            }
            SlotCountSet(); } }

    public Text ItemCountText;
    public Image ItemIcon;
    protected virtual void Start()
    {
        SlotCountSet();       
    }
    public bool SlotItemCheck(Item item, int Count)
    {
        if(item.ID == ItemInform.ID && Count > ItemInform.MaxCount - ItemInform.Count)
        {
            return true;
        }
        return false;
    }
    public void InputItem(Item item)
    {
        ItemInform = item;
        ItemIcon.sprite = item.Icon;
        ItemIcon.color = new Color(1, 1, 1, 1);
    }
    public void IconImageSet()
    {
        if (_itemInform != null)
        {
            ItemIcon.sprite = _itemInform.Icon;
            ItemIcon.color = new Color(1, 1, 1, 1);
            float IconSize = ItemIcon.rectTransform.rect.width;
            if (ItemIcon.sprite.rect.width > ItemIcon.sprite.rect.height)
                ItemIcon.rectTransform.sizeDelta = new Vector2(IconSize, ItemIcon.sprite.rect.height / ItemIcon.sprite.rect.width * IconSize);
            else
                ItemIcon.rectTransform.sizeDelta = new Vector2((ItemIcon.sprite.rect.width / ItemIcon.sprite.rect.height * IconSize), IconSize);
        }
        else
        {
            ItemIcon.sprite = null;
            ItemIcon.color = new Color(1, 1, 1, 0);
        }
        EventManager.Instance.PostNotification(EventType.GetItem, this, this);
    }
    public virtual void SlotCountSet()
    {
        if (_itemInform != null)
        {
            if (_itemInform.Count > 1)
                ItemCountText.text = _itemInform.Count.ToString();
            else
                ItemCountText.text = " ";
            if (_itemInform.Count <= 0)
                _itemInform = null;
        }
        else
            ItemCountText.text = " ";
        IconImageSet();
    }
    public void SlotClear()
    {
        ItemInform = null;
        ItemIcon.sprite = null;
        ItemIcon.color = new Color(1, 1, 1, 0);
        ItemCountText.text = "";      
    }
}
