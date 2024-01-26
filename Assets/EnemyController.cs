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
    public GameObject jokeContainer;
    [FoldoutGroup("Joke")]
    public Image jokeBar;
    [FoldoutGroup("Joke")]
    public CustomGameEvent OnJokeFinished;
    private Coroutine jokeRoutine;
    #endregion

    private bool isTellingJoke;
    private Coroutine destroySelfRoutine;

    private bool isFacingRight = true;
    private bool IsPlayerToMyRight => playerPos.value.x > transform.position.x;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        physics = GetComponent<PhysicsApplier>();
    }
    private void OnEnable()
    {
        state = EnemyState.Idle;
        GoToFollow();
        destroySelfRoutine = null;
        isTellingJoke = false;
        jokeRoutine = null;
    }

    private void Update()
    {
        if (state is EnemyState.Idle || state is EnemyState.Follow)
        {
            CheckFlip();
        }

        if (state is EnemyState.Follow)
        {
            float step = speed * Time.deltaTime;

            // move sprite towards the target location
            transform.position = Vector2.MoveTowards(transform.position, playerPos.value, step);
        }
    }

    #region States
    public void GoToIdle()
    {
        if (state is EnemyState.Impact or EnemyState.Fly) return;

        state = EnemyState.Idle;

        if (!isTellingJoke)
        {
            anim.Play("Idle");
        } 
        
        else
        {
            anim.Play("IdleJoke");
        }

        physics.FreezeRigidbody();
    }

    public void GoToFollow()
    {
        if (state is EnemyState.Impact or EnemyState.Fly) return;
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
    }


    public void StopJoke()
    {
        if (jokeRoutine != null)
        {
            StopCoroutine(jokeRoutine);
            jokeContainer.SetActive(false);
        }
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
