using UnityEngine;
using UnityEngine.Events;

public class PlayerControl : MonoBehaviour
{
    public static bool onDoor = false;
    public static PlayerControl Instance { get; private set; }
    public static UnityEvent PlayerDoorInteraction { get; } = new();

    protected void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        //door usage control
        if (Input.GetKeyDown(KeyCode.E) && onDoor) PlayerDoorInteraction?.Invoke();
        if (Input.GetButtonDown("Fire1")) Debug.Log("attack");
    }
}