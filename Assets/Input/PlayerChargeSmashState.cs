using UnityEngine;

public class PlayerChargeSmashState : PlayerChargeState
{
    public PlayerChargeSmashState(PlayerController player, FiniteStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void ChangeBarColor()
    {
        player.ShowHitTypeIcon(HitType.Smash);
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (player.chargeAmount >= 1 || !player.inputs.IsPressingHit)
        {
            if (isAnimationFinished)
            {
                stateMachine.ChangeState(player.SmashState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}