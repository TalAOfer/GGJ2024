using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerController controller;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        controller = GetComponentInParent<PlayerController>();
    }

    private void Update()
    {
        bool isMoving = controller.inputs.MoveValue != Vector2.zero;
        anim.SetBool("isMoving", isMoving);

        anim.SetBool("isCharging", controller.punchHandler.IsCharging);
    }
}
