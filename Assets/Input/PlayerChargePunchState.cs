using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerChargePunchState : PlayerChargeState
{
    public PlayerChargePunchState(PlayerController player, FiniteStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void ChangeBarColor()
    {
        player.ShowHitTypeIcon(HitType.Punch);
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

        if (player.chargeAmount >= playerData.punchMaxCharge)
        {
            stateMachine.ChangeState(player.ChargeSmashState);
        }

        else if (!player.inputs.IsPressingHit)
        {
            stateMachine.ChangeState(player.PunchState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
