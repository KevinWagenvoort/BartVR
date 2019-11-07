using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendMessage : MonoBehaviour
{
    public Text Text;
    public GameObject PrivateApp;

    private Message CurrentMessage;
    private PrivateAppScript PrivateAppScript;

    // SteamVR
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device controller;

    // Start is called before the first frame update
    void Start()
    {
        trackedObject = GetComponentInParent<SteamVR_TrackedObject>();
        PrivateAppScript = PrivateApp.GetComponent<PrivateAppScript>();
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
            if (controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
            {
                SendCurrentMessage();
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }

        //PC
        if (Input.GetKeyUp(KeyCode.Return))
        {
            SendCurrentMessage();
        }
    }

    public void SetMessage(string message, Sender sender, Message.Type type, List<string> possibleAnswers = null, Sprite photo = null)
    {
        Text.text = message;
        CurrentMessage = new Message(message, sender, type, possibleAnswers, photo);
        gameObject.SetActive(true);
    }

    private void SendCurrentMessage()
    {
        PrivateAppScript.ChatApp.Send(CurrentMessage);
        PrivateAppScript.Tutorial();
        gameObject.SetActive(false);
    }
}
