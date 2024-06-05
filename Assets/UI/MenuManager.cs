using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    private VisualElement container;
    private Button start_Button;
    private Button level_Button;
    private Button setting_Button;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        start_Button = root.Q<Button>("Start");
        level_Button = root.Q<Button>("Level");
        setting_Button = root.Q<Button>("Setting");

        start_Button.clickable.clicked += () =>
        {
            startGame();
        };



    }
    void Update()
    {

    }
    private void startGame()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(sceneIndex);
    }

}
