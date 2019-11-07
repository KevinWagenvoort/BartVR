using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MKChat : MonoBehaviour
{
    public GameObject NeighbourhoodApp;
    public Button SendButton;
    public GameObject ReceivedBubble, ReceivedPhotoBubble;
    public GameObject SendBubble;
    public TMP_Dropdown Dropdown;

    private NeighbourhoodAppScript NeighbourhoodAppScript;

    private List<Message> RenderedMessages = new List<Message>();
    private List<GameObject> CloneMessages = new List<GameObject>();
    private List<GameObject> ChoiceBubbleTextList = new List<GameObject>();
    private Message CurrentMessage;

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
        if (CurrentMessage != null)
        {
            SendCurrentMessage();
        }
        else
        {
            NeighbourhoodAppScript.SendChoice(Dropdown.value);
        }
    }

    void MoveAllMessages(int distance = 200)
    {
        foreach (GameObject message in CloneMessages)
        {
            Vector3 oldPos = message.transform.localPosition;
            oldPos.y += distance;
            message.transform.localPosition = oldPos;
        }
    }

    void CheckNewMessage()
    {
        Message LastMessage = NeighbourhoodAppScript.ChatApp.GetLastMessage();

        if (!RenderedMessages.Contains(LastMessage) && LastMessage != null)
        {
            if (LastMessage.type == Message.Type.Photo)
            {
                MoveAllMessages(510);
            } else
            {
                MoveAllMessages(200);
            }
            RenderedMessages.Add(LastMessage);
            GameObject newMessage;
            if (LastMessage.sender.role == Sender.Role.Meldkamer)
            {
                newMessage = Instantiate(SendBubble, SendBubble.transform.parent);
            }
            else
            {
                if (LastMessage.type == Message.Type.Photo)
                {
                    newMessage = Instantiate(ReceivedPhotoBubble, ReceivedPhotoBubble.transform.parent);
                    Image photoComponent = newMessage.transform.Find("BubbleImage").Find("Photo").gameObject.GetComponent<Image>();
                    photoComponent.sprite = LastMessage.photo;
                    photoComponent.preserveAspect = true;
                }
                else
                {
                    newMessage = Instantiate(ReceivedBubble, ReceivedBubble.transform.parent);
                }
            }
            Transform bubbleImage = newMessage.transform.Find("BubbleImage");
            if (LastMessage.type != Message.Type.Photo)
            {
                bubbleImage.Find("MessageText").gameObject.GetComponent<TMP_Text>().text = LastMessage.message;
            }
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

    public void SetMessage(string message, Sender sender, Message.Type type, List<string> possibleAnswers = null, Sprite photo = null)
    {
        Dropdown.ClearOptions();
        Dropdown.AddOptions(new List<string>() { message });
        CurrentMessage = new Message(message, sender, type, possibleAnswers, photo);
        Dropdown.interactable = false;
        SendButton.interactable = true;
    }

    private void SendCurrentMessage()
    {
        NeighbourhoodAppScript.SendMessage(CurrentMessage);
        SendButton.interactable = false;
        CurrentMessage = null;
    }
}
