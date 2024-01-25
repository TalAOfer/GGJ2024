using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private FollowPlayer followPlayer;

    public void OnHit()
    {
        followPlayer.enabled = false;
    }
}
