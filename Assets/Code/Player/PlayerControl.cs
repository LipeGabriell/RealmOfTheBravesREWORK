using UnityEngine;
using UnityEngine.Events;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl Instance { get; private set; }
    public static UnityEvent PlayerDoorInteraction { get; private set; } = new();
    public static bool onDoor = false;
    protected void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        //door usage control
        if (Input.GetKeyDown(KeyCode.E) && onDoor) PlayerDoorInteraction?.Invoke();
        if (Input.GetButtonDown("Fire1")) {
            Debug.Log("attack");
        }
    
    }

}