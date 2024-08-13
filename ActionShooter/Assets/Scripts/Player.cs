using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerControlls Controlls { get; private set; }
    public PlayerAim aim { get; private set; }

    private void Awake()
    {
        Controlls = new PlayerControlls();
        aim = GetComponent<PlayerAim>();
    }
    
    private void OnEnable()
    {
        Controlls.Enable();
    }
    private void OnDisable()
    {
        Controlls.Disable();
    }
}
