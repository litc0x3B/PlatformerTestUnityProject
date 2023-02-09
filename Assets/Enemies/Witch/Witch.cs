using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BasicMovement))]
public class Witch : MonoBehaviour, IHealth
{
    [SerializeField] private float _hp = 10;
    [SerializeField] private float _visionDistance = 10;
    [SerializeField] private float _maxAttackDistance = 8;
    [SerializeField] private float _minAttackDistance = 2;
    [SerializeField] private Transform _visionRayOrigin;
    [SerializeField] private LayerMask _visionRayLayer;
    [SerializeField] private float _timeUntilChangeDirection = 1;
    [SerializeField] private float _invincibilitySeconds = 0.5F;


    private BasicMovement _basicMovement;
    private Animator _animator;
    private Vector2 _startPos;
    private Transform _player;
    private float _walkDirX = -1;
    private float _targetPosX = 0;


    private IEnumerator ChangeWalkDir()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeUntilChangeDirection);
            _walkDirX = -_walkDirX;
        }
    }

    private IEnumerator InvincibilityCountdown()
    {
        yield return new WaitForSeconds(_invincibilitySeconds);
        _animator.SetBool("IsInvincible", false);
    }

    private void StopAttaking()
    {
        _animator.SetBool("IsAttaking", false);
    }



    public void SetInvincible()
    {
        _animator.SetBool("IsInvincible", true);
        StartCoroutine("InvincibilityCountdown");
    }

    private bool IsIdling()
    {
        return !_animator.GetBool("IsAttaking") && !_animator.GetBool("IsWalking") && !_animator.GetBool("IsCharging") && !_animator.GetBool("IsInvincible");
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _basicMovement = GetComponent<BasicMovement>();
        _startPos = transform.position;
        StartCoroutine("ChangeWalkDir");
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(_visionRayOrigin.position, transform.right, _visionDistance, _visionRayLayer);
        if (!_player && hit.collider != null && hit.collider.gameObject.tag == "Player")
        {
            _player = hit.collider.gameObject.transform;
            StopCoroutine("ChangeWalkDir");
            _animator.SetBool("IsCharging", true);
        }

        if (_animator.GetBool("IsInvincible") || _animator.GetBool("IsDying"))
        {
            return;
        }

        if (!_player)
        {
            _basicMovement.Walk(_walkDirX);
        }


        if (_player && !_animator.GetBool("IsAttaking"))
        {
            float diffWithPlayerNormalizedX = _player.position.x - transform.position.x > 0 ? 1 : -1;

            float offsetFromPlayerX = _minAttackDistance + (_maxAttackDistance - _minAttackDistance) / 2;
            float _targetPosXRight = _player.position.x + offsetFromPlayerX;
            float _targetPosXLeft = _player.position.x - offsetFromPlayerX;


            if (Mathf.Abs(_targetPosXLeft - transform.position.x) < Mathf.Abs(_targetPosXRight - transform.position.y))
            {
                _targetPosX = _targetPosXLeft;
            }
            else
            {
                _targetPosX = _targetPosXRight;
            }


            if (_minAttackDistance <= Mathf.Abs(_player.position.x - transform.position.x) && Mathf.Abs(_player.position.x - transform.position.x) <= _maxAttackDistance && transform.right.x == diffWithPlayerNormalizedX)
            {
                _animator.SetBool("IsAttaking", true);
            }
            else
            {
                _basicMovement.Walk(_targetPosX - transform.position.x);
            }
        }
    }

    public void TakeDamage(float damage)
    {

        if (_animator.GetBool("IsInvincible") || _animator.GetBool("IsDying"))
        {
            return;
        }

        _hp -= damage;
        if (_hp <= 0)
        {
            _hp = 0;
            _animator.SetBool("IsDying", true);
        }

        SetInvincible();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_visionRayOrigin.position, _visionRayOrigin.position + transform.right * _visionDistance);
        Gizmos.DrawLine(transform.position + _minAttackDistance * transform.right, transform.position + _maxAttackDistance * transform.right);

        if (_player)
        {
            Gizmos.DrawWireSphere(_targetPosX * Vector3.right + transform.position.y * Vector3.up, 1);
        }
    }

}
