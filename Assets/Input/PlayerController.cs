using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerController : MonoBehaviour
{
    public Vector2 moveValue = new();
    public void OnMove(CallbackContext ctx)
    {
        moveValue = ctx.ReadValue<Vector2>();
    }
}
