using UnityEngine;

public class DatabaseHandler : MonoBehaviour
{
    [field: SerializeField] public DungeonData DungeonData;
    [field: SerializeField] public EntitiesData EntitiesData;
    public static DatabaseHandler Instance { get; private set; }

    private void Awake()
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