using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerChargeState : PlayerBattleState
{
    public PlayerChargeState(PlayerController player, FiniteStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        player.CheckFlip(false);
    }

    public override void Enter()
    {
        base.Enter();
        ChangeBarColor();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        IncrementCharge();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public abstract void ChangeBarColor();

    private void IncrementCharge()
    {
        if (player.chargeAmount < 1f)
        {
            // Calculate the time elapsed since the start
            float timeElapsed = Time.time - player.chargeStartTime;
            // Increment the value based on the elapsed time and duration
            player.chargeAmount = timeElapsed / playerData.maxChargeDuration;
            player.chargeAmount = Mathf.Clamp(player.chargeAmount, 0f, 1f); // Ensure the value doesn't exceed 1
        }
    }
}


