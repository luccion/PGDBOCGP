
using UnityEngine;

public class Item : Interaction
{
    public bool isUsed = false;
    public override bool OnInteract(ICreatureController creatureController)
    {
        Debug.Log("use item");
        return false;
    }
}

