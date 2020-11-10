using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SelectItem : MonoBehaviour
{
    public static SelectItem Instance;
    RectTransform Rect;
    [SerializeField]
    Item _itemInform;
    public Item ItemInform
    {
        get { CountSet();  return _itemInform; }
        set
        {
            _itemInform = value;
            if (_itemInform != null)
                _itemInform._CountChange += CountSet;
            CountSet();
        }
    }

    public Text ItemCountText;
    public Image ItemIcon;
    void Start()
    {
        Rect = GetComponent<RectTransform>();
        Instance = this;
        SlotClear();
    }
    private void Update()
    {
        //Rect.anchoredPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = Input.mousePosition;
    }
    public bool SlotItemCheck(Item item, int Count)
    {
        if (item.ID == ItemInform.ID && Count > ItemInform.MaxCount - ItemInform.Count)
        {
            return true;
        }
        return false;
    }

    public void InputItem(Item item)
    {
        ItemInform = item;
        ItemIcon.sprite = item.Icon;
        CountSet();
    }
    public void IconImageSet()
    {
        if (_itemInform != null)
        {
            ItemIcon.sprite = _itemInform.Icon;
            ItemIcon.color = new Color(1, 1, 1, 0.5f);
        }
        else
        {
            ItemIcon.sprite = null;
            ItemIcon.color = new Color(1, 1, 1, 0);
        }
    }
    public virtual void CountSet()
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
