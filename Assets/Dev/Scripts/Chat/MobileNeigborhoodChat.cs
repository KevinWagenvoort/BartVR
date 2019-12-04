using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileNeigborhoodChat : MonoBehaviour
{
    public GameObject NeighbourhoodApp;

    public GameObject ReceivedBubble;
    public GameObject ReceivedLocationBubble;
    public GameObject SendBubble;
    public GameObject ChoiceBubbles;
    public GameObject MapOpenBubble;
    public GameObject SendPhotoBubble;
    public GameObject CameraOpenBubble;
    public Text SubjectText;

    private NeighbourhoodAppScript NeighbourhoodAppScript;
    private ChatSwitcher ChatSwitcher;
    private List<Message> RenderedMessages = new List<Message>();
    private List<GameObject> CloneMessages = new List<GameObject>();
    private List<GameObject> ChoiceBubbleTextList = new List<GameObject>();
    private Message.Type PreviousMessageType;

    // Start is called before the first frame update
    void Start()
    {
        NeighbourhoodAppScript = NeighbourhoodApp.GetComponent<NeighbourhoodAppScript>();
        ChatSwitcher = gameObject.GetComponent<ChatSwitcher>();
        foreach (Transform child in ChoiceBubbles.transform)
        {
            ChoiceBubbleTextList.Add(child.Find("Text").gameObject);
        }
        SubjectText.text = "Buurtapp Pizzalanden";
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
            oldPos.y += 40;
            message.transform.localPosition = oldPos;
        }
    }

    void CheckNewMessage()
    {
        Message LastMessage = NeighbourhoodAppScript.ChatApp.GetLastMessage();

        if (!RenderedMessages.Contains(LastMessage) && LastMessage != null && DistanceTrigger.TutorialControlRoomIsDone)
        {
            MoveAllMessages();
            RenderedMessages.Add(LastMessage);
            GameObject newMessage;
            if (LastMessage.sender.role == Sender.Role.Burger)
            {
                if (LastMessage.type == Message.Type.Photo || LastMessage.type == Message.Type.PhotoQuestionTrigger)
                {
                    newMessage = Instantiate(SendPhotoBubble, SendPhotoBubble.transform.parent);
                    Image photoComponent = newMessage.transform.Find("BubbleImage").Find("Photo").gameObject.GetComponent<Image>();
                    photoComponent.sprite = LastMessage.photo;
                    photoComponent.preserveAspect = true;
                }
                else
                {
                    newMessage = Instantiate(SendBubble, SendBubble.transform.parent);
                    MapOpenBubble.SetActive(false);
                }
            }
            else
            {
                if (DistanceTrigger.StartedSendingMessages)
                {
                    Vibrations.Pulse(0.5f);
                }
                if (LastMessage.type == Message.Type.Location)
                {
                    newMessage = Instantiate(ReceivedLocationBubble, ReceivedLocationBubble.transform.parent);
                    MapOpenBubble.SetActive(true);
                } else if (LastMessage.type == Message.Type.PhotoRequest)
                {
                    newMessage = Instantiate(ReceivedBubble, ReceivedBubble.transform.parent);
                    CameraOpenBubble.SetActive(true);
                }
                else
                {
                    newMessage = Instantiate(ReceivedBubble, ReceivedBubble.transform.parent);
                    MapOpenBubble.SetActive(false);
                }
            }
            Transform bubbleImage = newMessage.transform.Find("BubbleImage");
            if (LastMessage.type != Message.Type.Photo && LastMessage.type != Message.Type.PhotoQuestionTrigger)
            {
                bubbleImage.Find("MessageText").gameObject.GetComponent<Text>().text = LastMessage.message;
            }
            if (LastMessage.sender.role != Sender.Role.Burger)
            {
                bubbleImage.Find("MessageSenderName").gameObject.GetComponent<Text>().text = LastMessage.sender.name;
            }
            newMessage.SetActive(true);
            CloneMessages.Add(newMessage);

            if (LastMessage.type == Message.Type.Question && LastMessage.sender.role != Sender.Role.Burger)
            {
                int choiceBubbleNumber = 0;
                foreach (string choice in LastMessage.possibleAnswers)
                {
                    ChoiceBubbleTextList[choiceBubbleNumber].GetComponent<Text>().text = choice;
                    choiceBubbleNumber++;
                }
                ChoiceBubbles.SetActive(true);
            }
            else if (LastMessage.type != Message.Type.QuestionFollowup)
            {
                ChoiceBubbles.SetActive(false);
            }
            PreviousMessageType = LastMessage.type;
        }
    }

    public void TriggerNotification()
    {
        ChatSwitcher.OpenNeighbourhoodApp();
    }
}
