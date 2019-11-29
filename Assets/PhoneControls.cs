using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneControls : MonoBehaviour
{
    //private
    private GameObject currentApp;

    //public
    [Header("2 = Camera, 3 = Map")]
    [Header("0 = MainMenu, 1 = Chat")]
    public List<GameObject> Apps = new List<GameObject>();
    public GameObject MenuTutorial;
    public GameObject NewMessageIcon;
    public GameObject GoBackButton;

    // Start is called before the first frame update
    void Start()
    {
        currentApp = Apps[0];
    }

    // Update is called once per frame
    void Update()
    {
        PCNavigation();
    }

    //PC controls
    void PCNavigation()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            OpenChat();
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            OpenCamera();
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            OpenMap();
        }
        else if (Input.GetKeyUp(KeyCode.Backspace))
        {
            OpenMainMenu();
        }
    }

    void DeactivateApps()
    {
        foreach (GameObject app in Apps)
        {
            app.SetActive(false);
        }
        MenuTutorial.SetActive(false);
    }

    public void OpenMainMenu()
    {
        GoBackButton.SetActive(false);
        DeactivateApps();
        Apps[0].SetActive(true);
        currentApp = Apps[0];
    }

    public void OpenChat()
    {
        NewMessageIcon.SetActive(false);
        GoBackButton.SetActive(true);
        DeactivateApps();
        Apps[1].SetActive(true);
        currentApp = Apps[1];
    }

    public void OpenCamera()
    {
        GoBackButton.SetActive(true);
        DeactivateApps();
        Apps[2].SetActive(true);
        currentApp = Apps[2];
    }

    public void OpenMap()
    {
        GoBackButton.SetActive(true);
        DeactivateApps();
        Apps[3].SetActive(true);
        currentApp = Apps[3];
    }
}
