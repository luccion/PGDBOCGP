using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "VoidEvent", menuName = "Event/VoidEvent", order = 0)]
public class VoidEvent : ScriptableObject
{

    public UnityAction listeners;

    // 注册事件监听器
    public void Register(UnityAction listener)
    {
        listeners += listener;
    }

    // 注销事件监听器
    public void Unregister(UnityAction listener)
    {
        listeners -= listener;
    }

    // 触发事件
    public void Invoke()
    {
        if (listeners != null)
        {
            listeners.Invoke();
        }
    }
}

