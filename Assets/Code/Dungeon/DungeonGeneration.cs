using System;
using System.Collections.Generic;
using System.Linq;
using NavMeshPlus.Components;
using UnityEngine;

public class DungeonGeneration : MonoBehaviour
{
    private const int ROOM_SPACING = 20;
    public static readonly Dictionary<Vector2Int, Room> SpawnedRooms = new();
    [SerializeField] private int minRoomCount;
    [SerializeField] private int maxRoomCount;

    private void Start()
    {
        var initialRoom = InstantiateRoom(Vector2Int.zero);

        GenerateNewRoom(Vector2Int.zero, initialRoom.availableDoors);

        if (SpawnedRooms.Count < minRoomCount)
        {
            FindObjectsByType<Room>(FindObjectsInactive.Include, FindObjectsSortMode.None)
                .ToList()
                .ForEach(Destroy);
            SpawnedRooms.Clear();
            Start();
            return;
        }

        FixRooms();

        // GetComponent<Unity.AI.Navigation.NavMeshSurface>().BuildNavMesh();
        // GetComponent<NavMeshSurface>().BuildNavMesh();

        foreach (var room in SpawnedRooms.Values.Skip(1).SkipLast(1)) room.Spawn();
    }

    private void GenerateNewRoom(Vector2Int oldID, Directions[] doors)
    {
        foreach (var direction in doors)
        {
            if (SpawnedRooms.Count > maxRoomCount) return;

            var nextPosition = direction switch
            {
                Directions.Up => Vector2Int.up,
                Directions.Down => Vector2Int.down,
                Directions.Left => Vector2Int.left,
                Directions.Right => Vector2Int.right,
                _ => throw new Exception("Porta não definida")
            };

            var newID = oldID + nextPosition;

            if (SpawnedRooms.ContainsKey(newID)) continue;

            var newRoom = InstantiateRoom(newID, direction);
            GenerateNewRoom(newID, newRoom.availableDoors);
        }
    }

    private void FixRooms()
    {
        foreach (var position in SpawnedRooms.Keys.ToList())
        {
            var currentRoom = SpawnedRooms[position];

            var validDoors = new List<Directions>();

            if (SpawnedRooms.ContainsKey(position + Vector2Int.up)) validDoors.Add(Directions.Up);
            if (SpawnedRooms.ContainsKey(position + Vector2Int.down)) validDoors.Add(Directions.Down);
            if (SpawnedRooms.ContainsKey(position + Vector2Int.left)) validDoors.Add(Directions.Left);
            if (SpawnedRooms.ContainsKey(position + Vector2Int.right)) validDoors.Add(Directions.Right);

            if (currentRoom.availableDoors.OrderBy(d => d).SequenceEqual(validDoors.OrderBy(d => d)))
                continue;

            var correctRoom = DatabaseHandler.Instance.DungeonData.GetExactRoom(validDoors.ToArray());

            if (correctRoom == null) continue;

            Destroy(currentRoom.gameObject);
            var newRoom = Instantiate(correctRoom, currentRoom.transform.position, Quaternion.identity, transform);
            SpawnedRooms[position] = newRoom;
        }
    }

    private Room InstantiateRoom(Vector2Int id, Directions? neededDoor = null)
    {
        var newRoom =
            Instantiate(
                DatabaseHandler.Instance.DungeonData.GetRandomRoom(neededDoor),
                new Vector2(id.x * ROOM_SPACING, id.y * ROOM_SPACING), Quaternion.identity, transform);
        SpawnedRooms.Add(id, newRoom);
        return newRoom;
    }
}