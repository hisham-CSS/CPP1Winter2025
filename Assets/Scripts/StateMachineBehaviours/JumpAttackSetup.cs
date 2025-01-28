using UnityEngine;

public class JumpAttackSetup : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Rigidbody2D rb = animator.gameObject.GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector3.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }
}
