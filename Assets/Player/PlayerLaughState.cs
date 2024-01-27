using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaughState : PlayerState
{
    public PlayerLaughState(PlayerController player, FiniteStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
}
