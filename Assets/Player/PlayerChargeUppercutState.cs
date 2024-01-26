using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChargeUppercutState : PlayerChargeState
{
    public PlayerChargeUppercutState(PlayerController player, FiniteStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void ChangeBarColor()
    {
        player.ChangeBarColor(Color.white);
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        player.chargeStartTime = Time.time;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (player.chargeAmount >= playerData.uppercutMaxCharge)
        {
            stateMachine.ChangeState(player.ChargePunchState);
        }

        else if (!player.inputs.IsPressingHit)
        {

            stateMachine.ChangeState(player.UppercutState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}