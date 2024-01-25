using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gravity Effect")]
public class ApplyGravityEffect : ImpactEffect
{
    [SerializeField] private float amount;
    public override void Apply(Transform targetTransform, Rigidbody2D rb)
    {
        rb.gravityScale = amount;
    }
}
