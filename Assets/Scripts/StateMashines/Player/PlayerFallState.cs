using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        PlayerKeyInput.KeysMoveUnpressed += StopMoveX;
        PlayerKeyInput.KeyRightPressed += MoveRight;
        PlayerKeyInput.KeyLeftPressed += MoveLeft;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerKeyInput.KeysMoveUnpressed -= StopMoveX;
        PlayerKeyInput.KeyRightPressed -= MoveRight;
        PlayerKeyInput.KeyLeftPressed -= MoveLeft;
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}
