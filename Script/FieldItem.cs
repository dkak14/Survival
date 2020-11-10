using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItem : MonoBehaviour
{
    public int ID;
    SpriteRenderer Render;
    Rigidbody2D Rigid;
    Item ItemInform;
    bool CanGet;
    private void Awake()
    {
        Render = GetComponent<SpriteRenderer>();
        Rigid = GetComponent<Rigidbody2D>();
    }
    public void SetFieldItem(int ID, int Count, Vector2 Pos)
    {
        ItemInform = ItemDataManager.Instance.GetItem(ID);
        ItemInform.Count = Count;
        transform.position = Pos;
        Render.sprite = ItemInform.Icon;
    }
    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (CanGet)
            {
                Inventory PlayerInventory = collision.GetComponent<Inventory>();
                if(PlayerInventory.SearchSlot(ItemInform))
                {
                    CanGet = false;
                    EventManager.Instance.PostNotification(EventType.GetFieldItem, this, this);
                }
            }
        }
    }
    IEnumerator Spawn()
    {
        float x = Random.Range((float)-2, 2);
        float y = Random.Range((float)-2, 2);
        float decreaseX = x;
        float decreaseY = y;
        while (Mathf.Abs(x) > 0.1 && Mathf.Abs(y) > 0.1)
        {
            x -= decreaseX * 0.6f * Time.deltaTime;
            y -= decreaseY * 0.6f * Time.deltaTime;
            Rigid.velocity = new Vector2(x, y);
            yield return null;
        }
        Rigid.velocity = new Vector2(0, 0);
        CanGet = true;
    }
}
