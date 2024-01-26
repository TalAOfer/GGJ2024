﻿using UnityEngine;

public class PlayerUppercutState : PlayerAttackState
{
    public PlayerUppercutState(PlayerController player, FiniteStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        Vector2 boxSize = player.upperCutCollider.size;
        Vector2 boxCenter = (Vector2)player.transform.position + player.upperCutCollider.offset;
        float angle = player.transform.eulerAngles.z; // Assuming the rotation of the collider is the same as the GameObject

        // Perform the BoxCastAll
        RaycastHit2D[] hits = Physics2D.BoxCastAll(boxCenter, boxSize, angle, Vector2.zero, 0f, playerData.hittable);

        // Iterate through all hits
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                Hittable hittable = hit.collider.GetComponent<Hittable>();
                if (hittable != null)
                {
                    Vector2 hitDirection = GetDirection(player.transform, hit.transform);
                    hittable.Hit(hitDirection, HitType.Vertical);
                }
            }
        }
    }
}

