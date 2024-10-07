using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Interaction
{
    bool isLucky;
    public override void OnBeforeInteract(ICreatureController creatureController)
    {
        isLucky = creatureController.GetLucky();
        creatureController.Animator.SetTrigger("Jump");
        if (!isLucky)
        {
            Debug.Log(creatureController.Name + " lucky");
            creatureController.Animator.SetTrigger("Fall");
        }


    }
    override public bool OnAfterInteract(ICreatureController creatureController)
    {
        if (isLucky)
        {
            creatureController.Animator.SetTrigger("Fall");
            creatureController.Animator.SetTrigger("Run");
        }
        else
        {
            creatureController.RunCoroutine(BlockCoroutine(creatureController));
        }

        return true;
    }
    IEnumerator BlockCoroutine(ICreatureController creatureController)
    {

        creatureController.StateMachine.TransitionTo(CreatureState.BLOCK);
        creatureController.SetSpeed(0);
        yield return new WaitForSeconds(0.2f);
        creatureController.Animator.SetTrigger("Fall");
        creatureController.StateMachine.TransitionTo(CreatureState.RUN);
        creatureController.Animator.SetTrigger("Run");
        Debug.Log(creatureController.Name + " unlucky");

    }
}
