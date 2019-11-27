using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public GameObject VirtualPhone;
    public GameObject ChoiceBubblesPrivate, ChoiceBubblesNeighbourhood;

    private PhoneControls PhoneControls;
    private ChoiceNavigation ChoiceNavigationPrivate, ChoiceNavigationNeighbourhood;
    private bool isTriggerable = true;

    // Start is called before the first frame update
    void Start()
    {
        PhoneControls = VirtualPhone.GetComponent<PhoneControls>();
        ChoiceNavigationPrivate = ChoiceBubblesPrivate.GetComponent<ChoiceNavigation>();
        ChoiceNavigationNeighbourhood = ChoiceBubblesNeighbourhood.GetComponent<ChoiceNavigation>();
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
                    collider.gameObject.GetComponent<MapOpenScript>().OpenMap();
                    break;
                case "CameraOpenBubble":
                    collider.gameObject.GetComponent<CameraOpenScript>().OpenCamera();
                    break;
            }
            isTriggerable = false;
            Invoke("MakeTriggerable", 1);
        }
        Debug.Log(collider.gameObject.name);
    }

    private void MakeTriggerable()
    {
        isTriggerable = true;
    }
}
