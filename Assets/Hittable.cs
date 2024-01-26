using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hittable : MonoBehaviour
{
    public CustomGameEvent OnImpact;
    public CustomGameEvent OnPostImpact;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Hit(Vector2 direction, HitType hitType)
    {
        OnImpact.Invoke(this, direction);
        StartCoroutine(HitRoutine(direction, hitType));
    }

    public IEnumerator HitRoutine(Vector2 direction, HitType hitType)
    {
        yield return new WaitForSeconds(0.15f);

        Vector2 calcDirection = AdjustHitDirection(direction, hitType, 5f, 0.25f);
        OnPostImpact.Invoke(this, calcDirection);
    }

    public Vector2 AdjustHitDirection(Vector2 originalDirection, HitType hitType, float amplifyFactor, float dampenFactor)
    {
        Vector2 adjustedDirection = originalDirection;

        switch (hitType)
        {
            case HitType.Vertical:
                adjustedDirection.x *= dampenFactor; // Dampen horizontal component
                adjustedDirection.y *= amplifyFactor; // Amplify vertical component
                break;

            case HitType.Horizontal:
                adjustedDirection.x *= amplifyFactor; // Amplify horizontal component
                adjustedDirection.y *= dampenFactor; // Dampen vertical component
                break;
        }

        adjustedDirection.y = Mathf.Abs(adjustedDirection.y);

        return adjustedDirection;
    }
}

public enum HitType
{
    Vertical,
    Horizontal
}
