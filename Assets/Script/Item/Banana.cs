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
    [SerializeField] int ID;
    public override int price { get => base.price; }
    public override bool OnAfterInteract(ICreatureController creatureController)
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
        creatureController.Animator.SetTrigger("Roll");
        creatureController.StateMachine.TransitionTo(CreatureState.BLOCK);

        creatureController.SetSpeed(0);
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
        creatureController.StateMachine.TransitionTo(CreatureState.RUN);
        Debug.Log("oops");
    }


}
