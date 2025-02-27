using UnityEngine;

public class PlayerMovement : MovementComponent
{
    public new void Move(Vector2 position)
    {
        transform.position = transform.position + baseSpeed * Time.deltaTime * (Vector3)position;
    }

    void Update()
    {
        var pos = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Move(pos);
    }

}