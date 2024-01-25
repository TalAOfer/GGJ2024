using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayDead()
    {
        StartCoroutine(HitRoutine());
    }

    private IEnumerator HitRoutine()
    {
        anim.Play("Enemy_Impact");
        yield return new WaitForSeconds(0.15f);
        anim.Play("Enemy_AfterImpact");
    }
}
