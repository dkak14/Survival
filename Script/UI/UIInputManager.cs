using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIInputManager : MonoBehaviour
{
    delegate void Listener(ArrayList touches);
    event Listener touchBegin, touchMove, touchEnd;
    public GameObject Inventory;
    public EventHandler OnPressSpace;
    public EventHandler OpenInventory;
    void Start()
    {
        int count = Input.touchCount;
        if (count == 0) return;

        //이벤트를 체크할 플래그
        bool begin, move, end;
        begin = move = end = false;

        //인자로 보낼 ArrayList;
        ArrayList result = new ArrayList();

        for (int i = 0; i < count ; i++ ){
            Touch touch = Input.GetTouch(i);
            result.Add(touch); //보낼 인자에 추가
            if (touch.phase == TouchPhase.Began && touchBegin != null) begin = true;
            else if (touch.phase == TouchPhase.Moved && touchMove != null) move = true;
            else if (touch.phase == TouchPhase.Ended && touchEnd != null) end = true;
        }

        //포인트중 하나라도 상태를 가졌다면..
        if (begin) touchBegin(result);
        if (end) touchEnd(result);
        if (move) touchMove(result);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(Inventory.activeSelf == false)
            Inventory.SetActive(true);
            else
                Inventory.SetActive(false);
        }
    }
}
