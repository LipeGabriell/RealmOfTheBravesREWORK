
using System.Collections;
using UnityEngine;

public class SlimeMovement : MovementComponent
{

    private bool canJump = true;
    [SerializeField] private float jumpCooldown = 2f;
    protected override void Move(Vector2 position)
    {
        // if (Target != null && canJump)
        // {
        //     StartCoroutine(JumpCooldown());
        //     Brain.RigidBody.linearVelocity = Vector3.zero;
        //     Brain.RigidBody.AddForce((.position - transform.position).normalized * baseSpeed, ForceMode2D.Force);
        // }
    }

    private IEnumerator JumpCooldown()
    {
        canJump = false;
        yield return new WaitForSeconds(jumpCooldown);
        canJump = true;
    }
}
