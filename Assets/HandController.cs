using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public GameObject VirtualPhone;
    public GameObject ChoiceBubblesPrivate;

    private PhoneControls PhoneControls;
    private ChoiceNavigation ChoiceNavigationPrivate;

    // Start is called before the first frame update
    void Start()
    {
        PhoneControls = VirtualPhone.GetComponent<PhoneControls>();
        ChoiceNavigationPrivate = ChoiceBubblesPrivate.GetComponent<ChoiceNavigation>();
    }

    //VR touch controls
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "VRUIButton")
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
            }
        }
        Debug.Log(collider.gameObject.name);
    }
}
