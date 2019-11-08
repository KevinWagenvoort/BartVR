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
    public GameObject LocalChat;
    public Color SelectedColor;
    public Color DefaultColor;

    //private
    private CurrentlySelected Selected;
    private int PrevSelected = 0;
    private LocalChatScript LocalChatScript;
    private Vector3 defaultScale;

    // SteamVR
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device controller;

    // Start is called before the first frame update
    void Start()
    {
        trackedObject = GetComponentInParent<SteamVR_TrackedObject>();
        LocalChatScript = LocalChat.GetComponent<LocalChatScript>();
        LocalChatScript.PizzaSuspects();
    }

    private void Awake()
    {
        defaultScale = Buttons[0].transform.localScale;
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
                if (controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).y > 0.7f)
                {
                    PrevSelected = Selected.selected;
                    Selected.Previous();
                    ButtonColor();
                }
                else if (controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).y < -0.7f)
                {
                    PrevSelected = Selected.selected;
                    Selected.Next();
                    ButtonColor();
                }
            }

            if (controller.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                LocalChatScript.SendChoice(Selected.selected);
                DisableButtons();
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
            LocalChatScript.SendChoice(Selected.selected);
            DisableButtons();
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
        Buttons[0].transform.localScale = defaultScale;
        ButtonColor();
    }

    public void SetChoice(string choice)
    {
        DisableButtons();
        Buttons[0].transform.Find("ChoiceText").gameObject.GetComponent<TMP_Text>().text = choice;
        Buttons[0].SetActive(true);
        Buttons[0].transform.localScale = (defaultScale + new Vector3(1, 1, 1));
        Selected = new CurrentlySelected(0, 0);
        ButtonColor();
    }
}
