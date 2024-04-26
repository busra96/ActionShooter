using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControlls _controls;
    private CharacterController _characterController;

    [Header("Movement Info")]
    [SerializeField]private float _walkSpeed;
    private Vector3 movementDirection;
    private float _gravityScale = 9.81f;
    private float verticalVelocity;

    [Header("Aim Info")]
    [SerializeField] private Transform _aim;
    [SerializeField] private LayerMask _aimLayerMask;
    private Vector3 _lookingDirection;

    private Vector2 _moveInput;
    private Vector2 _aimInput;

    private void Awake()
    {
        _controls = new PlayerControlls();

        _controls.Character.Movement.performed += context => _moveInput = context.ReadValue<Vector2>();
        _controls.Character.Movement.canceled += context => _moveInput = Vector2.zero;

        _controls.Character.Aim.performed += context => _aimInput = context.ReadValue<Vector2>();
        _controls.Character.Aim.canceled += context => _aimInput = Vector2.zero;

    }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        ApplyMovement();

        AimTowardMouse();
    }

    private void AimTowardMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(_aimInput);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, _aimLayerMask))
        {
            _lookingDirection = hitInfo.point - transform.position;
            _lookingDirection.y = 0f;
            _lookingDirection.Normalize();

            transform.forward = _lookingDirection;

            _aim.position = new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z);
        }
    }

    private void ApplyMovement()
    {
        movementDirection = new Vector3(_moveInput.x, 0, _moveInput.y);
        ApplyGravity();

        if (movementDirection.magnitude > 0)
        {
            _characterController.Move(movementDirection * Time.deltaTime * _walkSpeed);
        }
    }

    private void ApplyGravity()
    {
        if (!_characterController.isGrounded)
        {
            verticalVelocity -= _gravityScale * Time.deltaTime;
            movementDirection.y = verticalVelocity;
        }
        else
            verticalVelocity = -.5f;

    }

    private void OnEnable()
    {
        _controls.Enable();
    }
    private void OnDisable()
    {
        _controls.Disable();
    }
}
