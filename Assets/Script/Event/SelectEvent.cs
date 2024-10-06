using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SelectEvent", menuName = "Event/SelectEvent", order = 0)]
public class SelectEvent : ScriptableObject
{
    public UnityAction<ICreatureController> listeners;

    // 注册事件监听器
    public void Register(UnityAction<ICreatureController> listener)
    {
        listeners += listener;
    }

    // 注销事件监听器
    public void Unregister(UnityAction<ICreatureController> listener)
    {
        listeners -= listener;
    }

    // 触发事件
    public void Invoke(ICreatureController creatureController)
    {
        if (listeners != null)
        {
            listeners?.Invoke(creatureController);
        }
    }
}
