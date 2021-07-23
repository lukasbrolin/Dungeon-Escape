using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator[] _animator;

    void Start()
    {
        _animator = GetComponentsInChildren<Animator>();
    }

    public void Move(float move)
    {
        _animator[0].SetFloat("Move", Mathf.Abs(move));
    }

    public void Jump(bool toggle)
    {
        _animator[0].SetBool("Jump", toggle);
    }

    public void Attack()
    {
        if (!_animator[0].GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            Debug.Log("Im out");
            _animator[0].SetTrigger("Attack");
            _animator[1].SetTrigger("Sword_Attack");
        }
        /*else if (_animator[0].GetCurrentAnimatorStateInfo(0).IsName("Attack") && _animator[0].GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            Debug.Log("I'm in, whats my mission");
            _animator[0].SetTrigger("Attack");
            _animator[1].SetTrigger("Sword_Attack");
            //_animator[0].ResetTrigger("Attack");
            
        }*/
        Debug.Log(_animator[0].GetCurrentAnimatorStateInfo(0).IsName("Attack"));
           
    }

    public void ResetAttack()
    {
        _animator[0].SetTrigger("Attack");
        _animator[1].SetTrigger("Sword_Attack");
        //_animator[0].ResetTrigger("Attack");
        Debug.Log("Resetting");
    }
}
