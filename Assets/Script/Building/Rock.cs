using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Interaction
{
    override public bool OnInteract(ICreatureController creatureController)
    {

        creatureController.RunCoroutine(BlockCoroutine(creatureController));
        return true;
    }
    IEnumerator BlockCoroutine(ICreatureController creatureController)
    {
        //如果足够幸运
        if (creatureController.GetLucky())
        {
            creatureController.Animator.SetTrigger("Jump");
            creatureController.Animator.SetTrigger("Fall");
            Debug.Log(creatureController.Name + " lucky");
            yield break;
        }
        else
        {
            creatureController.StateMachine.TransitionTo(CreatureState.BLOCK);

            creatureController.SetSpeed(0);
            yield return new WaitForSeconds(0.2f);
            creatureController.StateMachine.TransitionTo(CreatureState.RUN);
            Debug.Log(creatureController.Name + " unlucky");
        }

        Debug.Log("oops");
    }
}
