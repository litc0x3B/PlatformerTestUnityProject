using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _jumpForce = 1;
    [SerializeField] private Transform _isGroundedCheckOrigin;
    [SerializeField] private Vector2 _isGroundedCheckBox;


    private ContactFilter2D _contactFilter;
    private Collider2D _collider;
    private Animator _animator;
    private Rigidbody2D _rigidBody;
    private Vector3 _velocity;


    void Start()
    {
        _contactFilter.NoFilter();
        _contactFilter.useTriggers = false;
        IgnoreCollisions ignoreCollisions = GetComponent<IgnoreCollisions>();

        if (ignoreCollisions)
        {
            _contactFilter.layerMask = ignoreCollisions.CollideWithLayers;
            _contactFilter.useLayerMask = true;
        }
        _animator = GetComponentInChildren<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _collider = GetComponentInChildren<Collider2D>();
    }

    public bool PosAlmostEqual(Vector3 pos)
    {
        return (transform.position - pos).sqrMagnitude < Mathf.Pow(Time.fixedDeltaTime * _speed * 2, 2);
    }

    public bool PosAlmostEqual(float posX)
    {
        return Mathf.Abs(transform.position.x - posX) < Time.fixedDeltaTime * _speed * 2;
    }

    public void Walk(float xAxis)
    {
        _velocity.x = xAxis;
        _velocity.x = _speed * _velocity.normalized.x * Time.fixedDeltaTime;
    }

    private void FixedUpdate()
    {
        
        if (_velocity != Vector3.zero)
        {
            RaycastHit2D[] hit = new RaycastHit2D[1];
            if (_collider.Cast(_velocity + Vector3.up * 0.01F, _contactFilter, hit, _velocity.magnitude) == 0)
            {
                _animator.SetBool("IsWalking", true);
                Quaternion rotation = Quaternion.FromToRotation(transform.right, _velocity);
                transform.rotation = transform.rotation * rotation;
                transform.position += _velocity;
                _velocity = Vector3.zero;
            }
 
        }
        else
        {
            _animator.SetBool("IsWalking", false);
        }

        

    }

    public bool Jump()
    {
        Collider2D collider = Physics2D.OverlapArea
            (
                (Vector2)_isGroundedCheckOrigin.position - _isGroundedCheckBox / 2, 
                (Vector2)_isGroundedCheckOrigin.position + _isGroundedCheckBox / 2,
                _contactFilter.layerMask
            );

        if (collider != null)
        {
            _rigidBody.AddForce(_jumpForce * Vector2.up);
            return true;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(_isGroundedCheckOrigin.position, _isGroundedCheckBox);
    }
}