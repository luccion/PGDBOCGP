using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockState : MonoBehaviour
{

    CreatureController player;
    public BlockState(CreatureController player)
    {
        this.player = player;
    }

    public void EnterState()
    {
        Debug.Log("jump");
    }

    public void ExitState()
    {

    }

    public void UpdateState()
    {
        // if (player.IsOnGround)
        // {
        //     player.TransitionTo(CreatureState.WALK);
        // }

    }

}
