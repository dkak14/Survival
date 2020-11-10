using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class JoystickM : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    RectTransform Rect;
    Vector2 touch = Vector2.zero;
    public RectTransform Handle;
    public JoystickValue Value;

    float widthHalf;
    void Start()
    {
        Rect = GetComponent<RectTransform>();
        widthHalf = Rect.sizeDelta.x * 0.5f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        touch = (eventData.position - Rect.anchoredPosition) / widthHalf;
        if (touch.magnitude > 1)
            touch = touch.normalized;
        Value.JoyTouch = touch;
        Handle.anchoredPosition = touch * widthHalf;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Value.JoyTouch = Vector2.zero;
        Handle.anchoredPosition = Vector2.zero;
    }
}
