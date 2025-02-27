public class EnemyAttack : AttackComponent
{
    protected Entity self;
    protected HealthComponent TargetHealth;

    private void Awake()
    {
        self = GetComponent<Entity>();
    }
}