using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Player _player;
    private PlayerControlls _controls;
    private CharacterController _characterController;
    private Animator _animator;

    [Header("Movement Info")] 
    [SerializeField]private float _walkSpeed;
    [SerializeField]private float _runSpeed;
    private float _speed;
    private Vector3 movementDirection;
    private float _gravityScale = 9.81f;
    private float verticalVelocity;
    private bool _isRunning;

    [Header("Aim Info")]
    [SerializeField] private Transform _aim;
    [SerializeField] private LayerMask _aimLayerMask;
    private Vector3 _lookingDirection;

    private Vector2 _moveInput;
    private Vector2 _aimInput;
    
    private void Start()
    {
        _player = GetComponent<Player>();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();

        _speed = _walkSpeed;
        
        AssignInputEvents();
    }

    private void Update()
    {
        ApplyMovement();

        AimTowardMouse();

        AnimController();
    }

    private void AnimController()
    {
        float xVelocity = Vector3.Dot(movementDirection.normalized, transform.right);
        float zVelocity = Vector3.Dot(movementDirection.normalized, transform.forward);
        
        _animator.SetFloat("xVelocity", xVelocity, .1f, Time.deltaTime);
        _animator.SetFloat("zVelocity", zVelocity, .1f, Time.deltaTime);

        bool playRunAnimation = _isRunning && movementDirection.magnitude > 0;
        _animator.SetBool("isRunning", playRunAnimation);
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

            _aim.position = new Vector3(hitInfo.point.x, transform.position.y + 1, hitInfo.point.z);
        }
    }

    private void ApplyMovement()
    {
        movementDirection = new Vector3(_moveInput.x, 0, _moveInput.y);
        ApplyGravity();

        if (movementDirection.magnitude > 0)
        {
            _characterController.Move(movementDirection * Time.deltaTime * _speed);
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

    #region New Input System
        private void AssignInputEvents()
        {

            _controls = _player.Controlls;

            _controls.Character.Movement.performed += context => _moveInput = context.ReadValue<Vector2>();
            _controls.Character.Movement.canceled += context => _moveInput = Vector2.zero;

            _controls.Character.Aim.performed += context => _aimInput = context.ReadValue<Vector2>();
            _controls.Character.Aim.canceled += context => _aimInput = Vector2.zero;

            _controls.Character.Run.performed += context =>
            {
                _isRunning = true;
                _speed = _runSpeed;
            };
            _controls.Character.Run.canceled += context =>
            {
                _isRunning = true;
                _speed = _walkSpeed;
            };
        }
        
    #endregion
}
