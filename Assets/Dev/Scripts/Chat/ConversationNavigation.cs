﻿using System;
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
    private bool CanSend = false;

    // Start is called before the first frame update
    void Start()
    {
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
        if (Application.isEditor)
        {
            Navigation();
        }
    }

    void Navigation()
    {
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
        else if (Input.GetKeyUp(KeyCode.Return) && CanSend)
        {
            LocalChatScript.SendChoice(Selected.selected);
            DisableButtons();
            CanSend = false;
        }
    }

    public void TouchButtonChoice(int buttonNumber)
    {
        if (CanSend)
        {
            LocalChatScript.SendChoice(buttonNumber);
            DisableButtons();
            CanSend = false;
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
        CanSend = true;
    }

    public void SetChoice(string choice)
    {
        DisableButtons();
        Buttons[0].transform.Find("ChoiceText").gameObject.GetComponent<TMP_Text>().text = choice;
        Buttons[0].SetActive(true);
        Buttons[0].transform.localScale = (defaultScale + new Vector3(1, 1, 1));
        Selected = new CurrentlySelected(0, 0);
        ButtonColor();
        CanSend = true;
    }
}
