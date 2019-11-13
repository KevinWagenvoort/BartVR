using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTrigger : MonoBehaviour
{
    public GameObject CameraRig, Phone, LeftHand, NeighbourhoodApp, PhoneChatAppPanel;
    public static bool TutorialBurgerIsDone = false;
    public static bool TutorialControlRoomIsDone = false;
    public static bool ConversationIsDone = false;
    private bool phoneIsActive = true;
    private bool StartedSendingMessages = false;
    private bool NotificationSent = false;
    private NeighbourhoodAppScript NeighbourhoodAppScript;
    private MobileNeigborhoodChat MobileNeigborhoodChat;

    private void Start()
    {
        NeighbourhoodAppScript = NeighbourhoodApp.GetComponent<NeighbourhoodAppScript>();
        MobileNeigborhoodChat = PhoneChatAppPanel.GetComponent<MobileNeigborhoodChat>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(CameraRig.transform.position, gameObject.transform.position);
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
        }

        if (distance < 160 && phoneIsActive && TutorialBurgerIsDone && TutorialControlRoomIsDone && !StartedSendingMessages)
        {
            NeighbourhoodAppScript.Scenario();
            StartedSendingMessages = true;
        }
        if (distance < 102 && phoneIsActive && TutorialBurgerIsDone && TutorialControlRoomIsDone && !NotificationSent)
        {
            MobileNeigborhoodChat.TriggerNotification();
            NotificationSent = true;
        }
    }
}
