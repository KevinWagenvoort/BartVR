using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MKChat : MonoBehaviour
{
    public GameObject NeighbourhoodApp;
    public Button SendButton;
    public GameObject ReceivedBubble;
    public GameObject SendBubble;
    public TMP_Dropdown Dropdown;

    private NeighbourhoodAppScript NeighbourhoodAppScript;

    private List<Message> RenderedMessages = new List<Message>();
    private List<GameObject> CloneMessages = new List<GameObject>();
    private List<GameObject> ChoiceBubbleTextList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        NeighbourhoodAppScript = NeighbourhoodApp.GetComponent<NeighbourhoodAppScript>();
        SendButton.onClick.AddListener(OnClickHandler);
        Dropdown.ClearOptions();
    }

    // Update is called once per frame
    void Update()
    {
        CheckNewMessage();
    }

    void OnClickHandler()
    {
        NeighbourhoodAppScript.SendChoice(Dropdown.value);
    }

    void MoveAllMessages()
    {
        foreach (GameObject message in CloneMessages)
        {
            Vector3 oldPos = message.transform.localPosition;
            oldPos.y += 200;
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
            if (LastMessage.sender.role == Sender.Role.Meldkamer)
            {
                newMessage = Instantiate(SendBubble, SendBubble.transform.parent);
            }
            else
            {
                newMessage = Instantiate(ReceivedBubble, ReceivedBubble.transform.parent);
            }
            Transform bubbleImage = newMessage.transform.Find("BubbleImage");
            bubbleImage.Find("MessageText").gameObject.GetComponent<TMP_Text>().text = LastMessage.message;
            if (LastMessage.sender.role != Sender.Role.Meldkamer)
            {
                bubbleImage.Find("MessageSenderName").gameObject.GetComponent<TMP_Text>().text = LastMessage.sender.name;
            }
            if (LastMessage.type == Message.Type.QuestionTrigger)
            {
                Dropdown.ClearOptions();
                Dropdown.AddOptions(LastMessage.possibleAnswers);
                Dropdown.interactable = true;
                SendButton.interactable = true;
            } else
            {
                Dropdown.interactable = false;
                SendButton.interactable = false;
            }
            newMessage.SetActive(true);
            CloneMessages.Add(newMessage);
        }
    }
}
