using UnityEngine;

[CreateAssetMenu(fileName = "Animation", menuName = "Entity Animation", order = 0)]
public class EntityAnimation : ScriptableObject
{
    [field: SerializeField] public Sprite sprite;
}