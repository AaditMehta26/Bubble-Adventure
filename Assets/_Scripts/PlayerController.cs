using System.Collections;
using _Scripts.Manager;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    
    #region [Serialised]fields
        //references
        [SerializeField]private ParticleSystem deathParticles;
        [SerializeField] private Transform indicator;
        //variables
        [SerializeField]float gravity = -9.81f;
        [SerializeField] private float maxVelocity = 0.5f;
        [SerializeField]private float forceAmount;
        [Range(0f, 1f)]
        [SerializeField]private float slomoTime;

        [SerializeField] private float rotationSpeed;
    #endregion
    #region Fields
    //references
    private InputSystem_Actions _playerActions;
    private Rigidbody2D _playerRb;
    //variables
    private Vector3 _originalScale;
    private Vector2 _startPosition;
    private Vector2 _lastPosition;
    private Vector2 _dragVector;
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


    private void OnEnable()
    {
        _playerActions.Player.Enable();
        _playerActions.Player.Press.started += OnPress;
        
        _playerActions.Player.Press.canceled += OnLift;
    }
    private void Update()
    {
        HandleIndicator();
    }
    private void OnPress(InputAction.CallbackContext obj)
    {
        _startPosition = Mouse.current.position.ReadValue();
        _playerActions.Player.Look.performed += OnDrag;
        Time.timeScale = slomoTime;
        
        indicator.gameObject.SetActive(true);
    }
    private void OnDrag(InputAction.CallbackContext obj)
    {
        _lastPosition = Mouse.current.position.ReadValue();
        _dragVector = _lastPosition - _startPosition;
    }

    private void HandleIndicator()
    {
        if (_dragVector.sqrMagnitude > 0)
        {
            float angle = Mathf.Atan2(_dragVector.x, -_dragVector.y) * Mathf.Rad2Deg;
            
            var strength = _dragVector.magnitude/80;
            var direction =- _dragVector.normalized;

            indicator.transform.position =  Vector3.ClampMagnitude((Vector2)transform.position + direction * strength, maxVelocity);
            indicator.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void OnLift(InputAction.CallbackContext obj)
    {
        _lastPosition = Mouse.current.position.ReadValue();
        _playerActions.Player.Look.performed -= OnDrag;
        
        _dragVector = _lastPosition - _startPosition;
        Time.timeScale = 1f;
        ApplyForce(_dragVector);
        
        indicator.gameObject.SetActive(false);

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
            StartCoroutine(Burst());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            SceneController.Instance.NextLevel();
        }
        
        else if (collision.gameObject.CompareTag("Fan"))
        {
            
        }
    }

    IEnumerator Burst()
    {
        var particle =Instantiate(deathParticles,transform.position,Quaternion.identity);
        particle.gameObject.SetActive(true);
        particle.Play();
        
        SceneController.Instance.ReLoadScene();
        Destroy(gameObject,0.1f);
        yield return new WaitForSeconds(particle.main.duration);
    }

    private void OnDisable()
    {
        _playerActions.Player.Disable();
        _playerActions.Player.Press.started -= OnPress;

        _playerActions.Player.Press.canceled -= OnLift;
    }
}
