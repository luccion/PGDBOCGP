using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 一次性香蕉
/// </summary> <summary>
/// 
/// </summary>
public class Banana : Item
{
    public override int price { get => base.price; }
    public override bool OnInteract(ICreatureController creatureController)
    {
        if (!isUsed)
        {
            creatureController.RunCoroutine(BlockCoroutine(creatureController));

            return true;
        }
        else
        {
            return false;
        }


    }
    IEnumerator BlockCoroutine(ICreatureController creatureController)
    {
        isUsed = true;
        creatureController.StateMachine.TransitionTo(CreatureState.BLOCK);
        creatureController.SetSpeed(0);
        //creatureController.Animator.SetTrigger("Roll");
        creatureController.Animator.SetTrigger("Rolling");
        yield return new WaitForSeconds(2f);
        creatureController.Animator.SetTrigger("EndRoll");
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
        creatureController.StateMachine.TransitionTo(CreatureState.RUN);



        Debug.Log("oops");
    }


}
