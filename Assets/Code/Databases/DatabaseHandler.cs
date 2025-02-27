using UnityEngine;

public class DatabaseHandler : MonoBehaviour
{
    public static DatabaseHandler Instance { get; private set; }
    [field: SerializeField] public DungeonData DungeonData;
    [field: SerializeField] public EntitiesData EntitiesData;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

#if UNITY_EDITOR
        DungeonData.UpdateDatabase();
        EntitiesData.UpdateDatabase();
#endif

    }
}