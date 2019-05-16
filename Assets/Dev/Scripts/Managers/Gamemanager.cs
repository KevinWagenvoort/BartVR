﻿using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

public class GameManager : MonoBehaviour {
    [SerializeField]
    private GameObject NPCValueText;
    [SerializeField]
    private Slider NPCValueSlider;
    [SerializeField]
    private GameObject miniMenu;

    enum InputSetting {
        None,
        Movement,
        Mode,
        Scenario
    }

    public enum PlayingMode {
        Multiplayer,
        Singleplayer
    }

    public enum Movement {
        FacingDirection,
        ControllerDirection,
        Teleport
    }

    private List<string> multiplayerScenes = new List<string>();
    private List<string> singleplayerScenes = new List<string>();

    public static int amountOfNpcsToSpawn;
    public static PlayingMode currentMode = PlayingMode.Multiplayer;
    public static Movement currentMovement = Movement.FacingDirection;
    public static int currentScenario;
    public static bool DesktopMode = true;

    public void StartGame() {
        Debug.Log("Starting up game");
        if (DesktopMode) 
            SceneManager.LoadScene(currentScenario + 1);
         else 
            SceneManager.LoadScene(multiplayerScenes.Count + currentScenario + 1);
        //Start time
        Time.timeScale = 1;
    }

    private void Awake() {
        // Set initial value to 1
        amountOfNpcsToSpawn = (int) FindObjectOfType<Slider>().value;

        // ----------------------------------- CHANGE TO CHECK MODEL NAME FOR QUEST --------------------------------
        if (!XRDevice.model.ToLower().Contains("vive") && !XRDevice.model.ToLower().Contains("cv") && !XRDevice.model.ToLower().Contains("rift")) {
            miniMenu.SetActive(true);
            DesktopMode = false;
        }

        DirectoryInfo multiDI = new DirectoryInfo("Assets/Scenes/Multiplayer Scenes");
        DirectoryInfo singleDI = new DirectoryInfo("Assets/Scenes/Singleplayer Scenes");

        foreach (FileInfo file in multiDI.GetFiles("*.unity")) {
            string scene = file.Name.Replace(".unity", "");
            multiplayerScenes.Add(scene);
        }

        foreach (FileInfo file in singleDI.GetFiles("*.unity")) {
            string scene = file.Name.Replace(".unity", "");
            singleplayerScenes.Add(scene);
        }
    }

    public void Next(GameObject settingField) {
        InputSetting setting = GetSetting(settingField.name);

        switch (setting) {
            case InputSetting.Mode:
                if ((int)currentMode < Enum.GetNames(typeof(PlayingMode)).Length - 1)
                    currentMode++;
                else
                    currentMode = 0;
                SetModeText(settingField);
                break;
            case InputSetting.Movement:
                if ((int)currentMovement < Enum.GetNames(typeof(Movement)).Length - 1)
                    currentMovement++;
                else
                    currentMovement = 0;
                SetMovementText(settingField);
                break;
            case InputSetting.Scenario:
                if (DesktopMode) {
                    if (currentScenario < multiplayerScenes.Count - 1)
                        currentScenario++;
                    else
                        currentScenario = 0;
                    settingField.GetComponent<InputField>().text = multiplayerScenes[currentScenario];
                } else {
                    if (currentScenario < singleplayerScenes.Count - 1)
                        currentScenario++;
                    else
                        currentScenario = 0;
                    settingField.GetComponent<InputField>().text = singleplayerScenes[currentScenario];
                }
                break;
        }
    }

    public void Previous(GameObject settingField) {
        InputSetting setting = GetSetting(settingField.name);

        switch (setting) {
            case InputSetting.Mode:
                if (currentMode > 0)
                    currentMode--;
                else
                    currentMode = (PlayingMode)Enum.GetNames(typeof(PlayingMode)).Length - 1;
                SetModeText(settingField);
                break;
            case InputSetting.Movement:
                if (currentMovement > 0)
                    currentMovement--;
                else
                    currentMovement = (Movement)Enum.GetNames(typeof(Movement)).Length - 1;
                SetMovementText(settingField);
                break;
            case InputSetting.Scenario:
                if (DesktopMode) {
                    if (currentScenario > 0)
                        currentScenario--;
                    else
                        currentScenario = multiplayerScenes.Count - 1;
                    settingField.GetComponent<InputField>().text = multiplayerScenes[currentScenario];
                } else {

                    if (currentScenario > 0)
                        currentScenario--;
                    else
                        currentScenario = singleplayerScenes.Count - 1;
                    settingField.GetComponent<InputField>().text = singleplayerScenes[currentScenario];
                }
                break;
        }
    }

    public void IncrementSlider(Slider slider) {
        slider.value += 10;
    }

    public void DecrementSlider(Slider slider) {
        slider.value -= 10;
    }

    private InputSetting GetSetting(string name) {
        switch (name) {
            case "PlayingModeInputField":
                return InputSetting.Mode;
            case "MovementInputField":
                return InputSetting.Movement;
            case "ScenarioInputField":
                return InputSetting.Scenario;
            default:
                return InputSetting.None;
        }
    }

    private void SetMovementText(GameObject settingField) {
        switch (currentMovement) {
            case Movement.ControllerDirection:
                settingField.GetComponent<InputField>().text = "Lopen richting controller";
                break;
            case Movement.Teleport:
                settingField.GetComponent<InputField>().text = "Teleporteren";
                break;
            default:
                settingField.GetComponent<InputField>().text = "Lopen richting kijkrichting";
                break;
        }
    }

    private void SetModeText(GameObject settingField) {
        switch (currentMode) {
            case PlayingMode.Singleplayer:
                settingField.GetComponent<InputField>().text = "Singleplayer";
                break;
            default:
                settingField.GetComponent<InputField>().text = "Multiplayer";
                break;
        }
    }

    public void ChangedNPCValue() {
        NPCValueText.GetComponent<Text>().text = NPCValueSlider.value.ToString();
        amountOfNpcsToSpawn = (int)NPCValueSlider.value;
    }
}
