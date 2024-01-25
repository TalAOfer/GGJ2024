using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunchManager : MonoBehaviour
{
    private PlayerController playerController;
    private PlayerInputHandler inputs => playerController.inputs;
    public LayerMask layerMask;
    private BoxCollider2D boxCollider;

    [ShowInInspector, ReadOnly]
    private bool _isCharging;
    public bool IsCharging
    {
        get { return _isCharging; }
        private set { _isCharging = value; }
    }

    void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (!IsCharging && inputs.IsPressingHit)
        {
            IsCharging = true;
        }

        else if (IsCharging && !inputs.IsPressingHit)
        {
            IsCharging = false;
            Hit();
        }
    }

    public void Hit()
    {
        Vector2 boxSize = boxCollider.size;
        Vector2 boxCenter = (Vector2)transform.position + boxCollider.offset;
        float angle = transform.eulerAngles.z; // Assuming the rotation of the collider is the same as the GameObject

        // Perform the BoxCastAll
        RaycastHit2D[] hits = Physics2D.BoxCastAll(boxCenter, boxSize, angle, Vector2.zero, 0f, layerMask);

        // Iterate through all hits
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                Debug.Log("found coll");
                Hittable hittable = hit.collider.GetComponent<Hittable>();
                if (hittable != null)
                {
                    Debug.Log("found hit");
                    Vector2 hitDirection = GetDirection(transform.parent, hit.transform);
                    hittable.Hit(hitDirection);
                }
            }
        }
    }

    Vector2 GetDirection(Transform from, Transform to)
    {
        return (to.position - from.position).normalized;
    }
}
