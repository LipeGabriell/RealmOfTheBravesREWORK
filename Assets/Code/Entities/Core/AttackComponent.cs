using System.Collections;
using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    [SerializeField] protected int damage;
    [SerializeField] protected int critChance;
    [SerializeField] protected float critDamage;
    [SerializeField] protected float attacksPerSecond;
    private bool canAttack = false;
    public int Damage => Random.Range(0, 101) > critChance ? damage : damage + Mathf.CeilToInt(damage * critDamage);
    void Awake()
    {
        StartCoroutine(AttackCooldown());
    }

    public virtual void Attack()
    {
        StartCoroutine(AttackCooldown());
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(60 / attacksPerSecond / 60);
        canAttack = true;
    }

}