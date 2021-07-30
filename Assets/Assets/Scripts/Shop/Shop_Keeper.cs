using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_Keeper : MonoBehaviour
{
    [SerializeField]
    private GameObject shopPanel;

    private int _selectedItem;
    private int _selectedItemCost;
    private Player _player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            _player = collision.GetComponent<Player>();
            if (_player != null)
            {
                UIManager.Instance.OpenShop(_player.diamonds);
            }
            shopPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            ShopExit();
        }
    }

    void ShopExit()
    {
        UIManager.Instance.selectionImg.enabled = false;
        shopPanel.SetActive(false);
    }

    public void SelectItem(int item)
    {
        UIManager.Instance.selectionImg.enabled = true;
        _selectedItem = item;
        switch (item)
        {
            case 0:
                UIManager.Instance.SetSelectionY(100f);
                _selectedItemCost = 20;
                break;
            case 1:
                _selectedItemCost = 40;
                UIManager.Instance.SetSelectionY(0f);
                break;
            case 2:
                _selectedItemCost = 10;
                UIManager.Instance.SetSelectionY(-100f);
                break;
        }
    }

    public void Buy()
    {
        if(_player.diamonds >= _selectedItemCost)
        {
            _player.diamonds -= _selectedItemCost;
            UIManager.Instance.OpenShop(_player.diamonds);
            if (_selectedItem == 0)
            {
                GameManager.Instance.HasFlameSword = true;
            }
            else if (_selectedItem == 1)
            {
                GameManager.Instance.HasBootsOfFlight = true;
            }
            else if (_selectedItem == 2)
            {
                GameManager.Instance.HasKeyToCastle = true;
            }
            ShopExit();
        }
        else
        {
            Debug.Log("You dont have enough credit");
            ShopExit();
        }
    }
}
