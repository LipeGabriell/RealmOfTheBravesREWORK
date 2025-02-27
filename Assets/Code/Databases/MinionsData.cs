using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Entities Data", menuName = "Create Entities Database", order = 0)]
public class EntitiesData : ScriptableObject
{
    [SerializeField] private Entity[][] EntitiesByTypes;
    public Entity[] GetEntityByType(EntityType type)
    {
        return EntitiesByTypes[(int)type].ToArray();
    }
#if UNITY_EDITOR

    public void UpdateDatabase()
    {
        var filesNames = Directory.GetFiles("Assets/Entities/Enemies", "*.prefab", SearchOption.AllDirectories);
        EntitiesByTypes = new Entity[Enum.GetValues(typeof(EntityType)).Length][];
        
        for (int i = 0; i < Enum.GetValues(typeof(EntityType)).Length; i++)
        {
            EntitiesByTypes[i] = new Entity[0];
        }

        foreach (var file in filesNames)
        {
            if (file.Contains("Base")) continue;
            var Entity = AssetDatabase.LoadAssetAtPath<Entity>(file.Replace(@"\", "/"));
            EntitiesByTypes[(int)Entity.Type] = EntitiesByTypes[(int)Entity.Type].Append(Entity).ToArray();

        }
    }
#endif 

}



