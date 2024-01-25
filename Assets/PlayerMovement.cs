
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f; // Speed of the player
    private PlayerController controller;
    private Rigidbody2D rb;
    private bool movementLocked;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }



    private void FixedUpdate()
    {
        if (!controller.punchHandler.IsCharging)
        {
            MovePlayer();
        }
    }

    void MovePlayer()
    {

        Vector2 move = controller.inputs.MoveValue;

        if (move != Vector2.zero)
        {
            // Apply force for movement
            rb.AddForce(move * speed);
        }
        else
        {
            // If MoveValue is Vector2.zero, stop immediately
            rb.velocity = Vector2.zero;
        }

        //Limit the maximum velocity
        if (rb.velocity.magnitude > speed)
        {
            rb.velocity = rb.velocity.normalized * speed;
        }
    }
    public void LockMovement() => movementLocked = true;
    public void UnlockMovement() => movementLocked = false;
}