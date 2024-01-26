using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class PlayerState 
{
    protected PlayerController player;
    protected FiniteStateMachine stateMachine;
    protected PlayerData playerData;
    protected string animBoolName;

    protected float startTime;
    protected bool isAnimationFinished;
    protected bool isExitingState;

    public PlayerState(PlayerController player, FiniteStateMachine stateMachine, PlayerData playerData, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
        this.playerData = playerData;
    }

    public virtual void DoChecks()
    {
    }

    public virtual void Enter()
    {
        player.Anim.SetBool(animBoolName, true);
        startTime = Time.time;
        DoChecks();
        //Debug.Log("Hello from " + animBoolName);
        isAnimationFinished = false;
        isExitingState = false;
    }

    public virtual void Exit() 
    { 
        player.Anim.SetBool(animBoolName, false);
        isExitingState = true;
    }
    public virtual void LogicUpdate() { }
    public virtual void PhysicsUpdate() 
    {
        DoChecks();
    }

    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;

}
