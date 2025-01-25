using System;
using UnityEngine;
using UnityEngine.Serialization;

public class BirdMovement : MonoBehaviour
{
    [SerializeField]private Transform pos1;
    [SerializeField]private Transform pos2;
    [SerializeField]private float birdspeed;
    
    SpriteRenderer _spriteRenderer;

    private Transform _target;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        

        _target = pos2;
    }

    private void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, _target.position, birdspeed *Time.deltaTime);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == _target.gameObject)
        {
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
            if (_target == pos1) _target = pos2;
            else _target = pos1;
        }
    }
}
