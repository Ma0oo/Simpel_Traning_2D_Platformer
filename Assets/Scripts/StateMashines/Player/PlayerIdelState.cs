using UnityEngine;

public class PlayerIdelState : PlayerBaseState
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        if (PlayerKeyInput)
        {
            PlayerKeyInput.KeyRightPressed += MoveRight;
            PlayerKeyInput.KeyLeftPressed += MoveLeft;
            PlayerKeyInput.KeysMoveUnpressed += StopMoveX;
            PlayerKeyInput.KeyJumptPressed += Jump;
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (PlayerKeyInput)
        {
            PlayerKeyInput.KeyRightPressed -= MoveRight;
            PlayerKeyInput.KeyLeftPressed -= MoveLeft;
            PlayerKeyInput.KeysMoveUnpressed -= StopMoveX;
            PlayerKeyInput.KeyJumptPressed -= Jump;
        }
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}