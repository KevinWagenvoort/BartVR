using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTrigger : MonoBehaviour
{
    public GameObject CameraRig, Phone, LeftHand, NeighbourhoodApp, PhoneChatAppPanel, PhoneMapPanel, PhoneMainMenuPanel, PhoneCameraPanel, FeedbackScreenMK, FeedbackScreenVR;
    public static bool TutorialBurgerIsDone = false;
    public static bool TutorialControlRoomIsDone = false;
    public static bool ConversationIsDone = false;
    public static bool VandalismHasHappend = false;
    public static bool StartScenarioDone = false;
    private bool FeedbackShown = false;
    private bool phoneIsActive = true;
    public static bool StartedSendingMessages = false;
    public static bool ScenarioIsDone = false;
    private bool NotificationSent = false;
    private NeighbourhoodAppScript NeighbourhoodAppScript;
    private MobileNeigborhoodChat MobileNeigborhoodChat;
    private VandalismController VandalismController;
    private GameObject[] Officers;

    private void Start()
    {
        NeighbourhoodAppScript = NeighbourhoodApp.GetComponent<NeighbourhoodAppScript>();
        MobileNeigborhoodChat = PhoneChatAppPanel.GetComponent<MobileNeigborhoodChat>();
        VandalismController = gameObject.GetComponent<VandalismController>();
        Officers = GameObject.FindGameObjectsWithTag("Officer");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(CameraRig.transform.position, gameObject.transform.position);
        if (distance < 37 && phoneIsActive && TutorialBurgerIsDone && TutorialControlRoomIsDone && !ConversationIsDone && StartScenarioDone)
        {
            Phone.SetActive(false);
            LeftHand.SetActive(true);
            phoneIsActive = false;
        } else if (distance >= 37 && !phoneIsActive && TutorialBurgerIsDone && TutorialControlRoomIsDone && !ConversationIsDone && StartScenarioDone)
        {
            Phone.SetActive(true);
            LeftHand.SetActive(false);
            phoneIsActive = true;
        } else if (distance >= 37 && ConversationIsDone && !VandalismHasHappend)//Conversation is over and player walked away
        {
            VandalismHasHappend = true;
            VandalismController.StartVandalism();
        }

        if (distance < 160 && phoneIsActive && TutorialBurgerIsDone && TutorialControlRoomIsDone && !StartedSendingMessages)
        {
            StartedSendingMessages = true;
        }
        if (distance < 102 && phoneIsActive && TutorialBurgerIsDone && TutorialControlRoomIsDone && !NotificationSent)
        {
            MobileNeigborhoodChat.TriggerNotification();
            NotificationSent = true;
            PhoneChatAppPanel.SetActive(true);
            PhoneMainMenuPanel.SetActive(false);
            PhoneCameraPanel.SetActive(false);
            PhoneMapPanel.SetActive(false);
        }

        if (VandalismHasHappend && !FeedbackShown && ScenarioIsDone)
        {
            foreach (GameObject officer in Officers)
            {
                float distanceToOfficer = Vector3.Distance(officer.transform.position, gameObject.transform.position);
                if (distanceToOfficer < 32)
                {
                    FeedbackShown = true;
                    FeedbackScreenMK.SetActive(true);
                    FeedbackScreenVR.SetActive(true);
                }
            }
        }
    }
}
