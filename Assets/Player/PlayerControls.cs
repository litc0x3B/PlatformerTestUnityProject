using UnityEngine;

[RequireComponent(typeof(BasicMovement))]
public class PlayerControls : MonoBehaviour
{
    private BasicMovement _basicMovement;
    private Animator _animator;
    private AudioSource _audioSource;

    private void Start()
    {
        _basicMovement = GetComponent<BasicMovement>();
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }

    private void StopAttacking()
    {
        _animator.SetBool("IsAttacking", false);
    }

    void Update()
    {

        if (_animator.GetBool("IsAttacking"))
        {
            return;
        }

        _basicMovement.Walk(Input.GetAxis("Horizontal"));
        if (Input.GetButtonDown("Jump"))
        {
            _basicMovement.Jump();
        }

        if (Input.GetButtonDown("Attack"))
        {
            _animator.SetBool("IsAttacking", true);
            if (_audioSource != null)
            {
                _audioSource.Play();
            }
            
        }

    }
}
