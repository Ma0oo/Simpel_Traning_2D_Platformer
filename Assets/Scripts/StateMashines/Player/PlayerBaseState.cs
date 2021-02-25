using UnityEngine;

public abstract class PlayerBaseState : StateMachineBehaviour
{
    protected PlayerInput PlayerKeyInput;
    protected MoverByRigibody PlayerMover;

    private Animator _animator;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _animator = animator;
        PlayerKeyInput = animator.GetComponent<PlayerInput>();
        PlayerMover = animator.GetComponent<MoverByRigibody>();
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }

    protected void MoveRight()
    {
        PlayerMover.SetDiractionMoveX(1);
    }

    protected void MoveLeft()
    {
        PlayerMover.SetDiractionMoveX(-1);
    }

    protected void StopMoveX()
    {
        PlayerMover.SetVelocity(new Vector2(0, PlayerMover.Velocity.y));
    }
    
    protected void Jump()
    {
        PlayerMover.Jump();
        _animator.SetTrigger("Jump");
    }
}
