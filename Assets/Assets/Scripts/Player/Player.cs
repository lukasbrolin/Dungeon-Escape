using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField]
    private float _runSpeed;
    [SerializeField]
    private float _jumpForce;

    private bool _isGrounded = false;
    private PlayerAnimation _playerAnim;
    private SpriteRenderer[] _spriteRend;

    private bool _resetJump = false;
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
        if (Input.GetMouseButton(0) && isGrounded())
        {
            _playerAnim.Attack();
        }
    }

    void Movement()
    {
        float move = Input.GetAxisRaw("Horizontal");
        _isGrounded = isGrounded();

        FlipSprite(move);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
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

    bool isGrounded()
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
}
