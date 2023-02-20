using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    [SerializeField] private float _movementSpeed = 5, _smoothRotation = 0.2f;
    private Vector2 _movement = Vector2.zero;
    private Vector3 rotVelocity;
    private Animator _animator;
    private Rigidbody _rigidbody;
    [SerializeField] private SpringArm _springArm;
    public bool canRotate = true;
    public bool canMove = true;

    [HideInInspector] public Vector3 topPosition;
    [HideInInspector] public Vector3 botPosition;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 cameraDir = Camera.main.transform.forward;
        cameraDir.y = 0f;
        if(canRotate)
        {
            if (_springArm.cameraStatus == CameraStatus.FirstPerson)
            {
                transform.forward = cameraDir;
            }
            else
            {
                transform.forward = Vector3.SmoothDamp(transform.forward, cameraDir, ref rotVelocity, _smoothRotation);
            }
        }

        if (canMove)
        {
            Vector3 movement = new Vector3(_movement.x, 0, _movement.y);
            moveCharacter(movement);
        }
        else
        {
            LadderMovement();
        }
        
        

    }

    //Values are already normalized through the new Input System
    public void Move(InputAction.CallbackContext ctx)
    {
        if (PlayerInteractionAnim.AnimationInProgress)
        {
            _movement = Vector2.zero;
            return;
        }
        _movement = ctx.ReadValue<Vector2>();
        _animator.SetFloat("Speed", _movement.sqrMagnitude == 0 ? 0 : 2);
    }

    void moveCharacter(Vector3 direction)
    {
        // Convert direction into Rigidbody space.
        direction = _rigidbody.rotation * direction;

        _rigidbody.MovePosition(_rigidbody.position + direction * _movementSpeed * Time.fixedDeltaTime);
    }

    void LadderMovement()
    {
        if (_movement.y > 0)
        {
            Vector3 dir = (topPosition - transform.position).normalized;
            _rigidbody.MovePosition(_rigidbody.position + dir * _movementSpeed * Time.fixedDeltaTime);
        }
        else if (_movement.y < 0)
        {
            Vector3 dir = (botPosition - transform.position).normalized;
            _rigidbody.MovePosition(_rigidbody.position + dir * _movementSpeed * Time.fixedDeltaTime);
        }
    }
}
