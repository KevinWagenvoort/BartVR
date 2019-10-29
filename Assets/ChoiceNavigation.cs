using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceNavigation : MonoBehaviour
{
    //public
    [SerializeField]
    public List<ButtonProperties> Buttons;

    //private
    public CurrentlySelected CurrentlySelected = new CurrentlySelected(0, 2);
    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var activeCount = 0;
        foreach (ButtonProperties bp in Buttons)
        {
            if (bp.Active)
            {
                activeCount++;
            }
        }
        CurrentlySelected = new CurrentlySelected(0, activeCount - 1);

        Navigation();
        UpdateAllButtons();
    }

    void Navigation()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            DeselectAllButtons();
            CurrentlySelected.Previous();
            Buttons[CurrentlySelected.selected].Selected = true;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            DeselectAllButtons();
            CurrentlySelected.Next();
            Buttons[CurrentlySelected.selected].Selected = true;
        }
    }

    void DeselectAllButtons()
    {
        foreach (ButtonProperties bp in Buttons)
        {
            bp.Selected = false;
        }
    }

    void UpdateAllButtons()
    {
        Buttons[CurrentlySelected.selected].Selected = true;
        foreach (ButtonProperties bp in Buttons)
        {
            bp.Update();
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

    public void Update()
    {
        if (this.Active)
        {
            Enable();
        } else
        {
            Disable();
        }
        
        if (this.Selected)
        {
            ButtonObject.transform.GetComponent<Outline>().enabled = true;
        }
        else
        {
            ButtonObject.transform.GetComponent<Outline>().enabled = false;
        }
    }

    public void Disable()
    {
        this.ButtonObject.SetActive(false);
    }

    public void Enable()
    {
        this.ButtonObject.SetActive(true);
    }

    public string Print()
    {
        return ButtonObject.transform.Find("Text").transform.GetComponent<Text>().text;
    }
}

public class CurrentlySelected
{
    public int selected;
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