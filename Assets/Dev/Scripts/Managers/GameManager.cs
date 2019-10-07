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
    [SerializeField]
    private GameObject NPCValueText;
    [SerializeField]
    private Slider NPCValueSlider;
    [SerializeField]
    private GameObject MovementSetting;
    [SerializeField]
    private GameObject miniMenu;

    public static int amountOfNpcsToSpawn;
    public static MovementMode currentMovement = MovementMode.Teleport;
    public static int currentScenario = 1;
    public static bool DesktopMode = true;

    public void StartGame()
    {

        SceneManager.LoadScene(currentScenario);
        //Start time
        Time.timeScale = 1;
    }

    private void Awake()
    {
        //Set default text
        SetMovementText();
        NPCValueSlider.value = 40;
        ChangedNPCValue();

        // Set initial value to 1
        amountOfNpcsToSpawn = (int)FindObjectOfType<Slider>().value;

        // ----------------------------------- CHANGE TO CHECK MODEL NAME FOR QUEST --------------------------------
        if (!XRDevice.model.ToLower().Contains("vive") && !XRDevice.model.ToLower().Contains("cv") && !XRDevice.model.ToLower().Contains("rift"))
        {
            miniMenu.SetActive(true);
            DesktopMode = false;
        }
        
    }

    public void Next(GameObject settingField)
    {
        InputSetting setting = GetSetting(settingField.name);

        switch (setting)
        {
            case InputSetting.Movement:
                if ((int)currentMovement < Enum.GetNames(typeof(MovementMode)).Length - 1)
                    currentMovement++;
                else
                    currentMovement = 0;
                SetMovementText();
                break;
        }
    }

    public void Previous(GameObject settingField)
    {
        InputSetting setting = GetSetting(settingField.name);
        
    }

    public void IncrementSlider(Slider slider)
    {
        slider.value += 10;
    }

    public void DecrementSlider(Slider slider)
    {
        slider.value -= 10;
    }

    private InputSetting GetSetting(string name)
    {
        switch (name)
        {
            case "MovementInputField":
                return InputSetting.Movement;
            default:
                return InputSetting.None;
        }
    }

    private void SetMovementText()
    {
        switch (currentMovement)
        {
            case MovementMode.ControllerDirection:
                MovementSetting.GetComponent<InputField>().text = "Lopen richting controller";
                break;
            case MovementMode.Teleport:
                MovementSetting.GetComponent<InputField>().text = "Teleporteren";
                break;
            default:
                MovementSetting.GetComponent<InputField>().text = "Lopen richting kijkrichting";
                break;
        }
    }

    public void ChangedNPCValue()
    {
        NPCValueText.GetComponent<Text>().text = NPCValueSlider.value.ToString();
        amountOfNpcsToSpawn = (int)NPCValueSlider.value;
    }
}
