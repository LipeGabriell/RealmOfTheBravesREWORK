using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Room : MonoBehaviour
{
    public readonly UnityEvent<GameObject> OnPlayerEnter = new();
    public readonly UnityEvent<GameObject> OnPlayerLeft = new();

    private void Awake()
    {
        OnPlayerEnter.AddListener(player => roomCamera.gameObject.SetActive(true));
        OnPlayerLeft.AddListener(player => roomCamera.gameObject.SetActive(false));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            OnPlayerEnter?.Invoke(collision.gameObject);
        else if (collision.gameObject.CompareTag("Enemy")) EnemiesCount++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            OnPlayerLeft?.Invoke(collision.gameObject);
        else if (collision.gameObject.CompareTag("Enemy")) EnemiesCount--;
    }

    public Transform GetDoorPosition(Directions direction)
    {
        var correctDirection = direction.GetOpositeDoor();

        return correctDirection switch
        {
            Directions.Up => upDoor.transform,
            Directions.Down => downDoor.transform,
            Directions.Left => leftDoor.transform,
            Directions.Right => rightDoor.transform,
            _ => throw new ArgumentOutOfRangeException("Direction not found")
        };
    }

    public void Spawn()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        var availableEnemies = DatabaseHandler.Instance.EntitiesData.GetEntityByType(EnemiesType).ToList();

        do
        {
            if (availableEnemies.Count == 0) break;

            var selectedEnemy = availableEnemies.GetRandomElement();
            if (RoomEnemiesCost - selectedEnemy.CostValue < 0 || selectedEnemy.CostValue > MaxEnemyCost ||
                selectedEnemy.CostValue < MinEnemyCost)
            {
                availableEnemies.Remove(selectedEnemy);
                continue;
            }

            var enemy = Instantiate(selectedEnemy, transform.position, Quaternion.identity, transform);
            Debug.Log("spawned enemy: ",enemy);
            RoomEnemiesCost -= enemy.CostValue;

            OnPlayerEnter.AddListener(player =>
            {
                if (enemy) enemy.Target = player;
            });


            yield return null;
        } while (RoomEnemiesCost > 0 || availableEnemies.Count > 0);
    }


    public void SpawnBoss()
    {
    }

    #region Room Enemies

    [Header("Room Enemies")]
    [field: SerializeField]
    public int RoomEnemiesCost { get; private set; }

    [field: SerializeField] public int MaxEnemyCost { get; private set; }
    [field: SerializeField] public int MinEnemyCost { get; private set; }
    [field: SerializeField] public EntityType[] EnemiesType { get; private set; }
    public int EnemiesCount { get; private set; }

    #endregion

    #region Room Stuffs

    [Header("Room Parts")]
    [field: SerializeField]
    public Type RoomType { get; private set; }

    [field: SerializeField] public Directions[] availableDoors { get; private set; }
    [SerializeField] private Camera roomCamera;

    [SerializeField] private Transform upDoor, downDoor, leftDoor, rightDoor;

    #endregion
}