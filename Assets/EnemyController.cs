using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Windows;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private FollowPlayer followPlayer;

    private bool isFacingRight = true;
    private bool IsPlayerToMyRight => followPlayer.playerPos.value.x > transform.position.x;

    private void OnEnable()
    {
        followPlayer.enabled = true;
    }

    public void DestroySelfInTime()
    {
        StartCoroutine(DestroyRoutine());   
    }

    private IEnumerator DestroyRoutine()
    {
        yield return new WaitForSeconds(1);
        Pooler.Despawn(gameObject);
    }

    private void Update()
    {
        CheckFlip();
    }

    public void CheckFlip()
    {
        if (!followPlayer.enabled) return;

        if (isFacingRight && !IsPlayerToMyRight)
        {
            Flip();
        }

        else if (!isFacingRight && IsPlayerToMyRight)
        {
            Flip();
        }
    }

    public void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 tempScale = transform.localScale;
        tempScale.x *= -1;
        transform.localScale = tempScale;
    }

    public void OnHit()
    {
        followPlayer.enabled = false;
    }
}
