using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
    [SerializeField]
    private Transform _acidPoint;
    [SerializeField]
    private GameObject _acid;

    public override void Init()
    {
        base.Init();
        _idleAnimation = "Idle";
        mobName = "Spider";
    }

    public override void Movement()
    {

        if (!isHit)
        {
            transform.position = Vector3.MoveTowards(transform.position, _currentTarget, _speed * Time.deltaTime);
        }

        float distance = Vector3.Distance(transform.localPosition, player.transform.localPosition);

        if (distance > 10f)
        {
            isHit = false;
            _animator.SetBool("InCombat", false);
        }
    }

    public void Attack()
    {
        Debug.Log("Attack");
        Instantiate(_acid, _acidPoint.position, Quaternion.identity);
    }
}
