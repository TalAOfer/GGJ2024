using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsApplier : MonoBehaviour
{
    public ImpactEffect impactEffect;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void FreezeRigidbody()
    {
        rb.gravityScale = 0f;
        rb.velocity = Vector3.zero;
    }

    public void ApplyEffects(Component sender, object data)
    {
        Vector2 force = (Vector2)data;
        impactEffect.Apply(transform, rb, force);
    }

    public void DieAfterDelay()
    {
        StartCoroutine(Die());
    }
    public IEnumerator Die()
    {
        yield return new WaitForSeconds(3);
        Pooler.Despawn(gameObject);
    }

    private void OnDisable()
    {
        rb.gravityScale = 0f;
        rb.velocity = Vector3.zero;
    }
}
