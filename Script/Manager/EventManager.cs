using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using System;

public enum EventType
{
    MainSlotChange, GetItem, GetFieldItem, SelectCraftSlot, CollisionBuilding, UnCollisionBuilding
}
public interface IListener
{
    void OnEvent(EventType _EventType, Component Sender, object Param = null);
}
public class EventManager : MonoBehaviour
{
    
    private Dictionary<EventType, List<IListener>> Listeners = new Dictionary<EventType, List<IListener>>();

    private static EventManager eventManager;
    #region Instance
    public static EventManager Instance;

    #endregion

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    /// <summary>리스너 배열에 지정된 리스너 오브젝트를 추가하기 위한 함수</summary>
    public void AddListener(EventType _EventType, IListener Listener)
    {
        List<IListener> ListenList;
        if (Listeners.TryGetValue(_EventType, out ListenList))
        {
            ListenList.Add(Listener);
            return;
        }

        ListenList = new List<IListener>();
        ListenList.Add(Listener);
        Listeners.Add(_EventType, ListenList);
    }
    public void PostNotification(EventType _Eventtype, Component Sender, object Param = null)
    {
        List<IListener> ListenList = null;

        if (!Listeners.TryGetValue(_Eventtype, out ListenList))
            return;
        for( int i = 0; i < ListenList.Count; i ++)
        {
            if(!ListenList[i].Equals(null))
            {
                ListenList[i].OnEvent(_Eventtype, Sender, Param);
            }
        }
    }
    public void RemoveEvent(EventType _EventType)
    {
        Listeners.Remove(_EventType);
    }
    public void RemoveRedundancies()
    {
        Dictionary<EventType, List<IListener>> TempListeners = new Dictionary<EventType, List<IListener>>();

        foreach(KeyValuePair<EventType, List<IListener>> Item in Listeners)
        {
            for (int i = Item.Value.Count - 1; i >= 0; i--)
            {
                if (Item.Value[i].Equals(null))
                    Item.Value.RemoveAt(i);
            }
            if(Item.Value.Count>0)
            {
                TempListeners.Add(Item.Key, Item.Value);
            }
        }
        Listeners = TempListeners;
    }
    private void OnEnable()
    {
    }
    private void OnLevelWasLoaded()
    {
        RemoveRedundancies();
    }
}
