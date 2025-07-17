using System.Collections;
using System.Collections.Generic;
using TMPro;
using Tuleeeeee.Manager;
using UnityEngine;

public class WinScreen : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI winMessage;
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnWin += OnWin;
        }
    }
    void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnWin += OnWin;
        }
    }
    public void OnWin()
    {
        Debug.Log("You won the game!");

        this.gameObject.SetActive(true);
        
        if (winMessage != null)
        {
            winMessage.text = "You Win!";
        }

        TimeManager.PauseGame();
    }
}
