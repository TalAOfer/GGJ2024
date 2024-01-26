
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public PlayerInputHandler inputs;

    public PlayerData playerData;
    // Update is called once per frame

    public Vector2Variable playerPos;
    private bool isFacingRight;

    public float chargeStartTime;
    public float chargeAmount;
    public Image chargeFill;

    public BoxCollider2D upperCutCollider;
    public BoxCollider2D punchCollider;
    public CapsuleCollider2D smashCollider;

    #region Components

    public Animator Anim { get; private set; }
    
    public Rigidbody2D RB { get; private set; }
    #endregion

    #region FSM
    public FiniteStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerChargeUppercutState ChargeUppercutState { get; private set; }
    public PlayerUppercutState UppercutState { get; private set; }
    public PlayerChargePunchState ChargePunchState { get; private set; }
    public PlayerPunchState PunchState { get; private set; }
    public PlayerChargeSmashState ChargeSmashState { get; private set; }
    public PlayerSmashState SmashState { get; private set; }

    #endregion

    private void Awake()
    {
        Anim = GetComponent<Animator>();
        RB =GetComponent<Rigidbody2D>(); 

        isFacingRight = true;

        StateMachine = new FiniteStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, playerData, "Idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "Run");
        ChargeUppercutState = new PlayerChargeUppercutState(this, StateMachine, playerData, "ChargePunch");
        UppercutState = new PlayerUppercutState(this, StateMachine, playerData, "Uppercut");
        ChargePunchState = new PlayerChargePunchState(this, StateMachine, playerData, "ChargePunch");
        PunchState = new PlayerPunchState(this, StateMachine, playerData, "Punch");
        ChargeSmashState = new PlayerChargeSmashState(this, StateMachine, playerData, "ChargeSmash");
        SmashState = new PlayerSmashState(this, StateMachine, playerData, "Smash");
    }

    private void Start()
    {
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        playerPos.value = transform.position;
        HandleBar();

        StateMachine.CurrentState.LogicUpdate();
    }

    private void HandleBar()
    {
        chargeFill.fillAmount = chargeAmount;
    }

    public void ChangeBarColor(Color color)
    {
        chargeFill.color = color;
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    #region Flip

    public void CheckFlip(bool resetMovementOnFlip)
    {
        if ((isFacingRight && inputs.MoveValue.x < 0)
            || (!isFacingRight && inputs.MoveValue.x > 0))
        {
            Flip();

            if (resetMovementOnFlip)
            {
                ResetMovement();
            }
        }
    }

    public void ResetMovement()
    {
        RB.velocity = Vector2.zero;
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
