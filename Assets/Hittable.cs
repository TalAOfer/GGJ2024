using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hittable : MonoBehaviour
{
    public CustomGameEvent OnImpact;
    public CustomGameEvent OnPostImpact;
    [SerializeField] private HitData hitData;
    [SerializeField] private float impactDelay = 0.1f;

    public void Hit(Vector2 direction, HitType hitType)
    {
        OnImpact.Invoke(this, direction);
        StartCoroutine(HitRoutine(direction, hitType));
    }

    public IEnumerator HitRoutine(Vector2 direction, HitType hitType)
    {
        yield return new WaitForSeconds(impactDelay);

        Vector2 force = GetForce(direction, hitType);
        OnPostImpact.Invoke(this, force);
    }

    public Vector2 GetForce(Vector2 originalDirection, HitType hitType)
    {
        Vector2 force = new Vector2();

        switch (hitType)
        {
            case HitType.Uppercut:
                // Apply a strong upward force and a slight push in the opposite horizontal direction
                force.y = hitData.uppercutDirection.y * hitData.uppercutForce;
                force.x = -Mathf.Sign(originalDirection.x) * hitData.uppercutDirection.x * hitData.uppercutForce;
                break;
            case HitType.Punch:
                // Apply a constant force in the direction of the punch with a slight upward component
                force.x = Mathf.Sign(originalDirection.x) * hitData.punchDirection.x * hitData.punchForce;
                force.y = hitData.punchDirection.y * hitData.punchForce;
                break;
            case HitType.Smash:
                // Apply a strong upward force and a slight push in the opposite horizontal direction
                force.y = hitData.smashDirection.y * hitData.smashForce;
                force.x = -Mathf.Sign(originalDirection.x) * hitData.smashDirection.x * hitData.smashForce;
                break;
        }

        return force;
    }
}

public enum HitType
{
    Uppercut,
    Punch,
    Smash,
}
