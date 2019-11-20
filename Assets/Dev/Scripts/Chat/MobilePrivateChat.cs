using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MobilePrivateChat : MonoBehaviour
{
    public GameObject PrivateApp;

    public GameObject ReceivedBubble;
    public GameObject ReceivedLocationBubble;
    public GameObject SendBubble;
    public GameObject ChoiceBubbles;
    public GameObject MapOpenBubble;
    private PrivateAppScript PrivateAppScript;
    public Text SubjectText;

    private List<Message> RenderedMessages = new List<Message>();
    private List<GameObject> CloneMessages = new List<GameObject>();
    private List<GameObject> ChoiceBubbleTextList = new List<GameObject>();

    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device controller;

    // Start is called before the first frame update
    void Start()
    {
        trackedObject = GetComponentInParent<SteamVR_TrackedObject>();
        controller = SteamVR_Controller.Input((int)trackedObject.index);

        PrivateAppScript = PrivateApp.GetComponent<PrivateAppScript>();
        foreach (Transform child in ChoiceBubbles.transform)
        {
            ChoiceBubbleTextList.Add(child.Find("Text").gameObject);
        }
        SubjectText.text = "Vriend";
        PrivateAppScript.Tutorial();
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
        Message LastMessage = PrivateAppScript.ChatApp.GetLastMessage();

        if (!RenderedMessages.Contains(LastMessage) && LastMessage != null)
        {
            MoveAllMessages();
            RenderedMessages.Add(LastMessage);
            GameObject newMessage;
            if (LastMessage.sender.role == Sender.Role.Burger)
            {
                newMessage = Instantiate(SendBubble, SendBubble.transform.parent);
                MapOpenBubble.SetActive(false);
            } else
            {
                StartCoroutine(LongVibration(0.5f, 10000));
                if (LastMessage.type == Message.Type.Location)
                {
                    newMessage = Instantiate(ReceivedLocationBubble, ReceivedLocationBubble.transform.parent);
                    MapOpenBubble.SetActive(true);
                }
                else
                {
                    newMessage = Instantiate(ReceivedBubble, ReceivedBubble.transform.parent);
                    MapOpenBubble.SetActive(false);
                }
            }
            Transform bubbleImage = newMessage.transform.Find("BubbleImage");
            bubbleImage.Find("MessageText").gameObject.GetComponent<Text>().text = LastMessage.message;
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
        }
    }



    IEnumerator LongVibration(float seconds, ushort strength)
    {
        for (float i = 0; i < seconds; i += Time.deltaTime)
        {
            try
            {
                controller.TriggerHapticPulse(strength);
            }
            catch
            {

            }
            yield return null; //every single frame for the duration of "length" you will vibrate at "strength" amount
        }
    }
}
