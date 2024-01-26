using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffectApplier : MonoBehaviour
{
    public ImpactEffect impactEffect;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ApplyEffects(Component sender, object data)
    {
        Vector2 hitDirection = (Vector2)data;
        impactEffect.Apply(transform, rb, hitDirection);
    }

    private void OnDisable()
    {
        rb.gravityScale = 0f;
        rb.velocity = Vector3.zero;
    }
}
