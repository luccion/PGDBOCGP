using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactions : MonoBehaviour
{
    abstract public bool OnInteract(ICreatureController creatureController);
}
