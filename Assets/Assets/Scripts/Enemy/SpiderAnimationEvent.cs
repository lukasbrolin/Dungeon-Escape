using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimationEvent : MonoBehaviour
{
    private Spider _spider;

    public void Awake()
    {
        _spider = GetComponentInParent<Spider>();
    }
    public void Fire()
    {
        AudioManager.instance.Play("Spider_Attack");
        _spider.Attack();
    }

}
