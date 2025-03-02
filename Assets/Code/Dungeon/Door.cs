using System;
using System.Linq;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Directions DoorDirection;
    [SerializeField] private Room thisRoom;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (thisRoom.EnemiesCount > 0) return;

        if (!collision.gameObject.CompareTag("Player")) return;

        PlayerControl.PlayerDoorInteraction.AddListener(GoToNextRoom);
        PlayerControl.onDoor = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        PlayerControl.PlayerDoorInteraction.RemoveAllListeners();
        PlayerControl.onDoor = false;
    }

    private void GoToNextRoom()
    {
        var position = DungeonGeneration.SpawnedRooms.FirstOrDefault(pair => pair.Value == GetComponentInParent<Room>())
            .Key;
        var newPosition = DoorDirection switch
        {
            Directions.Up => DungeonGeneration.SpawnedRooms[position + Vector2Int.up].GetDoorPosition(Directions.Up),
            Directions.Down => DungeonGeneration.SpawnedRooms[position + Vector2Int.down]
                .GetDoorPosition(Directions.Down),
            Directions.Left => DungeonGeneration.SpawnedRooms[position + Vector2Int.left]
                .GetDoorPosition(Directions.Left),
            Directions.Right => DungeonGeneration.SpawnedRooms[position + Vector2Int.right]
                .GetDoorPosition(Directions.Right),
            _ => throw new ArgumentOutOfRangeException("Direction not found")
        };

        PlayerControl.Instance.transform.position = newPosition.position;
    }
}