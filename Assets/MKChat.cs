using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MKChat : MonoBehaviour
{
    public Button MKSendButton;
    public Dropdown MKDropDown;

    public GameObject NeighbourhoodApp;
    private NeighbourhoodAppScript NeighbourhoodAppScript;
    private MKChatAppScript mKChatAppScript;

    public GameObject MKChatBoxSend, sendTextBubblesObject; //leftside chatbox + bubbles
    public GameObject receiveTextBubblesObject; //rightside bubbles
    public GameObject receiveText, sendText; //ui text

    [SerializeField]
    List<MessageTest> messageList = new List<MessageTest>();


    // Start is called before the first frame update
    void Start()
    {
        Button btn = MKSendButton.GetComponent<Button>();
        btn.onClick.AddListener(OnClickHandler);

        ReceiveMessageChat("Hoi");

        NeighbourhoodAppScript = NeighbourhoodApp.GetComponent<NeighbourhoodAppScript>();
        mKChatAppScript = mKChatAppScript.GetComponent<MKChatAppScript>();
    }

    void OnClickHandler()
    {
        Debug.Log(MKDropDown.options[MKDropDown.value].text);
        NeighbourhoodAppScript.TemplateConversation();

        GameObject newText = Instantiate(sendText, sendTextBubblesObject.transform);
        GameObject textBubble = Instantiate(sendTextBubblesObject, MKChatBoxSend.transform);
        mKChatAppScript.TemplateConversation();
        SendMessageToChat(MKDropDown.options[MKDropDown.value].text);


    }

    public void ReceiveMessageChat(string text)
    {
        MessageTest newMessage = new MessageTest();
        newMessage.text = text;

        GameObject newText = Instantiate(receiveText, receiveTextBubblesObject.transform);
        GameObject textBubble = Instantiate(receiveTextBubblesObject, MKChatBoxSend.transform);
        newMessage.textObject = newText.GetComponent<Text>();
        newMessage.textObject.text = newMessage.text;
        messageList.Add(newMessage);
    }

    public void SendMessageToChat(string text)
    {

        MessageTest newMessage = new MessageTest();

        newMessage.text = text;

        GameObject newText = Instantiate(sendTextBubblesObject, MKChatBoxSend.transform);

        newMessage.textObject = newText.GetComponent<Text>();

        newMessage.textObject.text = newMessage.text;

        messageList.Add(newMessage);
    }
}

[System.Serializable]
public class MessageTest
{
    public string text;
    public Text textObject;
}
