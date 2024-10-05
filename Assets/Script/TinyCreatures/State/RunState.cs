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
        Debug.Log("enter Run");
    }

    public void ExitState()
    {
        Debug.Log("exit Run");
    }

    public void UpdateState()
    {
        if (!player.GetIsStop())
            player.HandleMove();
    }
}

