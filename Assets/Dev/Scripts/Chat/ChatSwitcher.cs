using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatSwitcher : MonoBehaviour
{
    //public
    public GameObject NeighbourhoodApp;
    public GameObject PrivateApp;

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
