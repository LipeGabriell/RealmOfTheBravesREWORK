using UnityEngine;

public class SlimeAttack : AttackComponent
{
    private HealthComponent TargetHealth;
    public override void Attack()
    {
        var withCrit = Random.Range(1, 101) < critChance;
        TargetHealth.TakeDamage(!withCrit ? damage : damage + Mathf.CeilToInt(damage * critChance));
        TargetHealth = null;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Attacking", gameObject);
            TargetHealth = collision.gameObject.GetComponent<HealthComponent>();
            Attack();
        }
    }
}
