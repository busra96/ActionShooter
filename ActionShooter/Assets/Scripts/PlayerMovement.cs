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
    [SerializeField]private float _turnSpeed;
    private float _speed;
    private float _gravityScale = 9.81f;
    private float verticalVelocity;
  
    private Vector3 movementDirection;
    private Vector2 _moveInput;
  
    private bool _isRunning;

    
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
        ApplyRotation();
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

    private void ApplyRotation()
    {
        Vector3  _lookingDirection = _player.aim.GetMousePosition() - transform.position;
        _lookingDirection.y = 0f;
        _lookingDirection.Normalize();

        Quaternion desiredRotation = Quaternion.LookRotation(_lookingDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, _turnSpeed * Time.deltaTime);
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
