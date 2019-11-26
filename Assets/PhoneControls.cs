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
        Debug.Log("PhoneControls");
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

    //VR touch controls
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "VRUIButton")
        {
            switch(collider.gameObject.name)
            {
                case "ButtonChat":
                    OpenChat();
                    break;
                case "ButtonCamera":
                    OpenCamera();
                    break;
                case "ButtonMap":
                    OpenMap();
                    break;
                default:
                    Debug.Log(collider.gameObject.name);
                    break;
            }
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

    void OpenChat()
    {
        DeactivateApps();
        Apps[1].SetActive(true);
        currentApp = Apps[1];
    }

    void OpenCamera()
    {
        DeactivateApps();
        Apps[2].SetActive(true);
        currentApp = Apps[2];
    }

    void OpenMap()
    {
        DeactivateApps();
        Apps[3].SetActive(true);
        currentApp = Apps[3];
    }
}
