using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendMessage : MonoBehaviour
{
    public Text Text;
    public GameObject PrivateApp, NeighbourhoodApp;
    public bool isNeighbourhood;

    private Message CurrentMessage;
    private PrivateAppScript PrivateAppScript;
    private NeighbourhoodAppScript NeighbourhoodAppScript;

    // Start is called before the first frame update
    void Start()
    {
        if (isNeighbourhood)
        {
            NeighbourhoodAppScript = NeighbourhoodApp.GetComponent<NeighbourhoodAppScript>();
        }
        else
        {
            PrivateAppScript = PrivateApp.GetComponent<PrivateAppScript>();
        }
    }

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
            SendCurrentMessage();
        }
    }

    public void SetMessage(string message, Sender sender, Message.Type type, List<string> possibleAnswers = null, Sprite photo = null, string keywords = null)
    {
        Text.text = message;
        CurrentMessage = new Message(message, sender, type, possibleAnswers, photo, keywords);
        gameObject.SetActive(true);
    }

    public void SendCurrentMessage()
    {
        gameObject.SetActive(false);
        if (isNeighbourhood)
        {
            NeighbourhoodAppScript.ChatApp.Send(CurrentMessage);
            NeighbourhoodAppScript.Scenario();
        }
        else
        {
            PrivateAppScript.ChatApp.Send(CurrentMessage);
            PrivateAppScript.Tutorial();
        }
    }
}
