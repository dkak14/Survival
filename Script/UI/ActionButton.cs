using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ActionButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public JoystickValue Value;

    public void OnPointerDown(PointerEventData eventData)
    {
        Value.ActionTouch = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Value.ActionTouch = false;
    }

}

