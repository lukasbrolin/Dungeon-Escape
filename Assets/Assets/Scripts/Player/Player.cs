using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class Player : MonoBehaviour, IDamagable
{
    private Rigidbody2D _rb;

    [SerializeField]
    private float _runSpeed;
    [SerializeField]
    private float _jumpForce;
    [SerializeField]
    private float _fallMultiplier;
    [SerializeField]
    private float _jumpMultiplier;
    [SerializeField]
    private float _attackRate;

    private float nextAttackTime = 0f;
    private float timerJump = 0f;
    private bool _isGrounded = false;
    private bool _resetJump = false;
    private bool _jumpReq = false;

    private PlayerAnimation _playerAnim;
    private SpriteRenderer[] _spriteRend;
    private BoxCollider2D _boxCollider;

    public int Health { get; set; }
    public int diamonds;

    void Start()
    {
        this.Health = 4;
        _rb = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<PlayerAnimation>();
        _spriteRend = GetComponentsInChildren<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        Movement();
        Attack();
    }

    void FixedUpdate()
    {
        if (_jumpReq)
        {
            JumpForce();
        }

        JumpCheck();
    }

    void Attack()
    {
        if(Time.time >= nextAttackTime)
        {

            if (Input.GetKeyDown(KeyCode.LeftControl) && IsGrounded())
            {
                Debug.Log(Time.time);

                nextAttackTime = Time.time + 1f / _attackRate;
                AudioManager.instance.Play("Player_Attack");
                _playerAnim.Attack();
            }
        }
        
    }

    void Movement()
    {
        float move = Input.GetAxisRaw("Horizontal");
        _isGrounded = IsGrounded();

        FlipSprite(move);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            timerJump = Time.time + 0.6f;
            _jumpReq = true;
            AudioManager.instance.Play("Player_Jump");
            _playerAnim.Jump(true);
            StartCoroutine(ResetJumpRoutine());
        }
        _rb.velocity = new Vector2(move * _runSpeed, _rb.velocity.y);
        _playerAnim.Move(move);
    }

    void JumpCheck()
    {
        if (_rb.velocity.y < 0)
        {
            _rb.gravityScale = _fallMultiplier;
        }
        else if (_rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            _rb.gravityScale = _jumpMultiplier;
        }
        else if (Input.GetKey(KeyCode.Space) && GameManager.Instance.HasBootsOfFlight)
        {
            if (Time.time <= timerJump)
            {
                _rb.gravityScale = 0f;
            }
            else
            {
                _rb.gravityScale = 1f;
            }
        }
        else
        {
            _rb.gravityScale = 1f;
        }
    }

    void JumpForce()
    {
        _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        _jumpReq = false;
    }

    void FlipSprite(float move)
    {
        if (move > 0)
        {
            _boxCollider.offset = new Vector2(-0.1f, _boxCollider.offset.y);
            _spriteRend[0].flipX = false;
            _spriteRend[1].flipY = false;
        }
        else if (move < 0)
        {
            _boxCollider.offset = new Vector2(0.1f, _boxCollider.offset.y); 
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
        UIManager.Instance.UpdateLives(Health);
        Health -= damageAmount;
        if (Health <= 0)
        {
            AudioManager.instance.Play("Player_Death");
            _playerAnim.Death();
            Destroy(gameObject,2f);
        }
        else
        {
            AudioManager.instance.Play("Player_Hit");
            _playerAnim.Hit();
        }
    }

    public void AddDiamonds(int amount)
    {
        diamonds += amount;
        AudioManager.instance.Play("Player_Pickup");
        UIManager.Instance.UpdateGemCount(diamonds);
    }

}
