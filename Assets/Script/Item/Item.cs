
using UnityEngine;

public class Item : Interaction
{
    public bool isUsed = false;
    virtual public int price { get => 1; }
    public SpriteRenderer spriteRenderer;
    public override bool OnInteract(ICreatureController creatureController)
    {
        Debug.Log("use item");
        return false;
    }
}

