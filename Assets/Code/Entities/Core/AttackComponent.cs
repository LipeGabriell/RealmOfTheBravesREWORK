using System.Threading;
using System.Collections;
using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    private float attackTimer = 0;
    [SerializeField] protected int damage;
    [SerializeField] protected int critChance;
    [SerializeField] protected float critDamage;
    [SerializeField] protected float attackCooldown;
    private bool canAttack = true;
    public int Damage => Random.Range(0, 101) > critChance ? damage : damage + Mathf.CeilToInt(damage * critDamage);
    private void Update()
    {
        if (canAttack) return;
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackCooldown) canAttack = true;
    }

    public virtual void Attack()
    {
        canAttack = false;
    }



}