using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MobileChatScript : MonoBehaviour
{
    public GameObject NeighbourhoodApp;
    public GameObject PrivateApp;

    public GameObject ReceivedBubble;
    public GameObject SendBubble;

    private NeighbourhoodAppScript NeighbourhoodAppScript;
    private PrivateAppScript PrivateAppScript;

    private List<Message> RenderedMessages = new List<Message>();
    private List<GameObject> CloneMessages = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        NeighbourhoodAppScript = NeighbourhoodApp.GetComponent<NeighbourhoodAppScript>();
        PrivateAppScript = PrivateApp.GetComponent<PrivateAppScript>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckNewMessage();
    }

    void MoveAllMessages()
    {
        foreach (GameObject message in CloneMessages)
        {
            Vector3 oldPos = message.transform.localPosition;
            oldPos.y += 30;
            message.transform.localPosition = oldPos;
        }
    }

    void CheckNewMessage()
    {
        Message LastMessage = NeighbourhoodAppScript.ChatApp.GetLastMessage();

        if (!RenderedMessages.Contains(LastMessage) && LastMessage != null)
        {
            MoveAllMessages();
            RenderedMessages.Add(LastMessage);
            GameObject newMessage;
            if (LastMessage.sender.role == Sender.Role.Burger)
            {
                newMessage = Instantiate(SendBubble, SendBubble.transform.parent);
            } else
            {
                newMessage = Instantiate(ReceivedBubble, ReceivedBubble.transform.parent);
            }
            newMessage.transform.Find("BubbleImage").gameObject.transform.Find("MessageText").gameObject.GetComponent<Text>().text = LastMessage.message;
            newMessage.SetActive(true);
            CloneMessages.Add(newMessage);
        }
    }
}
