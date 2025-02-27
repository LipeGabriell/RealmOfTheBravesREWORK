// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Linq;
// using UnityEditor;
// using UnityEngine;

// [CreateAssetMenu(fileName = "Entities Data", menuName = "Create Entities Database", order = 0)]
// public class EntitiesData : ScriptableObject
// {
//     [SerializeField] private EntityBrainArray[] EntitiesByTypes;
//     public EntityBrain[] GetEntityByType(EntityType type)
//     {
//         return EntitiesByTypes[(int)type].Entities.ToArray();
//     }
// #if UNITY_EDITOR

//     public void UpdateDatabase()
//     {
//         var filesNames = Directory.GetFiles("Assets/Entities/Enemies", "*.prefab", SearchOption.AllDirectories);
//         EntitiesByTypes = new EntityBrainArray[Enum.GetValues(typeof(EntityType)).Length];

//         foreach (var file in filesNames)
//         {
//             if (file.Contains("Base")) continue;
//             var Entity = AssetDatabase.LoadAssetAtPath<EntityBrain>(file.Replace(@"\", "/"));

//             EntitiesByTypes[(int)Entity.Type] ??= new();
//             EntitiesByTypes[(int)Entity.Type].AddEntity(Entity);
//         }
//     }
// #endif 

// }

// [Serializable]
// public class EntityBrainArray
// {
//     [field: SerializeField] public List<EntityBrain> Entities { get; private set; }

//     public void AddEntity(EntityBrain Entity)
//     {
//         Entities ??= new();
//         Entities.Add(Entity);
//     }
// }

