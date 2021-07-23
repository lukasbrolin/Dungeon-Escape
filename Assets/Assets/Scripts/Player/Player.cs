using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    private Rigidbody2D _rb;

    [SerializeField]
    private float _runSpeed;
    [SerializeField]
    private float _jumpForce;
    [SerializeField]
    private float _attackSpeed;

    private bool _isGrounded = false;
    private bool _isAttacking = false;
    private PlayerAnimation _playerAnim;
    private SpriteRenderer[] _spriteRend;

    private bool _resetJump = false;

    public int Health { get; set; }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<PlayerAnimation>();
        _spriteRend = GetComponentsInChildren<SpriteRenderer>();
    }

    void Update()
    {
        Movement();
        Attack();
    }

    void Attack()
    {
        if (Input.GetMouseButton(0) && IsGrounded() && _isAttacking == false)
        {
            TriggerAttacking();
            _playerAnim.Attack();
            StartCoroutine(AttackTimer());
        }
    }

    private IEnumerator AttackTimer()
    {
        yield return new WaitForSeconds(_attackSpeed);
        Debug.Log("Timer");
        _playerAnim.ResetAttack();
        TriggerAttacking();
    }

    private void TriggerAttacking()
    {
        if(_isAttacking)
        {
            _isAttacking = false;
        }
        else if (!_isAttacking)
        {
            _isAttacking = true;
        }
    }

    void Movement()
    {
        float move = Input.GetAxisRaw("Horizontal");
        _isGrounded = IsGrounded();

        FlipSprite(move);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
            _playerAnim.Jump(true);
            StartCoroutine(ResetJumpRoutine());
        }
        _rb.velocity = new Vector2(move * _runSpeed, _rb.velocity.y);
        _playerAnim.Move(move);
    }

    void FlipSprite(float move)
    {
        if (move > 0)
        {
            _spriteRend[0].flipX = false;
            _spriteRend[1].flipY = false;
        }
        else if (move < 0)
        {
            _spriteRend[0].flipX = true;
            _spriteRend[1].flipY = true;
        }
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, 1 << 8);

        if (hit.collider != null)
        {
            if (!_resetJump)
            {
                _playerAnim.Jump(false);
                return true;
            }
        }
        return false;
    }

    IEnumerator ResetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }

    public void Damage(int damageAmount)
    {
        Debug.Log("Damage()");
    }
}
