using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Interaction
{
    override public bool OnInteract(ICreatureController creatureController)
    {
        //如果足够幸运
        if (creatureController.GetLucky())
        {
            Debug.Log(creatureController.Name + " lucky");
        }
        else
        {
            creatureController.StateMachine.TransitionTo(CreatureState.BLOCK);
            Debug.Log(creatureController.Name + " unlucky");
        }

        Debug.Log("oops");
        return true;
    }
}
