using System;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    private Player player;
    private PlayerControlls controls;

    [Header("Aim Info")]
    [SerializeField] private Transform _aim;
    [SerializeField] private LayerMask _aimLayerMask;
    private Vector3 _lookingDirection;
    private Vector2 aimInput;

    private void Start()
    {
        player = GetComponent<Player>();
        AssignInputEvents();
    }

    private void Update()
    {
        _aim.position = new Vector3(GetMousePosition().x, transform.position.y + 1, GetMousePosition().z);
    }

    public Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(aimInput);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, _aimLayerMask))
            return hitInfo.point;
        
        return Vector3.zero;
    }
    private void AssignInputEvents()
    {
        controls = player.Controlls;

        controls.Character.Aim.performed += context => aimInput = context.ReadValue<Vector2>();
        controls.Character.Aim.canceled += context => aimInput = Vector2.zero;
    }
}
