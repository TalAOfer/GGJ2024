using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hittable : MonoBehaviour
{
    public CustomGameEvent OnHit;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnDisable()
    {
        rb.gravityScale = 0f;
        rb.velocity = Vector3.zero;
    }
    public void Hit(Vector2 direction)
    {
        OnHit.Invoke(this, null);
        StartCoroutine(HitRoutine(direction));
    }

    public IEnumerator HitRoutine(Vector2 direction)
    {
        yield return new WaitForSeconds(0.15f);
        rb.AddForce(direction * 10, ForceMode2D.Impulse);
        rb.gravityScale = 2;
    }
}
