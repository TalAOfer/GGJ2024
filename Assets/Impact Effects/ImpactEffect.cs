using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ImpactEffect
{
    public ImpactEffectType types;

    public float flyForce = 10;
    public float gravityScale = 2;
    public float spinForce = 10;

    public void Apply(Transform targetTransform, Rigidbody2D rb, Vector2 hitDirection)
    {
        if (types.HasFlag(ImpactEffectType.Fly))
        {
            Fly(rb, hitDirection);
            rb.gravityScale = gravityScale;
        }

        if (types.HasFlag(ImpactEffectType.Spin))
        {
            Spin(rb, hitDirection);
        }
    }

    public void Fly(Rigidbody2D rb, Vector2 hitDirection)
    {
        rb.AddForce(hitDirection * flyForce, ForceMode2D.Impulse);
    }

    public void Spin(Rigidbody2D rb, Vector2 hitDirection)
    {
        rb.AddTorque(spinForce * -hitDirection.x);
    }
}

[Flags]
public enum ImpactEffectType
{
    Fly = 1,
    Spin = 2,
}
