using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatSwitcher : MonoBehaviour
{
    //public
    public GameObject NeighbourhoodApp;
    public GameObject PrivateApp;

    // SteamVR
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device controller;

    // Start is called before the first frame update
    void Start()
    {
        trackedObject = GetComponentInParent<SteamVR_TrackedObject>();
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
                if (controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x > 0.5f)
                {
                    SwitchApps();
                }
                else if (controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x < -0.5f)
                {
                    SwitchApps();
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }

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
}
