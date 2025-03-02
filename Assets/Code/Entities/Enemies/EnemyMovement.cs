using UnityEngine;

public class EnemyMovement : MovementComponent
{
    private Entity self;

    private void Awake()
    {
        self = GetComponent<Entity>();
    }

    private void Update()
    {
        if (self.Target)
            Move(self.Target.transform.position);
    }

    protected override void Move(Vector2 position)
    {
        transform.position = Vector2.MoveTowards(transform.position, position, baseSpeed * Time.deltaTime);
    }
}