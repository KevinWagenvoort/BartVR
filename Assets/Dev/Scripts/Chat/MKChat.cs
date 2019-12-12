using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MKChat : MonoBehaviour
{
    public GameObject NeighbourhoodApp;
    public Button SendButton;
    public GameObject ReceivedBubble, ReceivedPhotoBubble, Content;
    public GameObject SendBubble;
    public TMP_Dropdown Dropdown;
    public GameObject Arrow;
    public Text KeywordText;

    public GameObject popup;
    public GameObject popupMap;
    public Button closePopup;
    public Button closePopupMap;

    private NeighbourhoodAppScript NeighbourhoodAppScript;

    private List<Message> RenderedMessages = new List<Message>();
    private List<GameObject> CloneMessages = new List<GameObject>();
    private Message CurrentMessage;
    private List<int> SendChoices = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        NeighbourhoodAppScript = NeighbourhoodApp.GetComponent<NeighbourhoodAppScript>();
        
        SendButton.onClick.AddListener(OnClickHandler);
        Dropdown.ClearOptions();
        NeighbourhoodAppScript.Tutorial();
    }

    // Update is called once per frame
    void Update()
    {
        CheckNewMessage();
    }

    //void OnClickHandlerMap()
    //{
    //    if (popupMap.active)
    //    {
    //        popupMap.SetActive(false);
    //        closePopup.onClick.AddListener(OnClickHandlerPopup);
    //    }
    //}

    //void OnClickHandlerPopup()
    //{
    //    if (popup.active)
    //    {
    //        popup.SetActive(false);
    //        NeighbourhoodAppScript.Tutorial();
    //    }
    //}

    void OnClickHandler()
    {
        if (CurrentMessage != null)
        {
            SendCurrentMessage();
        }
        else
        {
            NeighbourhoodAppScript.SendChoice(Dropdown.options[Dropdown.value].text);
            SendChoices.Add(Dropdown.value);
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
            if (LastMessage.type == Message.Type.Photo || LastMessage.type == Message.Type.PhotoQuestionTrigger)
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
                newMessage = Instantiate(SendBubble, Content.transform);
            }
            else
            {
                if (LastMessage.type == Message.Type.Photo || LastMessage.type == Message.Type.PhotoQuestionTrigger)
                {
                    newMessage = Instantiate(ReceivedPhotoBubble, Content.transform);
                    Image photoComponent = newMessage.transform.Find("BubbleImage").Find("Photo").gameObject.GetComponent<Image>();
                    photoComponent.sprite = LastMessage.photo;
                    photoComponent.preserveAspect = true;
                }
                else
                {
                    newMessage = Instantiate(ReceivedBubble, Content.transform);
                }
            }
            Transform bubbleImage = newMessage.transform.Find("BubbleImage");
            if (LastMessage.type != Message.Type.Photo && LastMessage.type != Message.Type.PhotoQuestionTrigger)
            {
                bubbleImage.Find("MessageText").gameObject.GetComponent<TMP_Text>().text = LastMessage.message;
            }
            if (LastMessage.sender.role != Sender.Role.Meldkamer)
            {
                bubbleImage.Find("MessageSenderName").gameObject.GetComponent<TMP_Text>().text = LastMessage.sender.name;
            }
            if (LastMessage.type == Message.Type.QuestionTrigger || LastMessage.type == Message.Type.PhotoQuestionTrigger || LastMessage.type == Message.Type.QuestionTriggerAnswer || LastMessage.type == Message.Type.PhotoQuestionTrigger)
            {
                if (LastMessage.type == Message.Type.QuestionTrigger)//New question
                {
                    //Empty dropdown
                    Dropdown.ClearOptions();
                    SendChoices = new List<int>();
                    //Fill dropdown
                    Dropdown.AddOptions(LastMessage.possibleAnswers);
                    //Enable dropdown + button
                    Dropdown.interactable = true;
                    Arrow.SetActive(true);
                    Invoke("ActivateSendButton", 1);
                } else if (LastMessage.type == Message.Type.QuestionTriggerAnswer || LastMessage.type == Message.Type.PhotoQuestionTrigger)//Old question
                {
                    Dropdown.ClearOptions();
                    Dropdown.AddOptions(LastMessage.possibleAnswers);
                    Dropdown.interactable = true;
                    Invoke("ActivateSendButton", 1);
                    Arrow.SetActive(true);
                }
            } else
            {
                Dropdown.interactable = false;
                SendButton.interactable = false;
                Arrow.SetActive(false);
            }
            if (LastMessage.keywords != null)
            {
                KeywordText.text += LastMessage.keywords + " ";
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
        Invoke("ActivateSendButton", 1);
    }

    private void ActivateSendButton()
    {
        SendButton.interactable = true;
    }

    private void SendCurrentMessage()
    {
        NeighbourhoodAppScript.SendMessage(CurrentMessage);
        SendButton.interactable = false;
        CurrentMessage = null;
        Dropdown.ClearOptions();
    }

    public void ResetMK()
    {
        foreach (GameObject gj in CloneMessages)
        {
            Destroy(gj.gameObject);
        }
        CloneMessages = new List<GameObject>();
        RenderedMessages = new List<Message>();
        CurrentMessage = null;
        KeywordText.text = "";
    }
}
