using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Valve.VR;

public class EscapeMenu : MonoBehaviour
{
    public Button ReturnToGameButton, ReturnToMenuButton, ExitButton;

    // Start is called before the first frame update
    void Start()
    {
        if (ReturnToGameButton != null)
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
        DistanceTrigger.TutorialBurgerIsDone = false;
        DistanceTrigger.TutorialControlRoomIsDone = false;
        DistanceTrigger.ConversationIsDone = false;
        DistanceTrigger.VandalismHasHappend = false;
        DistanceTrigger.StartedSendingMessages = false;
        DistanceTrigger.ScenarioIsDone = false;
        DistanceTrigger.StartScenarioDone = false;
        SteamVR_LoadLevel.Begin("MainMenu");
    }

    void ExitGame()
    {
        Application.Quit();
    }
}
