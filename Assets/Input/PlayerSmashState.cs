using UnityEngine;

public class PlayerSmashState : PlayerAttackState
{
    public PlayerSmashState(PlayerController player, FiniteStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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
        CapsuleCollider2D punchCollider = player.smashCollider;
        Vector2 capsuleSize = punchCollider.size;
        Vector2 capsuleCenter = (Vector2)player.transform.position + punchCollider.offset;
        float angle = player.transform.eulerAngles.z; // Assuming the rotation of the collider is the same as the GameObject

        // Determine the direction of the capsule based on its orientation
        CapsuleDirection2D capsuleDirection = punchCollider.direction;
        Vector2 castDirection = capsuleDirection == CapsuleDirection2D.Vertical ? Vector2.up : Vector2.right;

        // Perform the CapsuleCastAll
        RaycastHit2D[] hits = Physics2D.CapsuleCastAll(capsuleCenter, capsuleSize, capsuleDirection, angle, castDirection, 0f, playerData.hittable);

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
