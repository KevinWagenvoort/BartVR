using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

enum InputSetting
{
    None,
    Movement,
}

public enum MovementMode
{
    FacingDirection,
    ControllerDirection,
    Teleport
}


public class GameManager : MonoBehaviour
{

    public static int amountOfNpcsToSpawn = 0;
    public static int currentScenario = 1;

    public void StartGame()
    {
        //Spawn npc
        amountOfNpcsToSpawn = 10;
        //Load scene
        SceneManager.LoadScene(currentScenario);
        //Start time
        Time.timeScale = 1;
    }
}
