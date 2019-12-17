using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;

public class TeleportTutorial : MonoBehaviour
{
    public SteamVR_Action_Boolean teleportAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Teleport");
    public Sprite ViveSprite;
    private int teleportCount = 0;

    private void Start()
    {
        if (!XRDevice.model.Contains("Oculus"))
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = ViveSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (teleportAction.GetStateUp(SteamVR_Input_Sources.RightHand) || teleportAction.GetStateUp(SteamVR_Input_Sources.LeftHand))
        {
            teleportCount++;
        }
        if (teleportCount > 2)
        {
            gameObject.SetActive(false);
        }
    }
}
