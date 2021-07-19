using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    private bool _canDamage = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("what");
        Debug.Log(other.name);

        IDamagable hit = other.GetComponent<IDamagable>();

        if(hit != null)
        {
            if (_canDamage)
            {
                hit.Damage(30);
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
