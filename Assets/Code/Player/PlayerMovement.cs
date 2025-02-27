using UnityEngine;

public class PlayerMovement : MovementComponent
{
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public new void Move(Vector2 position)
    {
        rb.MovePosition(transform.position + baseSpeed * Time.deltaTime * new Vector3(position.x, position.y, 0).normalized);
    }

}