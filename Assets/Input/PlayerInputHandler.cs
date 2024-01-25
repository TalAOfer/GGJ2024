using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [ShowInInspector, ReadOnly] 
    private Vector2 _moveValue = new Vector2();
    public Vector2 MoveValue
    {
        get { return _moveValue; }
        private set { _moveValue = value; }
    }

    [ShowInInspector, ReadOnly] 
    private bool _isPressingHit;
    public bool IsPressingHit
    {
        get { return _isPressingHit; }
        private set { _isPressingHit = value; }
    }

    //public CustomGameEvent OnHitPress;
    //public CustomGameEvent OnHitRelease;

    public void OnMove(InputAction.CallbackContext ctx)
    {
        MoveValue = ctx.ReadValue<Vector2>();
    }

    // Add a method for the hit action if needed
    public void OnHit(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            IsPressingHit = true;
            //OnHitPress.Invoke(this, null);
        }
        else if (ctx.canceled)
        {
            IsPressingHit = false;
            //OnHitRelease.Invoke(this, null);
        }
    }
}
