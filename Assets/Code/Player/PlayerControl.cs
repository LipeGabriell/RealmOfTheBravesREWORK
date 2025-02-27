using UnityEngine;
using UnityEngine.Events;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl Instance { get; private set; }
    public static UnityEvent PlayerDoorInteraction { get; private set; } = new();
    private PlayerMovement movement;
    public static bool onDoor = false;
    protected void Awake()
    {
        Instance = this;
        movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        //door usage control
        if (Input.GetKeyDown(KeyCode.E) && onDoor) PlayerDoorInteraction?.Invoke();
        if (Input.GetButtonDown("Fire1")) PlayerDoorInteraction?.Invoke();
        
        var pos = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        movement.Move(pos);
    }

}