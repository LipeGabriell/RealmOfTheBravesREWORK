using UnityEngine;

public class SlimeAttack : EnemyAttack
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Attacking", gameObject);
            TargetHealth ??= self.Target.GetComponent<HealthComponent>();
            Attack(TargetHealth);
        }
    }

    public override void Attack(HealthComponent target)
    {
        var withCrit = Random.Range(1, 101) < critChance;
        target.TakeDamage(!withCrit ? damage : damage + Mathf.CeilToInt(damage * critChance));
    }
}