using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneControls : MonoBehaviour
{
    //private
    private GameObject currentApp;

    //public
    [Header("2 = Camera, 3 = Map")]
    [Header("0 = MainMenu, 1 = Chat")]
    public List<GameObject> Apps = new List<GameObject>();

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
    }

    void OpenMainMenu()
    {
        DeactivateApps();
        Apps[0].SetActive(true);
        currentApp = Apps[0];
    }

    public void OpenChat()
    {
        DeactivateApps();
        Apps[1].SetActive(true);
        currentApp = Apps[1];
    }

    public void OpenCamera()
    {
        DeactivateApps();
        Apps[2].SetActive(true);
        currentApp = Apps[2];
    }

    public void OpenMap()
    {
        DeactivateApps();
        Apps[3].SetActive(true);
        currentApp = Apps[3];
    }
}
