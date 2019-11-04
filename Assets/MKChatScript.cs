using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MKChatScript : MonoBehaviour
{
    public GameObject MKChatApp;

    public GameObject ReceivedBubble;
    public GameObject SendBubble;
    
    private MKChatAppScript MKChatAppScript;

    private List<Message> RenderedMessages = new List<Message>();
    private List<GameObject> CloneMessages = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        MKChatAppScript = MKChatApp.GetComponent<MKChatAppScript>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckNewMessage();
    }

    

    void CheckNewMessage()
    {
        Message LastMessage = MKChatAppScript.ChatApp.GetLastMessage();

        if (!RenderedMessages.Contains(LastMessage))
        {
            RenderedMessages.Add(LastMessage);
            GameObject newMessage;
            if (LastMessage.sender.role == Sender.Role.Meldkamer)
            {
                newMessage = Instantiate(SendBubble, SendBubble.transform.parent);
            }
            else
            {
                newMessage = Instantiate(ReceivedBubble, ReceivedBubble.transform.parent);
            }
            newMessage.transform.Find("MessageText").gameObject.GetComponent<TextMeshProUGUI>().text = LastMessage.message;
            newMessage.SetActive(true);
            CloneMessages.Add(newMessage);
        }
    }
}
