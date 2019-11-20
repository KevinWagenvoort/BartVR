using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatSwitcher : MonoBehaviour
{
    //public
    public GameObject NeighbourhoodApp;
    public GameObject PrivateApp;

    // Update is called once per frame
    void Update()
    {
        Navigation();
    }

    void Navigation()
    {
        //PC
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            SwitchApps();
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            SwitchApps();
        }
    }

    void SwitchApps()
    {
        NeighbourhoodApp.SetActive(!NeighbourhoodApp.activeInHierarchy);
        PrivateApp.SetActive(!PrivateApp.activeInHierarchy);
    }

    public void OpenNeighbourhoodApp()
    {
        NeighbourhoodApp.SetActive(true);
        PrivateApp.SetActive(false);
    }
}
