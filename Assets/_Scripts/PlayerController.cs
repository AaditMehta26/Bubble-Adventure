using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    
    #region [Serialised]Variables
        [SerializeField]float gravity = -9.81f;
        [SerializeField] private Rigidbody bubbleRigidbody; // Reference to the Rigidbody
        [SerializeField] private float maxSqueeze = 0.5f;   // Maximum amount to squeeze/stretch
        [SerializeField] private float squeezeSpeed = 5f;   // Speed of the smooth scaling transition
        [SerializeField] private float minVelocity = 0.1f;
        [SerializeField] private float maxVelocity = 0.5f;
        [SerializeField] float maxTrembleIntensity = 0.1f; 
        [SerializeField] float trembleFrequency = 20f; 
        [SerializeField]private float forceAmount;
        [Range(0f, 1f)]
        [SerializeField]private float slomotime;
    #endregion
    #region Variables
    private InputSystem_Actions _playerActions;
    
    private Rigidbody2D _playerrb;
    
    private Vector3 _originalScale;
    private Vector2 _startPosition;
    private Vector2 _lastPosition;
    private Vector2 _dragvector;
    private Vector3 _originalPosition;
    #endregion

    private void Awake()
    {
        InitComponents();
    }
    

    private void InitComponents()
    {
        _playerActions = new ();
        _playerrb = GetComponent<Rigidbody2D>();
        _playerrb.gravityScale = gravity;
    }

    private void Start()
    {
        _originalPosition = transform.localPosition;
    }

    void Update()
    {
        
        
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
        Time.timeScale = slomotime;
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
        
        _dragvector = _lastPosition - _startPosition;
        Time.timeScale = 1f;
        ApplyForce(_dragvector);
    }

    private void ApplyForce(Vector2 dragVector)
    {
        _playerrb.linearVelocity = Vector2.zero;
        _playerrb.angularVelocity = 0;
        _playerrb.AddForceAtPosition(Vector2.ClampMagnitude(forceAmount * -dragVector,maxVelocity),transform.position, ForceMode2D.Impulse);
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
        Destroy(this.gameObject);
    }
}
