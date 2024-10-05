using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Interactions
{
    override public bool OnInteract(ICreatureController creatureController)
    {
        Debug.Log("oops");
        return true;
    }
}
