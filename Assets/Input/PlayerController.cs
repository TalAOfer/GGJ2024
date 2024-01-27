
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public PlayerInputHandler inputs;
    public PlayerInput inputManager;
    [SerializeField] private GameEvent OnDeath;
    [SerializeField] private GameEvent OnHealthChange;
    public GameEvent ShakeScreen;
    public PlayerData playerData;
    // Update is called once per frame

    public Vector2Variable playerPos;
    public bool IsFacingRight { get; private set; }

    #region Health
    private int currentHealth;
    #endregion

    #region Hit Colliders

    [FoldoutGroup("Hit Colliders")]
    public BoxCollider2D upperCutCollider;
    [FoldoutGroup("Hit Colliders")]
    public BoxCollider2D punchCollider;
    [FoldoutGroup("Hit Colliders")]
    public CapsuleCollider2D smashCollider;

    #endregion

    #region Punch Meter
    [FoldoutGroup("Punch Meter")]
    public float chargeStartTime;
    [FoldoutGroup("Punch Meter")]
    public float chargeAmount;
    [FoldoutGroup("Punch Meter")]
    public Image chargeFill;
    [FoldoutGroup("Punch Meter")]
    public Color inactive;
    [FoldoutGroup("Punch Meter")]
    public Image uppercutIcon;
    [FoldoutGroup("Punch Meter")]
    public Image punchIcon;
    [FoldoutGroup("Punch Meter")]
    public Image smashIcon;
    [FoldoutGroup("Punch Meter")]
    #endregion

    #region Components

    public Animator Anim { get; private set; }

    public Rigidbody2D RB { get; private set; }
    #endregion

    public SpriteRenderer sr;
    public Material flashMaterial;
    public Material defaultMaterial;

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

    public PlayerLaughState LaughState { get; private set; }


    #endregion

    private void Awake()
    {
        Anim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        defaultMaterial = sr.material;

        FMODUnity.RuntimeManager.PlayOneShot("event:/BGM", transform.position);

        inputManager.enabled = true;
        IsFacingRight = true;
        currentHealth = 3;

        StateMachine = new FiniteStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, playerData, "Idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "Run");
        ChargeUppercutState = new PlayerChargeUppercutState(this, StateMachine, playerData, "ChargePunch");
        UppercutState = new PlayerUppercutState(this, StateMachine, playerData, "Uppercut");
        ChargePunchState = new PlayerChargePunchState(this, StateMachine, playerData, "ChargePunch");
        PunchState = new PlayerPunchState(this, StateMachine, playerData, "Punch");
        ChargeSmashState = new PlayerChargeSmashState(this, StateMachine, playerData, "ChargeSmash");
        SmashState = new PlayerSmashState(this, StateMachine, playerData, "Smash");
        LaughState = new PlayerLaughState(this, StateMachine, playerData, "Laugh");

    }

    private void Start()
    {
        StateMachine.Initialize(IdleState);
        ResetHitTypeIcon();
    }

    private void Update()
    {
        playerPos.value = transform.position;
        HandleBar();

        StateMachine.CurrentState.LogicUpdate();
    }

    private void ApplyHitFlash()
    {
        StartCoroutine(HitFlash(0.125f));
    }

    private IEnumerator HitFlash(float flashTime)
    {
        sr.material = flashMaterial;
        yield return new WaitForSeconds(flashTime);
        sr.material = defaultMaterial;
    }

    public void TakeDamage()
    {
        currentHealth -= 1;

        if (currentHealth >= 0)
        {
            OnHealthChange.Raise(this, currentHealth);
            ApplyHitFlash();
            FMODUnity.RuntimeManager.PlayOneShot("event:/Punch hit", transform.position);
        }

        if (currentHealth <= 0) 
        {
            smashCollider.size = new Vector2(20, 20);
            StateMachine.ChangeState(ChargeSmashState);
            OnDeath.Raise(); 
            inputManager.enabled = false;
        }
    }

    public void Laugh()
    {
        StateMachine.ChangeState(LaughState);
        FMODUnity.RuntimeManager.PlayOneShot("event:/Laughing", transform.position);
    }

    private void HandleBar()
    {
        chargeFill.fillAmount = chargeAmount;
    }

    public void ResetHitTypeIcon()
    {
        uppercutIcon.color = inactive;
        punchIcon.color = inactive;
        smashIcon.color = inactive;
    }

    public void ShowHitTypeIcon(HitType hitType)
    {
        switch (hitType)
        {
            case HitType.Uppercut:
                uppercutIcon.color = Color.white;
                punchIcon.color = inactive;
                smashIcon.color = inactive;
                break;
            case HitType.Punch:
                uppercutIcon.color = inactive;
                punchIcon.color = Color.white;
                smashIcon.color = inactive;
                break;
            case HitType.Smash:
                uppercutIcon.color = inactive;
                punchIcon.color = inactive;
                smashIcon.color = Color.white;
                break;
        }
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
        if ((IsFacingRight && inputs.MoveValue.x < 0)
            || (!IsFacingRight && inputs.MoveValue.x > 0))
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
        IsFacingRight = !IsFacingRight;
        Vector3 tempScale = transform.localScale;
        tempScale.x *= -1;
        transform.localScale = tempScale;
    }

    #endregion
}
