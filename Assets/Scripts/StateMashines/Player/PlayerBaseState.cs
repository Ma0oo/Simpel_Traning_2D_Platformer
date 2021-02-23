using UnityEngine;

public abstract class PlayerBaseState : StateMachineBehaviour
{
    protected PlayerInput PlayerKeyInput;
    protected Mover PlayerMover;

    private Animator _animator;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _animator = animator;
        PlayerKeyInput = animator.GetComponent<PlayerInput>();
        PlayerMover = animator.GetComponent<Mover>();
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }

    protected void MoveRight()
    {
        PlayerMover.SetDiractionMove(PlayerMover.transform.right);
    }

    protected void MoveLeft()
    {
        PlayerMover.SetDiractionMove(PlayerMover.transform.right * -1);
    }

    protected void StopMove()
    {
        PlayerMover.SetDiractionMove(Vector2.zero);
    }
    
    protected void SetJumpVar()
    {
        _animator.SetTrigger("Jump");
    }
}
