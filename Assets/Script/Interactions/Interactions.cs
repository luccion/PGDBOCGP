using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interaction : MonoBehaviour
{
    abstract public bool OnAfterInteract(ICreatureController creatureController);
    virtual public void OnBeforeInteract(ICreatureController creatureController)
    {

    }
}
