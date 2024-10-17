using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupTutorial : MonoBehaviour
{
    public GameObject[] popUps;
    public PlayerInputHandler playerInputHandler;
    private int popUpIndex = 0;
    void Update()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            // Activate the current pop-up and deactivate others
            if (i == popUpIndex)
            {
                popUps[i].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
            }
        }

        // Logic to change pop-up index based on input
        if (popUpIndex == 0)
        {
            if (playerInputHandler.NormInputX != 0)
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 1)
        {
            if (playerInputHandler.JumpInput)
            {
                popUpIndex++;
            }
        }
    }
}
