using UnityEngine;


public class JumpState : IGameState
{
    CreatureController player;
    public JumpState(CreatureController player)
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

