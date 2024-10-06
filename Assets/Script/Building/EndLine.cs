using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EndLine : Interaction
{
    [SerializeField] SelectEvent OnWin;
    bool end = false;
    override public bool OnInteract(ICreatureController creatureController)
    {
        if (end) return false;
        OnWin.Invoke(creatureController);
        end = true;
        Debug.Log("win!!");
        return true;
    }
}
