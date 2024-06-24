using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerControlls Controlls;

    private void Awake()
    {
        Controlls = new PlayerControlls();
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
