using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "Dungeons Data", menuName = "Create Dungeons Database", order = 0)]
public class DungeonData : ScriptableObject
{
    [field: SerializeField] public List<Room> RoomPrefabs { get; private set; }
    [field: SerializeField] public List<Room> LeftRooms { get; private set; }
    [field: SerializeField] public List<Room> RightRooms { get; private set; }
    [field: SerializeField] public List<Room> UpRooms { get; private set; }
    [field: SerializeField] public List<Room> DownRooms { get; private set; }

#if UNITY_EDITOR


    public void UpdateDatabase()
    {
        var filesNames = Directory.GetFiles("Assets/Dungeons", "*.prefab", SearchOption.AllDirectories);

        RoomPrefabs = new List<Room>();
        RightRooms = new List<Room>();
        LeftRooms = new List<Room>();
        UpRooms = new List<Room>();
        DownRooms = new List<Room>();

        foreach (var file in filesNames)
        {
            var room = AssetDatabase.LoadAssetAtPath<Room>(file.Replace(@"\", "/"));
            RoomPrefabs.Add(room);

            foreach (var door in room.availableDoors)
                switch (door)
                {
                    case Directions.Up: UpRooms.Add(room); break;
                    case Directions.Down: DownRooms.Add(room); break;
                    case Directions.Left: LeftRooms.Add(room); break;
                    case Directions.Right: RightRooms.Add(room); break;
                }
        }
    }
#endif

    public Room GetRandomRoom(Directions? requiredDoor = null)
    {
        if (!requiredDoor.HasValue) return RoomPrefabs.GetRandomElement();

        return requiredDoor.Value.GetOpositeDoor() switch
        {
            Directions.Up => UpRooms.GetRandomElement(),
            Directions.Down => DownRooms.GetRandomElement(),
            Directions.Left => LeftRooms.GetRandomElement(),
            Directions.Right => RightRooms.GetRandomElement(),
            _ => throw new InvalidOperationException("Porta não é válida, deveria ter retornado no null")
        };
    }

    public Room GetExactRoom(Directions[] directions)
    {
        return RoomPrefabs.Where(room => room.availableDoors.OrderBy(d => d).SequenceEqual(directions.OrderBy(d => d)))
            .ToArray().GetRandomElement();
    }
}