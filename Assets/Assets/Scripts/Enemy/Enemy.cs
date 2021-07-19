using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField]
    protected int _health;
    [SerializeField]
    protected float _speed;
    [SerializeField]
    protected int _gems;
    [SerializeField]
    protected Transform pointA, pointB;

    protected Vector3 _currentTarget;
    protected Animator _animator;
    protected SpriteRenderer _spriteRenderer;
    protected string _idleAnimation;

    protected bool isHit = false;

    public virtual void Init()
    {
        _animator = GetComponentInChildren<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        Init();
    }

    public virtual void Movement()
    {
        if (transform.position == pointA.position)
        {
            _animator.SetTrigger("Idle");
            _currentTarget = pointB.position;
        }
        else if (transform.position == pointB.position)
        {
            _animator.SetTrigger("Idle");
            _currentTarget = pointA.position;
        }

        if (!isHit)
        {
            transform.position = Vector3.MoveTowards(transform.position, _currentTarget, _speed * Time.deltaTime);
        }

        float distance = Mathf.Abs(transform.position.x - Player.player.transform.position.x);
        
        if(Mathf.Abs(distance) > 2f)
        {
            isHit = false;
            _animator.SetBool("InCombat", false);
        }
    }

    public virtual void CheckFacing()
    {
        if ((_currentTarget.x - transform.position.x < 0))
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }
    }

    public virtual void Update()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName(_idleAnimation))
        {
            return;
        }
        CheckFacing();
        Movement();
    }

    public void Damage(int damageAmount)
    {
        _health -= damageAmount;
        _animator.SetTrigger("Hit");
        isHit = true;
        _animator.SetBool("InCombat", true);
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

