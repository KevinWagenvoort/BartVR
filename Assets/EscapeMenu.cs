using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EscapeMenu : MonoBehaviour
{
    public Button ReturnToGameButton, ReturnToMenuButton, ExitButton;

    // Start is called before the first frame update
    void Start()
    {
        ReturnToGameButton.onClick.AddListener(ReturnToGame);
        ReturnToMenuButton.onClick.AddListener(ReturnToMenu);
        ExitButton.onClick.AddListener(ExitGame);
    }

    void ReturnToGame()
    {
        gameObject.SetActive(false);
    }

    void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void ExitGame()
    {
        Application.Quit();
    }
}
