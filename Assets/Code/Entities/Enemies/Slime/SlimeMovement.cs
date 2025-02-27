
using System.Collections;
using UnityEngine;

public class SlimeMovement : MovementComponent
{
    private Rigidbody2D rb;
    private bool canJump = true;
    [SerializeField] private float jumpCooldown = 2f;
    private float jumpTimer = 0;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    protected override void Move(Vector2 position)
    {
        if (canJump)
        {
            StartCoroutine(JumpCooldown());
            rb.linearVelocity = Vector3.zero;
            rb.AddForce((position - (Vector2)transform.position).normalized * baseSpeed, ForceMode2D.Force);
        }
    }

    private void Update()
    {
        if (canJump) return;
        jumpTimer += Time.deltaTime;
        if (jumpTimer >= jumpCooldown) canJump = true;
    }

    private IEnumerator JumpCooldown()
    {
        canJump = false;
        yield return new WaitForSeconds(jumpCooldown);
        canJump = true;
    }
}
