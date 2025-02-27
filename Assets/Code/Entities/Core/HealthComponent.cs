using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] protected int maxHealth;
    protected int health;
    private void Awake()
    {
        health = maxHealth;
    }

    private void Start()
    {

    }

    private void Update() { }

    public virtual void TakeDamage(int damage)
    {
        if (health == 0) return;

        health = Mathf.Max(0, health - damage);

        if (health == 0) Die();
    }

    protected virtual void Die() { }
}