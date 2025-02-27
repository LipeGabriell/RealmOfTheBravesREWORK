using UnityEngine;

public class EnemyMovement : MovementComponent
{

    private Entity self;

    void Awake()
    {
        self = GetComponent<Entity>();
    }

    protected override void Move(Vector2 position)
    {

        transform.position = Vector2.MoveTowards(transform.position, position, baseSpeed * Time.deltaTime);

    }
    private void Update()
    {
        if (self.Target != null)
            Move(self.Target.transform.position);
    }
}