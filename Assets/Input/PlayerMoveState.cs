using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerMoveState : PlayerNormalState
{
    public PlayerMoveState(PlayerController player, FiniteStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
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

        if (player.inputs.MoveValue == Vector2.zero)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        MovePlayer();
    }

    public void MovePlayer()
    {
        Vector2 move = player.inputs.MoveValue;

        if (move != Vector2.zero)
        {
            // Apply force for movement
            player.RB.AddForce(move * playerData.moveSpeed);
        }
        else
        {
            // If MoveValue is Vector2.zero, stop immediately
            player.ResetMovement();
        }

        //Limit the maximum velocity
        if (player.RB.velocity.magnitude > playerData.moveSpeed)
        {
            player.RB.velocity = player.RB.velocity.normalized * playerData.moveSpeed;
        }
    }


}
