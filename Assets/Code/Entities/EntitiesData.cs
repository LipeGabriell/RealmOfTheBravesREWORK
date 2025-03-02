using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Entities Data", menuName = "Create Entities Database", order = 0)]
public class EntitiesData : ScriptableObject
{
    private Entity[][] entitiesByTypes;

    public Entity[] GetEntityByType(EntityType type)
    {
        return entitiesByTypes[(int)type].ToArray();
    }

    public Entity[] GetEntityByType(EntityType[] type)
    {
        var selectedEntities = new List<Entity>();
        foreach (var entityType in type)
        {
            entitiesByTypes[(int)entityType].ToList().ForEach(entity => selectedEntities.Add(entity));
        }

        return selectedEntities.ToArray();
    }
#if UNITY_EDITOR

    public void UpdateDatabase()
    {
        var filesNames = Directory.GetFiles("Assets/Entities/Enemies", "*.prefab", SearchOption.AllDirectories);
        entitiesByTypes = new Entity[Enum.GetValues(typeof(EntityType)).Length][];

        for (var i = 0; i < Enum.GetValues(typeof(EntityType)).Length; i++) entitiesByTypes[i] = Array.Empty<Entity>();

        foreach (var file in filesNames)
        {
            if (file.Contains("Base")) continue;
            var entity = AssetDatabase.LoadAssetAtPath<Entity>(file.Replace(@"\", "/"));
            Debug.Log(entity);
            entitiesByTypes[(int)entity.Type] = entitiesByTypes[(int)entity.Type].Append(entity).ToArray();
        }
    }
#endif
}