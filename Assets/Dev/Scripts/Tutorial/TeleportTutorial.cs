using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTutorial : MonoBehaviour
{

    private int teleportCount = 0;

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
        try
        {
            controller = SteamVR_Controller.Input((int)trackedObject.index);
            if (controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
            {
                teleportCount++;
                if (teleportCount == 3)
                {
                    gameObject.SetActive(false);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }
}
