
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerInputHandler inputs;
    public PlayerPunchManager punchHandler;
    public PlayerAnimation animationHandler;


    public Vector2Variable playerPos;
    // Update is called once per frame

    private bool isFacingRight;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        isFacingRight = true;
    }

    void Update()
    {
        CheckFlip();

        playerPos.value = transform.position;
    }


    #region Flip

    public void CheckFlip()
    {
        if (isFacingRight && inputs.MoveValue.x < 0)
        {
            Flip();
        }

        else if (!isFacingRight && inputs.MoveValue.x > 0)
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

    #endregion
}
