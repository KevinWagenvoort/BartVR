using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CameraOpenScript : MonoBehaviour
{
    public GameObject ChatAppPanel;
    public GameObject VirtualCameraPanel;

    // Update is called once per frame
    void Update()
    {
        Navigation();
    }

    void Navigation()
    {
        //PC
        if (Input.GetKeyUp(KeyCode.Return))
        {
            OpenCamera();
        }
    }

    public void OpenCamera()
    {
        ChatAppPanel.SetActive(false);
        VirtualCameraPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
