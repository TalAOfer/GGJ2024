using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ImpactEffect : ScriptableObject
{
    public abstract void Apply(Transform targetTransform, Rigidbody2D rb);
}
