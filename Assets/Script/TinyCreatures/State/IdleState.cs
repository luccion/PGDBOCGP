using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IGameState
{
    CreatureController player;
    public IdleState(CreatureController player)
    {
        this.player = player;
    }

    public void EnterState()
    {
        Debug.Log("预备!");
    }

    public void ExitState()
    {
        Debug.Log("跑");
    }

    public void UpdateState()
    {

    }
}

