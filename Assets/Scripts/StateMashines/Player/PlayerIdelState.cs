using UnityEngine;

public class PlayerIdelState : PlayerBaseState
{
    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        base.OnStateMachineEnter(animator, stateMachinePathHash);
    }
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        if (PlayerKeyInput)
        {
            PlayerKeyInput.KeyRightPressed += MoveRight;
            PlayerKeyInput.KeyLeftPressed += MoveLeft;
            PlayerKeyInput.KeyMoveUnpressed += StopMove;
            PlayerKeyInput.KeyJumptPressed += PlayerMover.Jump;
            PlayerKeyInput.KeyJumptPressed += SetJumpVar;
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (PlayerKeyInput)
        {
            PlayerKeyInput.KeyRightPressed -= MoveRight;
            PlayerKeyInput.KeyLeftPressed -= MoveLeft;
            PlayerKeyInput.KeyMoveUnpressed -= StopMove;
            PlayerKeyInput.KeyJumptPressed -= PlayerMover.Jump;
            PlayerKeyInput.KeyJumptPressed -= SetJumpVar;
        }
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}