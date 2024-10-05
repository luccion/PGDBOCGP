using UnityEngine;


public class RunningState : IGameState
{
    CreatureController player;
    public RunningState(CreatureController player)
    {
        this.player = player;
    }

    public void EnterState()
    {
        Debug.Log("enter walk");
    }

    public void ExitState()
    {
        Debug.Log("exit walk");
    }

    public void UpdateState()
    {
        if (player.GetIsStop())
            player.HandleMove();
    }
}

