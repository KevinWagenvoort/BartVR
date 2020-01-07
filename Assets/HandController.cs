using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public GameObject VirtualPhone;
    public GameObject ChoiceBubblesPrivate, ChoiceBubblesNeighbourhood, ChoiceBubblesLocal, VirtualCameraPanel, Teleporting;

    private PhoneControls PhoneControls;
    private ChoiceNavigation ChoiceNavigationPrivate, ChoiceNavigationNeighbourhood;
    private ConversationNavigation ConversationNavigation;
    private PhotoHandler PhotoHandler;
    private bool isTriggerable = true;

    // Start is called before the first frame update
    void Start()
    {
        PhoneControls = VirtualPhone.GetComponent<PhoneControls>();
        ChoiceNavigationPrivate = ChoiceBubblesPrivate.GetComponent<ChoiceNavigation>();
        ChoiceNavigationNeighbourhood = ChoiceBubblesNeighbourhood.GetComponent<ChoiceNavigation>();
        ConversationNavigation = ChoiceBubblesLocal.GetComponent<ConversationNavigation>();
        PhotoHandler = VirtualCameraPanel.GetComponent<PhotoHandler>();
    }

    //VR touch controls
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "VRUIButton" && isTriggerable)
        {
            switch (collider.gameObject.name)
            {
                case "ButtonChat":
                    PhoneControls.OpenChat();
                    break;
                case "ButtonCamera":
                    PhoneControls.OpenCamera();
                    break;
                case "ButtonMap":
                    PhoneControls.OpenMap();
                    break;
                case "GoBackButton":
                    PhoneControls.OpenMainMenu();
                        break;
                case "ChoiceBubble A":
                    ChoiceNavigationPrivate.TouchButtonChoice(0);
                    break;
                case "ChoiceBubble B":
                    ChoiceNavigationPrivate.TouchButtonChoice(1);
                    break;
                case "ChoiceBubble C":
                    ChoiceNavigationPrivate.TouchButtonChoice(2);
                    break;
                case "ChoiceBubble A Neighbourhood":
                    ChoiceNavigationNeighbourhood.TouchButtonChoice(0);
                    break;
                case "ChoiceBubble B Neighbourhood":
                    ChoiceNavigationNeighbourhood.TouchButtonChoice(1);
                    break;
                case "SendMessage":
                    collider.gameObject.GetComponent<SendMessage>().SendCurrentMessage();
                    break;
                case "MapOpenBubble":
                    PhoneControls.OpenMap();
                    collider.gameObject.SetActive(false);
                    break;
                case "CameraOpenBubble":
                    PhoneControls.OpenCamera();
                    collider.gameObject.SetActive(false);
                    break;
                case "ChoiceBubble A Local":
                    ConversationNavigation.TouchButtonChoice(0);
                    break;
                case "ChoiceBubble B Local":
                    ConversationNavigation.TouchButtonChoice(1);
                    break;
                case "ChoiceBubble C Local":
                    ConversationNavigation.TouchButtonChoice(2);
                    break;
                case "TakePicture":
                    PhotoHandler.TakePhotoButton();
                    break;
            }
            isTriggerable = false;
        } else
        {
            Teleporting.SetActive(false);
            Debug.Log(collider.name);
        }
    }

    void OnTriggerExit(Collider collision)
    {
        Teleporting.SetActive(true);
        Invoke("MakeTriggerable", 1);
    }

    private void MakeTriggerable()
    {
        isTriggerable = true;
    }
}
