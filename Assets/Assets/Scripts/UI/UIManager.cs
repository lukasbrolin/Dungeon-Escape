using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                throw new UnityException();
            }
            return _instance;
        }
    }

    public Text _playerGemCount;
    public Image selectionImg;
    public Image[] healthBars;
    public Text gemCountText;

    private void Awake()
    {
        _instance = this;
    }
    public void OpenShop(int gemCount)
    {
        _playerGemCount.text = "" + gemCount + "G";
    }

    public void SetSelectionY(float y)
    {
        Debug.Log(y);
        selectionImg.transform.localPosition = new Vector2(selectionImg.transform.localPosition.x,y);

    }

    public void UpdateLives(int livesRemaining)
    {
        for(int i = 0; i < livesRemaining; i++)
        {
            if(i == livesRemaining-1)
            {
                Debug.Log(i);
                healthBars[i].enabled = false;
            }
        }
    }

    public void UpdateGemCount(int gemCount)
    {
        gemCountText.text = gemCount.ToString();
    }
}
