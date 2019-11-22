using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOpenScript : MonoBehaviour
{
    public GameObject ChatAppPanel;
    public GameObject VirtualCameraPanel;

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
            if (controller.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                OpenCamera();
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }

        //PC
        if (Input.GetKeyUp(KeyCode.Return))
        {
            OpenCamera();
        }
    }

    void OpenCamera()
    {
        ChatAppPanel.SetActive(false);
        VirtualCameraPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
