using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICreatureController
{
    string Name { get; }
    bool GetLucky();
    void SetSpeed(int speed);
    Transform CreatureTransform { get; }
    StateMachine StateMachine { get; }
    TinyCreatureSO tinyCreatureSO { get; }
    // 开启协程的包装方法
    public void RunCoroutine(IEnumerator coroutine);
}
