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
        _animator[0].SetTrigger("Attack");
        _animator[1].ResetTrigger("Sword_Attack");
        _animator[1].SetTrigger("Sword_Attack");
    }
}
