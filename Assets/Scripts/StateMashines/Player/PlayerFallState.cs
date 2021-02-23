using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        PlayerKeyInput.KeyMoveUnpressed += StopMove;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerKeyInput.KeyMoveUnpressed -= StopMove;
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}
