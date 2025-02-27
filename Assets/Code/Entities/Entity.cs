using UnityEngine;

public class Entity : MonoBehaviour
{
    [field: SerializeField] public EntityType Type { get; private set; }
    [field: SerializeField] public int CostValue { get; private set; }
}