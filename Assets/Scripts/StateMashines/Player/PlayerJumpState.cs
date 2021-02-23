using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        PlayerKeyInput.KeyRightPressed += MoveRight;
        PlayerKeyInput.KeyLeftPressed += MoveLeft;
        PlayerKeyInput.KeyMoveUnpressed += StopMove;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerKeyInput.KeyRightPressed -= MoveRight;
        PlayerKeyInput.KeyLeftPressed -= MoveLeft;
        PlayerKeyInput.KeyMoveUnpressed -= StopMove;
        StopMove();
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}
