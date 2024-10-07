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
        player.Animator.SetTrigger("Run");
        Debug.Log("enter Run");
    }

    public void ExitState()
    {
        Debug.Log("exit Run");
        player.Animator.SetFloat("SpeedP", player.GetSpeedPercent());
    }

    public void UpdateState()
    {
        if (!player.GetIsStop())
            player.HandleMove();
    }
}

