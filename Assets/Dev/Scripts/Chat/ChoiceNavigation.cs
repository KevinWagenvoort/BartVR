using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceNavigation : MonoBehaviour
{
    //public
    [SerializeField]
    public bool IsPrivate;
    public List<ButtonProperties> Buttons;
    public GameObject NeighbourhoodApp;
    public GameObject PrivateApp;

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
        Selected = new CurrentlySelected(0, ButtonActiveCount() - 1);
        DisableButtons();
        NeighbourhoodAppScript = NeighbourhoodApp.GetComponent<NeighbourhoodAppScript>();
        PrivateAppScript = PrivateApp.GetComponent<PrivateAppScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Navigation();
        ButtonOutline();
    }

    void Navigation()
    {
        //VR
        try
        {
            controller = SteamVR_Controller.Input((int)trackedObject.index);
            if (controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
            {
                if (controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).y > 0.1f)
                {
                    PrevSelected = Selected.selected;
                    Selected.Previous();
                } else if (controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).y < -0.1f)
                {
                    PrevSelected = Selected.selected;
                    Selected.Next();
                }
            }

            if (controller.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                if (IsPrivate)
                {
                    PrivateAppScript.SendChoice(Selected.selected);
                }
                else
                {
                    NeighbourhoodAppScript.SendChoiceCitizen(Buttons[Selected.selected].GetText());
                }
            }
        } catch (Exception e)
        {
            Debug.LogError(e);
        }

        //PC
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            PrevSelected = Selected.selected;
            Selected.Previous();
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            PrevSelected = Selected.selected;
            Selected.Next();
        } else if (Input.GetKeyUp(KeyCode.Return))
        {
            if (IsPrivate)
            {
                PrivateAppScript.SendChoice(Selected.selected);
            }
            else
            {
                NeighbourhoodAppScript.SendChoiceCitizen(Buttons[Selected.selected].GetText());
            }
        }
    }

    int ButtonActiveCount()
    {
        int ac = 0;
        foreach (ButtonProperties bp in Buttons)
        {
            if (bp.Active)
            {
                ac++;
            }
        }
        return ac;
    }

    void ButtonOutline()
    {
        Buttons[PrevSelected].ButtonObject.transform.GetComponent<Outline>().enabled = false;
        Buttons[Selected.selected].ButtonObject.transform.GetComponent<Outline>().enabled = true;
    }

    void DisableButtons()
    {
        foreach (ButtonProperties bp in Buttons)
        {
            bp.ButtonObject.SetActive(bp.Active);
        }
    }
}

[System.Serializable]
public class ButtonProperties
{

    public GameObject ButtonObject;
    public bool Active;

    [HideInInspector]
    public Color UnselectedColor = Color.blue;
    [HideInInspector]
    public Color SelectedColor = Color.green;
    [HideInInspector]
    public bool Selected = false;

    public ButtonProperties(GameObject ButtonObject, bool Active)
    {
        this.ButtonObject = ButtonObject;
        this.Active = Active;
    }

    public string GetText()
    {
        return ButtonObject.transform.Find("Text").GetComponent<Text>().text;
    }
}

public class CurrentlySelected
{
    public int selected = 0;
    public int max;
    public int min = 0;

    public CurrentlySelected(int selected = 0, int max = 0)
    {
        this.selected = selected;
        this.max = max;
    }

    public void Next()
    {
        selected++;
        if (selected > max)
        {
            selected = min;
        }
    }

    public void Previous()
    {
        selected--;
        if (selected < min)
        {
            selected = max;
        }
    }
}