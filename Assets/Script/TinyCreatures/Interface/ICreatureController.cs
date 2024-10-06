using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICreatureController
{
    string Name { get; }
    bool GetLucky();
    Transform CreatureTransform { get; }
    StateMachine StateMachine { get; }
    TinyCreatureSO tinyCreatureSO { get; }
}
