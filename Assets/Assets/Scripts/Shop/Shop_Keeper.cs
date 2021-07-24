using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_Keeper : MonoBehaviour
{
    [SerializeField]
    private GameObject _canvas;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            _canvas.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            _canvas.SetActive(false);
        }
    }
}
