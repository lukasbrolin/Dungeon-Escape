using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField]
    private GameObject _transition;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            _transition.SetActive(false);
            _transition.SetActive(true);
            collision.transform.position = new Vector3(0, 3f, 0f);

            IDamagable hit = collision.GetComponent<IDamagable>();

            if (hit != null)
            {
                hit.Damage(1);
            }
        }
    }
}
