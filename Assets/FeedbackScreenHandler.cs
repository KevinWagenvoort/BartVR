using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FeedbackScreenHandler : MonoBehaviour
{
    public Button BackToMenu;
    
    void Start()
    {
        BackToMenu.onClick.AddListener(GoBackToMenu);
    }

    void GoBackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
