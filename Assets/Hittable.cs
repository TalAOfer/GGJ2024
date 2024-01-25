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

    public void Hit(Vector2 direction)
    {
        rb.AddForce(direction * 10, ForceMode2D.Impulse);
        rb.gravityScale = 2;
        OnHit.Invoke(this, null);
    }
}
