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

    public void PlayImpact()
    {
        anim.Play("Enemy_Impact");
    }

    public void PlayPostImpact()
    {
        anim.Play("Enemy_AfterImpact");
    }
}
