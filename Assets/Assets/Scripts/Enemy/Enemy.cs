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
    [SerializeField]
    protected Diamond _diamondPrefab;
    [SerializeField]
    protected int _gemAmount;

    protected Vector3 _currentTarget;
    protected Animator _animator;
    protected SpriteRenderer _spriteRenderer;
    protected string _idleAnimation;

    protected bool isHit = false;
    protected bool isDead = false;
    protected string mobName;
    protected bool _inRange = false;

    protected Player player;

    public virtual void Init()
    {
        _animator = GetComponentInChildren<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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
            _animator.SetTrigger("Moving");
            transform.position = Vector3.MoveTowards(transform.position, _currentTarget, _speed * Time.deltaTime);
        }

        float distance = Vector3.Distance(transform.localPosition, player.transform.localPosition);
        
        if(distance > 1f && isHit)
        {
            _inRange = false;
            Debug.Log(player.transform.localPosition.y - transform.localPosition.y);
            if(Mathf.Abs(player.transform.localPosition.y - transform.localPosition.y) > 0.5f)
            {
                isHit = false;
                _animator.SetBool("InCombat", false);
            }
            else
            {
                _animator.SetTrigger("Moving");
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y,0f), _speed * Time.deltaTime);
            }
        }
        else if(distance < 1f && isHit)
        {
            _inRange = true;
            _animator.SetTrigger("Idle");
        }
    }

    public void IfInRange()
    {
        if (_inRange)
        {
            _animator.SetBool("InRange",true);
        }
        else if (!_inRange)
        {
            _animator.SetBool("InRange", false);
        }
    }

    public virtual void CheckFacing()
    {
        if(_currentTarget.x - transform.position.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }
        Vector3 direction = player.transform.localPosition - transform.localPosition;

        if(direction.x > 0 && _animator.GetBool("InCombat"))
        {
            _spriteRenderer.flipX = false;
        }
        else if(direction.x < 0 && _animator.GetBool("InCombat"))
        {
            _spriteRenderer.flipX = true;
        }
    }

    public virtual void Update()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName(_idleAnimation) && !_animator.GetBool("InCombat") || isDead)
        {
            return;
        }
        CheckFacing();
        Movement();
        IfInRange();
    }

    public virtual void Damage(int damageAmount)
    {
        _health -= damageAmount;
        
        if (_health <= 0)
        {
            isDead = true;
            AudioManager.instance.Play(mobName + "_Death");
            _animator.Play("Death");
            Destroy(gameObject,5f);
            float diff = 0;
            for(int i = 0; i < _gemAmount; i++)
            {
                Diamond diamond = Instantiate(_diamondPrefab, new Vector2(transform.position.x + diff, player.transform.position.y), Quaternion.identity);
                diff += 0.2f;
            }
        }
        else
        {
            AudioManager.instance.Play(mobName + "_Hit");
            _animator.SetTrigger("Hit");
            _animator.SetBool("InCombat", true);
        }
        isHit = true;

    }
    public void PlayAttack()
    {
        AudioManager.instance.Play(mobName + "_Attack");
    }
}

