using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConversationNavigation : MonoBehaviour
{
    //public
    [SerializeField]
    public bool IsPrivate;
    public List<GameObject> Buttons;
    public GameObject NeighbourhoodApp;
    public GameObject PrivateApp;
    public Color SelectedColor;
    public Color DefaultColor;

    //private
    private CurrentlySelected Selected;
    private int PrevSelected = 0;
    private NeighbourhoodAppScript NeighbourhoodAppScript;
    private PrivateAppScript PrivateAppScript;

    // SteamVR
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device controller;

    // Start is called before the first frame update
    void Start()
    {
        trackedObject = GetComponentInParent<SteamVR_TrackedObject>();
        SetChoices(new List<string>() { "Hallo 1", "Hallo 3" });
    }

    // Update is called once per frame
    void Update()
    {
        Navigation();
    }

    void Navigation()
    {
        //VR
        try
        {
            controller = SteamVR_Controller.Input((int)trackedObject.index);
            if (controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
            {
                if (controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).y > 0.3f)
                {
                    PrevSelected = Selected.selected;
                    Selected.Previous();
                    ButtonColor();
                }
                else if (controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).y < -0.3f)
                {
                    PrevSelected = Selected.selected;
                    Selected.Next();
                    ButtonColor();
                }
            }

            if (controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
            {
                Debug.Log(Buttons[Selected.selected].transform.Find("ChoiceText").GetComponent<Text>().text);
                SetChoices(new List<string>() { "Hallo Reach", "Hallo 4", "Hallo 5" });
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }

        //PC
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            PrevSelected = Selected.selected;
            Selected.Previous();
            ButtonColor();
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            PrevSelected = Selected.selected;
            Selected.Next();
            ButtonColor();
        }
        else if (Input.GetKeyUp(KeyCode.Return))
        {
            Debug.Log(Buttons[Selected.selected].transform.Find("ChoiceText").gameObject.GetComponent<TMP_Text>().text);
            SetChoices(new List<string>() { "Hallo Reach", "Hallo 4", "Hallo 5" });
        }
    }

    void ButtonColor()
    {
        Buttons[PrevSelected].GetComponent<Image>().color = DefaultColor;
        Buttons[Selected.selected].GetComponent<Image>().color = SelectedColor;
    }

    void DisableButtons()
    {
        foreach (GameObject bp in Buttons)
        {
            bp.SetActive(false);
            bp.GetComponent<Image>().color = DefaultColor;
        }
    }

    public void SetChoices(List<string> choices)
    {
        DisableButtons();
        int i = 0;
        foreach (GameObject button in Buttons)
        {
            if (i < choices.Count)
            {
                button.transform.Find("ChoiceText").gameObject.GetComponent<TMP_Text>().text = choices[i];
                button.SetActive(true);
            }
            i++;
        }
        Selected = new CurrentlySelected(0, choices.Count - 1);
        ButtonColor();
    }
}
