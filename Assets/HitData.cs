using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Hit Data")]
public class HitData : ScriptableObject
{
    public float uppercutForce;
    public Vector2 uppercutDirection;
    public float punchForce;
    public Vector2 punchDirection;
    public float smashForce;
    public Vector2 smashDirection;
}
