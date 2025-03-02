using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    [SerializeField] protected float baseSpeed;

    protected virtual void Move(Vector2 position)
    {
    }
}