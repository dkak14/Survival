using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : BuildingBase
{
    public GameObject Leap;
    SpriteRenderer LeapRender;
    protected override void Start()
    {
        base.Start();
        LeapRender = Leap.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
            Hp -= 1;
    }
    protected override void OnDamageEffect(int Damage)
    {
        StartCoroutine(Shake(Damage));
    }
    IEnumerator Shake(int Damage)
    {
        Vector2 Pos = Rigid.position;
        float x = Random.Range(-0.1f, 0.1f);
        float y = Random.Range(-0.1f, 0.1f);
       Rigid.position = new Vector2(Pos.x + x, Pos.y + y);
       yield return new WaitForSeconds(0.05f);
       Rigid.position = Pos;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            LeapRender.color = new Color(1, 1, 1, 0.5f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            LeapRender.color = new Color(1, 1, 1, 1);
        }
    }
}
