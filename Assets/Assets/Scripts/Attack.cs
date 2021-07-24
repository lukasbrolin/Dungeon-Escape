using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    private int _damage;

    private bool _canDamage = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamagable hit = other.GetComponent<IDamagable>();

        if(hit != null)
        {
            if (_canDamage)
            {
                hit.Damage(_damage);
                _canDamage = false;
                StartCoroutine(ResetCooldown());
            }
        }
    }

    IEnumerator ResetCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        _canDamage = true;
    }
}
