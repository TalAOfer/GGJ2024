using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAttackState : PlayerBattleState
{
    public PlayerAttackState(PlayerController player, FiniteStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.ResetMovement();
        player.inputs.ResetHit();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            if (player.inputs.MoveValue == Vector2.zero)
            {
                stateMachine.ChangeState(player.IdleState);
            } 
            
            else
            {
                stateMachine.ChangeState(player.MoveState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        player.chargeAmount = 0;
        player.ResetHitTypeIcon();
        TriggerAttack();
    }

    public abstract void TriggerAttack();

    protected Vector2 GetDirection(Transform from, Transform to)
    {
        return (to.position - from.position).normalized;
    }
}