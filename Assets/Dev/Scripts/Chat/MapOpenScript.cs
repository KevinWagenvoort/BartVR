using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapOpenScript : MonoBehaviour
{
    public GameObject ChatAppPanel;
    public GameObject MinimapPanel;
    public GameObject TeleportTutorial;
    public GameObject MapPath;

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
                OpenMap();
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }

        //PC
        if (Input.GetKeyUp(KeyCode.Return))
        {
            OpenMap();
        }
    }

    void OpenMap()
    {
        ChatAppPanel.SetActive(false);
        MinimapPanel.SetActive(true);
        TeleportTutorial.SetActive(true);
        MapPath.SetActive(true);
        gameObject.SetActive(false);
    }
}
