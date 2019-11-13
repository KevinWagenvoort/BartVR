using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTrigger : MonoBehaviour
{
    public GameObject CameraRig, Phone, LeftHand, NeighbourhoodApp, PhoneChatAppPanel;
    public static bool TutorialBurgerIsDone = false;
    public static bool TutorialControlRoomIsDone = false;
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
        if (distance < 37 && phoneIsActive && TutorialBurgerIsDone && TutorialControlRoomIsDone)
        {
            Phone.SetActive(false);
            LeftHand.SetActive(true);
            phoneIsActive = false;
        } else if (distance >= 37 && !phoneIsActive && TutorialBurgerIsDone && TutorialControlRoomIsDone)
        {
            Phone.SetActive(true);
            LeftHand.SetActive(false);
            phoneIsActive = true;
        }
        // TODO: SET TUTORIAL STUFF BACK
        if (distance < 160 && phoneIsActive && !TutorialBurgerIsDone && !TutorialControlRoomIsDone && !StartedSendingMessages)
        {
            Debug.Log("Start sending messages");
            NeighbourhoodAppScript.StartScenarioMessages();
            StartedSendingMessages = true;
        }
        if (distance < 102 && phoneIsActive && !TutorialBurgerIsDone && !TutorialControlRoomIsDone && !NotificationSent)
        {
            Debug.Log("Notification on phone");
            MobileNeigborhoodChat.TriggerNotification();
            NotificationSent = true;
        }
    }
}
