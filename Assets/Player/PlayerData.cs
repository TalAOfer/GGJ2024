using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Player Data")]
public class PlayerData : ScriptableObject
{
    public float moveSpeed;
    public LayerMask hittable;

    [Range(0, 1)]
    public float uppercutMaxCharge;
    [Range(0,1)]
    public float punchMaxCharge;
    public float maxChargeDuration;
}
