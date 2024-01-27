using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.Windows;

public class EnemyController : MonoBehaviour
{
    public EnemyState state;

    [SerializeField] private float speed;
    [SerializeField] private LayerMask playerMask;

    [FoldoutGroup("Dependencies")]
    public Vector2Variable playerPos;

    #region Components
    [FoldoutGroup("Components")]
    [SerializeField] private Transform body;
    [FoldoutGroup("Components")]
    private Animator anim;
    [FoldoutGroup("Components")]
    private PhysicsApplier physics;
    #endregion

    #region
    [FoldoutGroup("Joke")]
    public float jokeLength;
    [FoldoutGroup("Joke")]
    public float distance = 5.0f;
    [FoldoutGroup("Joke")]
    public GameObject jokeContainer;
    [FoldoutGroup("Joke")]
    public Image jokeBar;
    [FoldoutGroup("Joke")]
    public CustomGameEvent OnJokeFinished;
    private Coroutine jokeRoutine;
    private float timer;
    [SerializeField] private float jokeStartTime;
    private bool didTellJoke;
    #endregion

    private bool isTellingJoke;
    private Coroutine destroySelfRoutine;

    private bool isFacingRight = true;
    private bool IsPlayerToMyRight => playerPos.value.x > transform.position.x;

    private bool isEnemyInSight;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        physics = GetComponent<PhysicsApplier>();
    }
    private void OnEnable()
    {
        GoToFollow();
        //jokeCollider.SetActive(true);
        destroySelfRoutine = null;
        isTellingJoke = false;
        didTellJoke = false;
        jokeRoutine = null;
    }

    private void Update()
    {
        if (!didTellJoke)
        {
            timer += Time.deltaTime;
            if (timer > jokeStartTime)
            {
                didTellJoke = true;
                if (state is EnemyState.Idle or EnemyState.Follow)
                {
                    InitiateJoke();
                }
            }
        }

        if (state is EnemyState.Idle || state is EnemyState.Follow)
        {
            CheckFlip();
        }

        // Perform the raycast
        Vector2 lookDirection = isFacingRight ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, lookDirection, distance, playerMask);

        // Check if the ray hit something
        isEnemyInSight = (hit.collider != null);

        if (state is EnemyState.Follow)
        {
            if (isEnemyInSight) GoToIdle();
            float step = speed * Time.deltaTime;

            // move sprite towards the target location
            transform.position = Vector2.MoveTowards(transform.position, playerPos.value, step);
        }

        else if (state is EnemyState.Idle)
        {
            if (!isEnemyInSight) GoToFollow();
        }
    }


    private void OnDrawGizmos()
    {
        // Draw the ray in the editor
        Gizmos.color = Color.red;
        Vector3 endPosition = transform.position + (Vector3)(Vector2.right.normalized * distance);
        Gizmos.DrawLine(transform.position, endPosition);

        // Draw a hit point if the ray hits something
        Vector2 lookDirection = isFacingRight ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, lookDirection, distance);
        if (hit.collider != null)
        {
            Gizmos.DrawSphere(hit.point, 0.1f);
        }
    }

    #region States
    public void GoToIdle()
    {
        state = EnemyState.Idle;

        if (!isTellingJoke) anim.Play("Idle");
        else anim.Play("IdleJoke");

        physics.FreezeRigidbody();
    }

    public void GoToFollow()
    {
        state = EnemyState.Follow;
        if (!isTellingJoke)
        {
            anim.Play("Run");
        }

        else
        {
            anim.Play("RunJoke");
        }
    }
    public void GoToImpact(Component sender, object data)
    {
        state = EnemyState.Impact;
        StopJoke();
        anim.Play("Impact");
        physics.FreezeRigidbody();

    }

    public void GoToFly(Component sender, object data)
    {
        state = EnemyState.Fly;
        anim.Play("Fly");
        StartDeathClock();
        Vector2 force = (Vector2)data;
        physics.ApplyEffects(this, force);
    }

    #endregion

    #region Joke

    public void InitiateJoke()
    {
        if (isTellingJoke) return;

        isTellingJoke = true;
        jokeContainer.SetActive(true);
        jokeRoutine = StartCoroutine(JokeRoutine());
        if (state is EnemyState.Idle) anim.Play("IdleJoke");
        else if (state is EnemyState.Follow) anim.Play("RunJoke");
    }


    public void StopJoke()
    {
        if (!isTellingJoke) return;

        isTellingJoke = false;
        timer = 0;
        StopCoroutine(jokeRoutine);
        jokeContainer.SetActive(false);
        jokeBar.fillAmount = 0;
    }

    private IEnumerator JokeRoutine()
    {
        float timer = 0;
        while (timer < jokeLength)
        {
            timer += Time.deltaTime;
            jokeBar.fillAmount = Mathf.Lerp(0, 1, timer / jokeLength);
            yield return null;
        }

        StopJoke();
        OnJokeFinished.Invoke(this, null);
    }

    #endregion

    public void StartDeathClock()
    {
        if (destroySelfRoutine != null)
        {
            StopCoroutine(destroySelfRoutine);
        }

        destroySelfRoutine = StartCoroutine(DeathRoutine());
    }

    private IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(2);
        Pooler.Despawn(gameObject);
    }

    public void CheckFlip()
    {
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
        Vector3 tempScale = body.localScale;
        tempScale.x *= -1;
        body.localScale = tempScale;
    }
}

public enum EnemyState
{
    Idle,
    Follow,
    Impact,
    Fly,
}
