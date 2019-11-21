using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTrigger : MonoBehaviour
{
    public GameObject CameraRig, Phone, LeftHand, NeighbourhoodApp, PhoneChatAppPanel;
    public static bool TutorialBurgerIsDone = false;
    public static bool TutorialControlRoomIsDone = false;
    public static bool ConversationIsDone = false;
    public bool VandalismHasHappend = false;
    private bool phoneIsActive = true;
    public static bool StartedSendingMessages = false;
    private bool NotificationSent = false;
    private NeighbourhoodAppScript NeighbourhoodAppScript;
    private MobileNeigborhoodChat MobileNeigborhoodChat;
    private VandalismController VandalismController;
    private GameObject Officer;

    private void Start()
    {
        NeighbourhoodAppScript = NeighbourhoodApp.GetComponent<NeighbourhoodAppScript>();
        MobileNeigborhoodChat = PhoneChatAppPanel.GetComponent<MobileNeigborhoodChat>();
        VandalismController = gameObject.GetComponent<VandalismController>();
        Officer = GameObject.FindGameObjectsWithTag("Officer")[0];
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(CameraRig.transform.position, gameObject.transform.position);
        float distanceToOfficer = Vector3.Distance(Officer.transform.position, gameObject.transform.position);
        if (distance < 37 && phoneIsActive && TutorialBurgerIsDone && TutorialControlRoomIsDone && !ConversationIsDone)
        {
            Phone.SetActive(false);
            LeftHand.SetActive(true);
            phoneIsActive = false;
        } else if (distance >= 37 && !phoneIsActive && TutorialBurgerIsDone && TutorialControlRoomIsDone && !ConversationIsDone)
        {
            Phone.SetActive(true);
            LeftHand.SetActive(false);
            phoneIsActive = true;
        } else if (distance >= 37 && ConversationIsDone && !VandalismHasHappend)//Conversation is over and player walked away
        {
            VandalismHasHappend = true;
            VandalismController.StartVandalism();
        }

        if (distanceToOfficer < 32 && VandalismHasHappend)
        {
            // feedback scherm
        }

        if (distance < 160 && phoneIsActive && TutorialBurgerIsDone && TutorialControlRoomIsDone && !StartedSendingMessages)
        {
            StartedSendingMessages = true;
        }
        if (distance < 102 && phoneIsActive && TutorialBurgerIsDone && TutorialControlRoomIsDone && !NotificationSent)
        {
            MobileNeigborhoodChat.TriggerNotification();
            NotificationSent = true;
        }

        Debug.Log(distanceToOfficer);
    }
}
