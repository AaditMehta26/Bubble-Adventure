using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;


public class PlayerController : MonoBehaviour
{
    
    #region [Serialised]Variables
        [SerializeField]float gravity = -9.81f;
        [SerializeField]private ParticleSystem deathParticles;
        [SerializeField] private float maxVelocity = 0.5f;
        [SerializeField]private float forceAmount;
        [Range(0f, 1f)]
        [SerializeField]private float slomoTime;
    #endregion
    #region Variables

    private InputSystem_Actions _playerActions;
    private Rigidbody2D _playerRb;
    private Vector3 _originalScale;
    private Vector2 _startPosition;
    private Vector2 _lastPosition;
    private Vector2 _dragVector;
    private Vector3 _originalPosition;
    #endregion

    private void Awake()
    {
        InitComponents();
    }
    

    private void InitComponents()
    {
        _playerActions = new ();
        _playerRb = GetComponent<Rigidbody2D>();
        _playerRb.gravityScale = gravity;
    }

    private void Start()
    {
        _originalPosition = transform.localPosition;
    }
    

    private void OnEnable()
    {
        _playerActions.Player.Enable();
        _playerActions.Player.Press.started += OnPress;
        
        _playerActions.Player.Press.canceled += OnLift;
    }
    private void OnPress(InputAction.CallbackContext obj)
    {
        _startPosition = Mouse.current.position.ReadValue();
        print("Pressed" + _startPosition);
        _playerActions.Player.Look.performed += OnDrag;
        Time.timeScale = slomoTime;
    }
    private void OnDrag(InputAction.CallbackContext obj)
    {
        _lastPosition = Mouse.current.position.ReadValue();
        print("dragging" + _lastPosition);
    }
    private void OnLift(InputAction.CallbackContext obj)
    {
        _lastPosition = Mouse.current.position.ReadValue();
        print("Lifted" + _lastPosition);
        _playerActions.Player.Look.performed -= OnDrag;
        
        _dragVector = _lastPosition - _startPosition;
        Time.timeScale = 1f;
        ApplyForce(_dragVector);
    }

    private void ApplyForce(Vector2 dragVector)
    {
        _playerRb.linearVelocity = Vector2.zero;
        _playerRb.angularVelocity = 0;
        _playerRb.AddForceAtPosition(Vector2.ClampMagnitude(forceAmount * -dragVector,maxVelocity),transform.position, ForceMode2D.Impulse);
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Burst();
        }
    }

    void Burst()
    {
        Instantiate(deathParticles,transform.position,Quaternion.identity).Play();
        Destroy(gameObject);
    }
}
